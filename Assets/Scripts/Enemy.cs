using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{
    [SerializeField]private int health = 100;
    private int _maxHealth;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private FloatingHealthBar healthSlider;
    private void Start()
    {
        _maxHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on enemy!");
        }
        originalColor = spriteRenderer.color;
        healthSlider = GetComponentInChildren<FloatingHealthBar>();
        healthSlider.UpdateHealthBar(health, _maxHealth);
    }

    public void TakeDamage(int dmg)
    {
        StartCoroutine(DamageFlash());
        health -= dmg;
        healthSlider.UpdateHealthBar(health, _maxHealth);
        Debug.Log(gameObject.name + " took " + dmg + " damage. HP left: " + health);
        
        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator DamageFlash()
    {
        Debug.Log("start");
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f); // duration of flash
        spriteRenderer.color = originalColor; // revert back
        Debug.Log("end");
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }

}
