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
        set
        {
            maxAmount = value;
            slider.maxValue = value;
        }
    }

    public int MinAmount
    {
        get => minAmount;
        set
        {
            minAmount = value;
            slider.minValue = value;
        }
    }

    public int Amount
    {
        get => amount;
        set { slider.value = value; }
    }
}
