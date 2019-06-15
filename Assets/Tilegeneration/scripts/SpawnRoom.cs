using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour {

    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;
    public float spawnRate;
    public GameObject[] closedRooms;

    void Update() {

        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if (roomDetection == null && levelGen.stopGeneration == true) {
            int rand = Random.Range(0, closedRooms.Length);
            Instantiate(closedRooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
