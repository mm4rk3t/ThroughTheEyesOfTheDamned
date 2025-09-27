using System.Diagnostics;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Boss bossScript;


    [SerializeField] private Vector2 _moveDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bossScript = GetComponent<Boss>();
        _moveDirection = bossScript.MoveDirection;
    }


    private void Update()
    {
        if (bossScript.IsMoving == true)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsShooting", true);
        }
    }
    


}
