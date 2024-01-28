using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnEndWorkDay;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    [SerializeField]
    private AudioSource _ticking;

    [SerializeField, Range(0,59)]
    private int _startMinute = 0;
    [SerializeField, Range(0,23)]
    private int _startHour = 0;

    [SerializeField, Range(0, 59)]
    private int _endMinute = 0;
    [SerializeField, Range(0, 23)]
    private int _endHour = 0;

    [SerializeField, Min(0)]
    private float _minuteToRealTime = 0.5f; // second Real time to minute Game time
    private float timer;

    public static TimeManager Singleton;

    public Vector2 GetStartTime()
    {
        return new(_startHour, _startMinute);
    }

    public Vector2 GetEndTime()
    {
        return new(_endHour, _endMinute);
    }

    public bool IsEndWorkDay()
    {
        return Hour >= _endHour && Minute >= _endMinute;
    }

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        Minute = _startMinute;
        Hour = _startHour;
        timer = _minuteToRealTime;
    }

    private void Update()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();

            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();

                _ticking?.Play();
            }
            if (Hour >= 24)
            {
                Hour = 0;
            }

            timer = _minuteToRealTime;
        }

        if (Hour == _endHour && Minute == _endMinute)
        {
            OnEndWorkDay.Invoke();
        }
    }
}
