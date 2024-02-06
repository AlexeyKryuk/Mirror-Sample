using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class CellCollection : NetworkBehaviour
{
    [SerializeField] private List<Cell> _cells;

    public List<Cell> Cells => _cells;
}