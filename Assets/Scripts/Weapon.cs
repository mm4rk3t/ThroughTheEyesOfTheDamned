using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected AudioSource _attackSound;
    [SerializeField] protected int _attackDmg = 35;
    [SerializeField] protected bool _hasParry = false;//Activate if weapon CAN parry
    [SerializeField] protected bool _isParry = true;//Serialized --> just for debugging --->Changes in Input reading from the Weapon (in prototype just "Sword" script for now)
    public bool IsParrying { get{ return _isParry; } set{_isParry = value; } } //---> For Weapon with _hasParry = true to collect
    protected SpriteRenderer _sprite;
    [SerializeField] protected bool _isAttacking = false;
    protected BoxCollider2D _attackCollider;
    protected CapsuleCollider2D _ParryCollider;
    [SerializeField]protected float _parryWindow = 0.5f;
    

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && _isParry == false)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(_attackDmg);
        }
        if (other.CompareTag("Proyectile"))
        {
            Proyectile Proyectile = other.GetComponent<Proyectile>();
            if (Proyectile._IsParryAble == true && _isParry == true)
            {
                StartCoroutine(Parry(_parryWindow/2));
            }
        }
    }

    
    protected IEnumerator SwordRoutine()
    {
        //Renamed SwordRoutine Variables
        //Activates collider, sprite, attacking Bool and Play sound
        _attackSound.Play();
        _sprite.enabled = true;
        _attackCollider.enabled = true;
        _isAttacking = true;
        yield return new WaitForSeconds(0.25f);
        _sprite.enabled = false;
        _attackCollider.enabled = false;
        _isAttacking = false;
    }
    protected void GetComponents()
    {
        //Collects every component in use for any weapon in order to make Parry Coroutine work --> Called from Sword script --> Escalable to new weapons in the future
        _ParryCollider = GetComponent<CapsuleCollider2D>();//Parry collider recommended to be CapsuleCollider2D ---> Need further testing with BoxCollider for Gameplay
        _attackCollider = GetComponent<BoxCollider2D>();//Attack collider should be always BoxCollider2D
        _sprite = GetComponent<SpriteRenderer>();

        _ParryCollider.enabled = false;
        _attackCollider.enabled = false;
        _sprite.enabled = false;
    }
    protected IEnumerator Parry(float _parryWindow)
    {
        //_*_*_*_Remember to add Parry Sound at least_*_*_*_ either in here or in Bullet Script
        //Start the Parry Coroutine
        ParryComponents(true);
        transform.localRotation = Quaternion.Euler(0, 0, 90);//Rotates weapon for simulating "Parry" (provisory) ---> Need to add a (change of color/sprite or something like that,a parry sound and basic feedback)
        yield return new WaitForSeconds(_parryWindow);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        ParryComponents(false);
    }
    protected void ParryComponents(bool value)
    {
        //Enable-Disable Components just for Parry Coroutine
        _sprite.enabled = value;
        _ParryCollider.enabled = value;
        _isParry = value;
    }
}
