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
    private int _simpleFoodCount;
    [SerializeField]
    private int _hardFoodCount;
    private string[] _simpleFoodList =
    {
        "Egg",
        "Milk",
        "Yeast",
    };
    private string[] _hardFoodList =
    {
        "Flour",
        "RoundBread",
    };

    [Header("TESTING")]
    [SerializeField]
    private List<string> _wishList;
    [SerializeField]
    private List<bool> _statusList;

    private void Start()
    {
        _paperUI = FindObjectOfType<OrderPaperUI>();

        System.Random random = new System.Random();
        for (int i = 0; i < _simpleFoodCount; i++)
        {

            _wishList.Add((_simpleFoodList[random.Next(0, _simpleFoodList.Length)]));
            _statusList.Add(false);
        }
        for (int i = 0; i < _hardFoodCount; i++)
        {

            _wishList.Add((_hardFoodList[random.Next(0, _hardFoodList.Length)]));
            _statusList.Add(false);
        }
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        SwitchAtionUI();
        OrderTextUpdate();
        Invoke(nameof(SwitchAtionUI), _delayTime);
    }

    private void OrderTextUpdate()
    {
        string text = "";
        for (int i = 0, count = _wishList.Count; i < count; i++)
        {
            text += (_statusList[i] ? "[*] " : "[ ] ") + _wishList[i] + "\n";
        }
        Debug.Log(text);
        _paperUI.ChangeText(text);
    }

    private void SwitchAtionUI()
    {
        _paperUI.SwitchActive();
    }
}
