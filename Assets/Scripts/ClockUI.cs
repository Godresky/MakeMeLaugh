using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [SerializeField]
    private Transform _minuteArrow;
    [SerializeField] 
    private Transform _hourArrow;

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }


    private void UpdateTime()
    {
        _minuteArrow.eulerAngles = new Vector3(0, 0, -(float)TimeManager.Minute * 6f);
        _hourArrow.eulerAngles = new Vector3(0, 0, -((float)TimeManager.Hour * 60 + (float)TimeManager.Minute) * 0.25f);
    }
}
