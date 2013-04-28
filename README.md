# NDex

Unified algorithm support for indexed .NET collections.

Download using NuGet: [NDex](http://nuget.org/packages/ndex)

## Overview
There are a lot of classes in .NET that implement the `IList<T>` interface, including `T[]`, `List<T>` and `ObservableCollection<T>`. However, the `IList<T>` interface is really limited and most of its subclasses support very few operations. Only the `List<T>` class has a decent set of algorithms, and even then it is quite limited. LINQ provides a lot more functionality, but every operation creates a new collection. When working with a collection in-place is a must, you are forced to either deal with what .NET gives you or spin your own algorithms.

NDex is a heavily tested and efficient algorithms library for working with indexed collections in-place. Not only does it provide access to algorithms not otherwise available in .NET, it has useful overloads of those algorithms you're already familiar with.

## Sublist
In order to access the algorithms provided by NDex, you must wrap your list with a `Sublist`. `Sublist` allows you to specify a range over a list in which you want to apply an operation. There are occasions when you only want to sort part of a list or search for a value after a particular index. Instead of providing a dozen overloads of each algorithm accepting a `startIndex` and `count` argument, you just always pass in a `Sublist`. NDex is smart and will work against the underlying list, so there's no overhead for wrapping a list.

The `Sublist` class has another benefit. In order for the algorithms to work directly with the underlying list, the type of the list must be known at compile time. Otherwise, all the operations would require polymorphic calls to the `IList<T>` interface. The overhead of these polymorphic calls has a dramatic impact on performance once a collection exceeds about 10,000 items. The `Sublist` class keeps track of the type of the underlying list via a generic argument. Normally, you don't need to know what that means in order to use NDex effectively, but I will try to explain. The `Sublist` class has the following signature:

    public sealed class Sublist<TList, T> : IExpandableSublist<T>
        where TList : IList<T>
        
Developers unfamiliar with generics may be confused by this signature. Basically, it says that the `Sublist` class has two generic arguments: 1) the type of the list being wrapped and 2) the type of the elements in the list. The underlying list is *constrained* to being an `IList<T>`.

### Using ToSublist
Fortunately, you can avoid typing out this long signature simply by using the `ToSublist` extension methods. For example:

    int[] values = new int[] { 5, 4, 2, 1, 3, 8, 9, 7, 6 };
    var sublist = values.ToSublist();
    Sublist.QuickSort(sublist);  // 1, 2, 3, 4, 5, 6, 7, 8, 9
    
The compiler is smart enough to infer the generic arguments when you call `ToSublist`. Furthermore, you can use `var` to avoid even more typing. In the example above, it is important to point out that the original `values` array will be modified eventhough the `sublist` variable was passed to the algorithm. Again, the `Sublist` is just a thin wrapper around the underlying list.

### Using Sublist to Create a View
The real power of `Sublist` is in its ability to represent a range over a list. For instance, the following example will first partition a list and then sort the two partitions:

    int[] values = new int[] { 5, 4, 2, 1, 3, 8, 9, 7, 6 };
    var sublist = values.ToSublist();
    int index = Sublist.Partition(sublist, x => x % 2 == 0);  // move evens to the front
    var front = values.ToSublist(0, index);  // range over the even numbers
    var back = values.ToSublist(index);  // range over the odd numbers
    Sublist.QuickSort(front);  // 2, 4, 6, 8
    Sublist.QuickSort(back); // 1, 3, 5, 7, 9
    
The `ToSublist` extension methods accepts two integer arguments: an offset and a count. The offset is the index into the underlying list where the range should begin. The count is the number of items that should be in the `Sublist`. If only the offset is provided, the range will include the rest of the list. Once the `Sublist` is created, these can be accessed via the `Offset` and `Count` properties. A nice feature of `Sublist` is that the `Offset` and `Count` properties are editable. If the `Offset` is modified such that the `Count` would be invalidated, it is automatically resized. `Sublist` also has a property for the underlying list, so you can always get back to it.

