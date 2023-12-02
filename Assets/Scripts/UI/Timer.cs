using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    #region Variables

    private TextMeshProUGUI _timerText;
    enum TimerType { Countdown, Stopwatch }
    [SerializeField] private TimerType timerType;


    [SerializeField] private float initialTime;
    [SerializeField] private float timeToDisplay;

    private bool _isRunning = false;

    #endregion

    private void Awake()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        timeToDisplay = initialTime;
    }

    void Start()
    {
        GameManager.instance.TimerStart.AddListener(OnTimerStart);
        GameManager.instance.TimerStop.AddListener(OnTimerStop);
        GameManager.instance.TimerUpdate.AddListener(OnTimerUpdate);
        // OnTimerStart(); // force timer to start

        // wait 3 seconds and invoke timerstart gameplay event()
        // GameManager.instance.TimerStart.Invoke();

        // if there is a countdown for the player to start, call on timer stop here then start timer via gameplay event
    }

    private void OnTimerStart() => _isRunning = true;
    private void OnTimerStop() => _isRunning = false;
    private void OnTimerUpdate(float value) => timeToDisplay = value;
    private Color ChangeColor(float value)
    {
        return value switch
        {
            float val when val / initialTime >= 2f / 3f => Color.green,
            float val when val / initialTime >= 1f / 3f => Color.yellow,
            float val when val / initialTime < 1f / 3f => Color.red,
            _ => Color.black,
        };
    }
    private void Update()
    {
        if (!_isRunning) return;

        // Overtime
        if (timerType == TimerType.Countdown && timeToDisplay < 0.0f)
        {
            GameManager.instance.OnStopTimer();
        }
        timeToDisplay += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        _timerText.text = timeSpan.ToString(@"mm\:ss");
        _timerText.color = ChangeColor(timeToDisplay);
    }
}
