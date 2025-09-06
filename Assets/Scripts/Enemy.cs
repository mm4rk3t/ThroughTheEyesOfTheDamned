using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private int health = 100;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on enemy!");
        }
        originalColor = spriteRenderer.color;

    }

    public void TakeDamage(int dmg)
    {
        StartCoroutine(DamageFlash());
        health -= dmg;
        Debug.Log(gameObject.name + " took " + dmg + " damage. HP left: " + health);
        
        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator DamageFlash()
    {
        Debug.Log("start");
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f); // duration of flash
        spriteRenderer.color = originalColor; // revert back
        Debug.Log("end");
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }

}
