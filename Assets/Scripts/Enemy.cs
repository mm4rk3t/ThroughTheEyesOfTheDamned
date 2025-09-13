using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


public abstract class Enemy : MonoBehaviour
{

    [Header("Health Components")]
    [SerializeField]protected int health = 100;
    [SerializeField] protected Canvas healthBar;
    [SerializeField] private Transform _hBarPos;//Posicion para la barra de vida
    protected FloatingHealthBar healthSlider;
    protected int _maxHealth;

    [Header("Combat Components")]
    [SerializeField] protected Transform _bulletPivot;
    [SerializeField] protected Transform _bulletSpawn;
    [SerializeField] public List<GameObject> _bullets = new List<GameObject>();
    [SerializeField] protected float _timer;
    [SerializeField] protected float _shootTimer;
    protected GameObject _playerPos;
    public GameObject PlayerPos { get{return _playerPos; } set { _playerPos = value; } }
    public float shootTimer { get{return _shootTimer; } set{_shootTimer = value; } }
    public Transform bulletPivot{ get{return _bulletPivot; } set{ _bulletPivot  = value; } }//PIVOT FOR AIMING
    public Transform bulletSpawnPoint { get { return _bulletSpawn; } set { _bulletSpawn = value; } }//SPAWNPOINT BULLET


    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private void Start()
    {
        
        PlayerPos = GameObject.Find("Player");
        if (PlayerPos== null)
        {
            Debug.Log("Error: Player Not found In Scene");
        }
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

    public void Shoot()
    {

    }

}
