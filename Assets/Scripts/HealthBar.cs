using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] private Image fill;
    public Gradient gradient;

    
    public void SetMaxHealth(int h)
    {
        slider.maxValue = h;
        slider.value = h;

        fill.color =  gradient.Evaluate(1f);
    }
    
    public void SetHealth(int h)
    {
        slider.value = h;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
    