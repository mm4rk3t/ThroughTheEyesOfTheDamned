using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : Proyectile
{
    //BulletMovement
    private Rigidbody2D rb;
    private EnemyShooting _playerPos;
    private Vector2 direction;
    private void Start()
    {
        _playerPos = (EnemyShooting)FindAnyObjectByType(typeof(EnemyShooting));
        rb = GetComponent<Rigidbody2D>();
        if (_playerPos == null)
        {
            Debug.LogError("Player position not found:");
        }
        SetDirection(_playerPos.PlayerPos);//Make the direction the bullet will travel
        Destroy(gameObject, 5f);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * _pVelocity;

        transform.rotation = Quaternion.Euler(0,0,_playerPos.DegreeRotation);
    }

    public void SetDirection(Vector3 newDirection)
    {
        //direction = newDirection.normalized;
        direction = newDirection - transform.position;
    }
}
