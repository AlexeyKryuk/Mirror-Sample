using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Match : NetworkBehaviour
{
    [SerializeField] private string _id;

    public readonly List<GameObject> Players = new List<GameObject>();

    public Match(string id, GameObject player)
    {
        _id = id;
        Players.Add(player);
    }

    public string ID => _id;
}
