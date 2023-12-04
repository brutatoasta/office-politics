using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;


public class TimerAnalog : MonoBehaviour
{
    #region Variables
    enum TimerType { Countdown, Stopwatch }
    [SerializeField] private TimerType timerType;
    [SerializeField] private bool _isRunning = false;
    const float hoursInDay = 24,
    minutesInHour = 60,
    workHours = 5 + 12 - 8, // 9h workday
    startHourOffset = 8f,
    workdayDuration = 20f, // TODO: use GameSO or RunSO
    // day starts at 8 am, ends at 5pm
    // 9 hour workday
    hoursToDegrees = 360 / 12,
    minutesToDegrees = 360 / 60;

    float elapsedInGameHours, currentHour, currentMinutes = 0f;
    public float elapsedRealSeconds = 0f;
    public RectTransform minuteHand;
    public RectTransform hourHand;
    public Image Circle;
    public Color currentColor;

    #endregion

    void Start()
    {
        GameManager.instance.TimerStart.AddListener(OnTimerStart);
        GameManager.instance.TimerStop.AddListener(OnTimerStop);
        GameManager.instance.TimerUpdate.AddListener(OnTimerUpdate);
        Circle = transform.GetChild(0).GetComponent<Image>();
        minuteHand = transform.GetChild(1).GetComponent<RectTransform>();
        hourHand = transform.GetChild(2).GetComponent<RectTransform>();
        currentColor = Circle.color;
    }

    private void OnTimerStart() => _isRunning = true;
    private void OnTimerStop() => _isRunning = false;
    private void OnTimerRestart()
    {
        elapsedRealSeconds = 0f;
        currentColor = Color.white;
    }
    private void SetTimerType(TimerType type) => timerType = type;
    private void OnTimerUpdate(float value) => elapsedRealSeconds = value;
    private Color ChangeColor(float value)
    {

        return value switch
        {
            float val when CloseEnough(val / workdayDuration, 2f / 3f, 0.01) => timerType == TimerType.Stopwatch ? Color.red : Color.yellow,
            float val when CloseEnough(val / workdayDuration, 1f / 3f, 0.01) => timerType == TimerType.Stopwatch ? Color.yellow : Color.red,
            _ => currentColor,
        };



    }
    static bool CloseEnough(double value1, double value2, double acceptableDifference)
    {
        return Math.Abs(value1 - value2) <= acceptableDifference;
    }
    private void Update()
    {
        if (!_isRunning)
        {
            return;
        }
        else
        {
            // calculate time
            elapsedRealSeconds += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;
            elapsedInGameHours = elapsedRealSeconds * workHours / workdayDuration;

            currentHour = (startHourOffset + elapsedInGameHours) % 12;
            currentMinutes = elapsedInGameHours * minutesInHour % minutesInHour;
            hourHand.rotation = Quaternion.Euler(0, 0, -currentHour * hoursToDegrees);
            minuteHand.rotation = Quaternion.Euler(0, 0, -currentMinutes * minutesToDegrees);
        }

        // Overtime
        if (elapsedRealSeconds >= workdayDuration)
        {
            GameManager.instance.OnStopTimer();
        }
        if (currentColor != ChangeColor(elapsedRealSeconds))
        {
            // @Natthan this is where you can call the sounds
            currentColor = ChangeColor(elapsedRealSeconds);
            StartCoroutine(TimerShader(currentColor));
        }
    }
    IEnumerator TimerShader(Color color)
    {
        // blink 5 times
        for (int i = 0; i < 10; i++)
        {
            Circle.color = (i % 2 == 0) ? color : Color.white;
            yield return new WaitForSecondsRealtime(0.1f);
        }

    }
}
