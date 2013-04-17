# NDex

Unified algorithm support for indexed .NET collections.

Download using NuGet: [NDex](http://nuget.org/packages/ndex)

## Overview
There are a lot of classes in .NET that implement the `IList<T>` interface, including `List<T>`, `T[]` and `ObservableCollection<T>`. The LINQ algorithms create new collections, so they don't work when collections need to be modified in-place. The problem is that `List<T>` is the only class with a decent set of algorithms, and even those aren't as flexible as needed.

NDex provides algorithms that work with all instances of `IList<T>`. Additionally, the library is designed to avoid the overhead of working against an interface (due to polymorphism). This means NDex runs extremely fast when working with large collections (10,000+ items). Best of all, NDex provides many useful algorithms that are missing from .NET and it provides convenient overloads of those you're already familiar with.

## An STL for Indexed Collections
People who are familiar with the C++ STL will appreciate NDex's semantics. Unlike .NET methods that return `-1` to indicate "not found", NDex will return an index past the end of the list. This makes it really easy to chain calls to different algorithms without needing to constantly check for magic values.

Instead of using iterators, NDex uses the `Sublist` class to divide `IList<T>` instances into chunks. A `Sublist` is defined by providing a list, an offset and a length. Any algorithms performed on the `Sublist` will only impact the piece of the `IList` that's wrapped.

    var values = new int[] { 10, 4, 2, 3, 1, 7, 8, 9, 5, 6 };
    var sublist = values.ToSublist(2, 6);
    Sublist.QuickSort(sublist);  // 10, 4, 1, 2, 3, 7, 8, 9, 5, 6
    
Other algorithms, such as set and heap operations, behave similarly to the STL's implementation. For those not familiar with the STL, there are a large number of unit tests for finding examples.
