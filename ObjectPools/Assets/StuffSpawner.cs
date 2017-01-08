using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawner : MonoBehaviour {

    public Stuff[] stuffPrefabs;

    public FloatRange timeBetweenSpawns, scale, 
        randomVelocity, randomAngularVelocity;
    float timeSinceLastSpawn = 0;
    float currentSpawnDelay = 0;

    public float velocity;

    public Material stuffMaterial;

    void FixedUpdate()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if( timeSinceLastSpawn >= currentSpawnDelay)
        {
            timeSinceLastSpawn -= currentSpawnDelay;
            currentSpawnDelay = timeBetweenSpawns.RandomInRange;
            SpawnStuff();
        }
    }

    void SpawnStuff()
    {
        Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
        //Stuff stuff = Instantiate<Stuff>(prefab);
        Stuff stuff = prefab.Create<Stuff>();
        stuff.transform.localPosition = transform.position;
        stuff.transform.localScale = Vector3.one * scale.RandomInRange;
        stuff.transform.localRotation = Random.rotation;

        stuff.Body.velocity = transform.up * velocity
            + Random.onUnitSphere * randomVelocity.RandomInRange;

        stuff.Body.angularVelocity = Random.onUnitSphere 
            * randomAngularVelocity.RandomInRange;

        stuff.SetMaterial(stuffMaterial);
    }
}
