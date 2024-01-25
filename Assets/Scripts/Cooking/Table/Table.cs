using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Table : MonoBehaviour
{
    [SerializeField] private RollStick _rollStick;
    [SerializeField] private BowlWithJam _bowlwithJam;

    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent(out Dough dough)){
            _bowlwithJam.GetDough(dough);
            _rollStick.GetDough(dough);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.TryGetComponent(out Dough dough))
            dough.IsReady();
    }

}
