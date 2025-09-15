using System.Collections;
using UnityEngine;

public interface IDamageable
{

    public void TakeDamage(int dmg)
    {
        /*
        StartCoroutine(DamageFlash());
        health -= dmg;
        healthSlider.UpdateHealthBar(health, _maxHealth);
        Debug.Log(gameObject.name + " took " + dmg + " damage. HP left: " + health);

        if (health <= 0)
        {
            Die();
        }
        */
    }

    public IEnumerator DamageFlash()
    {
        /*
        Debug.Log("start");
        spriteRenderer.color = Color.red;
        spriteRenderer.color = originalColor; // revert back
        Debug.Log("end");
        */
        yield return new WaitForSeconds(0.1f); // duration of flash
    }

    public void Die() { }
}
