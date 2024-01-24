using System.Collections;
using UnityEngine;

public class Dough : PickableItem
{
    [SerializeField]
    private State _state;

    public State CurrentState { get => _state; }

    [Space(2)]
    [Header("Grow")]
    [SerializeField] private float _growTime;
    [Range(1,7)]
    [SerializeField] private float _endScale;

    private void Start(){
        Grow();
    }

    public void Grow() => StartCoroutine(Growing());

    public void Bake(){

    }

    public void Rollig(float endScale){
        _state = State.Rolled;
        transform.localScale *= endScale;
    }

    public void Filling() => _state = State.Filled;

    public void IsReady(){
        if (_state == State.Filled || _state == State.Rolled)
            _state = State.ReadyForBaking;
    }

    private IEnumerator Growing(){
        yield return new WaitForSeconds(_growTime);
        transform.localScale *= _endScale;
    }

    public enum State{
        Unrised,
        Rised,
        ReadyForBaking,
        Rolled,
        Filled,
        Cooked
    }
}
