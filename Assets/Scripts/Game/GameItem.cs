using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public GameItemType Type { get; private set; }

    public void SetType(GameItemType type)
    {
        Type = type;
    }
}
