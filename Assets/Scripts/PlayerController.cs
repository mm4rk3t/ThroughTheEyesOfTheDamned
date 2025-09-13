using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.Compilation;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Camera cam;
    private GameObject pointer;
    private GameObject rotatePivot;
    //[SerializeField] private GameObject sword;
    public AudioSource attackSound;
    private int lives = 3;
    private int attackDmg = 35;
    private bool isAttacking = false;
    private float angleDeg;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        pointer = transform.GetChild(0).gameObject;
        rotatePivot = transform.GetChild(1).gameObject;
        Cursor.visible = true;
        //sword.SetActive(false);

    }

    void Update()
    {
        // Input WASD
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

        /*
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("attack");
            StartCoroutine(SwordRoutine());
        }
        */
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene currentScene = SceneManager.GetActiveScene();  // get current scene
            SceneManager.LoadScene(currentScene.name);           // reload it
        }

    }
    
    /*
    private IEnumerator SwordRoutine()
    {
        attackSound.Play();
        sword.SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(0.25f);
        sword.SetActive(false);
        isAttacking = false;
    }
    */

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }



}