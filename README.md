# NDex

Unified algorithm support for indexed .NET collections.

Download using NuGet: [NDex](http://nuget.org/packages/ndex)

## Overview
There are a lot of classes in .NET that implement the `IList<T>` interface, including `T[]`, `List<T>` and `ObservableCollection<T>`. However, the `IList<T>` interface is really limited and most of its subclasses support very few operations. Only the `List<T>` class has a decent set of algorithms, and even then those are quite limited. LINQ provides a lot more functionality, but every operation creates a new collection. When working with a collection in-place is a must, you are forced to either deal with what .NET gives you or spin your own algorithms.

NDex is a heavily tested and efficient algorithms library for working with indexed collections in-place. Not only does it provide access to algorithms not otherwise available in .NET, it has useful overloads of those algorithms you're already familiar with.

## Sublist
In order to access the algorithms provided by NDex, you must wrap your list with a `Sublist`. `Sublist` allows you to specify a range over a list in which you want to apply operations. There are occasions when you only want to work in part of a list. Instead of providing a dozen overloads of each algorithm accepting a `startIndex` and `count` argument, you just always pass in a `Sublist`. NDex is smart and will work against the underlying list, so there's no overhead for wrapping a list.

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
    
The `ToSublist` extension methods accepts two integer arguments: an offset and a count. The offset is the index into the underlying list where the range should begin. The count is the number of items that should be in the `Sublist`. If only the offset is provided, the range will include the rest of the list. Once the `Sublist` is created, these can be accessed via the `Offset` and `Count` properties. `Sublist` also has a property for the underlying list, `List`, so you can always get back to it.

### Nesting, Shifting and Resizing
You should never need to wrap a `Sublist` with another `Sublist`. Instead, you should call the `Nest` method to create a new `Sublist`. The offset is in terms of the `Sublist`, not the underlying list. Just remember that the nested `Sublist` has to fit within the confines of the outer `Sublist`. There's no overhead for creating multiple nested `Sublist`s - they all work directly against the underlying list.

The `Sublist` class supplies a `Shift` method for moving the offset to the left or the right. It accepts a second parameter, `isChecked`, that when `false`, allows the operation to automatically shrink the `Sublist` if it goes past the end of the underlying list. If `isChecked` is `true`, an exception will be thrown.

Similarly, the `Resize` method allows you to resize the `Sublist` without modifying the `Offset`. It also accepts a second parameter, `isChecked`, that when `false`, allows the operation to limit the size of the sublist. If `isChecked` is `true`, an exception will be thrown.

### Sublists Can Be Invalidated
Since `Sublist` is just a thin wrapper around another list, it is possible that operations on the underlying list will invalidate the `Sublist`. Consider this example:

    var list = new List<int>() { 1, 2, 3, 4, 5 };
    var sublist = list.ToSublist();
    list.Remove(3);  // the sublist is now too big
    
Algorithms that modify a `Sublist` will return a *new* `Sublist`. If you are dealing with multiple `Sublist`s, you will need to take extra care.

### The Empty Sublist Trick
There is a useful trick you can perform using a `Sublist` with a `Count` of zero, so I will mention it here. There are a handful of algorithms that start with `Add`. These allow you to add new values to the end of a list. But what if you wanted to add items the front or the middle of a list? You can use the same `Add` methods as before, just create an empty `Sublist` whose offset is at the index you want to insert into. For example:

    List<int> values = new List<int>() { 1, 2, 3, 7, 8, 9 };
    var source = new int[] { 4, 5, 6 }.ToSublist();
    var destination = values.ToSublist(3, 0);  // Count of zero
    Sublist.Add(source, destination);  // 1, 2, 3, 4, 5, 6, 7, 8, 9
    
Those familiar with data structures might be concerned about the performance implications of inserting into the middle of a list. This is less of a concern with NDex - it is optimized to handle inserting multiple items into the middle of a list efficiently (at the cost of a single shift in items). Of course, you won't see any benefit at all if you call `Add` for each item individually. In that case, it might be more efficient to first create a second list and then insert it. Even more efficient would be to add to the end of the list and perform a `RotateLeft`.

### IReadOnlySublist, IMutableSublist and IExpandableSublist
There are three interfaces returned by the `ToSublist` method. The `IReadOnlySublist` interface prevents any modification to the underlying list. The `IMutableSublist` allows a value to be replaced at a particular index. Finally, `IExpandableSublist` allows items to be added or removed to the underlying list.

For instance, an array (`int[]`) has a fixed size. Calling `ToSublist` on an array will return a `IMutableSublist`. Algorithms guaranteeing that they will not add or remove items will accept an `IMutableSublist`.

## Substring
`String`s are immutable in .NET. NDex allows you to call algorithms on `string`s via the `Substring` class. This class creates a read-only wrapper around a `string`. You can get back to the original `string` using the `Value` property.

You create a `Substring` by calling `ToSubstring` on a `string`. Internally, this creates a thin `IList<char>` class that redirects to the underlying `string`. Of course, this class is read-only and so `Substring` implements the `IReadOnlySublist` interface.

If you want to manipulate `string`s, you will need to first convert your `string` into a `char[]` or a `List<char>`, using the LINQ `ToArray` or `ToList` algorithms, respectively. For example:

    var greeting = "Hello, World";
    var substring = greeting.ToSubstring();
    var sequence = "World".ToSubstring();
    int index = Sublist.IndexOfSequence(substring, sequence);
    List<char> list = new List<char>();
    Sublist.Add(greeting.Nest(0, index), list.ToSublist());  // "Hello, "
    Sublist.Add("Bob".ToSubstring(), list.ToSublist());  // "Hello, Bob"
    greeting = new String(list.ToArray());  // convert back to a string

## ReversedList
The `ReversedList` class creates a view over a list, creating the illusion that the items are reversed. `ReversedList` solves some tricky problems.

### Copying Items Backwards (but not in reverse...)
For a more interesting use of `ReversedList`, imagine that you wanted to shift items in a list to the right. You could try to use the `Copy` algorithm, but the results would probably surprise you. Here's what that would look like:

    // We're expecting: 1, 1, 2, 3, 4, 5
    int[] values = new int[] { 1, 2, 3, 4, 5, 0 };
    var source = values.ToSublist(0, 4);
    var destination = values.ToSublist(1);
    Sublist.Copy(source, destination);  // 1, 1, 1, 1, 1, 1
    
Inspecting the `Copy` algorithm, you'd see why this happens. Before the `2` can be copied, it is overwritten with `1`. This continues until the `1` gets propogated all the way to the right. Copying in the opposite direction, back-to-front, works as expected because the values aren't overwritten before they can be copied. Since NDex doesn't provide an algorithm for copying backward, it would seem like the only other option would be to write the code by hand.

But wait! We can solve this problem using the `ReversedList` class. This is the updated code:

    int[] values = new int[] { 1, 2, 3, 4, 5, 0 };
    var source = values.ToSublist(0, 4).Reversed();
    var destination = values.ToSublist(1).Reversed();
    Sublist.Copy(source, destination);
    
Here we call the `Reversed` extension method on each `Sublist`. It takes a little diagramming or a lot of imagination to see why this code works. At a high level, it is basically performing a back-to-front copy, but since the back is the front and the front is the back... it does the right thing. It's so odd, most people aren't likely to think of it on their own. But, hey - it works!

### Once a Sublist, Always a Sublist
What's cool is that the `Reversed` extension method is smart and will return a new `Sublist` if called on a `Sublist`. It returns a `Sublist` because all of the algorithms work against a `Sublist`. If it didn't you'd have to write a bunch of code to wrap your `ReversedList`s with another `Sublist`s. Not only would this be tedious, it would result in a minor performance penalty. If you need to get at the `ReversedList`, you can always use `Sublist`'s `List` property.

### Reversing a ReversedList
`Reversed` will return the original, underlying list if you try to reverse a reversed list. This is a good thing because you don't want to add a bunch of unnecessary layers on top of your lists. But wait! What if you reverse a sublist and then reverse it again?!?! Don't worry, NDex handles that, too. Essentially, calling `Reversed` two times in a row is a no-op.

### Translating Indicies
Some algorithms will return an index, such as `IndexOf`. You can find the last index of a value simply by calling `Reversed` on this list first. The only problem is that the index is in terms of the reversed list, not the underlying list. In order to get the index into the underlying list, just call the `BaseIndex` method on the `ReversedList`.

    int[] values = new int[] { 1, 2, 3, 4 };
    var reversed = values.Reversed();
    int index = Sublist.IndexOf(reversed.ToSublist(), 3);  // 1
    index = reversed.BaseIndex(index);  // 2

You have to be a little more careful when searching for duplicate items or the last sub-sequence. For example, here is the correct way to find the last sub-sequence in a list:

    // Find the last occurrence of 1, 2, 3
    int[] values = new int[] { 1, 2, 3, 4, 5, 4, 1, 2, 3, 4, 5, 2, 3, 1, 2, 4 };
    var reversed = values.ToSublist().Reversed();
    var sequence = new int[] { 1, 2, 3 }.ToSublist().Reversed();
    int index = Sublist.IndexOfSequence(reversed, sequence);  // returns 7, not 9!
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
.NET provides a `ReadOnlyCollection<T>` class in the `System.Collections.ObjectModel` namespace. Every now and then, you decide to expose a collection to other classes via a property (or whatever). However, you don't want other code messing with the internal state of your class. To prevent this, you can wrap your collection with a `ReadOnlyCollection`. In most applications, this is a perfectly acceptable solution.

However, in applications involving large amounts of data, the `ReadOnlyCollection<T>` class can lead to performance problems. Internally, the underlying list is stored as an `IList<T>`. Any operations performed on the list will incur the overhead of a polymorphic call.

NDex provides a slight variation to this class called `ReadOnlyList`. Similar to `Sublist`, it remembers the type of the underlying list so there is no overhead. It also provides a property, `List`, to get the original, underlying list.

The real reason to use `ReadOnlyList` is that you can call `ToSublist` on it, which will return an `IReadOnlySublist`. From there, you can use any of the read-only algorithms on it. Technically, you can wrap a `ReadOnlyCollection`, too, but that's when those polymorphic calls will suddenly matter again.

## Algorithms
NDex provides a large number of algorithms. They are optimized to perform as fast as possible without sacrificing safety. They provide various overloads to make them as reusable as possible, making them ideal when working with user-defined types.

The NDex algorithms follow different conventions than the built-in .NET algorithms. Read the following sections to see how they differ. These differences have a large impact on the code you write. Ultimately, code written using NDex will be more compact.

### Adding and Removing Items
Most of the NDex algorithms do not change the size of the underlying list. The `Add`* methods will add items to the end of a list. The only other algorithm that changes the list is `RemoveRange`, which will remove all the items in a `Sublist`.

A common mistake is that programmers expect `RemoveIf` and `RemoveDuplicates` to actually remove items. As strange as it might seem, these methods just shift items to the front of the list. These methods return an index - everything after that index is considered garbage. Here is how you can actually remove items from the underlying list:

    List<int> values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int index = Sublist.RemoveIf(values.ToSublist(), x => x % 2 != 0);  // shift evens forward
    Sublist.RemoveRange(values.ToSublist(index));  // 2, 4, 6, 8
    
Basically, you must call `RemoveRange` to actually shrink the list. You might be wondering why the items aren't actually removed. The `RemoveIf` and `RemoveDuplicates` methods don't assume that the underlying list can be resized. Imagine if you wanted to remove items from an array - arrays are a fixed size, so a `NotSupportedException` would be thrown. Another `Sublist` can always be used to *simulate* that an array is resized. Your code should try to limit calls to `RemoveRange`, only removing items after all processing is finished.

### Search Algorithms
When a .NET search algorithm can't find a value, it returns `-1`. NDex does something completely differently. It will instead return an index past the end of the `Sublist`. For example:

    int[] values = new int[] { 1, 2, 4, 5 };
    int index = Sublist.IndexOf(values.ToSublist(), 3);  // 4
    
You can always check to see if the match was a success by checking if the returned index equals the `Count` of the sublist.

Returning an index past the end of the list actually makes for cleaner code. Returning `-1`, your code would need to ask two questions: 1) am I past the end of the list? and 2) did I find the value? With NDex, you just need to ask whether you're past the end of the list. Here's an example that removes every occurrence of a sequence from a list:

    List<int> values = new List<int> { 1, 2, 3, 4, 5, 4, 1, 2, 3, 4, 5, 2, 3, 1, 2, 4 };
    int[] sequence = new int[] { 1, 2, 3 };
    int index = 0;
    while (index < values.Length)
    {
        var sublist = values.ToSublist(index);
        index = Sublist.IndexOfSequence(sublist, sequence.ToSublist());
        var garabage = sublist.Nest(index, 0);  // avoid assuming length
        garbage.Resize(sequence.Length, false);  // will do nothing if at end
        Sublist.RemoveRange(garbage);
    }
    
