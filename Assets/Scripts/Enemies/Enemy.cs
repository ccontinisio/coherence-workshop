using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Common functionality to all enemies
/// </summary>
public class Enemy : MonoBehaviour, IHealth
{
    public int health = 1;
    public float speed = 1f;
    public GameObject damageSparksPrefab;

    public event UnityAction<IHealth> Dead;
    
    protected Transform _playerTransform;
    protected float _lookForPlayerInterval = 1.0f;
    
    /// <summary>
    /// Continuously looks for the closest player by calling <see cref="FindClosestPlayer"/>,
    /// until manually stopped with StopCoroutine.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator KeepLookingForPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(_lookForPlayerInterval);
            FindClosestPlayer();
        }
    }
    
    /// <summary>
    /// Gets all the existing player characters, and finds the one that is closest.
    /// </summary>
    protected void FindClosestPlayer()
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

    /// <summary>
    /// Adds or subtracts a quantity of health from the existing health.
    /// If health goes to zero, invokes <see cref="Die"/>.
    /// </summary>
    /// <param name="healthChange"></param>
    public void ChangeHealth(int healthChange)
    {
        Instantiate(damageSparksPrefab, transform.position, Quaternion.identity);
        health += healthChange;
        
        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Destroys the object when health reached zero.
    /// Override to implement unique custom behaviour.
    /// </summary>
    protected virtual void Die()
    {
        Dead?.Invoke(this);
        Destroy(gameObject);
    }
}