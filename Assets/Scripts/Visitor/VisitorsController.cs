using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class VisitorsController : MonoBehaviour
{
    [Header("Visitors Setting")]
    [SerializeField]
    private VisitorAI[] _visitorsList;
    [SerializeField]
    private GameObject[] _tableSpotsList;
    [SerializeField, Range(1, 3)]
    private int _visitLimit = 2;
    [SerializeField, Min(1)]
    private int _todayVisitorCount;
    public int TodayVisitorCount { get => _todayVisitorCount; }
    [SerializeField]
    private GameObject _textEndOfShift;

    [Header("Wait Setting")]
    [SerializeField]
    private float _comeWaitTime;
    [SerializeField]
    private float _LeaveWaitTime;
    [SerializeField]
    private float _endTextWaitTime;

    [Header("TEST SerializeFields")]
    [SerializeField]
    private int _visitorCount = 0;
    [SerializeField]
    private int _servedVisitors = 0;
    [SerializeField]
    private VisitorPlate _plate;
    [SerializeField]
    private int _calledVisitorID = 0;
    [SerializeField]
    private int _leavedVisitorID = 0;
    [SerializeField]
    private bool _isEndOfQueue = false;
    public bool IsEndOfQueue { get => _isEndOfQueue; }
    [SerializeField]
    private bool _isShiftStarted = false;   // Value of Shift begining

    private void Start()
    {
        for (int vi = 0, si = 0, vl = _visitorsList.Length, tsl = _tableSpotsList.Length; vi < vl; vi++, si++)
        {
            if (si == tsl)
            {
                si = 0;
            }
            _visitorsList[vi].SetTablePoint(_tableSpotsList[si]);
        }
        _isShiftStarted = true;
    }

    private void Update()
    {
        if (_isEndOfQueue || !_isShiftStarted) return;

        if (_calledVisitorID == _visitorsList.Length)
        {
            _calledVisitorID = 0;
        }
        if (_leavedVisitorID == _visitorsList.Length)
        {
            _leavedVisitorID = 0;
        }

        for (int vi = 0, vl = _visitorsList.Length; vi < vl; vi++)
        {
            CheckPlate(vi);
            if (_plate != null && _plate.DishMark != 0)
            {
                _plate.LiftDown();
                switch (_plate.DishMark)
                {
                    case 1:
                        UncallVisitor(VisitorAI.Mood.Funny);
                        break;
                    case -1:
                        UncallVisitor(VisitorAI.Mood.Sad);
                        break;
                }
            }
        }
    }

    private void CheckPlate(int visitorID)
    {
        _plate = _visitorsList[visitorID].TablePoint.GetComponentInChildren<VisitorPlate>();
        if (_plate != null)
        {
            _plate.SetWishDish(_visitorsList[visitorID].WishDish);
        }
    }

    public bool IsFreeSpots()
    {
        return _visitorCount < _visitLimit; 
    }

    public void CallVisitor()
    {
        // ******* NEED wait _visitorWaitTime *******
        _visitorsList[_calledVisitorID].Come();
        CheckPlate(_calledVisitorID);
        _visitorCount++;
        _calledVisitorID++;
    }

    public void UncallVisitor(VisitorAI.Mood mood)
    {
        _visitorsList[_leavedVisitorID].SetMood(mood);
        // ******* NEED wait _visitorWaitTime *******
        _visitorsList[_leavedVisitorID].Leave();
        _visitorCount--;
        _leavedVisitorID++;
        _servedVisitors++;
        if (_servedVisitors == _todayVisitorCount)
        {
            _isEndOfQueue = true;
        }
    }

    public void EndQueue()
    {
        _isEndOfQueue = true;
        // ******* NEED wait _EndTextWaitTime *******
        _textEndOfShift.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.GetComponent<VisitorAI>()?.SetPaperStatus(true);
        if (_plate != null)
        {
            _plate.LiftUp();
        }
        //else
        //{
        //    _plate = _visitorsList[_visitorID].TablePoint.GetComponentInChildren<VisitorPlate>(true);
        //    if (_plate != null)
        //    {
        //        _plate.LiftUp();
        //        _plate.SetWishDish(_visitorsList[_visitorID].WishDish);
        //    }
        //}
    }
}