### Sublists Can Be Invalidated
Since `Sublist` is just a wrapper around another list, it is possible that operations on the underlying list will invalidate the `Sublist`. Consider this example:

    var list = new List<int>() { 1, 2 3, 4, 5 };
    var sublist = list.ToSublist();
    list.Remove(3);  // the sublist is now too big

### The Empty Sublist Trick
There is a useful trick you can perform using a `Sublist` with a `Count` of zero, so I will mention it here. There are a handful of algorithms that start with `Add`. These allow you to add new values to the end of a list. But what if you wanted to add items the front or the middle of a list? You can use the same `Add` methods as before, just create an empty `Sublist` whose offset is at the index you want to insert into. For example:

    List<int> values = new List<int>() { 1, 2, 3, 7, 8, 9 };
    var source = new int[] { 4, 5, 6 }.ToSublist();
    var destination = values.ToSublist(3, 0);  // Count of zero
    Sublist.Add(source, destination);  // 1, 2, 3, 4, 5, 6, 7, 8, 9
    
Those familiar with data structures might be concerned about the performance implications of inserting into the middle of a list. This is less of a concern with NDex - it is optimized to handle inserting multiple items into the middle of a list efficiently (at the cost of a single shift in items). Of course, you won't see any benefit at all if you call `Add` for each item individually. In that case, it might be more efficient to first create a second list and then insert it. Even more efficient would be to add to the end of the list and perform a `RotateLeft`.

### IReadOnlySublist, IMutableSublist and IExpandableSublist
There are three interfaces returned by the `ToSublist` method. The `IReadOnlySublist` interface prevents any modification to the underlying list. The `IMutableSublist` allows a value to be replaced at a particular index. Finally, `IExpandableSublist` allows items to be removed or added to the underlying list.

For instance, an array (`int[]`) has a fixed size. Calling `ToSublist` on an array will return a `IMutableSublist`. Algorithms guaranteeing that they will not add or remove items will accept an `IMutableSublist`.

## Substring
`String`s are immutable in .NET. NDex allows you to call algorithms on `string`s via the `Substring` class. This class creates a read-only wrapper around a `string`. You can get back to the original `string` using the `Value` property.

You create a `Substring` by calling `ToSubstring` on a `string`. Internally, this creates a thin `IList<char>` class that redirects to the underlying `string`. Of course, this class is read-only and so `Substring` implements the `IReadOnlySublist` interface.

If you want to manipulate `string`s, you will need to first convert your `string` into a `char[]` or a `List<char>`, using the LINQ `ToArray` or `ToList` algorithms, respectively. For example:

    var greeting = "Hello, World";
    var substring = greeting.ToSubstring();
    var sequence = "World".ToSubstring();
    int index = Sublist.IndexOfSequence(substring, sequence);
    List<char> list = greeting.ToList();
    Sublist.RemoveRange(list.ToSublist(index));  // "Hello, "
    Sublist.Add("Bob".ToSubstring(), list.ToSublist());  // "Hello, Bob"
    greeting = new String(list.ToArray());

## ReversedList
The `ReversedList` class creates a view over a list, creating the illusion that the items are reversed. `ReversedList` solves some tricky problems.

### Copying Items Backwards (but not reversed...)
For a more interesting use of `ReversedList`, imagine that you wanted to shift items in a list to the right. You could try to use the `Copy` algorithm, but the results would probably surprise you. Here's what that would look like:

    // We're expecting: 1, 1, 2, 3, 4, 5
    int[] values = new int[] { 1, 2, 3, 4, 5, 0 };
    var source = values.ToSublist(0, 4);
    var destination = values.ToSublist(1);
    Sublist.Copy(source, destination);  // 1, 1, 1, 1, 1, 1
    
Inspecting the `Copy` algorithm, you'd see why this happens. Before the `2` can be copied, it is overwritten with `1`. This continues until the `1` gets propogated all the way to the right. Copying in the opposite direction, back-to-front, works as expected because the values aren't overwritten before they can be copied. Since NDex doesn't provide an algorithm for copying backward, it would seem like the only other option would be to write the code by hand.

But wait, we can solve this problem using the `ReversedList` class. This is the updated code:

    int[] values = new int[] { 1, 2, 3, 4, 5, 0 };
    var source = values.ToSublist(0, 4).Reversed();
    var destination = values.ToSublist(1).Reversed();
    Sublist.Copy(source, destination);
    
