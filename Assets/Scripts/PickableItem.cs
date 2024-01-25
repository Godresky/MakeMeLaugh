using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class PickableItem : MonoBehaviour
{
    [SerializeField]
    private Outline _outline;
    [SerializeField]
    private Color _outlineColor = Color.cyan;
    [SerializeField]
    private float _outlineWidth = 10f;

    private bool _loadedOutline = false;

    private void Start()
    {
        SetOutline();
    }

    private void LateUpdate()
    {
        if (!_loadedOutline)
        {
            _loadedOutline = true;
            _outline.enabled = false;
        }
    }

    protected void SetOutline()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineColor = _outlineColor;
        _outline.OutlineWidth = _outlineWidth;
    }

    public void OnHoverEnter()
    {
        _outline.enabled = true;
    }

    public void OnHoverExit()
    {
        _outline.enabled = false;
    }
}
