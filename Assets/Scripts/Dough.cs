using System.Collections;
using UnityEngine;

public class Dough : MonoBehaviour
{
    [SerializeField]
    private State _type;

    public State CurrentState { get => _type; }

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

    private IEnumerator Growing(){
        yield return new WaitForSeconds(_growTime);
        transform.localScale *= _endScale;
    }

    public enum State
    {
        Unrised,
        Rised,
        ReadyForBaking,
        Rolled,
        Filled,
        Cooked
    }
}
