using UnityEngine;
using UnityEngine.UI;

public class ResourceMeter : MonoBehaviour
{
    public Slider slider;

    private int maxAmount;
    private int minAmount;
    private int amount;

    public int MaxAmount
    {
        get => maxAmount;
        set { slider.maxValue = value; }
    }

    public int MinAmount
    {
        get => minAmount;
        set { slider.minValue = value; }
    }

    public int Amount
    {
        get => amount;
        set
        {
            if (!(minAmount < value && value < maxAmount))
            {
                slider.value = value;
            }
            else
            {
                Debug.LogWarning($"Trying to set value of {slider.name} to {value}, which is greater than its max of {maxAmount}");
            }
        }
    }
}
