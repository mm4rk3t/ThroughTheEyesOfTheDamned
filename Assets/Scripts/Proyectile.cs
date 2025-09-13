using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Proyectile : MonoBehaviour
{
    [SerializeField] protected bool _isParryAble;
    [SerializeField] protected float _pVelocity;
    [SerializeField] protected AudioSource Sound;//Personal note : Remember Especifiying What is the purpose of this sound (OnParry, OnMovement, OnPlayerHit, OnEnviroment etc).
    protected SpriteRenderer _Color;
    public bool _IsParryAble{ get {return _isParryAble;}set{_isParryAble=value; }}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            Debug.Log("----HIT PLAYER----");
        }
        
        if (collision.GetComponent<Weapon>() && _isParryAble == true)//COMPRUEBA si colisiono con un arma, revisa si esta haciendo parry y si la bala instanciada es ParryAble
        {
            Debug.Log("------Bullet PARRIED!!------");
            Destroy(gameObject);
            //Remember to do something else than just destroying, add something for Feedback
        }
    }

}
