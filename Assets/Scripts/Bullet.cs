using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : Proyectile
{
    //BulletMovement
    private Vector2 moveDirection;
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
}
