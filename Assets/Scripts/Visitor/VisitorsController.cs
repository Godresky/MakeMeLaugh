using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class VisitorsController : MonoBehaviour
{
    [Header("Visitors Setting")]
    [SerializeField]
    private VisitorAI[] _visitorsPool;
    [SerializeField]
    private PlaceForVisitor[] _tablePlaces;

    //[Header("Wait Setting")]
    //[SerializeField]
    // private float _comeWaitTime;
    //[SerializeField]
    // private float _LeaveWaitTime;
    //[SerializeField]
    // private float _endTextWaitTime;

    private int _currentNumVisitors = 0;

    public static VisitorsController Singleton;

    private void Awake()
    {
        Singleton = this;
    }

    public bool IsFreePlaces()
    {
        return _currentNumVisitors < _tablePlaces.Length; 
    }

    public void CallVisitor()
    {
        // ******* NEED wait _visitorWaitTime *******

        if (!IsFreePlaces())
            return;

        VisitorAI visitor = GetVisitor();
        PlaceForVisitor place = GetPlaceForVisitor();

        visitor.Come(place);
        place.IsUsed = true;
        _currentNumVisitors++;
    }

    public void UncallVisitor(VisitorAI visitor)
    {
        visitor.Leave();

        _currentNumVisitors--;
    }

    private VisitorAI GetVisitor()
    {
        int id = Random.Range(0, _visitorsPool.Length);

        if (_visitorsPool[id].CurrentState == VisitorAI.State.NonVisitor)
        {
            return _visitorsPool[id];
        }

        return GetVisitor();
    }

    private PlaceForVisitor GetPlaceForVisitor()
    {
        int placeId = Random.Range(0, _tablePlaces.Length);

        if (!_tablePlaces[placeId].IsUsed)
        {
            return _tablePlaces[placeId];
        }

        return GetPlaceForVisitor();
    }
}

[System.Serializable]
public class PlaceForVisitor
{
    public Transform Transform;
    public VisitorPlate Plate;
    public bool IsUsed = false;
}