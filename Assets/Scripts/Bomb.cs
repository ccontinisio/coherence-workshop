using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    public int timeToExplosion = 5;
    public Renderer redShell;

    public event UnityAction Exploded;

    private float _timeLeft;

    private void Awake()
    {
        _timeLeft = timeToExplosion;
    }

    private void OnEnable()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    private IEnumerator ExplosionCoroutine()
    {
        while (_timeLeft > .02f)
        {
            yield return new WaitForSeconds(_timeLeft);

            _timeLeft *= .7f;
            redShell.enabled = true;

            yield return new WaitForSeconds(Time.deltaTime * 2f);
            redShell.enabled = false;
        }

        Explode();
    }

    private void Explode()
    {
        //TODO: Show explosion
        transform.SetParent(null, true);
        Exploded?.Invoke();
    }

    private void OnDisable()
    {
        StopCoroutine(ExplosionCoroutine());
    }
}