using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baking : MonoBehaviour
{
    [SerializeField]
    private Type _type;

    public Type CurrentType { get => _type; }
    public enum Type : int
    {
        CircleBread,
        SquareBreadWithFilling,
        RectangleBread,
        Rollet,
        Error
    }

}
