# NDex

Unified algorithm support for indexed .NET collections.

Download using NuGet: [NDex](http://nuget.org/packages/ndex)

## Overview
There are a lot of classes in .NET that implement the `IList<T>` interface, including `T[]`, `List<T>` and `ObservableCollection<T>`. However, the `IList<T>` interface is really limited and most of its subclasses support very few operations. Only the `List<T>` class has a decent set of algorithms, and even then those are quite limited. LINQ provides a lot more functionality, but every operation creates a new collection. When working with a collection in-place is a must, you are forced to either deal with what .NET gives you or spin your own algorithms.

NDex is a heavily tested and efficient algorithms library for working with indexed collections in-place. Not only does it provide access to algorithms not otherwise available in .NET, it has useful overloads of those algorithms you're already familiar with.

## Sublist
In order to access the algorithms provided by NDex, you must wrap your list with a `Sublist`. `Sublist` allows you to specify a range over a list in which you want to apply operations. There are occasions when you only want to work in part of a list. Instead of providing a dozen overloads of each algorithm accepting a `startIndex` and `count` argument, you just always pass in a `Sublist`. NDex is smart and will work against the underlying list, so there's no overhead for wrapping a list.

The `Sublist` class has another benefit. In order for the algorithms to work directly with the underlying list, the type of the list must be known at compile time. Otherwise, all the operations would require polymorphic calls to the `IList<T>` interface. The overhead of these polymorphic calls has a dramatic impact on performance once a collection exceeds about 10,000 items. The `Sublist` class keeps track of the type of the underlying list via a generic argument. Normally, you don't need to know what that means in order to use NDex effectively, but I will try to explain. The `Sublist` class has the following signature:

    public sealed class Sublist<TList, T> : IExpandableSublist<TList, T>
        where TList : IList<T>
        
Developers unfamiliar with generics may be confused by this signature. Basically, it says that the `Sublist` class has two generic arguments: 1) the type of the list being wrapped and 2) the type of the elements in the list. The underlying list is *constrained* to being an `IList<T>`.

### Using ToSublist
Fortunately, you can avoid typing out this long signature simply by using the `ToSublist` extension methods. For example:

    int[] values = new int[] { 5, 4, 2, 1, 3, 8, 9, 7, 6 };
    var sublist = values.ToSublist();
    sublist.Sort().InPlace();  // 1, 2, 3, 4, 5, 6, 7, 8, 9
    
The compiler is smart enough to infer the generic arguments when you call `ToSublist`. Furthermore, you can use `var` to avoid even more typing. In the example above, it is important to point out that the original `values` array will be modified even though the `sublist` variable was passed to the algorithm. Again, the `Sublist` is just a thin wrapper around the underlying list.

There is an extension method for the most common built-in collections. You can write your own `ToSublist` extension method if you really want to (just check out the source code).

### Using Sublist to Create a View
The real power of `Sublist` is in its ability to represent a range over a list. For instance, the following example will first partition a list and then sort the two partitions:

    int[] values = new int[] { 5, 4, 2, 1, 3, 8, 9, 7, 6 };
    var sublist = values.ToSublist();
    int index = sublist.Partition(x => x % 2 == 0).InPlace();  // move evens to the front
    var front = values.ToSublist(0, index);  // range over the even numbers
    var back = values.ToSublist(index);  // range over the odd numbers
    front.Sort().InPlace();  // 2, 4, 6, 8
    back.Sort().InPlace();  // 1, 3, 5, 7, 9
    
The `ToSublist` extension methods accepts two integer arguments: an offset and a count. The offset is the index into the underlying list where the range should begin. The count is the number of items that should be in the `Sublist`. If only the offset is provided, the range will include the rest of the list. Once the `Sublist` is created, these can be accessed via the `Offset` and `Count` properties. `Sublist` also has a property for the underlying list, `List`, so you can always get back to it.

### Nesting, Shifting and Resizing
You should never need to wrap a `Sublist` with another `Sublist`. Instead, you should call the `Nest` method to create a new `Sublist`. The offset is in terms of the `Sublist`, not the underlying list. Just remember that the nested `Sublist` has to fit within the confines of the outer `Sublist`. There's no overhead for creating multiple nested `Sublist`s - they all work directly against the underlying list.

The `Sublist` class supplies a `Shift` method for moving the offset to the left or the right. It accepts a second parameter, `isChecked`, that when `false`, allows the operation to automatically shrink the `Sublist` if it goes past the end of the underlying list. If `isChecked` is `true` and the operation would push the sublist off the end of the underlying list, an exception will be thrown.

