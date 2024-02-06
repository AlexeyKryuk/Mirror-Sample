using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color _selected;
    [SerializeField] private Color _highlighted;

    private Button _button;
    private Image _image;
    private Color _normal;
    private bool _isSelected;

    public bool IsSelected => _isSelected;

    public event Action<bool> Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = _button.targetGraphic as Image;

        _normal = _image.color;
    }

    private void OnEnable() => _button.onClick.AddListener(OnClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnClick);

    public void Cancel()
    {
        if (_isSelected)
        {
            _isSelected = false;
            _image.color = _isSelected ? _selected : _normal;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = _isSelected ? _selected : _highlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _isSelected ? _selected : _normal;
    }

    private void OnClick()
    {
        _isSelected = !_isSelected;
        _image.color = _isSelected ? _selected : _normal;

        Clicked?.Invoke(_isSelected);
    }
}
