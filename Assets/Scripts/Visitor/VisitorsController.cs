using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class VisitorsController : MonoBehaviour
{
    [SerializeField]
    private VisiterAI[] _visitor;
    [SerializeField]
    private int _visitorID = 0;
    [SerializeField]
    private GameObject[] _tableSpots;
    [SerializeField]
    private int _spotID = 0;
    [SerializeField]
    private Animator _animator;

    [Header("TEST Setting")]
    [SerializeField]
    private bool _timeToCome = false;
    [SerializeField]
    private bool _timeToLeave = false;
    [SerializeField]
    private bool _isVisiter = false;

    void Start()
    {
        for (int i = 0; i < _visitor.Length; i++)
        {
            _visitor[i].SetMoveSetting(_tableSpots[i], 0);
        }
    }

    void Update()
    {
        if (_timeToCome)
        {
            _isVisiter = true;
            _timeToCome = false;
            _visitor[_visitorID].Come();
        }
        if (_timeToLeave)
        {
            _isVisiter = false;
            _timeToLeave = false;
            _visitor[_visitorID].Leave();
        }
    }

    public bool GetVisitStatus()
    {
        return _isVisiter;
    }

    public void CallVisitor()
    {
        _timeToCome = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.GetComponent<VisiterAI>()?.SetPaperStatus(true);
    }
}
