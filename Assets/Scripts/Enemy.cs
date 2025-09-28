using UnityEngine;
using System.Collections;


public abstract class Enemy : MonoBehaviour, IDamageable
{

    [Header("Health Components")]
    [SerializeField] protected int health = 100;
    public int Health { get { return health; } }

    [SerializeField] protected Canvas healthBar;
    [SerializeField] protected Transform _hBarPos;//Posicion para la barra de vida
    protected FloatingHealthBar healthSlider;
    protected int _maxHealth;
    

    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;

    
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
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f); // duration of flash
        spriteRenderer.color = originalColor; // revert back
    }


    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet != null && bullet.isReflected)
        {
            TakeDamage(bullet._damage * 10);
            Destroy(bullet.gameObject);
        }
    }

}
