using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour {

    public GameObject[] objectsToSpawn;

    private void Start() {
        int rand = Random.Range(0, objectsToSpawn.Length);
        GameObject instance = Instantiate(objectsToSpawn[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