Here we call the `Reversed` extension method on each `Sublist`. It takes a little diagramming or a lot of imagination to see why this code works. At a high level, it is basically performing a back-to-front copy, but since the back is the front and the front is the back... it does the right thing. It's so odd, most people aren't likely to think of it on their own.

### Once a Sublist, Always a Sublist
What's cool is that the `Reversed` extension method is smart and will return a new `Sublist` if called on a `Sublist`. It returns a `Sublist` because all of the algorithms work against a `Sublist`. If it didn't you'd have to write a bunch of code to wrap your `ReversedList`s back to `Sublist`s. You can always get back to the underlying `ReversedList` by using `Sublist`'s `List` property.

### Reversing a ReversedList
`Reversed` will also return the original, underlying list if you try to reverse a reversed list. This is a good thing because you don't want to add a bunch of unnecessary layers on top of your lists. But wait! What if you reverse a sublist and then reverse it again?!?! Don't worry, NDex handles that, too. Essentially, calling `Reversed` two times in a row is a no-op.

### Translating Indicies
Some algorithms will return an index, such as `IndexOf`. You can find the last index of a value simply by calling `Reversed` on this list first. The only problem is that the index is in terms of the reversed list, not the underlying list. In order to get the index into the underlying list, just call the `BaseIndex` method on the `ReversedList`.

    int[] values = new int[] { 1, 2, 3, 4 };
    var reversed = values.Reversed();
    int index = Sublist.IndexOf(reversed.ToSublist(), 3);  // 1
    index = reversed.BaseIndex(index);  // 2

You have to be a little more careful when searching for duplicate items or the last sub-sequence. For example, here is the correct way to find last sub-sequence in a list:

    // Find the last occurrence of 1, 2, 3
    int[] values = new int[] { 1, 2, 3, 4, 5, 4, 1, 2, 3, 4, 5, 2, 3, 1, 2, 4 };
    var reversed = values.Reversed();
    var sequence = new int[] { 1, 2, 3 }.Reversed();
    int index = Sublist.IndexOfSequence(reversed.ToSublist(), sequence.ToSublist());  // returns 7, not 9!
    index += sequence.Count - 1;  // move to the front of the original sequence (10)
    index = reversed.BaseIndex(index);  // convert to an index into the original list (6)
    
The good news is you can hide all this complexity behind a helper method if necessary. Just know that this is really all the more complicated it can get.

## TypedList
If you are forced to work with non-generic collections, you can still use NDex. NDex provides a `TypedList` class that creates a type-safe wrapper around the non-generic `IList` interface. There is a `Typed<T>` extension method that you can call on `Array` or `ArrayList`. For example:

    ArrayList list = new ArrayList();
    var typed = list.Typed<int>();
    
From there, you can call `ToSublist` to gain access to the algorithms.

## ReadOnlyList
.NET provides a `ReadOnlyCollection<T>` class in the `System.Collections.ObjectModel` namespace. Every now and then, you decide to expose a collection to other classes via a property (or whatever). However, you don't want other code messing with the internal state of your class. To prevent this, you can wrap your collection with a `ReadOnlyCollection`. In most applications, this is a perfectly acceptable solution.

However, in applications involving large amounts of data, the `ReadOnlyCollection<T>` class can lead to performance problems. Internally, the underlying list is stored as an `IList<T>`. Any operations performed on the list will incur the overhead of a polymorphic call.

NDex provides a slight variation to this class called `ReadOnlyList`. Similar to `Sublist`, it remembers the type of the underlying list so there is no overhead. It also provides a property, `List`, to get the original, underlying list.

The real reason to use `ReadOnlyList` is that you can call `ToSublist` on it, which will return an `IReadOnlySublist`. From there, you can use any of the read-only algorithms on it. Technically, you can wrap a `ReadOnlyCollection`, too, but that's when those polymorphic calls will suddenly matter again.

## Algorithms

### Search Algorithms

### Sorting Algorithms

### Set Algorithms

### Heap Algorithms
