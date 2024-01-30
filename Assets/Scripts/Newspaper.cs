using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : MonoBehaviour
{
    public Article[] articleSet;
}

[System.Serializable]
public class Article
{
    [TextArea]
    public string title;
    [TextArea]
    public string content;
}