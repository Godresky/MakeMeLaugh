using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Food : PickableItem
{
    [SerializeField]
    private FoodType _foodType;

    public FoodType CurrentType { get => _foodType; }

    public enum FoodType
    {
        Egg,
        Milk,
        Yeast,
        Flour,
        Water,
        RoundBread,
        RectangularBread,
        SquareBreadWithFilling,
        Roll,
        PoisonousBread,
        WrongDough,
        BurntBread
    }

    public enum DoughIngridients
    {
        Egg = FoodType.Egg,
        Milk = FoodType.Milk,
        Yeast = FoodType.Yeast,
        Flour = FoodType.Flour,
        Water = FoodType.Water,
    }

    public enum Pastry
    {
        RoundBread = FoodType.RoundBread,
        RectangularBread = FoodType.RectangularBread,
        SquareBreadWithFilling = FoodType.SquareBreadWithFilling,
        Roll = FoodType.Roll,
        PoisonousBread = FoodType.PoisonousBread,
        WrongDough = FoodType.WrongDough,
        BurntBread = FoodType.BurntBread,
    }
}
