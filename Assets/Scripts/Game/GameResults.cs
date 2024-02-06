using Mirror;
using System;
using UnityEngine;

public class GameResults : NetworkBehaviour
{
    [SerializeField] private ItemSpawner _itemSpawner;
    [SerializeField] private FinishGameScreen _finishGameScreen;

    private GameRules _rules;

    private void Awake()
    {
        _rules = new GameRules();
    }

    private void OnEnable()
    {
        _itemSpawner.Spawned += CheckCompletion;
    }

    private void OnDisable()
    {
        _itemSpawner.Spawned -= CheckCompletion;
    }

    private void CheckCompletion(Cell cell)
    {
        if (_rules.IsComplete(cell))
        {
            _finishGameScreen.RpcOnGameFinished(_rules.CurrentType);
            Debug.Log(_rules.CurrentType + " won");
        }
    }
}
