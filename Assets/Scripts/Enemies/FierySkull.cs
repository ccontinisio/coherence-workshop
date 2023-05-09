using System.Collections;
using UnityEngine;

public class FierySkull : Enemy
{
    public float fireballInterval = 4f;
    public GameObject fireballPrefab;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        FindClosestPlayer();
    }

    private void OnEnable()
    {
        StartCoroutine(ShootFireball());
        StartCoroutine(KeepLookingForPlayer());
    }

    private void OnDisable()
    {
        StopCoroutine(ShootFireball());
        StopCoroutine(KeepLookingForPlayer());
    }

    private IEnumerator ShootFireball()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireballInterval);
            Instantiate(fireballPrefab, transform.position + transform.forward, transform.rotation);
        }
    }

    private void FixedUpdate()
    {
        if (_playerTransform == null) return;
        
        Vector3 directionToPlayer = (_playerTransform.position + Vector3.up - transform.position).normalized;
        _rigidbody.velocity = directionToPlayer * speed;
        _rigidbody.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
    }
}