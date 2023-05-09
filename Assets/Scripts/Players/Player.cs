using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IHealth
{
    public int health = 10;
    public int damage = 2;
    public TriggerDetection weaponTrigger;
    public CinemachineImpulseSource cinemachineImpulseSource;
    public GameObject damageSparksPrefab;
    
    public event UnityAction<IHealth> Dead;
    
    private int _maxHealth;

    private void Awake()
    {
        _maxHealth = health;
    }

    private void OnEnable()
    {
        weaponTrigger.TriggerEntered += DealDamage;
    }

    private void OnDisable()
    {
        weaponTrigger.TriggerEntered -= DealDamage;
    }

    private void DealDamage(GameObject enemy)
    {
        enemy.GetComponent<IHealth>().ChangeHealth(-damage);
    }

    public void ChangeHealth(int healthChange)
    {
        Instantiate(damageSparksPrefab, transform.position, Quaternion.identity);
        health += healthChange;
        health = Mathf.Clamp(health, 0, _maxHealth);

        if (healthChange < 0)
        {
            // Screen shake
            Vector2 randomImpulse = Random.insideUnitCircle.normalized * Random.Range(.2f, .4f);
            cinemachineImpulseSource.GenerateImpulseWithVelocity(new Vector3(randomImpulse.x, 0f, randomImpulse.y));
        }
        
        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Dead?.Invoke(this);
        Destroy(gameObject);
    }
}