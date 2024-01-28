using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPaperUI : MonoBehaviour
{
    private Animation _animation;
    private bool _isShow = false;
    private TextMeshProUGUI _text;

    private List<string> _orderTexts;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Show()
    {
        _isShow = !_isShow;
        _animator.SetBool("isShow", _isShow);
    }

    public void Hide()
    {
        _isShow = !_isShow;
        _animator.SetBool("isShow", _isShow);
    }

    public void SwitchActive()
    {
        _isShow = !_isShow;
        _animator.SetBool("isShow", _isShow);
    }

    public void ChangeText(int id)
    {
        _text.SetText(_orderTexts[id]);
    }
}
