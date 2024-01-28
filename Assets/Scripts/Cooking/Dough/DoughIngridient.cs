using UnityEngine;
    
public class DoughIngridient : PickableItem
{
    [SerializeField] 
    private Type _type;

    public Type CurrentType { get => _type; }

    public enum Type
    {
        Egg,
        Milk,
        Yeast,
        Flour,
        Water
    }
}
