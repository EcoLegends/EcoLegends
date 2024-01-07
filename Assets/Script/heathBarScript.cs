using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class heathBarScript : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI hpText;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        
        
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        hpText.text = health.ToString();
    }
}