Similarly, the `Resize` method allows you to resize the `Sublist` without modifying the `Offset`. It also accepts a second parameter, `isChecked`, that when `false`, allows the operation to limit the size of the sublist. If `isChecked` is `true` and the resizing would cause the sublist to go past the end of the underlying list, an exception will be thrown.

### Sublists Can Be Invalidated
Since `Sublist` is just a thin wrapper around another list, it is possible that operations on the underlying list will invalidate the `Sublist`. Consider this example:

    var list = new List<int>() { 1, 2, 3, 4, 5 };
    var sublist = list.ToSublist();
    list.Remove(3);  // the sublist is now too big
    
Algorithms that modify a `Sublist` will return a *new* `Sublist`. If you are dealing with multiple `Sublist`s, you will need to take extra care.

### The Empty Sublist Trick
There is a useful trick you can perform using a `Sublist` with a `Count` of zero, so I will mention it here. There are a handful of algorithms that add to the underlying list. These allow you to add new values to the end of a `Sublist`. But what if you wanted to add items to the front or the middle of a list? You can use the same `Add` methods as before, just create an empty `Sublist` whose offset is at the index you want to insert the values. For example:

    List<int> values = new List<int>() { 1, 2, 3, 7, 8, 9 };
    var source = new int[] { 4, 5, 6 }.ToSublist();
    var destination = values.ToSublist(3, 0);  // Count of zero
    source.AddTo(destination);  // 1, 2, 3, 4, 5, 6, 7, 8, 9
    
Those familiar with data structures might be concerned about the performance implications of inserting into the middle of a list. This is less of a concern with NDex - it is optimized to handle inserting multiple items into the middle of a list efficiently (at the cost of a single shift in items). Of course, you won't see any benefit at all if you call `AddTo` many, many times. In that case, it might be more efficient to first create a second list and then insert it. Even more efficient would be to add to the end of the list and perform a `RotateLeft`.

### IReadOnlySublist, IMutableSublist and IExpandableSublist
There are three interfaces returned by the `ToSublist` method. The `IReadOnlySublist` interface prevents any modification to the underlying list whatsoever. The `IMutableSublist` allows a value to be replaced at a particular index. Finally, `IExpandableSublist` allows items to be added to or removed from the underlying list.

For instance, an array (`int[]`) has a fixed size. Calling `ToSublist` on an array will return an `IMutableSublist`. Algorithms guaranteeing that they will not add or remove items will accept an `IMutableSublist`.

## Substring
`String`s are immutable in .NET. NDex allows you to call algorithms on `string`s via the `Substring` class. This class creates a read-only wrapper around a `string`. You can get back to the original `string` using the `Value` property.

You create a `Substring` by calling `ToSubstring` on a `string`. Internally, this creates a thin `IList<char>` class that redirects to the underlying `string`. Of course, this class is read-only and so `Substring` implements the `IReadOnlySublist` interface.

If you want to manipulate `string`s, you will need to first convert your `string` into a `char[]` or a `List<char>`, using the LINQ `ToArray` or `ToList` algorithms, respectively. For example:

    var greeting = "Hello, World";
    var substring = greeting.ToSubstring();
    var sequence = "World".ToSubstring();
    int index = substring.FindSequence(sequence);
    List<char> list = new List<char>();
    greeting.Nest(0, index).AddTo(list.ToSublist());  // "Hello, "
    "Bob".ToSubstring().AddTo(list.ToSublist());  // "Hello, Bob"
    greeting = new String(list.ToArray());  // convert back to a string

## ReversedList
The `ReversedList` class creates a view over a list, creating the illusion that the items are reversed. `ReversedList` can be used to solve some tricky problems.

### Copying Items Backward (but not in reverse...)
For an interesting use of `ReversedList`, imagine that you wanted to shift items in a list to the right. You could try to use the `CopyTo` algorithm, but the results would probably surprise you. Here's what that would look like:

    // We're expecting: 1, 1, 2, 3, 4, 5
    int[] values = new int[] { 1, 2, 3, 4, 5, 0 };
    var source = values.ToSublist(0, 5);
    var destination = values.ToSublist(1);
    source.CopyTo(destination);  // 1, 1, 1, 1, 1, 1
    
Inspecting the `CopyTo` algorithm, you'd see why this happens. Before the `2` can be copied, it is overwritten with `1`. This continues until the `1` gets propagated all the way to the right. Copying in the opposite direction, back-to-front, works as expected because the values aren't overwritten before they can be copied. Since NDex doesn't provide an algorithm for copying backward, it would seem like the only other option would be to write the code by-hand.

