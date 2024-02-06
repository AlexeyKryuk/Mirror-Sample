using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameRules
{
    private const uint Lenght = 4;

    public GameItemType CurrentType { get; private set; }

    public bool IsComplete(Cell cell)
    {
        if (cell.Item == null)
            return false;

        int count = 1;
        CurrentType = cell.Item.Type;

        foreach (var neighbor in cell.Neighbors)
        {
            if (neighbor.Value.Item != null)
            {
                if (neighbor.Value.Item.Type == CurrentType)
                {
                    count++;

                    Vector2Int target = neighbor.Key;
                    SearchInside(ref count, neighbor.Value, target);

                    target *= -1;

                    if (cell.Neighbors.ContainsKey(target))
                        SearchInside(ref count, cell, target);

                    if (count >= Lenght)
                        return true;
                    else
                        count = 1;
                }
            }
        }

        return count >= Lenght;
    }

    private void SearchInside(ref int count, Cell neighbor, Vector2Int target)
    {
        for (int i = 0; i < Lenght; i++)
        {
            var neighbors = neighbor.Neighbors;

            if (neighbors.ContainsKey(target) && HasItemWithType(neighbors[target], CurrentType))
            {
                count++;
                neighbor = neighbors[target];
            }
            else
                break;
        }
    }

    private bool HasItemWithType(Cell cell, GameItemType type)
    {
        if (cell.Item != null)
            return cell.Item.Type == type;

        return false;
    }
}
