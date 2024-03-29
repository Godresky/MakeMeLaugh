using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour, IInteractableWithPlayerObject
{
    [Header("Dough")]
    [SerializeField]
    private Dough _dough;
    [SerializeField]
    private Transform _doughSpawnpoint;
    [SerializeField]
    private GameObject _waterPlane;
    [Space(3)]
    [SerializeField]
    private List<DoughIngridient.Type> _ingridientsInBowl;

    [SerializeField]
    private bool _hasWater = false;

    public bool HasWater { 
        get => _hasWater;
        set
        { 
            _hasWater = value;
            _waterPlane.SetActive(_hasWater);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DoughIngridient ingridient))
        {
            if (ingridient.TryGetComponent(out FridgeItem item))
                item.IsUsing = true;

            _ingridientsInBowl.Add(ingridient.CurrentType);
        }
        else
            _dough.FutureBakingType = Baking.Type.Error;
    }

    private void OnTriggerExit(Collider other){
        if (other.TryGetComponent(out DoughIngridient ingridient)){
            _ingridientsInBowl.Remove(ingridient.CurrentType);

            if (ingridient.TryGetComponent(out FridgeItem item))
                item.IsUsing = false;
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
        if (CheckBowlRotation())
            return;

        if (!_ingridientsInBowl.Contains(DoughIngridient.Type.Egg) || !_ingridientsInBowl.Contains(DoughIngridient.Type.Flour) || !_hasWater || !_ingridientsInBowl.Contains(DoughIngridient.Type.Yeast))
            return;

        _ingridientsInBowl.Clear();

        Fridge.Singleton.UpdateFridge();
        Instantiate(_dough.gameObject, _doughSpawnpoint.position, _doughSpawnpoint.rotation);
        HasWater = false;
    }

    private void Update()
    {
        if (HasWater && CheckBowlRotation())
            HasWater = false;
    }

    private bool CheckBowlRotation()
    {
        return ((transform.rotation.x > 0.4f || transform.localRotation.x < -0.4f) || (transform.rotation.z > 0.4f || transform.localRotation.z < -0.4f));
    }

    public void Interact(){
        TryMakeDough();
    }
}