#### BinarySearch, LowerBound and UpperBound
If you have a sorted list, you can perform quick look-ups using the `BinarySearch`, `LowerBound` and `UpperBound` algorithms. There's also a `LowerAndUpperBound` method that finds both bounds in one go.

`LowerBound` returns the first index into the list where the item your looking for *could* be. If the item you're looking for is already in the list, it will be the index of the first occurrence. Otherwise, it will be the index where you can safely insert the item while keeping the list in sorted order.

`UpperBound` returns the index into the list past the last occurrence of the item you're looking for. If the item isn't in the list, `UpperBound` returns the same index as `LowerBound`.

You can use `LowerAndUpperBound` to get the indexes surrounding all occurrences of an item. For instance, the following example removes all occurrences of a particular value:

    List<int> values = new List<int>() { 1, 1, 2, 2, 2, 3, 3, 3, 4, 5, 5 };
    var result = Sublist.LowerAndUpperBound(values.ToSublist(), 3);
    int count = result.UpperBound - result.LowerBound;
    var occurrences = values.ToSublist(result.LowerBound, count);
    Sublist.RemoveRange(occurrences);  // 1, 1, 2, 2, 2, 4, 5, 5
    
The `BinarySearch` algorithm is similar to `LowerBound` in that it will find the first occurrence of a value. It returns a result class, providing an `Index` property. It also provides an `Exists` property that says whether the item was found. The result class automatically converts to a `Boolean` or an `Int32`, representing the `Exists` or the `Index` properties, respectively.

