using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class Dough : PickableItem
{
    [SerializeField]
    private State _state;
    [SerializeField]
    private Baking.Type _futureBakingType;

    [SerializeField]
    private bool _isReadyForBaking = false;
    [SerializeField]
    private bool _isPoisoned = false;
    [SerializeField]
    private bool _isIncorect = false;

    public State CurrentState { get => _state; }
    public Baking.Type FutureBakingType { get => _futureBakingType; set => _futureBakingType = value; }
    public bool IsReadyForBaking { get => _isReadyForBaking; }
    public bool IsPoisoned { get => _isPoisoned; set => _isPoisoned = value; }
    public bool IsIncorect { get => _isIncorect; set => _isIncorect = value; }

    [Space(2)]
    [Header("Grow")]
    [SerializeField]
    private float _growTime;
    [Range(1,7)]
    [SerializeField]
    private float _endScale;

    public void Grow() => StartCoroutine(Growing());

    public void Bake(){
        switch (_state){
            case State.Circle:
                _futureBakingType = Baking.Type.CircleBread;
                break;

            case State.Triangle:
                _futureBakingType = Baking.Type.RectangleBread;
                break;

            case State.SquareWithFilling:
                _futureBakingType = Baking.Type.SquareBreadWithFilling;
                break;

            case State.TriangleWithFilling:
                _futureBakingType = Baking.Type.Rollet;
                break;

            default:
                Destroy(gameObject);
                return;
                break;
        }

        BakeryController.Singleton.LoadCookedBaking(this, _futureBakingType);
    }

    public void Rolling()
    {
        if (_state == State.Circle)
        {
            ChangeState(State.Triangle);
        }
    }

    public void Filling()
    {
        switch (_state) {
            case State.Circle:
                ChangeState(State.SquareWithFilling);
                break;

            case State.Triangle:
                ChangeState(State.TriangleWithFilling);
                break;
        }
    }

    private void ChangeState(State state)
    {
        _state = state;
        if (state != State.Unrised && state != State.Rising && state != State.Circle)
            BakeryController.Singleton.LoadDough(this, _state);
    }

    private IEnumerator Growing(){
        ChangeState(State.Rising);
        yield return new WaitForSeconds(_growTime);
        transform.localScale *= _endScale;
        _isReadyForBaking = true;
        ChangeState(State.Circle);
    }

    public enum State
    {
        Unrised,
        Rising,
        Circle,
        Triangle,
        SquareWithFilling,
        TriangleWithFilling
    }
}
