using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(OrderTrigger))]
public class VisitorAI : MonoBehaviour, IInteractableWithPlayerObject
{
    [Header("Move Setting")]
    private PlaceForVisitor _visitorPlace;   // setted
    public Transform TablePoint { get => _visitorPlace.Transform; }

    private Vector3 _startPosition;

    [SerializeField]
    private Animator _animator;

    private OrderTrigger _orderTrigger;
    private NavMeshAgent _agent;

    // Texture Settings
    private GameObject _faceFuny;
    private GameObject _faceSad;
    private GameObject _faceNone;

    private State _state;

    [SerializeField]
    private UnityEvent OnGettingOrderEvent;

    public State CurrentState { get => _state; }

    private void Start()
    {
        _startPosition = GetComponent<Transform>().position;
        _agent = GetComponent<NavMeshAgent>();
        _orderTrigger = GetComponent<OrderTrigger>();
        for (int i = 0, children = transform.childCount; i < children; i++)
        {
            if (transform.GetChild(i).name == "_FunyHead")
            {
                _faceFuny = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name == "_SadHead")
            {
                _faceSad = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name == "_NoneHead")
            {
                _faceNone = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name == "_Order")
            {
                //_order = transform.GetChild(i).gameObject.GetComponent<OrderPaper>();
            }
        }
        SetMood(Mood.None);
        //_order.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_state == State.Visitor && Vector3.Distance(transform.position, _visitorPlace.Transform.position) <= 0.1f)
        {
            _animator.SetInteger("State", 0);
            _state = State.ReadyToDoOrder;
        }
        else if (_state == State.GotOrder && Vector3.Distance(transform.position, _startPosition) <= 0.1f)
        {
            _animator.SetInteger("State", 0);
            _state = State.NonVisitor;
        }
        
        if (_state == State.GotOrder || _state == State.Visitor)
        {
            _animator.SetInteger("State", 1);
        }
    }

    public void Come(PlaceForVisitor place)
    {
        _state = State.Visitor;

        _visitorPlace = place;

        _animator.SetInteger("State", 1);
        _agent.SetDestination(new Vector3(place.Transform.position.x, place.Transform.position.y, place.Transform.position.z));
    }

    public void Leave()
    {
        _agent.SetDestination(_startPosition);
        _animator.SetInteger("State", 1);
    }

    public void SetPaperStatus(bool status)
    {
        //_order.gameObject.SetActive(status);
    }

    public void SetMood(Mood mood)
    {
        if (mood == Mood.Funny)
        {
            _faceFuny.SetActive(true);
            _faceSad.SetActive(false);
            _faceNone.SetActive(false);
        }
        else if (mood == Mood.Sad)
        {
            _faceFuny.SetActive(false);
            _faceSad.SetActive(true);
            _faceNone.SetActive(false);
        }
        else if (mood == Mood.None)
        {
            _faceFuny.SetActive(false);
            _faceSad.SetActive(false);
            _faceNone.SetActive(true);
        }
    }

    public void Interact()
    {
        if (_state == State.ReadyToDoOrder)
        {
            _orderTrigger.OrderBaking(this);
            _state = State.DidOrder;
        }
        else if (_state == State.DidOrder)
        {
            if (_visitorPlace.Plate.BakingsInPlate.Count != 0)
            {
                GetBaking(_visitorPlace.Plate.BakingsInPlate[0]);
            }
            else
            {

            }
        }
    }

    public void OrderedBaking()
    {
        if (_state == State.DidOrder)
        {
            _visitorPlace.Plate.LiftUp();
        }
    }

    public void GetBaking(Baking baking)
    {
        if (baking == null)
            return;

        _state = State.GotOrder;
        if (baking.CurrentType == (Baking.Type)_orderTrigger.ChoosenBaking)
        {
            SetMood(Mood.Funny);
        }
        else
        {
            SetMood(Mood.Sad);
        }

        Destroy(baking.gameObject);
        _visitorPlace.Plate.LiftDown();
        _visitorPlace.IsUsed = false;

        VisitorsController.Singleton.UncallVisitor(this);
    }

    public enum Mood
    {
        Funny,
        Sad,
        None
    }

    public enum State
    {
        NonVisitor,
        Visitor,
        ReadyToDoOrder,
        DidOrder,
        GotOrder,
    }
}