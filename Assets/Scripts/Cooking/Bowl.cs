using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Food;

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
    private List<DoughIngridient.DoughIngridients> _ingridientsInBowl;

    [SerializeField]
    private bool _hasWater = false;

    public bool HasWater { get => _hasWater; set => _hasWater = value; }

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
        if (!_ingridientsInBowl.Contains(DoughIngridient.DoughIngridients.Egg) || !_ingridientsInBowl.Contains(DoughIngridient.DoughIngridients.Flour) || !_hasWater || !_ingridientsInBowl.Contains(DoughIngridient.DoughIngridients.Yeast))
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
