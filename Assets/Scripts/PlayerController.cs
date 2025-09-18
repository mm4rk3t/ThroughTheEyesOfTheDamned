using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Camera cam;
    private GameObject pointer;
    private GameObject rotatePivot;
    private float angleDeg;
    private int lives = 3;
    private bool isAttacking = false;
    private SpriteRenderer spriteRenderer;
    
    [Header("Health")]
    [SerializeField] int health = 100;
    [Header("Speed")]
    [SerializeField] private float moveSpeed = 5f;


    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        pointer = transform.GetChild(0).gameObject;
        rotatePivot = transform.GetChild(1).gameObject;
        Cursor.visible = true;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        // Input WASD
        if (Time.timeScale == 0) return;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        Vector3 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        angleDeg = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90;

        if(!isAttacking)
        { 
            rotatePivot.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        }
        pointer.transform.position = mousePos;
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene currentScene = SceneManager.GetActiveScene();  // get current scene
            SceneManager.LoadScene(currentScene.name);           // reload it
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int dmg)
    {
        
        StartCoroutine(DamageFlash());
        health -= dmg;
        //healthSlider.UpdateHealthBar(health, _maxHealth);
        Debug.Log(gameObject.name + " took " + dmg + " damage. HP left: " + health);

        if (health <= 0)
        {
            Die();
        }
        
    }

    public IEnumerator DamageFlash()
    {
        //Debug.Log("start");
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f); // duration of flash
        spriteRenderer.color = Color.white; // revert back
        //Debug.Log("end");
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}