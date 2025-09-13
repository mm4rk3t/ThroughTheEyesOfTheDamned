using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DummyControl : Enemy
{
    private float _playerDegree;
    private float _regenTimer = 5f;
    private void Update()
    {
        if (_timer<=0 & health!=_maxHealth)
        {
            health += 1;
            healthSlider.UpdateHealthBar(health, _maxHealth);
        }
        if (_timer>0)
        {
            _timer -= Time.deltaTime;
        }
        AimPlayer(bulletPivot);
    }
    public override void TakeDamage(int dmg)
    {
        if (health <= 0)
        {
            health = 0;
            _timer = _regenTimer;
        }
        else
        {
            health -= dmg;
            _timer = _regenTimer;
        }
        StartCoroutine(DamageFlash());
        healthSlider.UpdateHealthBar(health, _maxHealth);
    }
    private void AimPlayer(Transform pivot)
    {
        float degree = Mathf.Atan2(pivot.transform.position.y - _playerPos.transform.position.y, pivot.transform.position.x - _playerPos.transform.position.x) * Mathf.Rad2Deg;//ENEMIGO APUNTA AL JUGADOR
        pivot.transform.rotation = Quaternion.Euler(0, 0, degree);
        bulletSpawnPoint.rotation = Quaternion.Euler(0,0,degree);
    }
    
}
