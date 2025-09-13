using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DummyControl : Enemy
{
    private float _regenTimer = 5f;
    private float _timer;
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
    
}
