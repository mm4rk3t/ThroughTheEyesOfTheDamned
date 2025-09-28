using UnityEngine;
using System.Collections;

public class EnemyAnimatorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Boss bossScript;

    private Vector2 lastDirection;
    private const float directionThreshold = 0.1f;

    [Header("Animation Parameters")]
    [SerializeField] private string directionX= "X";
    [SerializeField] private string directionY= "Y";
    [SerializeField] private string isMovingParam = "IsMoving";

    private void Start()
    {
        animator = GetComponent<Animator>();
        bossScript = GetComponent<Boss>();
    }
    

    private void Update()
    {
        if (animator == null || bossScript == null) return;
        UpdateAnimation();
    }
    
    private void UpdateAnimation()
    {
        Vector2 currentDirection = bossScript.MoveDirection;

        bool isMoving = bossScript.IsMoving;
        animator.SetBool(isMovingParam, isMoving);
        if (isMoving == false)
        {
            animator.SetFloat(directionX, 0f);
            animator.SetFloat(directionY, 0f);
            Debug.Log("Not Moving");
            return;
        }
        if (Vector2.Distance(currentDirection, lastDirection) > directionThreshold)
        {
            SetAnimationParameters(currentDirection);
            lastDirection = currentDirection;
        }

    }

    private void SetAnimationParameters(Vector2 direction)
    {
        
        //Determina la direccion predominante
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            //moviento Horizontal predominante
            if (direction.x>0)
            {
                //Derecha
                animator.SetFloat(directionX,1f);
                animator.SetFloat(directionY,0f);
                Debug.Log("Animation right");
            }
            else
            {
                animator.SetFloat(directionX, -1f);
                animator.SetFloat(directionY, 0f);
                Debug.Log("Animation Left");
            }
        }
        else
        {
            //Movimiento Vertical predominante

            if (direction.y>0)
            {
                //Arriba
                animator.SetFloat(directionX, 0f);
                animator.SetFloat(directionY, 1f);
                Debug.Log("Animation Up");
            }
            else
            {
                //Abajo
                animator.SetFloat(directionX, 0f);
                animator.SetFloat(directionY, -1f);
                Debug.Log("Animation Up");
            }
        }
        
        HandleDiagonalDirection(direction);
        
    }

    private void HandleDiagonalDirection(Vector2 direction)
    {
        const float diagonalThreshold = 0.7f;//Para detectar direcciones diagonales

        //Top Right
        if (direction.x > diagonalThreshold && direction.y > diagonalThreshold)
        {
            animator.SetFloat(directionX, 1f);
            animator.SetFloat(directionY, 1f);
            Debug.Log("Animation: Top Right");
        }
        else if (direction.x < -diagonalThreshold && direction.y > diagonalThreshold) //Top Left
        {
            animator.SetFloat(directionX, -1f);
            animator.SetFloat(directionY, 1f);
            Debug.Log("Animation: Top Right");
        }
        else if (direction.x < -diagonalThreshold && direction.y < -diagonalThreshold)//Bottom Left
        {
            animator.SetFloat(directionX, -1f);
            animator.SetFloat(directionY, -1f);
            Debug.Log("Animation: Bottom Left");
        }
    }

    
}
