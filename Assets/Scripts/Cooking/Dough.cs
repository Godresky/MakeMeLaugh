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

    protected void Start()
    {
        base.Start();
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
    }

    public void Grow() => StartCoroutine(Growing());

    public void Bake(){
        ChangeState(State.Cooked);
    }

    public void Rolling(float endScale){
        _state = State.Rolled;
        transform.localScale *= endScale;
    }

    public void Filling() => _state = State.Filled;

    public void IsReady(){
        if (_state == State.Filled || _state == State.Rolled)
            _state = State.ReadyForBaking;
    }

    private IEnumerator Growing(){
        _state = State.Rising;
        yield return new WaitForSeconds(_growTime);
        transform.localScale *= _endScale;
        _state = State.ReadyForBaking;
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
        ReadyForBaking,
        Rolled,
        Filled,
        Cooked
    }
}
