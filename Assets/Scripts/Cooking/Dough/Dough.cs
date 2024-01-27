using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class Dough : PickableItem
{
    [SerializeField]
    private State _state;

    public State CurrentState { get => _state; }
    public BakingType Type { get;  set; }
    public bool IsReadyForBaking = false;

    [Space(2)]
    [Header("Grow")]
    [SerializeField]
    private float _growTime;
    [Range(1,7)]
    [SerializeField]
    private float _endScale;

    [SerializeField]
    private List<Mesh> _meshes;

    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

    private void Start()
    {
        base.Start();
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
    }

    public void Grow() => StartCoroutine(Growing());

    public void Bake(){
        switch (_state){
            case State.Circle:
                Type = BakingType.CircleBread;
                break;

            case State.Rectangle:
                Type = BakingType.RectangleBread;
                break;

            case State.SqueareWithJam:
                Type = BakingType.SqueareBreadWithJam;
                break;

            case State.RectangleWithJam:
                Type = BakingType.Rollet;
                break;
        }
    }

    public void Rolling(float endScale){
        _state = State.Rectangle;

        transform.localScale *= endScale;
    }

    public void Filling(){

        switch (_state) {
            case State.Circle:
                _state = State.SqueareWithJam;
                break;

            case State.Rectangle:
                _state= State.RectangleWithJam;
                break;
        }
    }

    private IEnumerator Growing(){
        _state = State.Rising;
        yield return new WaitForSeconds(_growTime);
        transform.localScale *= _endScale;
        IsReadyForBaking = true;
    }

    private void ChangeState(State newState)
    {
        _state = newState;
        _meshFilter.mesh = _meshes[(int)_state];
    }

    public enum State
    {
        Unrised,
        Rising,
        Rised,
        Rectangle,
        SqueareWithJam,
        RectangleWithJam,
        Cooked,
        Circle,
    }
}
