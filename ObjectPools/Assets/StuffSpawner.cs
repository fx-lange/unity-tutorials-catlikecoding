using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawner : MonoBehaviour {

    public Stuff[] stuffPrefabs;

    public float timeBetweenSpawns;
    float timeSinceLastSpawn = 0;

    void FixedUpdate()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if( timeSinceLastSpawn <= timeBetweenSpawns)
        {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnStuff();
        }
    }

    void SpawnStuff()
    {
        Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
        Stuff spawn = Instantiate<Stuff>(prefab);
        spawn.transform.localPosition = transform.localPosition;
    }
}
