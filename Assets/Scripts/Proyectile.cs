using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Proyectile : MonoBehaviour
{
    [SerializeField] protected bool _isParryAble;
    [SerializeField] protected float _pVelocity;
    [SerializeField] private int _damage;
    [SerializeField] protected AudioClip Sound;//Personal note : Remember Especifiying What is the purpose of this sound (OnParry, OnMovement, OnPlayerHit, OnEnviroment etc).
    protected SpriteRenderer _Color;
    public bool IsParryAble{ get {return _isParryAble;}set{_isParryAble=value; }}

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player!=null)
        {
            //Play AudioClip through SoundManager
            //Debug.Log("----HIT PLAYER----");
            player.TakeDamage(_damage);
            Destroy(gameObject);

        }
    }

}
