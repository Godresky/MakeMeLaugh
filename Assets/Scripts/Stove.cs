using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Stove : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fireEffect;
    [SerializeField] private float _bakingTime;

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Dough dough) && dough.IsReadyForBaking == true) {
            StartCoroutine(Baking(dough));
        }
    }

    private IEnumerator Baking(Dough dough){
        _fireEffect.Play();
        yield return new WaitForSeconds(_bakingTime);
        dough.Bake();
    }
}
