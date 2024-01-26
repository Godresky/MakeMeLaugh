using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPaper : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField]
    private OrderPaperUI _paperUI;
    [SerializeField]
    private float _delayTime;

    public void Interact()
    {
        gameObject.SetActive(false);
        SwitchAtionUI();
        Invoke(nameof(SwitchAtionUI), _delayTime);
    }

    private void SwitchAtionUI()
    {
        _paperUI.SwitchActive();
    }
}
