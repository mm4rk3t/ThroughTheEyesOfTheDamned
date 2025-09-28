using System.Collections;
using UnityEngine;

public interface IDamageable
{

    public void TakeDamage(int dmg)
    {
    }

    public IEnumerator DamageFlash()
    {
        yield return new WaitForSeconds(0.1f); // duration of flash
    }

    public void Die() { }
}
