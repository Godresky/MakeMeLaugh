using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    [SerializeField, Range(0,59)]
    private int _startMinute = 0;
    [SerializeField, Range(0,23)]
    private int _startHour = 0;

    [SerializeField, Min(0)]
    private float _minuteToRealTime = 0.5f; // second Real time to minute Game time
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        Minute = _startMinute;
        Hour = _startHour;
        timer = _minuteToRealTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();

            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
            }
            if (Hour >= 24)
            {
                Hour = 0;
            }

            timer = _minuteToRealTime;
        }
    }
}
