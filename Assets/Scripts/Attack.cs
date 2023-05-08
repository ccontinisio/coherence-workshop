using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public InputActionReference attackAction;
    public Animator animator;

    private void OnEnable()
    {
        attackAction.action.Enable();
        attackAction.action.performed += OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("Attack");
    }

    private void OnDisable()
    {
        attackAction.action.Disable();
        attackAction.action.performed -= OnAttackPerformed;
    }
}
