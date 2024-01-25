using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorsController : MonoBehaviour
{
    [SerializeField]
    private VisiterAI _visitor;
    [SerializeField]
    private Transform[] _tableSpots;

    [Header("Texures Setting")]
    [SerializeField]
    private Material _faceFuny;
    [SerializeField]
    private Material _faceSad;
    [SerializeField]
    private Material _faceNone;

    [Header("TEST Setting")]
    [SerializeField]
    private bool _timeToCome = false;
    [SerializeField]
    private bool _timeToLeave = false;
    [SerializeField]
    private bool _isVisiter = false;

    void Start()
    {
        _visitor.SetTexuresSetting(_faceFuny, _faceSad, _faceNone);
        _visitor.SetMoveSetting(_tableSpots[0].position, 0);
    }

    void Update()
    {
        if (_timeToCome)
        {
            _isVisiter = true;
            _timeToCome = false;
            _visitor.Come();
        }
        if (_timeToLeave)
        {
            _isVisiter = false;
            _timeToLeave = false;
            _visitor.Leave();
        }
    }

    public bool GetVisitorStatus()
    {
        return _isVisiter;
    }

    public void CallVisitor()
    {
        _timeToCome = true;
    }
}
