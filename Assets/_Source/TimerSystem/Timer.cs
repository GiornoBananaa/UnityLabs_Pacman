using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _elapsedTime;
    private float _endTime;

    public Action OnTimerEnd;

    private void Awake()
    {
        enabled = false;
    }

    public void Update()
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime>_endTime)
        {
            enabled = false;
            OnTimerEnd?.Invoke();
        }
    }

    public void SetTimer(float endTime)
    {
        _elapsedTime = 0;
        _endTime = endTime;
        enabled = true;
    }
}
