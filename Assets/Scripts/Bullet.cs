using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : Proyectile
{
    //BulletMovement
    private Vector2 moveDirection;
    public bool isReflected;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    private void FixedUpdate()
    {
        transform.Translate(moveDirection * _pVelocity * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        //Recibe la direccion desde el enemigo.
        moveDirection = newDirection.normalized;
    }

    public void Reflect(Vector2 newDirection)
    {
        isReflected = true;                 // <- from Proyectile
        moveDirection = newDirection.normalized;
        gameObject.tag = "EnemyProyectile";  // <- so it can hit enemies
    }


}
