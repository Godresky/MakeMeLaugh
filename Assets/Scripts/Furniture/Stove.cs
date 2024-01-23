using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Stove : Furniture
{
    [SerializeField] private ParticleSystem _fireEffect;
    [SerializeField] private float _bakingTime;

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Dough dough) && dough.CurrentState == Dough.State.ReadyForBaking && IsClosed==true) {
            StartCoroutine(Baking(dough));
        }
    }

    private IEnumerator Baking(Dough dough){
        _fireEffect.Play();
        yield return new WaitForSeconds(_bakingTime);
        dough.Bake();
    }
}
