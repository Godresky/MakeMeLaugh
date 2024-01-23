using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Bowl : MonoBehaviour
{
    [Header("Dough")]
    [SerializeField]
    private Animation _makingAnimation;
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
    }

    public void TryMakeDough()
    {
        if (!_ingridientsInBowl.Contains(DoughIngridient.Type.Egg) || !_ingridientsInBowl.Contains(DoughIngridient.Type.Flour) || !_ingridientsInBowl.Contains(DoughIngridient.Type.Water) || !_ingridientsInBowl.Contains(DoughIngridient.Type.Yeast))
            return;

        _ingridientsInBowl.Clear();
        //_makingAnimation.Play();
        _fridge.UpdateFridge();

        Instantiate(_dough.gameObject, _doughSpawnpoint.position, _doughSpawnpoint.rotation);
        HasWater = false;
    }
}
