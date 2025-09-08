using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
    }
    public void UpdateHealthBar(float health, float maxHealth)
    {
        Debug.Log("Updating Health: " + health / maxHealth);
        _healthSlider.value = health / maxHealth;
    }
}
