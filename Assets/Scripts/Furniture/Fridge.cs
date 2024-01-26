using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private List<FridgeItem> _ingridients;
    [SerializeField] private List<Transform> _defaultPositions;

    private void Start(){
        UpdateFridge();
    }

    public void UpdateFridge()
    {
        for(int i = 0; i < _ingridients.Count; i++) {
            _ingridients[i].gameObject.transform.SetPositionAndRotation(_defaultPositions[i].position, _defaultPositions[i].rotation);
        }
    }

    public void UpdateFridgeItem(FridgeItem fridgeItem)
    {
        if (_ingridients.Contains(fridgeItem))
        {
            for (int i = 0; i < _ingridients.Count; i++)
            {
                if (_ingridients[i] == fridgeItem)
                    _ingridients[i].gameObject.transform.SetPositionAndRotation(_defaultPositions[i].position, _defaultPositions[i].rotation);
            }
        }
    }
}
