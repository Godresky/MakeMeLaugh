using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TEST_TimeUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeText;

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
        _timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
    }
}
