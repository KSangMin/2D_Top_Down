using System;
using UnityEngine;

public class Stat
{
    private float _curValue;
    public float CurValue
    {
        get { return _curValue; }
        set
        {
            if(_curValue != value)
            {
                _curValue = value;
                OnStatChanged?.Invoke();
                OnStatPercentageChanged?.Invoke(GetPercentage());
            }
        }
    }
    public float minValue;
    public float maxValue;
    private float startValue;

    public Action OnStatChanged;
    public Action<float> OnStatPercentageChanged;

    public Stat(float startValue, float maxValue, float minValue = 0f)
    {
        this.startValue = startValue;
        this.CurValue = startValue;
        this.maxValue = maxValue;
        this.minValue = minValue;
    }

    public void Add(float amount)
    {
        CurValue = Mathf.Min(CurValue + amount, maxValue);
    }

    public void Extend(float amount)
    {
        maxValue += amount;
        CurValue += amount;
    }

    public void Subtract(float amount)
    {
        CurValue = Mathf.Max(CurValue - amount, minValue);
    }

    public void Shrink(float amount)
    {
        CurValue = MathF.Min(CurValue - amount, minValue);
        maxValue -= amount;
    }

    public float GetPercentage()
    {
        return CurValue / maxValue;
    }

    public void Reset()
    {
        CurValue = maxValue;
    }
}
