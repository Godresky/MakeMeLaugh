using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisitorsController : MonoBehaviour
{
    [SerializeField]
    private VisiterAI[] _visitors;
    [SerializeField]
    private GameObject[] _tableSpots;
    [SerializeField]
    private GameObject _textEndOfShift;
    [SerializeField]
    private float _visitorWaitTime;
    [SerializeField]
    private float _EndTextWaitTime;

    [Header("TEST SerializeFields")]
    [SerializeField]
    private VisitorPlate _plate;
    [SerializeField]
    private int _visitorID = 0;
    [SerializeField]
    private int _spotID = 0;
    [SerializeField]
    private bool _timeToCome = false;
    [SerializeField]
    private bool _timeToLeave = false;
    [SerializeField]
    private bool _isVisiter = false;
    [SerializeField]
    private bool _isEndOfQueue = false;

    private void Start()
    {
        for (int vi = 0, si = 0, vl = _visitors.Length, tsl = _tableSpots.Length; vi < vl; vi++, si++)
        {
            _visitors[vi].SetTablePoint(_tableSpots[si]);
            if (si == tsl)
            {
                si = 0;
            }
        }
    }

    private void Update()
    {
        if (_isEndOfQueue)
        {
            return;
        }
        if (_plate != null && _plate.DishMark != 0)
        {
            _plate.LiftDown();
            switch (_plate.DishMark)
            {
                case 1:
                    UncallVisitor("funy");
                    break;
                case -1:
                    UncallVisitor("sad");
                    break;
            }
        }
        if (_timeToCome)
        {
            // NEED wait _visitorWaitTime
            _isVisiter = true;
            _timeToCome = false;
            _visitors[_visitorID].Come();
        }
        else if (_timeToLeave)
        {
            // NEED wait _visitorWaitTime
            _isVisiter = false;
            _timeToLeave = false;
            _visitors[_visitorID].Leave();
            _visitorID++;
            _spotID++;
            if (_spotID == _tableSpots.Length)
            {
                _spotID = 0;
            }
            if (_visitorID == _visitors.Length)
            {
                _isEndOfQueue = true;
                // NEED wait _EndTextWaitTime
                _textEndOfShift.SetActive(true);
            }
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

    public void UncallVisitor(string mood)
    {
        _visitors[_visitorID].SetMood(mood);
        _timeToLeave = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.GetComponent<VisiterAI>()?.SetPaperStatus(true);
        _plate = _tableSpots[_spotID].GetComponentInChildren<VisitorPlate>();
        if (_plate != null)
        {
            _plate.LiftUp();
            _plate.SetWishDish(_visitors[_visitorID].WishDish);
        }
        else
        {
            _plate = _tableSpots[_spotID].GetComponentInChildren<VisitorPlate>(true);
            if (_plate != null)
            {
                _plate.LiftUp();
                _plate.SetWishDish(_visitors[_visitorID].WishDish);
            }
        }
    }
}
