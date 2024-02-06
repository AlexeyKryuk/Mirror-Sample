using System;
using System.Collections.Generic;

[Serializable]
public class TempDictionary<TKey, TValue>
{
    public List<TKey> Keys = new List<TKey>();
    public List<TValue> Values = new List<TValue>();

    public void Clear()
    {
        Keys.Clear();
        Values.Clear();
    }

    public void Add(TKey key, TValue value)
    {
        Keys.Add(key);
        Values.Add(value);
    }
}
