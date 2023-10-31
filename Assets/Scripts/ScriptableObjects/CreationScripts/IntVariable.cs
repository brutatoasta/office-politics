using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "ScriptableObjects/IntVariable", order = 3)]
public class IntVariable : Variable<int>
{

    public int maxValue;
    public override void SetValue(int value)
    {
        _value = (value < maxValue) ?
            (value < 0 ? 0 : value) :
            maxValue;
    }

    // overload
    public void SetValue(IntVariable value)
    {
        SetValue(value.Value);
    }

    public void ApplyChange(int amount)
    {
        this.Value += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        ApplyChange(amount.Value);
    }

}