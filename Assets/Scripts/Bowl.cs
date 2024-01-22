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
    private List<DoughIngridient.IngridientType> _ingridientsInBowl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DoughIngridient ingridient))
        {
            if (other.TryGetComponent(out Egg egg) && egg.IsBroken == true && egg.CurrentType == DoughIngridient.IngridientType.Egg)
                return;

            _ingridientsInBowl.Add(ingridient.CurrentType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DoughIngridient ingridient))
        {
            _ingridientsInBowl.Remove(ingridient.CurrentType);
        }
    }

    public void TryMakeDough()
    {
        if (!_ingridientsInBowl.Contains(DoughIngridient.IngridientType.Egg) || !_ingridientsInBowl.Contains(DoughIngridient.IngridientType.Flour) || !_ingridientsInBowl.Contains(DoughIngridient.IngridientType.Water) || !_ingridientsInBowl.Contains(DoughIngridient.IngridientType.Yeast))
            return;

        _ingridientsInBowl.Clear();
        //_makingAnimation.Play();
        //_fridge.UpdateFridge();

        Instantiate(_dough.gameObject, _doughSpawnpoint.position, _doughSpawnpoint.rotation);
    }
}
