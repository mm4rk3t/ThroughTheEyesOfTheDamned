using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    private GameObject Player;
    private Enemy enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        Player = GameObject.Find("Player");
        if (Player == null)
        {
            Debug.LogError("Player not found");
        }
    }

    private void Update()
    {
        enemy.shootTimer -= Time.deltaTime;
        if (enemy.shootTimer<=0)
        {
            Shoot();
        }

        
    }
    private void Shoot()
    {
        //float rotDegree= Mathf.Atan2(Player.transform.position.y - enemy.bulletSpawnpoint.position.y, Player.transform.position.x - enemy.bulletSpawnpoint.position.x)*Mathf.Rad2Deg;//Posicion del jugador

        int random = Random.Range(0, enemy._bullets.Count);
        Instantiate(enemy._bullets[random], enemy.bulletSpawnPoint.position,enemy.bulletPivot.rotation);
        enemy.shootTimer = 5f;
    }
}
