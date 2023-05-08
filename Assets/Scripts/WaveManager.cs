using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;

    private int _currentWave = -1;
    private int _currentSpawnPoint;

    public void Start()
    {
        BeginWave();
    }

    public void BeginWave()
    {
        _currentWave++;
        
        foreach (SubWave subwave in waves[_currentWave].subwaves)
        {
            for (int i = 0; i < subwave.quantity; i++)
            {
                Transform spawnPoint = spawnPoints[_currentSpawnPoint];
                GameObject newEnemy = Instantiate(subwave.prefab, spawnPoint.position, spawnPoint.rotation);
                
                _currentSpawnPoint = (_currentSpawnPoint + 1) % spawnPoints.Length;
            }
        }
    }
}
