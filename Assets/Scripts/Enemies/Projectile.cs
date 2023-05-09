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
        StartCoroutine(Remove());
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IHealth>().ChangeHealth(damage);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopCoroutine(Remove());
    }
}
