using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Stove : MonoBehaviour
{
    [SerializeField] private float _bakingTime;
    [SerializeField] private Door _door;

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Dough dough) && dough.CurrentState == Dough.State.ReadyForBaking && _door.IsOpen== false) {
            StartCoroutine(Baking(dough));
        }
    }

    private IEnumerator Baking(Dough dough){
        yield return new WaitForSeconds(_bakingTime);
        dough.Bake();
    }
}
