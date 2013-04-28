# NDex

Unified algorithm support for indexed .NET collections.

Download using NuGet: [NDex](http://nuget.org/packages/ndex)

## Overview
There are a lot of classes in .NET that implement the `IList<T>` interface, including `T[]`, `List<T>` and `ObservableCollection<T>`. However, the `IList<T>` interface is really limited and most of its subclasses support very few operations. Only the `List<T>` class has a decent set of algorithms, and even then it is quite limited. LINQ provides a lot more functionality, but every operation creates a new collection. When working with a collection in-place is a must, you are forced to either deal with what .NET gives you or spin your own algorithms.

NDex is a heavily tested and efficient algorithms library for working with indexed collections in-place. Not only does it provide access to algorithms not otherwise available in .NET, it has useful overloads of those algorithms you're already familiar with.

## Sublist
In order to access the algorithms provided by NDex, you must wrap your list with a `Sublist`. `Sublist` allows you to specify a range over a list in which you want to apply an operation. There are occasions when you only want to sort part of a list of search for a value after a particular index. Instead of providing a dozen overloads of each algorithm accepting a `startIndex` and `count` argument, you just always pass in a `Sublist`. NDex is smart and will work against the underlying list, so there's no overhead for wrapping a list.

The `Sublist` class has another benefit. In order for the algorithms to work directly with the underlying list, the type of the list must be known at compile time. Otherwise, all the operations would require polymorphic calls to the `IList<T>` interface. The overhead of these polymorphic calls has a dramatic impact on performance once a collection exceeds about 10,000 items. The `Sublist` class keeps track of the type of the underlying list via a generic argument. Normally, you don't need to know what that means in order to use NDex effectively, but I will try to explain. The `Sublist` class has the following signature:

    public sealed class Sublist<TList, T> : IList<T>
        where TList : IList<T>
        
Developers unfamiliar with generics may be confused by this signature. Basically, it says that the `Sublist` class implements `IList<T>` and has two generic arguments: 1) the type of the list being wrapped and 2) the type of the elements in the list. The underlying list is constrained to being an `IList<T>`. Fortunately, you can avoid typing out this long signature simply by using the `ToSublist` extension methods. For example:

    int[] values = new int[] { 5, 4, 2, 1, 3, 8, 9, 7, 6 };
    var sublist = values.ToSublist();
    Sublist.QuickSort(sublist);  // 1, 2, 3, 4, 5, 6, 7, 8, 9
    
The compiler is smart enough to infer the generic arguments when you call `ToSublist`. Furthermore, you can use `var` to avoid even more typing. In the example above, it is important to point out that the original `values` array will be modified eventhough the `sublist` variable was passed to the algorithm. Again, the `Sublist` is just a thin wrapper around the underlying list.

The real power of `Sublist` is in its ability to represent a range over a list. For instance, the following example will first partition a list and then sort the two partitions:

    int[] values = new int[] { 5, 4, 2, 1, 3, 8, 9, 7, 6 };
    var sublist = values.ToSublist();
    int index = Sublist.Partition(sublist, x => x % 2 == 0);  // move evens to the front
    var front = values.ToSublist(0, index);  // range over the even numbers
    var back = values.ToSublist(index);  // range over the odd numbers
    Sublist.QuickSort(front);  // 2, 4, 6, 8
    Sublist.QuickSort(back); // 1, 3, 5, 7, 9
    
The `ToSublist` extension methods accepts two integer arguments: an offset and a count. The offset is the index into the underlying list where the range should begin. The count is the number of items that should be in the `Sublist`. If only the offset is provided, the range will include the rest of the list. Once the `Sublist` is created, these can be accessed via the `Offset` and `Count` properties. A nice feature of `Sublist` is that the `Offset` and `Count` properties are editable. If the `Offset` is modified such that the `Count` would be invalidated, it is automatically resized. `Sublist` also has a property for the underlying list, so you can always get back to it.

There is a useful trick you can perform using a `Sublist` with a `Count` of zero, so I will mention it here. There are a handful of algorithms that start with `Add`. These allow you to add new values to the end of a list. But what if you wanted to add items the front or the middle of a list? You can use the same `Add` methods as before, just create an empty `Sublist` whose offset is at the index you want to insert into. For example:

    List<int> values = new List<int>() { 1, 2, 3, 7, 8, 9 };
    var newItems = new int[] { 4, 5, 6 };
    Sublist.Add(newItems.ToSublist(), values.ToSublist(3, 0));  // 1, 2, 3, 4, 5, 6, 7, 8, 9
    
