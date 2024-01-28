using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BakeryController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _cookedBakings;

    [SerializeField]
    private List<GameObject> _doughtStates;

    public static BakeryController Singleton;

    private void Awake()
    {
        Singleton = this;
    }

    public void LoadCookedBaking(Dough dough, Baking.Type type)
    {
        dough.GetComponent<Rigidbody>().Sleep();
        GameObject cookedBaking = Instantiate(_cookedBakings[(int)type], dough.transform.position, dough.transform.rotation);
        Baking cookedBakingClass = cookedBaking.GetComponent<Baking>();

        cookedBakingClass.IsPoisoned = dough.IsPoisoned;
        cookedBakingClass.IsIncorect = dough.IsIncorect;

        Destroy(dough.gameObject);
    }

    public void LoadDough(Dough oldDough, Dough.State state)
    {
        oldDough.GetComponent<Rigidbody>().Sleep();
        GameObject newDough = Instantiate(_doughtStates[(int)state], oldDough.transform.position, oldDough.transform.rotation);
        Dough newDoughClass = newDough.GetComponent<Dough>();

        newDoughClass.IsPoisoned = oldDough.IsPoisoned;
        newDoughClass.IsIncorect = oldDough.IsIncorect;

        Destroy(oldDough.gameObject);
    }
}
