using Unity.VisualScripting;
using UnityEngine;

public class Bullet : Proyectile
{
    //BulletMovement
    private Rigidbody2D rb;
    
    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
        rb.linearVelocity = _playerPos - transform.position * _pVelocity;
        //Dispara en direccion al jugador desde el spawnpoint de proyectiles del enemigo
    }

    
}
