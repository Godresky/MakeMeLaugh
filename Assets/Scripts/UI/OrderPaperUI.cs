using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPaperUI : MonoBehaviour
{
    private Animator _animator;
    private bool _isShow = false;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SwitchActive()
    {
        _isShow = !_isShow;
        _animator.SetBool("isShow", _isShow);
    }

    public void ChangeText(string text)
    {
        _text.SetText(text);
    }
}
