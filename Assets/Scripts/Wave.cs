using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveX", menuName = "Wave Configuration")]
public class Wave : ScriptableObject
{
    public SubWave[] subwaves;
}

[Serializable]
public struct SubWave
{
    public int quantity;
    public GameObject prefab;
}
