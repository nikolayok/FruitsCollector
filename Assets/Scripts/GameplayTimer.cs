using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameplayTimer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    //private int _currentHours = 0;
    private int _currentMilliseconds = 0;
    private int _currentSeconds = 0;
    private int _currentMinutes = 0;
    private float _currentTime = 0;
    private bool _isAlreadyWin = false;

    private void FixedUpdate()
    {
        if (_isAlreadyWin)
        {
            return;
        }

        TimerTick();
        SetTimerText();
    }

    private void TimerTick()
    {
        _currentTime += Time.fixedDeltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(_currentTime);
        _currentMilliseconds = timeSpan.Milliseconds;
        _currentSeconds = timeSpan.Seconds;
        _currentMinutes = timeSpan.Minutes;
        //_currentHours = timeSpan.Hours;
    }

    public void StopTimer()
    {
        _isAlreadyWin = true;
    }

    private void SetTimerText()
    {
        string timerText = "";

        //if (_currentHours < 10)
        //{
        //    timerText += "0";
        //}

        //timerText += _currentHours;
        //timerText += ":";

        if (_currentMinutes < 10)
        {
            timerText += "0";
        }

        timerText += _currentMinutes;
        timerText += ":";

        if (_currentSeconds < 10)
        {
            timerText += "0";
        }

        timerText += _currentSeconds;
        timerText += ":";

        if (_currentMilliseconds < 100)
        {
            timerText += "0";
        }

        timerText += _currentMilliseconds / 10;

        _timerText.text = timerText;
    }
}
