using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class Dough : PickableItem
{
    [SerializeField]
    private State _state;
    [SerializeField]
    private Baking.Type _futureBakingType;

    public State CurrentState { get => _state; }
    public Baking.Type FutureBakingType { get => _futureBakingType; set => _futureBakingType = value; }
    public bool IsReadyForBaking = false;

    [SerializeField]

    [Space(2)]
    [Header("Grow")]
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
                futureBakingType = Baking.Type.CircleBread;
                break;

            case State.Triangle:
                futureBakingType = Baking.Type.RectangleBread;
                break;

            case State.SquareWithFilling:
                futureBakingType = Baking.Type.SquareBreadWithFilling;
                break;

            case State.TriangleWithFilling:
                futureBakingType = Baking.Type.Rollet;
                break;
        }
    }

    public void Rolling(float endScale){
        _state = State.Triangle;

        transform.localScale *= endScale;
    }

    public void Filling(){

        switch (_state) {
            case State.Circle:
                _state = State.SquareWithFilling;
                break;

            case State.Triangle:
                _state= State.TriangleWithFilling;
                break;
        }
    }

    private IEnumerator Growing(){
        _state = State.Rising;
        yield return new WaitForSeconds(_growTime);
        _state = State.Circle;
        transform.localScale *= _endScale;
        IsReadyForBaking = true;
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
