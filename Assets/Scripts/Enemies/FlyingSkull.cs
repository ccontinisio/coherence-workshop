using UnityEngine;

public class FlyingSkull : Enemy
{
    public int damage = 1;
    public TriggerDetection damageTrigger;
    
    private Rigidbody _rigidbody;
    private float _interval = 1.0f;
    private float _timer;

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

    private void OnDisable()
    {
        damageTrigger.TriggerEntered -= DealDamage;
        StopCoroutine(KeepLookingForPlayer());
    }

    private void DealDamage(GameObject player)
    {
        player.GetComponent<IHealth>().ChangeHealth(-damage);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _interval)
        {
            FindClosestPlayer();
            _timer = 0.0f;
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
