using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private List<FridgeItem> _ingridients;
    [SerializeField] private List<Transform> _defaultPositions;

    private void Start(){
        UpdateFridge();
    }

    public void UpdateFridge(){
        for(int i = 0; i < _ingridients.Count; i++) {
            if (_ingridients[i].IsUsing == true) {
                _ingridients[i].IsUsing = false;
                _ingridients[i].gameObject.transform.SetPositionAndRotation(_defaultPositions[i].position, _defaultPositions[i].rotation);
            }
        }
    }
}
