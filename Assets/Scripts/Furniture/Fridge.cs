using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private List<FridgeItem> _ingridients;
    [SerializeField] private List<Transform> _defaultPositions;

    public static Fridge Singleton;

    private void Awake()
    {
        Singleton = this;
    }

    private void Start(){
        UpdateFridge();
    }

    public void UpdateFridge()
    {
        for(int i = 0; i < _ingridients.Count; i++) {
            if (_ingridients[i].IsUsing)
            {
                UpdateItem(_ingridients[i], _defaultPositions[i]);
                Debug.Log(_ingridients[i].gameObject);
            }
        }
    }

    public void UpdateFridgeItem(FridgeItem fridgeItem)
    {
        if (_ingridients.Contains(fridgeItem))
        {
            for (int i = 0; i < _ingridients.Count; i++)
            {
                if (_ingridients[i] == fridgeItem)
                {
                    UpdateItem(fridgeItem, _defaultPositions[i]);
                    break;
                }
            }
        }
    }

    private void UpdateItem(FridgeItem fridgeItem, Transform position)
    {
        fridgeItem.IsUsing = false;
        fridgeItem.Rigidbody.Sleep();
        fridgeItem.Rigidbody.velocity = Vector3.zero;
        fridgeItem.gameObject.transform.SetPositionAndRotation(position.position, position.rotation);
        fridgeItem.Rigidbody.WakeUp();
    }
}
