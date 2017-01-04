using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawner : MonoBehaviour {

    public Stuff[] stuffPrefabs;

    public float timeBetweenSpawns;
    float timeSinceLastSpawn = 0;

    public float velocity;

    void FixedUpdate()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if( timeSinceLastSpawn >= timeBetweenSpawns)
        {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnStuff();
        }
    }

    void SpawnStuff()
    {
        Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
        Stuff stuff = Instantiate<Stuff>(prefab);
        stuff.transform.localPosition = transform.position;
        stuff.Body.velocity = transform.up * velocity;
    }
}
