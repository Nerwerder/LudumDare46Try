using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSpawner : MonoBehaviour
{
    public Transform insectParent = null;
    public float minSpawnDistance = 0f;
    public float maxSpawnDistance = 0f;

    /// <summary>
    /// Every X seconds a Insect Spawns
    /// </summary>
    public float averageSpawnTime = 0f;

    /// <summary>
    /// Varianc for the SpawnTime every spawnTime +- [minSpawnTimeVariance, spawnTimeVariance] a Insect Spawns
    /// </summary>
    public float minSpawnTimeVariance = 0f;

    /// <summary>
    /// Varianc for the SpawnTime every spawnTime +- [minSpawnTimeVariance, maxSpawnTimeVariance] a Insect Spawns
    /// </summary>
    public float maxSpawnTimeVariance = 0f;

    /// <summary>
    /// Every Inect decreases the averageSpawnTime by XXX %
    /// </summary>
    public float spawnSpeedup = 0f;

    /// <summary>
    /// List of different Insects to spawn
    /// </summary>
    public List<Insect> insects;

    private float timer = 0f;
    private float nextSpawn = 0f;
    void Start() {
        nextSpawn = CalculateNextSpawnTime();
    }

    void Update() {
        timer += Time.deltaTime;
        if(timer >= nextSpawn) {
            SpawnInsect();
            nextSpawn = CalculateNextSpawnTime();
            timer = 0f;
        }
    }

    float CalculateNextSpawnTime() {
        float variance = Random.Range(0f, maxSpawnTimeVariance);
        float time = (Random.value > 0.5f) ? (averageSpawnTime - variance) : (averageSpawnTime + variance);
        if(time <= 0) {
            time = 0.1f;
        }
        return time;
    }

    void SpawnInsect() {
        //Which Insect so Spawn
        Debug.Assert(insects.Count >= 1);
        int insectIndex = Random.Range(0, (insects.Count-1));
        Insect insect = insects[insectIndex];

        //Figure out where to Spawn it
        Vector3 spawnPoint = RandomPointOnCircleEdge(Random.Range(minSpawnDistance, maxSpawnDistance));

        //Spawn the insect
        Instantiate(insect, spawnPoint, Quaternion.identity, insectParent);

        averageSpawnTime /= spawnSpeedup;
        minSpawnTimeVariance /= spawnSpeedup;
        maxSpawnTimeVariance /= spawnSpeedup;
    }

    private Vector3 RandomPointOnCircleEdge(float radius) {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        return new Vector3(vector2.x, 0, vector2.y);
    }
}