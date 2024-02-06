using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DictionaryExtension
{
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
         this TempDictionary<TKey, TValue> list)
    {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        for (int i = 0; i < list.Keys.Count; i++)
            dictionary.Add(list.Keys[i], list.Values[i]);

        return dictionary;
    }
}