But wait! We can solve this problem using the `ReversedList` class. This is the updated code:

    int[] values = new int[] { 1, 2, 3, 4, 5, 0 };
    var source = values.ToSublist(0, 5).Reversed();
    var destination = values.ToSublist(1).Reversed();
    source.CopyTo(destination);
    
Here we call the `Reversed` extension method on each `Sublist`. It takes a little diagramming or a lot of imagination to see why this code works. At a high level, it is basically performing a back-to-front copy, but since the back is the front and the front is the back... it does the right thing. It's so odd, most people aren't likely to think of it on their own. But, hey - it works!

### Once a Sublist, Always a Sublist
What's cool is that the `Reversed` extension method is smart and will return a new `Sublist` if called on a `Sublist`. It returns a `Sublist` because all of the algorithms work against a `Sublist`. If it didn't you'd have to write a bunch of code to wrap your `ReversedList`s with another `Sublist`s. Not only would this be tedious, it would result in a minor performance penalty. If you need to get at the `ReversedList`, you can always use `Sublist`'s `List` property.

### Reversing a ReversedList
`Reversed` will return the original, underlying list if you try to reverse a reversed list. This is a good thing because you don't want to add a bunch of unnecessary layers on top of your lists. But wait! What if you reverse a sublist and then reverse it again?!?! Don't worry, NDex handles that, too. Essentially, calling `Reversed` two times in a row is a no-op.

### Translating Indices
Some algorithms will return an index, such as `Find`. You can find the last index of a value simply by calling `Reversed` on this list first. The only problem is that the index is in terms of the reversed list, not the underlying list. In order to get the index into the underlying list, just call the `BaseIndex` method on the `ReversedList`.

    int[] values = new int[] { 1, 2, 3, 4 };
    var reversed = values.Reversed();
    int index = reversed.ToSublist().Find(3);  // 1
    index = reversed.BaseIndex(index);  // 2

You have to be a little more careful when searching for the last duplicate items or sub-sequence. For example, here is the correct way to find the last sub-sequence in a list:

    // Find the last occurrence of 1, 2, 3
    int[] values = new int[] { 1, 2, 3, 4, 5, 4, 1, 2, 3, 4, 5, 2, 3, 1, 2, 4 };
    var reversed = values.ToSublist().Reversed();
    var sequence = new int[] { 1, 2, 3 }.ToSublist().Reversed();  // reverse the sequence, too!!
    int index = reversed.FindSequence(sequence);  // returns 7, not 9!
    var lastSeqReversed = reversed.Nest(index, sequence.Count);  // wrap 3, 2, 1
    var lastSeq = lastSeqReversed.Reversed();  // restore it to 1, 2, 3
    index = lastSeq.Offset;  // 6
    
The good news is you can hide all this complexity behind a helper method if necessary. Just know that this is really all the more complicated it can get.

## TypedList
If you are forced to work with non-generic collections, you can still use NDex. NDex provides a `TypedList` class that creates a type-safe wrapper around the non-generic `IList` interface. There is a `Typed<T>` extension method that you can call on `Array` or `ArrayList`. For example:

    ArrayList list = new ArrayList();
    var typed = list.Typed<int>();
    
From there, you can call `ToSublist` to gain access to the algorithms.

## ReadOnlyList
.NET provides a `ReadOnlyCollection<T>` class in the `System.Collections.ObjectModel` namespace. Every now and then, library designers decide to expose a collection via a property or return value. However, those same designers don't want other code messing with the internal state of their classes. To prevent this, they can wrap their collections with a `ReadOnlyCollection`. In most applications, this is a perfectly acceptable solution.

However, in applications involving large amounts of data, the `ReadOnlyCollection<T>` class can lead to performance problems. Internally, the underlying list is stored as an `IList<T>`. Any operations performed on the list will incur the overhead of a polymorphic call.

NDex provides a slight variation to this class called `ReadOnlyList`. Similar to `Sublist`, it remembers the type of the underlying list so there is no overhead. It also provides a property, `List`, to get the original, underlying list.

The real reason to use `ReadOnlyList` is that you can call `ToSublist` on it, which will return an `IReadOnlySublist`. From there, you can use any of the read-only algorithms on it. Technically, you can wrap a `ReadOnlyCollection`, too, but that's when those polymorphic calls will suddenly matter again.

