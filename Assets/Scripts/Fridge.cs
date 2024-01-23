using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private List<Transform> _ingridients;
    [SerializeField] private List<Transform> _defaultPositions;

    private void Start()
    {
        UpdateFridge();
    }

    public void UpdateFridge(){
        for(int i = 0; i < _ingridients.Count; i++) {
            _ingridients[i].SetPositionAndRotation(_defaultPositions[i].position, _defaultPositions[i].rotation);
        }
    }
}
