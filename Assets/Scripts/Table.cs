using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Table : MonoBehaviour
{
    [Header("Rolling")]
    [SerializeField] private float _endDoughScale;

    private Dough _dough;

    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent(out Dough dough))
            _dough = dough;
    }

    private void OnTriggerExit(Collider other){
        if (other.TryGetComponent(out Dough dough))
            dough.IsReady();
    }

    public void Fill(){
        _dough.Filling();
    }
    public void Roll(){
        _dough.Rollig(_endDoughScale);
    }
}
