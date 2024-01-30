using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements.Experimental;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(OrderTrigger))]
[RequireComponent(typeof(AudioSource))]
public class VisitorAI : MonoBehaviour, IInteractableWithPlayerObject
{
    [Header("Move Setting")]
    private PlaceForVisitor _visitorPlace;   // setted
    public Transform TablePoint { get => _visitorPlace.Transform; }

    private Vector3 _startPosition;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField]
    private Material _happyVisitor;
    [SerializeField]
    private Material _sadVisitor;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _happyVisitorAudio;
    [SerializeField]
    private AudioClip _sadVisitorAudio;

    private OrderPaperUI _orderPanel;

    private OrderTrigger _orderTrigger;
    private NavMeshAgent _agent;

    private State _state;

    [SerializeField]
    private UnityEvent OnGettingOrderEvent;

    public State CurrentState { get => _state; }

    private void Start()
    {
        _orderPanel = FindObjectOfType<OrderPaperUI>();
        _startPosition = GetComponent<Transform>().position;
        _agent = GetComponent<NavMeshAgent>();
        _orderTrigger = GetComponent<OrderTrigger>();
        _audioSource = GetComponent<AudioSource>();

        SetMood(Mood.None);
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
        SetMood(Mood.None);

        _visitorPlace = place;

        _animator.SetInteger("State", 1);
        _agent.SetDestination(new Vector3(place.Transform.position.x, place.Transform.position.y, place.Transform.position.z));
    }

    public void Leave()
    {
        _state = State.GotOrder;

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
            _skinnedMeshRenderer.material = _happyVisitor;
        }
        else if (mood == Mood.Sad)
        {
            _skinnedMeshRenderer.material = _sadVisitor;
        }
        else if (mood == Mood.None)
        {
            _skinnedMeshRenderer.material = _sadVisitor;
        }
    }

    public void Interact()
    {
        if (_state == State.ReadyToDoOrder)
        {
            _orderTrigger.OrderBaking(this);
            _state = State.DidOrder;
            _orderPanel.SetText((Baking.Type)_orderTrigger.ChoosenBaking);
            _orderPanel.NewOrder();
        }
        else if (_state == State.DidOrder)
        {
            if (_visitorPlace.Plate.BakingsInPlate.Count != 0)
            {
                GetBaking(_visitorPlace.Plate.BakingsInPlate[0]);
            }
            else
            {
                _orderPanel.Hide();
                _orderPanel.SetText((Baking.Type)_orderTrigger.ChoosenBaking);
                _orderPanel.Show();
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

        _orderPanel.ClearText();
        if (baking.CurrentType == (Baking.Type)_orderTrigger.ChoosenBaking)
        {
            SetMood(Mood.Funny);
            _audioSource.PlayOneShot(_happyVisitorAudio);
        }
        else
        {
            SetMood(Mood.Sad);
            _audioSource.PlayOneShot(_sadVisitorAudio);
        }

        Destroy(baking.gameObject);
        _visitorPlace.Plate.LiftDown();
        _visitorPlace.IsUsed = false;

        StartCoroutine(WaitToLeave());
    }

    private IEnumerator WaitToLeave()
    {
        yield return new WaitForSeconds(2f);
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