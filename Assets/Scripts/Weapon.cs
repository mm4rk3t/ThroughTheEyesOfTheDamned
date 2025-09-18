using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //This scripts needs polish--> need to start modifiying Sword for parrying or use an interface for handling the parry
    //need to make this abstact class more general
    //TRY DOING PARRY WITH INTERFACES INSTEAD OF HERENCY *-+-+-+-+-+-*_*-*-*-*-*-*-*
    [Header("SoundManagement")]
    [SerializeField] protected AudioClip _attackSound;
    [SerializeField] protected AudioClip _parrySound;


    [Header("CombatConfig")]
    [SerializeField] protected int _attackDmg = 35;
    [SerializeField] protected bool _hasParry = false;//Activate if weapon CAN parry
    [SerializeField] protected bool _isParry = false;//Serialized --> just for debugging --->Changes in Input reading from the Weapon (in prototype just in "Sword" gameObject for now)
    [SerializeField] protected bool _isAttacking = false;
    [SerializeField]protected float _parryWindow = 0.5f;
    public bool IsParrying { get{ return _isParry; } set{_isParry = value; } } //---> For Weapon with _hasParry = true to collect


    protected SpriteRenderer _sprite;
    protected BoxCollider2D _attackCollider;
    protected CapsuleCollider2D _ParryCollider;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();
        if (damageableObject!=null && _isParry==false)
        {
            damageableObject.TakeDamage(_attackDmg);
        }        
        //Collision with Proyectile
        if (other.CompareTag("Proyectile"))
        {
            Proyectile Proyectile = other.GetComponent<Proyectile>();
            //Check if proyectile is ParryAble and if Weapon(Sword) is Parrying
            if (Proyectile.IsParryAble == true && _isParry == true)
            {
                SoundManager.PlayerSound(_parrySound);
                ParryComponents(false);//disable components of sword because of parry
                
                Destroy(other.gameObject);//Destroy bullet
            }
        }
    }
    
    protected IEnumerator SwordRoutine()
    {
        //Renamed SwordRoutine Variables
        //Activates collider, sprite, attacking Bool and Play sound
        SoundManager.PlayerSound(_attackSound);//public static Script non instantiable
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
        _attackCollider = GetComponent<BoxCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();

        _ParryCollider.enabled = false;
        _attackCollider.enabled = false;
        _sprite.enabled = false;
    }
    protected IEnumerator Parry(float _parryWindow)
    {
        //Start the Parry Coroutine
        transform.localRotation = Quaternion.Euler(0, 0, 90);//Rotates weapon for simulating "Parry" (provisory) ---> Need to add a (change of color/sprite or something like that,a parry sound and basic feedback)
        ParryComponents(true);
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
