using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Vector2 MoveInput { get{return moveInput; } }

    private Camera cam;
    private GameObject pointer;
    private GameObject rotatePivot;
    private float angleDeg;
    private int lives = 3;
    private bool isAttacking = false;
    private SpriteRenderer spriteRenderer;
    Vector3 mousePos;

    [Header("Health")]
    [SerializeField] int health = 100;
    [SerializeField] private Image healthBarFill;
    private bool isDead;
    [Header("Speed")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("References")]
    private GameManager gameManager;
    private PlayerAnimatorController AnimatorController;

    void Start()
    {
        AnimatorController = GetComponent<PlayerAnimatorController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager==null)
        {
            Debug.Log("Game Manager Not found in Scene");
        }
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
        if (isDead == true) return;
        mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        pointer.transform.position = mousePos;
        PlayerMovement(mousePos);
        if(!isAttacking)
        { 
            rotatePivot.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        }
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Scene currentScene = SceneManager.GetActiveScene();  // get current scene
        //    SceneManager.LoadScene(currentScene.name);           // reload it
        //}

    }

    private void PlayerMovement(Vector3 mousepos)
    {
        if (Time.timeScale == 0) return;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        angleDeg = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90;


    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * moveInput);
    }

    public void TakeDamage(int dmg)
    {
        
        StartCoroutine(DamageFlash());
        health -= dmg;
        //healthSlider.UpdateHealthBar(health, _maxHealth);
        healthBarFill.fillAmount = (health/100f);
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
        isDead = true;
        AnimatorController.DeathAnimation(isDead);
        lives--;
        if (lives<0)
        {
            gameManager.ChangeScene("DefeatScene");
        }
        gameManager.OnDeath(isDead);
        while (isDead == true)
        {
            //wait R Input for respawn
            if (Input.GetKeyDown(KeyCode.R))
            {
                
                PlayerRespawn();
            }
        }
        
    }

    private void PlayerRespawn()
    {
        AnimatorController.DeathAnimation(!isDead);
        gameManager.OnDeath(!isDead);
        health = 100;
        healthBarFill.fillAmount = (health / 100f);
    }

}