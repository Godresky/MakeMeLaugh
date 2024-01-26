using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPaperUI : MonoBehaviour
{
    private Animator _animator;
    private bool _isShow = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchActive()
    {
        _isShow = !_isShow;
        _animator.SetBool("isShow", _isShow);
    }
}
