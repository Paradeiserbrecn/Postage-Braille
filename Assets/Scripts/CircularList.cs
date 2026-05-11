using System;
using System.Collections.Generic;

public class CircularList<T>
{
    private readonly List<T> _items;
    private int _currentIndex;

    public CircularList(IEnumerable<T> items)
    {
        _items = new List<T>(items);

        if (_items.Count == 0)
            throw new ArgumentException("Collection cannot be empty");
    }

    public CircularList()
    {
        _items = new List<T>();
    }

    public T Current => _items[_currentIndex];

    
    public T Next()
    {
        _currentIndex = (_currentIndex + 1) % _items.Count;
        return Current;
    }

    public T Previous()
    {
        _currentIndex = (_currentIndex - 1 + _items.Count) % _items.Count;
        return Current;
    }

    public void Clear()
    {
        _items.Clear();
        _currentIndex = 0;
    }

    public T this[int i] => _items[i];

    public void Add(T item)
    {
        _items.Add(item);
    }

    public override string ToString()
    {
        return _items.ToArray().ToString();
    }
}