## Algorithms
NDex provides a large number of algorithms. They are optimized to perform as fast as possible without sacrificing safety. They provide various overloads to make them as reusable as possible, making them ideal when working with user-defined types.

The NDex algorithms follow different conventions than the built-in .NET algorithms. Read the following sections to see how they differ. These differences have a large impact on the code you write. Ultimately, code written using NDex will be more compact.

### CopyTo, AddTo and InPlace Algorithms
Some algorithms can be performed in-place, copied over top of another list or added to the end of a list. Whenever you perform a copy or add, the source list is unmodified.

A common mistake is to forget to call `InPlace`. For example:

    var values = new List<int>() { 1, 2, -3, -4, 5 };
    values.Replace(i => i < 0, i => -i);  // won't do anything
    
The code above will essentially do nothing. Without calling `InPlace`, NDex doesn't know that you intend to do the operation in-place. The last line should look like this:

    values.Replace(i => i < 0, i => -1).InPlace();  // 1, 2, 3, 4, 5
    
When you call the `Replace` function you are creating an intermediate object. Most intermediate objects implement `IEnumerable<T>`, so you can use them in a `foreach` loop or intermix their use with LINQ:

    Random random = new Random();
    foreach (int value in values.RandomShuffle(random))
    {
        // do something with value
    }
    
Many of the names in NDex correspond with LINQ. This allows you to write code that initially works with LINQ, but allows you to switch over to NDex when performance matters. However, don't get the impression that you can switch between LINQ and NDex painlessly.

#### CopyTo and Destination Length
One thing to consider when using `CopyTo` is that it stops prematurely if the destination is too small. It returns indexes into the source and destination lists, indicating where the algorithm stopped. In some cases, you can use this information to "pick up" where the algorithm left off. In other cases, the results may surprise you. You should try to guarantee the length of the destination in order to avoid unexpected results.

### Search Algorithms
When a .NET search algorithm can't find a value, it returns `-1`. NDex does something completely differently. It will instead return an index past the end of the `Sublist`. For example:

    int[] values = new int[] { 1, 2, 4, 5 };
    int index = values.ToSublist().Find(3);  // 4
    
You can always check to see if the match was a success by checking if the returned index equals the `Count` of the `Sublist`.

Returning an index past the end of the list actually makes for cleaner code. Returning `-1`, your code would need to ask two questions: 1) am I past the end of the list? and 2) did I find the value? With NDex, you just need to ask whether you're past the end of the list. Here's an example that removes every occurrence of a sequence from a list:

    List<int> values = new List<int> { 1, 2, 3, 4, 5, 4, 1, 2, 3, 4, 5, 2, 3, 1, 2, 4 };
    int[] sequence = new int[] { 1, 2, 3 };
    int index = 0;
    while (index < values.Count)
    {
        var sublist = values.ToSublist(index);
        index += values.ToSublist(index).FindSequence(sequence.ToSublist());
        var garbage = values.ToSublist(index, 0);  // avoid assuming length
        garbage = garbage.Resize(sequence.Length, false);  // will do nothing if at end
        garbage.Clear();
    }
    
All of the `Find*` algorithms return a `SearchResult` object. This object contains two properties: `Index` and `Exists`. These will tell you where the search value was found and whether it was found at all. The `SearchResult` class will automatically convert to an `int` or a `bool` representing the two properties.
    
#### BinarySearch, LowerBound and UpperBound
If you have a sorted list, you can perform quick look-ups using the `BinarySearch`, `LowerBound` and `UpperBound` algorithms. There's also a `LowerAndUpperBound` method that finds both bounds in one go.

`LowerBound` returns the first index into the list where the item your looking for *could* be. If the item you're looking for is already in the list, it will be the index of the first occurrence. Otherwise, it will be the index where you can safely insert the item while keeping the list in sorted order.

`UpperBound` returns the index into the list past the last occurrence of the item you're looking for. If the item isn't in the list, `UpperBound` returns the same index as `LowerBound`.

You can use `LowerAndUpperBound` to get the indexes surrounding all occurrences of an item. For instance, the following example removes all occurrences of a particular value:

    List<int> values = new List<int>() { 1, 1, 2, 2, 2, 3, 3, 3, 4, 5, 5 };
    var result = values.ToSublist().LowerAndUpperBound(3);
    int count = result.UpperBound - result.LowerBound;
    var occurrences = values.ToSublist(result.LowerBound, count);
    occurrences.Clear();  // 1, 1, 2, 2, 2, 4, 5, 5

