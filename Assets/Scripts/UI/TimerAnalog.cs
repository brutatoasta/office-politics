using System;
using UnityEngine.UI;
using UnityEngine;


public class TimerAnalog : MonoBehaviour
{
    #region Variables

    private bool _isRunning = false;
    const float hoursInDay = 24,
    minutesInHour = 60,
    workHours = 5 + 12 - 8,
    startHourOffset = 8f,
    workdayDuration = 120f,
    // day starts at 8 am, ends at 5pm
    // 9 hour workday
    hoursToDegrees = 360 / 12,
    minutesToDegrees = 360 / 60;

    float elapsedRealSeconds, elapsedInGameHours, currentHour, currentMinutes = 0f;
    public RectTransform minuteHand;
    public RectTransform hourHand;
    public Image background;

    #endregion

    void Start()
    {
        GameManager.instance.TimerStart.AddListener(OnTimerStart);
        GameManager.instance.TimerStop.AddListener(OnTimerStop);
        // GameManager.instance.TimerUpdate.AddListener(OnTimerUpdate);
    }

    private void OnTimerStart() => _isRunning = true;
    private void OnTimerStop() => _isRunning = false;
    // private void OnTimerUpdate(float value) => timeToDisplay = value;
    private Color ChangeColor(float value)
    {
        return value switch
        {
            float val when val / workdayDuration >= 2f / 3f => Color.red,
            float val when val / workdayDuration >= 1f / 3f => Color.yellow,
            float val when val / workdayDuration < 1f / 3f => Color.green,
            _ => Color.black,
        };
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
            elapsedRealSeconds += Time.deltaTime;
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
        if (elapsedRealSeconds >= workdayDuration)
        {
            GameManager.instance.OnStopTimer();
        }
        background.color = ChangeColor(elapsedRealSeconds);
    }
}
