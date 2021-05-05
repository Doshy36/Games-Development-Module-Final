using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round", menuName = "Round", order = 1)]
public class Round : ScriptableObject
{

    public SpawnData[] data;

}

[System.Serializable]
public class SpawnData 
{
    public int enemies;
    public float spawnRate;
    public int[] levels;
    public float pauseTime;
}