using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour, IInteractableWithPlayerObject
{
    [Header("Dough")]
    [SerializeField]
    private Dough _dough;
    [SerializeField]
    private Transform _doughSpawnpoint;
    [Space(3)]
    [SerializeField]
    private Fridge _fridge;

    [SerializeField]
    private List<DoughIngridient.Type> _ingridientsInBowl;

    public bool HasWater = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DoughIngridient ingridient))
        {
            if (other.TryGetComponent(out Egg egg) && egg.IsBroken == true)
                return;

            _ingridientsInBowl.Add(ingridient.CurrentType);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.TryGetComponent(out DoughIngridient ingridient)){
            _ingridientsInBowl.Remove(ingridient.CurrentType);
        }
        if (other.TryGetComponent(out Dough dough))
        {
            if (dough.CurrentState == Dough.State.Unrised)
            {
                dough.Grow();
            }
        }
    }

    public void TryMakeDough()
    {
        if (!_ingridientsInBowl.Contains(DoughIngridient.Type.Egg) || !_ingridientsInBowl.Contains(DoughIngridient.Type.Flour) || !_ingridientsInBowl.Contains(DoughIngridient.Type.Water) || !_ingridientsInBowl.Contains(DoughIngridient.Type.Yeast))
            return;

        _ingridientsInBowl.Clear();
        _fridge.UpdateFridge();

        Instantiate(_dough.gameObject, _doughSpawnpoint.position, _doughSpawnpoint.rotation);
        HasWater = false;
    }

    public void Interact(){
        TryMakeDough();
    }
}
