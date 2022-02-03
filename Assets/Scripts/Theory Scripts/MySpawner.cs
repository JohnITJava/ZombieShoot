using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    [SerializeField] Transform[] SpawnPoints;

    [SerializeField] float SpawnTime, DelaySpawn;
    
    List<GameObject> sp_Enemy = new List<GameObject>();
    bool[] HasSpawned; 

    private void Start()
    {
        InvokeRepeating("Spawn", DelaySpawn, SpawnTime);

        HasSpawned = new bool[SpawnPoints.Length];
        for (int i = 0; i < HasSpawned.Length; i++)
            HasSpawned[i] = false;
    }

    void Spawn()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            if (HasSpawned[i] == false) break;
            if (sp_Enemy.Count >= SpawnPoints.Length) return;
            else break;
        }

        int num = Random.Range(0, Enemies.Length);
        GameObject go = Enemies[num];
        
        num = Random.Range(0, SpawnPoints.Length);        
        Transform tr = SpawnPoints[num];

        HasSpawned[num] = true;

        sp_Enemy.Add(Instantiate(go, tr.position, tr.rotation));
    }
}
