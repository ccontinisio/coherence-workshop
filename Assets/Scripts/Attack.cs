using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public InputActionReference attackAction;
    public Animator animator;
    public Collider attackTrigger;
    public Move moveComponent;

    private bool _canAttack;

    private void Awake()
    {
        attackTrigger.enabled = false;
        _canAttack = true;
    }

    private void OnEnable()
    {
        attackAction.action.Enable();
        attackAction.action.performed += OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        if (_canAttack)
            DoAttack();
    }

    private void DoAttack()
    {
        _canAttack = false;
        animator.SetTrigger("Attack");
        moveComponent.SetFrozen(true);
        
        // Animation Events (in the AnimationClip) will do the rest
    }
    
    public void Begin()
    {
        attackTrigger.enabled = true;
    }

    public void End()
    {
        moveComponent.SetFrozen(false);
        attackTrigger.enabled = false;
        _canAttack = true;
    }
    
    private void OnDisable()
    {
        attackAction.action.Disable();
        attackAction.action.performed -= OnAttackPerformed;
    }
}
