using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPaperUI : MonoBehaviour
{
    [SerializeField]
    private float _newOrderWaitTime = 1f;
    [SerializeField]
    private float _busyWaitTime = 1f;
    private Animator _animator;
    private TextMeshProUGUI _text;
    public string Text {get => _text.text; }
    private bool _isShown = false;

    //private List<string> _orderTexts;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        ClearText();
    }

    public void SwitchActive()
    {
        _isShown = !_isShown;
        _animator.SetBool("show", _isShown);
    }

    public void Show()
    {
        _isShown = true;
        _animator.SetBool("show", _isShown);
    }

    public void NewOrder()
    {
        _animator.SetBool("newOrder", true);
        Invoke(nameof(OffNewOrderValue), _newOrderWaitTime);
    }

    private void OffNewOrderValue()
    {
        _animator.SetBool("newOrder", false);

    }

    public void Busy()
    {
        _animator.SetBool("busy", true);
        Invoke(nameof(OffBusyValue), _busyWaitTime);
    }

    private void OffBusyValue()
    {
        _animator.SetBool("busy", false);

    }

    public void SetText(Baking.Type bakingType)
    {
        _text.SetText(bakingType.ToString());
    }

    public void ClearText()
    {
        _text.SetText("");
    }
}
