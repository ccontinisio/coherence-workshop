using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 4;

    private int _lifetime = 10;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    private void OnEnable()
    {
        StartCoroutine(Remove());
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IHealth healthComp)) healthComp.ChangeHealth(-damage);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        StopCoroutine(Remove());
    }
}
