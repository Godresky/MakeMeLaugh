using UnityEngine;

public class RollStick : MonoBehaviour, IInteractableWithPlayerObject
{
    private Dough _dough;
        
    public void GetDough(Dough dough){
        _dough = dough;
    }
    public void Interact(){
        _dough.Rolling();
    }

}
