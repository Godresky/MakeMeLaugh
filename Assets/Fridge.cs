using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private List<Transform> _ingridients;
    [SerializeField] private List<Vector3> _defaultPositions;

    public void UpdateFridge(){
        for(int i = 0; i < _ingridients.Count; i++) {
            _ingridients[i].position = _defaultPositions[i];
        }
    }
}
