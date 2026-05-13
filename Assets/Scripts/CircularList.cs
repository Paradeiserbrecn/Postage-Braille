using System;
using System.Collections.Generic;

public class CircularList<T> : List<T>
{
    private List<T> Items { get; }

    private int _currentIndex;

    public CircularList(IEnumerable<T> items)
    {
        Items = new List<T>(items);

        if (Items.Count == 0)
            throw new ArgumentException("Collection cannot be empty");
    }

    public CircularList()
    {
        Items = new List<T>();
    }

    public T Current => Items[_currentIndex];


    public T Next()
    {
        _currentIndex = (_currentIndex + 1) % Items.Count;
        return Current;
    }

    public T Previous()
    {
        _currentIndex = (_currentIndex - 1 + Items.Count) % Items.Count;
        return Current;
    }

    public new void Clear()
    {
        Items.Clear();
        _currentIndex = 0;
    }

    public override string ToString()
    {
        return string.Join(", ", Items);
    }

    public List<T> ToList() => Items;
}