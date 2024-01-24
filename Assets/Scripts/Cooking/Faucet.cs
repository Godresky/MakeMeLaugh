using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Faucet : MonoBehaviour
{
    public bool IsOpen = false;
    private void OnTriggerEnter(Collider other){
        if(other.TryGetComponent(out Bowl bowl) && IsOpen == true){
            bowl.HasWater = true;
        }
    }


}
