using UnityEngine;
using UnityEngine.UI;

public class ghostHealthBar : MonoBehaviour
{
    public Slider healthSlider; // Unity UI Slider
    public int currentHealth;     // Unity UI Text

    public Transform ghostTransform;

    void Update()
    {
        this.transform.position = ghostTransform.position + new Vector3(0, 0.8f, 0);
    }

    public void Set_MaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        Update_Health(maxHealth);
    }

    public void Set_Health(int currentHealth)
    {
        healthSlider.value = currentHealth;
        Update_Health(currentHealth);
    }

    private void Update_Health(int health)
    {
        currentHealth = health;
    }
}