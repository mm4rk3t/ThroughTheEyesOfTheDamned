using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerController player;
    private Vector2 direction;

    //Animation Parameters
    private string paramX = "X";
    private string paramY = "Y";
    private string IsMoving = "IsMoving";
    private void Start()
    {
        player = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        direction = player.MoveInput;
        if (direction.x == 0 && direction.y == 0)
        {
            animator.SetBool(IsMoving, false);
            animator.SetFloat(paramX, direction.x);
            animator.SetFloat(paramY, direction.y);
            return;
        }
        animator.SetBool(IsMoving, true);
        UpdateMovementAnimation(direction);
    }
    void UpdateMovementAnimation(Vector2 direction)
    {
        animator.SetFloat(paramX, direction.x);
        animator.SetFloat(paramY, direction.y);
    }

    public void DeathAnimation(bool value)
    {
        if (value == true)
        {
            Debug.Log("isDead animation:");
            animator.Play("Death");
            return;
        }
        animator.Play("Idle");
    }

    public void DisableAfterDeath()
    {
        animator.enabled = !animator.enabled;
    }


}