### Comparison Algorithms
If you need to compare two lists, you should use the `IsEqualTo`, `CompareTo` and `Mismatch` methods.

`IsEqualTo` returns `true` if the two lists have the same items and are the same length.

The `CompareTo` method will compare two lists in the same way `string`s are compared, using a [lexicographical comparison](http://en.wikipedia.org/wiki/Lexicographical_order). It will return `-1` if the first list is smaller or has an item smaller than what's in same position in the second list. It will return `1` if the first list is larger or has an item larger than what's in the same position in the second list. If the two lists are the same length and have the same items, it will return `0`.

Finally, `Mismatch` will return the index where two lists differ. If one list is shorter than the other, it will return the index past the end of the shorter list. If the items at a particular index are different, that index is returned. If all the items are the same and the lists are the same length, an index past the end of both lists will be returned.

### Sorting Algorithms
The sorting algorithms are pretty straight-forward. What might surprise you is that there are multiple sort algorithms. Sorting algorithms have multiple properties:

* Do they require additional storage?
* Are they stable?
* Do they sort the entire list?
* How fast do they perform?

Most of the sorting algorithms will do their jobs in-place. However, `StableSort` uses a separate buffer to do its job. The buffer can be almost any size, in case you need to conserve memory. By default, the buffer is half the size of the `Sublist`.

If calling a sorting algorithm on an already sorted list doesn't move around any items, it is considered stable. Occasionally, this is a useful property, especially when moving items is a costly operation (e.g., a collection of large structs).

Not all of the algorithms sort the entire `Sublist`. Namely, `PartialSort` will only sort a given number of values. This is different than nesting another `Sublist` and calling `Sort`. `PartialSort` is good when all you care about are the top "N" items. The `ItemAt` method is pretty interesting, too - it will move an item into the specified index as if the rest of the list was sorted. `ItemAt` is useful when you want to know what value came in Nth place.

### Set Algorithms
.NET `ISet<T>`s guarantee that every item is unique. NDex sets have an extra requirement: the sets must be *sorted*. Working with sorted sets typically results in faster set operations (O(N)), but it requires more effort on the programmer's part. For one, .NET set operations do not require that both collections be sets, whereas NDex algorithms do.

#### Creating a Set
It is easy to convert a list into a NDex set, using the `MakeSet` algorithm. A common mistake is to assume that the in-place version of `MakeSet` will remove items from the underlying list. Instead, the in-place `MakeSet` returns an index past the end of the set - any items past the index are considered garbage. If you want to shrink the list, you will have to do it manually. For example:

    List<int> values = new List<int>() { 1, 2, 3, 4, 2, 3, 4, 1, 5 };
    int index = values.ToSublist().MakeSet().InPlace();
    values.ToSublist(index).Clear();
    var set = values.ToSublist();  // 1, 2, 3, 4, 5

NDex doesn't assume that you want to re-size the list. This makes the algorithm more reusable since it can be run against `Sublist`s wrapping fixed-length collections. Even when you can remove the items, you should consider whether removing them is truly necessary.

#### Working with Sets
Once you have a set or two, you can pass them to any of the algorithms expecting sets. These include: `Union`, `Intersect`, `Except`, `SymmetricExcept` and `IsOverlapping`.

### Heap Algorithms
If you need to efficiently track items by priority, the [heap algorithms](http://en.wikipedia.org/wiki/Heap_data_structure) are what you are looking for. Working with heap algorithms is interesting because they have unique pre- and post-conditions. For instance, this is the code for adding to a heap:

    List<int> values = new List<int>() { 1, 2, 3, 4, 5 };
    values.ToSublist().MakeHeap().InPlace();  // 5, 4, 3, 1, 2
    values.Add(6);  // 5, 4, 1, 2, 3, 6 -> no longer a heap
    values.ToSublist().HeapAdd();  // 6, 4, 5, 1, 2, 3

As you can see in this example, in order to add an item to a heap, you first place it past the end of the heap. Then you call `HeapAdd` passing in a `Sublist` wrapping the heap and the new item. It will move the item up the heap into its proper location.

Removing an item from a heap is similar. First you call `HeapRemove` to move the top item past the end of the heap. Then you can remove the last item from the list:

    values.ToSublist().HeapRemove();  // 5, 4, 3, 1, 2, 6
    values.RemoveAt(values.Count - 1);

Anytime you have a heap, you can call `HeapSort` on it - `HeapSort` won't work on a list that isn't a proper heap. A benefit to `HeapSort` is that it has guaranteed runtime performance and can run faster than `StableSort` in some cases.
