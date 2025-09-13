using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


public abstract class Enemy : MonoBehaviour
{

    [Header("Health Components")]
    [SerializeField] protected int health = 100;
    [SerializeField] protected Canvas healthBar;
    [SerializeField] private Transform _hBarPos;//Posicion para la barra de vida
    protected FloatingHealthBar healthSlider;
    protected int _maxHealth;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private void Start()
    {
        _maxHealth = health;
        Instantiate(healthBar, _hBarPos);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on enemy!");
        }
        originalColor = spriteRenderer.color;
        healthSlider = GetComponentInChildren<FloatingHealthBar>();
        healthSlider.UpdateHealthBar(health, _maxHealth);
    }

    public virtual  void TakeDamage(int dmg)
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

    public IEnumerator DamageFlash()
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
