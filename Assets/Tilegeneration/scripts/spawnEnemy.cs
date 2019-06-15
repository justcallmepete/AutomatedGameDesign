using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour {

    public GameObject[] enemiesToSpawn;
    GameObject manager;

    int maxEnemies;
    int difficulty;


    private void Start() {
        manager = GameObject.FindGameObjectWithTag("manager");
        float spawnrate = GetSpawnRate((int)transform.parent.position.y);
        if (Random.value < spawnrate && difficulty < maxEnemies) {
            int rand = Random.Range(0, enemiesToSpawn.Length);
            GameObject instance = Instantiate(enemiesToSpawn[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
            UpdateEnemyCounter(maxEnemies);
        }
    }

    private float GetSpawnRate(int roomHeight) {
        switch (roomHeight) {
            case 15:
                maxEnemies = 3;
                difficulty = manager.GetComponent<Manager>().easyEnemies;
                return 0.2f;
            case 5:
                maxEnemies = 7;
                difficulty = manager.GetComponent<Manager>().mediumEnemies;
                return 0.4f;
            case -5:
                maxEnemies = 12;
                difficulty = manager.GetComponent<Manager>().intermediateEnemies;
                return 0.6f;
            case -15:
                maxEnemies = 16;
                difficulty = manager.GetComponent<Manager>().hardEnemies;
                return 0.8f;
            default:
                maxEnemies = 0;
                return 0;
        }
    }

    private void UpdateEnemyCounter(int maxEnemies) {
        switch (maxEnemies) {
            case 2:
                manager.GetComponent<Manager>().easyEnemies++; ;
                break;
            case 4:
               manager.GetComponent<Manager>().mediumEnemies++;
                break;
            case 6:
                manager.GetComponent<Manager>().intermediateEnemies++;
                break;
            case 8:
               manager.GetComponent<Manager>().hardEnemies++;
                break;
            default:               
                break;
        }
    }
}
