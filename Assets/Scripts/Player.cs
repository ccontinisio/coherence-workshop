using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour, IHealth
{
    public int health = 10;
    public int damage = 2;
    public TriggerDetection weaponTrigger;
    public CinemachineImpulseSource cinemachineImpulseSource;
    
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
        health += healthChange;
        health = Mathf.Clamp(health, 0, _maxHealth);
        Vector2 randomImpulse = Random.insideUnitCircle.normalized * Random.Range(.2f, .4f);
        cinemachineImpulseSource.GenerateImpulseWithVelocity(new Vector3(randomImpulse.x, 0f, randomImpulse.y));
        
        if (health == 0)
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