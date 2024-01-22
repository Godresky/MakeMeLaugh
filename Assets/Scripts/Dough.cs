using System.Collections;
using UnityEngine;

public class Dough : MonoBehaviour
{
    [Header("Needs")]
    public bool NeedToRoll = false;
    public bool NeedFilling = false;
    public bool IsReadyForBaking = false;
    [Space(2)]
    [Header("Grow")]
    [SerializeField] private float _growTime;
    [Range(1,7)]
    [SerializeField] private float _endScale;

    public void Grow() => StartCoroutine(Growing());
    public void Bake(){

    }
    private IEnumerator Growing(){
        yield return new WaitForSeconds(_growTime);
        transform.localScale *= _endScale;
    }
}
