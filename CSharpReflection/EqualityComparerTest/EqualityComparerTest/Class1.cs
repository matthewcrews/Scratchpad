namespace EqualityComparerTest;

using System.Collections.Generic;

public class GarbageComparer : IEqualityComparer<string>
{
    private string _input;

    public GarbageComparer(string x)
    {
        _input = x;
    }

    public bool Equals(string? x, string? y)
    {
        return true;
    }

    public int GetHashCode(string obj)
    {
        return 1;
    }
}
