using UnityEngine;

public class FlyingSkull : Enemy
{
    public int damage = 1;
    public TriggerDetection damageTrigger;
    
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
        damageTrigger.TriggerEntered += DealDamage;
        StartCoroutine(KeepLookingForPlayer());
    }

    private void DealDamage(GameObject player)
    {
        player.GetComponent<IHealth>().ChangeHealth(-damage);
    }

    private void FixedUpdate()
    {
        if (_playerTransform == null) return;
        
        Vector3 directionToPlayer = (_playerTransform.position + Vector3.up - transform.position).normalized;
        _rigidbody.velocity = directionToPlayer * speed;
        _rigidbody.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
    }

    private void OnDisable()
    {
        damageTrigger.TriggerEntered -= DealDamage;
        StopCoroutine(KeepLookingForPlayer());
    }
}