Those familiar with data structures might be concerned about the performance implications of inserting into the middle of a list. This is less of a concern with NDex - it is optimized to handle inserting multiple items into the middle of a list efficiently (at the cost of a single shift in items). Of course, you won't see any benefit at all if you call `Add` for each item individually. In that case, it might be more efficient to first create a second list and then insert it. Even more efficient would be to add to the end of the list and perform a `RotateLeft`.

## ReversedList
The `ReversedList` class creates a view over a list, creating the illusion that the items are reversed. `ReversedList` solves some tricky problems.

For a more interesting use of `ReversedList`, imagine that you wanted to shift items in a list to the right. You could try to use the `Copy` algorithm, but the results would probably surprise you. Here's what that would look like:

    // We're expecting: 1, 1, 2, 3, 4, 5
    int[] values = new int[] { 1, 2, 3, 4, 5, 0 };
    var source = values.ToSublist(0, 4);
    var destination = values.ToSublist(1);
    Sublist.Copy(source, destination);  // 1, 1, 1, 1, 1, 1
    
Inspecting the `Copy` algorithm, you'd see why this happens. Before the `2` can be copied, it is overwritten with `1`. This continues until the `1` gets propogated all the way. Copying in the opposite direction, back-to-front, works as expected because the values aren't overwritten. Since NDex doesn't provide an algorithm for copying backward, it would seem like the only other option would be to write the code by hand.

But wait, we can solve this problem using the `ReversedList` class. This is the updated code:

    int[] values = new int[] { 1, 2, 3, 4, 5, 0 };
    var source = values.ToSublist(0, 4).Reversed();
    var destination = values.ToSublist(1).Reversed();
    Sublist.Copy(source, destination);
    
Here we call the `Reversed` extension method on each `Sublist`. It takes a little diagramming or a lot of imagination to see why this code works. At a high level, it is basically performing a back-to-front copy, but since back is the front and the front is the back... it does the right thing. It's so odd, most people aren't likely to think of it own their own.

What's cool is that the `Reversed` extension method is smart and will return a new `Sublist` if called on a `Sublist`. It returns a `Sublist` because all of the algorithms work against a `Sublist`. If it didn't you'd have to write a bunch of code to convert instances of `ReversedList` back to `Sublist`s. You can always get back to the original `ReversedList` by using `Sublist`'s `List` property.

`Reversed` will also return the underlying list if you try to reverse a reversed list. This is a good thing because you don't want to add a bunch of unnecessary layers on top of your lists. But wait! What if you reverse a sublist and then reverse it again?!?! Don't worry, NDex handles that, too. Essentially, calling `Reversed` two times in a row is a no-op. Phew!

Some algorithms will return an index, such as `IndexOf`. You can find the last index of a value simply by calling `Reversed` first. The only problem is that the index is in terms of the reversed list, not the underlying list. In order to get the index into the underlying list, just call the `BaseIndex` method on the `ReversedList`. You have to be a little more careful when searching for multiple items, like when trying to find the last duplicate items or the last sub-sequence. For example, here is the correct way to find last sub-sequence in a list:

    // Find the last occurrence of 1, 2, 3
    int[] values = new int[] { 1, 2, 3, 4, 5, 4, 1, 2, 3, 4, 5, 2, 3, 1, 2, 4 };
    var reversed = values.ToSublist().Reversed();
    var sequence = new int[] { 1, 2, 3 }.ToSublist().Reversed();
    int index = Sublist.IndexOfSequence(reversed, sequence);  // returns 7, not 9!
    index += sequence.Count - 1;  // move to the front of the original sequence (10)
    index = reversed.List.BaseIndex(index);  // convert to an index into the original list (6)
    var last = values.ToSublist(index, sequence.Count);
    
The good news is you can hide all this complexity behind a helper method if necessary. This is really the most complex reverse operation supported by NDex. It's good to know that you can implement backward operations by combining forward operations with a `ReversedList`.

## TypedList

## ReadOnlyList

## Algorithms

### Search Algorithms

### Sorting Algorithms

### Set Algorithms

### Heap Algorithms