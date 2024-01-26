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
    [SerializeField]
    private int _simpleFoodCount;
    [SerializeField]
    private int _hardFoodCount;

    [Header("TESTING")]
    [SerializeField]
    private List<string> _wishList;

    private void Start()
    {
        _paperUI = FindObjectOfType<OrderPaperUI>();

        System.Random random = new System.Random();
        for (int i = 0; i < _simpleFoodCount; i++)
        {

            _wishList.Add((_simpleFoodList[random.Next(0, _simpleFoodList.Length)]));
        }
        for (int i = 0; i < _hardFoodCount; i++)
        {

            _wishList.Add((_hardFoodList[random.Next(0, _hardFoodList.Length)]));
        }
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        _paperUI.ChangeText(GetOrderText());
        SwitchAtionUI();
        Invoke(nameof(SwitchAtionUI), _delayTime);
    }

    private string GetOrderText()
    {
        string text = "";
        for (int i = 0, count = _wishList.Count; i < count; i++)
        {
            text += "[ ] " + _wishList[i] + "\n";
        }
        Debug.Log(text);
        return text;
    }

    private void SwitchAtionUI()
    {
        _paperUI.SwitchActive();
    }
}
