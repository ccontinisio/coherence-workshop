using UnityEngine;

/// <summary>
/// Common functionality to all enemies
/// </summary>
public class Enemy : MonoBehaviour, IHealth
{
    public int health = 1;
    public float speed = 1f;
    
    protected Transform _playerTransform;
    
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