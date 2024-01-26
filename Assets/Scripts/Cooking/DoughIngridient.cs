using UnityEngine;

public class DoughIngridient : Food
{
    [SerializeField]
    private DoughIngridients _type;

    public new DoughIngridients CurrentType { get => _type; }
}