### Comparison Algorithms
If you need to compare two lists, you should use the `AreEqual`, `Compare` and `Mismatch` methods.

`AreEqual` returns `true` if the two lists have the same items and are the same size.

The `Compare` method will compare two lists in the same way `string`s are compared, using a [lexicographical comparison](http://en.wikipedia.org/wiki/Lexicographical_order). It will return `-1` if the first list is smaller or has an item smaller than what's in same position in the second list. It will return `1` if the first list is larger or has an item larger than what's in the same position in the second list. If the two lists are the same size and have the same items, it will return `0`.

Finally, `Mismatch` will return the index where two lists differ. If one list is shorter than the other, it will return the index past the end of the shorter list. If the items at a particular index are different, that index is returned. If all the items are the same and the lists are the same size, an index past the end of both lists will be returned. Both `AreEqual` and `Compare` are implemented in terms of `Mismatch`.

### Sorting Algorithms
The sorting algorithms are pretty straight-forward. What might surprise you is that there are multiple sort algorithms. Sorting algorithms have multiple properties:

* Do they require additional storage?
* Are they stable?
* Do they sort the entire list?
* How fast do they perform?

Most of the sorting algorithms will do their jobs in-place. However, `MergeSort` uses a separate buffer to do its job. The buffer can be almost any size, in case you need to conserve memory. By default, the buffer is half the size of the `Sublist`.

If calling a sorting algorithm on an already sorted list doesn't move any items, it is considered stable. Occasionally, this is a useful property, especially when moving items is a costly operation (e.g., a collection of large structs). Both `MergeSort` and `InsertionSort` are stable sorting algorithms.

Not all of the algorithms sort the entire `Sublist`. Namely, `PartialSort` will only sort a given number of values. This is different than nesting another `Sublist` and calling `QuickSort`. `PartialSort` is good when all you care about are the top "N" items. The `ItemAt` method is pretty interesting, too - it will move an item into the specified index as if the rest of the list was sorted.

The benefit of having different sorting algorithms is that you can try them out and compare their run times. If you have a small collection, you can try `BubbleSort`, `SelectionSort` or `InsertionSort`. If the collection has more than a few dozen items, use `MergeSort`, `HashSort`, `ShellSort` or `QuickSort`. Most of the time, you are safe just calling `MergeSort` or `QuickSort` because these will call `InsertionSort` automatically when it makes sense. Use `MergeSort` when you need a stable algorithm and use `QuickSort` otherwise.

### Set Algorithms
.NET `ISet<T>`s guarantee that every item is unique. NDex sets have an extra requirement: the sets must be *sorted*. Working with sorted sets typically results in faster set operations (O(N)), but it requires more effort on the programmer's part. For one, .NET set operations do not require that both collections be sets, whereas NDex algorithms do.

#### Creating a Set
It is easy to convert a list into a NDex set, using the `MakeSet` algorithm. A common mistake is to assume that `MakeSet` will remove items from the underlying list. Instead, `MakeSet` returns an index past the end of the set - any items past the index are considered garbage. If you want to shrink the list, you will have to do it manually. For example:

    List<int> values = new List<int>() { 1, 2, 3, 4, 2, 3, 4, 1, 5 };
    int index = Sublist.MakeSet(values.ToSublist());
    Sublist.RemoveRange(values.ToSublist(index));
    var set = values.ToSublist();  // 1, 2, 3, 4, 5

NDex doesn't assume that you want to re-size the list. This also makes the algorithm more reusable since it can be run against `Sublist`s wrapping fixed-length collections. Even when you can remove the items, you should consider whether removing them is truly necessary.

#### Working with Sets
Once you have a set or two, you can pass them to any of the algorithms expecting sets. These include variations of union, intersection, difference and symmetric difference. There is a version of each algorithm that adds the items to a list (`Add*`) and another version that copies the items (`Copy*`). The `Copy*` algorithms return the index past the end of the new set. What's interesting about some of the `Copy*` algorithms is that the destination can be the same as one of sources, so you can save a lot of memory when necessary.

### Heap Algorithms
If you need to efficiently track items by priority, the [heap algorithms](http://en.wikipedia.org/wiki/Heap_data_structure) are what you are looking for. Working with heap algorithms is interesting because they have unique pre- and post-conditions. For instance, this is the code for adding to a heap:

    List<int> values = new List<int>() { 1, 2, 3, 4, 5 };
    Sublist.MakeHeap(values.ToSublist());  // 5, 4, 3, 1, 2
    values.Add(6);  // 5, 4, 1, 2, 3 -> no longer a heap
    Sublist.HeapAdd(values.ToSublist());  // 6, 4, 5, 1, 2, 3

As you can see in this example, in order to add an item to a heap, you first place it past the end of the heap. Then you call `HeapAdd` passing in a `Sublist` wrapping the heap and the new item. It will move the item up the heap into its proper location.

Removing an item from a heap is similar. First you call `HeapRemove` to move the top item past the end of the heap. Then you can remove the last item from the list:

    Sublist.HeapRemove(values.ToSublist());  // 5, 4, 3, 1, 2, 6
    values.RemoveAt(values.Count - 1);

Anytime you have a heap, you can call `HeapSort` on it - `HeapSort` won't work on a list that isn't a proper heap. A benefit to `HeapSort` is that it has guaranteed runtime performance and can run faster than `MergeSort` in some cases.

### Miscellaneous Algorithms
There are a handful of additional algorithms:

* `AddRandomSamples`
* `CopyRandomSamples`
* `CountIf`
* `Fill`
* `ForEach`
* `Partition`
* `PreviousPermutation`
* `RandomShuffle`
* `Replace`
* `Reverse`
* `RotateLeft`
* `NextPermutation`
* `SwapRanges`
* `TrueForAll`
