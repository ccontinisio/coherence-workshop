using System;
using System.Collections;
using UnityEngine;

public class BombSkeleton : Enemy
{
    public Move moveController;
    public Bomb bombHead;
    public Animator animator;
    
    private bool _exploded;

    private void Start()
    {
        FindClosestPlayer();
    }
    
    private void OnEnable()
    {
        bombHead.Exploded += OnBombHeadExploded;
        StartCoroutine(KeepLookingForPlayer());
    }

    private void Update()
    {
        if (!_exploded
            && _playerTransform != null)
        {
            Vector3 dirToPlayer = (_playerTransform.position - transform.position).normalized;
            moveController.MoveInput = dirToPlayer;
        }
        else
        {
            moveController.MoveInput = Vector3.zero;
        }
    }

    private void OnBombHeadExploded()
    {
        _exploded = true;
        animator.SetTrigger("Exploded");
        StartCoroutine(Remove());
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(2f);
        
        Die();
    }

    private void OnDisable()
    {
        bombHead.Exploded -= OnBombHeadExploded;
        StopCoroutine(KeepLookingForPlayer());
    }
}
