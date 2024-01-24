using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PickableItem : MonoBehaviour
{
    private Outline _outline;
    [SerializeField]
    private Color _outlineColor = Color.cyan;
    [SerializeField]
    private float _outlineWidth = 10f;


    void Start()
    {
        this.gameObject.AddComponent<Outline>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _outline.OutlineColor = _outlineColor;
        _outline.OutlineWidth = _outlineWidth;
    }

    public void SwitchOutlighting()
    {
        _outline.enabled = !_outline.enabled;
    }
}
