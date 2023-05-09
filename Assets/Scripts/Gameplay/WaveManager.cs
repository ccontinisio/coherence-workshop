using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Spawns enemies in waves. When an enemy dies (and invokes the <see cref="IHealth.Dead"/> event,
/// it checks whether all enemies are gone. When so, the next wave begins.
/// </summary>
public class WaveManager : MonoBehaviour
{
    public int waveInterval = 2;
    public WaveSO[] waves;
    public Transform[] spawnPoints;
    
    public event UnityAction<int> PointsScored;

    private int _currentWave = -1;
    private int _currentSpawnPoint;
    private int _remainingEnemies; // Used to track remaining enemies alive

    public void Start()
    {
        BeginWave();
    }

    public void BeginWave()
    {
        _currentWave++;
        _remainingEnemies = 0;
        
        Debug.Log($"Beginning wave {_currentWave}!");
        
        foreach (SubWave subwave in waves[_currentWave].subwaves)
        {
            for (int i = 0; i < subwave.quantity; i++)
            {
                Transform spawnPoint = spawnPoints[_currentSpawnPoint];
                GameObject newEnemy = Instantiate(subwave.prefab, spawnPoint.position, spawnPoint.rotation);
                newEnemy.GetComponent<IHealth>().Dead += OnEnemyDead;
                _remainingEnemies++;
                
                _currentSpawnPoint = (_currentSpawnPoint + 1) % spawnPoints.Length;
            }
        }
    }

    private void OnEnemyDead(IHealth enemy)
    {
        _remainingEnemies--;
        PointsScored?.Invoke(1); //The ScoreUI will be watching this
        enemy.Dead -= OnEnemyDead;
        
        if (_remainingEnemies == 0)
        {
            WaveOver();
        }
    }

    private void WaveOver()
    {
        Debug.Log($"Wave {_currentWave} over.");
        
        if (_currentWave + 1 < waves.Length)
        {
            StartCoroutine(PrepareNextWave());
        }
        else
        {
            //TODO
            Debug.Log("All waves ended!");
        }
    }

    private IEnumerator PrepareNextWave()
    {
        yield return new WaitForSeconds(waveInterval);
        
        BeginWave();
    }
}
