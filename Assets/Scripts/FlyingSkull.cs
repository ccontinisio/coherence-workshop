using UnityEngine;

public class FlyingSkull : MonoBehaviour, IHealth
{
    public int health = 1;
    public float speed = 1f;
    public int damage = 1;
    public TriggerDetection damageTrigger;
    
    private Transform _playerTransform;
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
    }

    private void OnDisable()
    {
        damageTrigger.TriggerEntered -= DealDamage;
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

    private void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            _playerTransform = players[0].transform;
            float closestDistance = Mathf.Infinity;
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _playerTransform = player.transform;
                }
            }
        }
        else
            _playerTransform = null;
    }
    
    public void ChangeHealth(int healthChange)
    {
        health += healthChange;
        
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //TODO
        Destroy(gameObject);
    }
}
