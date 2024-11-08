using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Action onTimerEnded;
    private float _duration;
    private float _elapsedTime;
    [HideInInspector] public bool isRunning;

    private void StartTimer()
    {
        StopAllCoroutines();
        _elapsedTime = 0;
        isRunning = true;
        StartCoroutine(TimerCoroutine());
    }

    protected void SetListener(Action onEnd)
    {
        onTimerEnded = onEnd;
    }

    public void StartTimer(float duration)
    {
        _duration = duration;
        StartTimer();
    }

    protected void SetDuration(float duration)
    {
        _duration = duration;
    }

    public virtual void Stop()
    {
        isRunning = false;
        StopAllCoroutines();
    }

    public void PauseTimer()
    {
        isRunning = false;
        StopAllCoroutines();
    }

    public void ResumeTimer()
    {
        isRunning = true;
        StartCoroutine(TimerCoroutine());
    }

    public void DecreaseElapsed(float time)
    {
        _elapsedTime -= time;
        _elapsedTime = Mathf.Clamp(_elapsedTime, 0, _duration);
    }

    public void ResetTimer()
    {
        _elapsedTime = 0;
        if (!isRunning) return;
        Stop();
        StartTimer(_duration);
    }

    public float GetProgress()
    {
        return Mathf.Clamp01(GetRemainingTime() / _duration);
    }

    protected float GetRemainingTime()
    {
        return Mathf.Clamp(_duration - _elapsedTime, 0, _duration);
    }

    private IEnumerator TimerCoroutine()
    {
        while (isRunning && _elapsedTime < _duration)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (!(_elapsedTime >= _duration)) yield break;
        isRunning = false;
        onTimerEnded?.Invoke();
    }
}