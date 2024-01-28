using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPaper : MonoBehaviour, IInteractableWithPlayerObject
{
    //[SerializeField]
    private OrderPaperUI _paperUI;
    [SerializeField]
    private float _delayTime;

    [Header("Wish Setting")]
    [SerializeField]
    private List<string> _dishesMenu;
    private int _dishID;

    public string WishDish { get => _dishesMenu[_dishID]; }

    private void Start()
    {
        _paperUI = FindObjectOfType<OrderPaperUI>();

        System.Random random = new System.Random();
        _dishID = random.Next(0, _dishesMenu.Count);
    }

    public void Interact()
    {
        //gameObject.SetActive(false);
        //OrderTextUpdate();
        //Invoke(nameof(SwitchAtionUI), 0.1f);
        //Invoke(nameof(SwitchAtionUI), _delayTime);
    }

    private void OrderTextUpdate()
    {
        string text = "* " + _dishesMenu[_dishID];
        //_paperUI.ChangeText(text);
    }

    private void SwitchAtionUI()
    {
        _paperUI.SwitchActive();
    }
}
