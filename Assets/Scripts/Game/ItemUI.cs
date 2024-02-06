using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private CustomButton _button;
    private GameItemData _data;

    public GameItemData Data => _data;

    public event Action<ItemUI> Clicked;

    private void Awake()
    {
        _button = GetComponent<CustomButton>();
    }

    private void OnEnable() => _button.Clicked += OnClick;
    private void OnDisable() => _button.Clicked -= OnClick;

    public void Cancel() => _button.Cancel();

    public void Init(GameItemData data)
    {
        _data = data;
        _icon.sprite = data.Icon;
    }

    private void OnClick(bool value)
    {
        Clicked?.Invoke(this);
    }
}
