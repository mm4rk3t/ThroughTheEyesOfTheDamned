using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private List<GameObject> bullets;
    [SerializeField] private Transform firePoint;

    [Header("Movement Stats")]
    [SerializeField] private float _moveSpeed = 2f;
    private Vector2 _moveDirection;
    public Vector2 MoveDirection { get { return _moveDirection; } }
    private bool _isMoving = false;
    public bool IsMoving { get{return _isMoving; } }
    [Header("AttackStats")]
    [SerializeField] private float _timeBetweenShots = 0.2f;
    private int _hitsTaken = 0;
    private bool _isEnrage = false;//Estado mejorado


    [Header("Fase control")]
    [SerializeField] private float _moveTime = 2f;
    [SerializeField] private float _aimTime = 1f;
    private bool _isShooting;
    public bool IsShooting { get{return _isShooting; } }
    [Header("Screen Limits")]
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    public override void TakeDamage(int dmg)
    {
        _hitsTaken++;
        Debug.Log("Jefe golpeado. Golpes recibidos: " + _hitsTaken);
        if (_hitsTaken>=2 & !_isEnrage)
        {
            _isEnrage = true;
            _moveSpeed *= 1.5f;
            _moveTime /= 2f;
            _aimTime /= 2f;
            Debug.Log("¡Enfurecido!");
        }
        StartCoroutine(DamageFlash());
        health -= dmg;
        healthSlider.UpdateHealthBar(health, _maxHealth);
        Debug.Log(gameObject.name + " took " + dmg + " damage. HP left: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            objectWidth = spriteRenderer.bounds.size.x / 2;
            objectHeight = spriteRenderer.bounds.size.y / 2;
        }
    }
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(BossLogicCoroutine());
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

    private void Update()
    {
        if (_isMoving)
        {
            transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);

            CheckAndReverseDirection();
        }
    }
    void LateUpdate()
    {
<<<<<<< HEAD
        // asdhaosj szioldfghipazfg jsedfgiolñushdfiop h
=======
        //movement limited to CameraView
>>>>>>> Alam
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        transform.position = viewPos;
    }

    IEnumerator BossLogicCoroutine()
    {
        
        while (true)
        {
            //Movement Fase:
            ChooseRandomDirection();
            _isMoving = true;
            yield return new WaitForSeconds(_moveTime);


            //Aiming Fase
            _isMoving = false;
            AimAtPlayer();
            yield return new WaitForSeconds(_aimTime);

            //AttackFase
            StartCoroutine(ShootRoutine());
            yield return new WaitForSeconds(_aimTime);
        }


    }

    void ChooseRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        _moveDirection = new Vector2(x, y);
    }
    void CheckAndReverseDirection()
    {
        Vector3 currenPos = transform.position;
        //calcula los limites
        float boundX = screenBounds.x - objectWidth;
        float boundY = screenBounds.y - objectHeight;

        //comprobar borde der/izq
        if ((currenPos.x >= boundX && _moveDirection.x > 0)|| (currenPos.x <= -boundX && _moveDirection.x<0))
        {
            _moveDirection.x *= -1;
        }

        //comprobar bordes sup e inf

        if ((currenPos.y >= boundY&& _moveDirection.y>0)|| (currenPos.y <= -boundY && _moveDirection.y < 0))
        {
            _moveDirection.y *= -1;
        }
    }
    void AimAtPlayer()
    {
        if (_player == null) return;
        Vector2 directionToPlayer = _player.position - firePoint.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
    IEnumerator ShootRoutine()
    {
        _isShooting = true;
        int shots = _isEnrage ? 2 : 1;//si esta enfurecido, dispara 2 veces, si no 1;
        for (int i = 0; i < shots; i++)
        {
            //disparo principal
            InstantiateProjectile(firePoint.up);
            //disparo Diagonal izq (-30grados)
            InstantiateProjectile(Quaternion.Euler(0,0,-30)*firePoint.up);
            //disparo Diagonal der (+30grados)
            InstantiateProjectile(Quaternion.Euler(0, 0, +30) * firePoint.up);
            if (shots > 1)
            {
                yield return new WaitForSeconds(_timeBetweenShots);
            }
        }
        _isShooting = false;
    }

    void InstantiateProjectile( Vector2 direction)
    {
        int rnd = Random.Range(0, bullets.Count);
        GameObject proj = Instantiate(bullets[rnd],firePoint.position,Quaternion.identity);
        proj.GetComponent<Bullet>().SetDirection(direction);
    }
}
