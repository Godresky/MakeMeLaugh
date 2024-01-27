using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baking : PickableItem
{
    [SerializeField]
    private Type _type;

    [SerializeField]
    private bool _isPoisoned = false;
    [SerializeField]
    private bool _isIncorect = false;

    public Type CurrentType { get => _type; }
    public bool IsPoisoned { get => _isPoisoned; set => _isPoisoned = value; }
    public bool IsIncorect { get => _isIncorect; set => _isIncorect = value; }

    public enum Type : int
    {
        CircleBread,
        SquareBreadWithFilling,
        RectangleBread,
        Rollet,
        Error
    }

}
