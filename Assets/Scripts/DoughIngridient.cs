using UnityEngine;

public class DoughIngridient : MonoBehaviour
{
    [SerializeField]
    private IngridientType _type;

    public IngridientType CurrentType { get => _type; }

    public enum IngridientType
    {
        Egg,
        Milk,
        Yeast,
        Flour,
        Water
    }
}
