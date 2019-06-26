using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour {

    //public GameObject[] enemiesToSpawn;
    GameObject manager;

    public List<GameObject> Baby = new List<GameObject>();
    public List<GameObject> Easy = new List<GameObject>();
    public List<GameObject> Medium = new List<GameObject>();
    public List<GameObject> Pain = new List<GameObject>();
    
    int maxEnemies; 
    int difficulty;


    private void Start() {

        
   /*      manager = GameObject.FindGameObjectWithTag("manager");
        float spawnrate = GetSpawnRate((int)transform.parent.position.y);
        if (Random.value < spawnrate && difficulty < maxEnemies) {
            int rand = Random.Range(0, enemiesToSpawn.Length);
            GameObject instance = Instantiate(enemiesToSpawn[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
            UpdateEnemyCounter(maxEnemies); */
        }
    
    public void spawnNewEnemy(EnemyDiff diff, Transform spawnPoint){
        switch (diff){
            case EnemyDiff.Baby:{
                int rnd = Random.Range(0,Baby.Count);
                GameObject enemy = Instantiate(Baby[rnd], spawnPoint.transform.position, Quaternion.identity);
                break;
            }
            case EnemyDiff.Easy:{
                int rnd = Random.Range(0,Easy.Count);
                GameObject enemy = Instantiate(Easy[rnd], spawnPoint.transform.position,Quaternion.identity);
                break;
            }
            case EnemyDiff.Medium:{
                int rnd = Random.Range(0,Medium.Count);
                GameObject enemy = Instantiate(Medium[rnd], spawnPoint.transform.position,Quaternion.identity);
                break;
            }
            case EnemyDiff.Pain:{
                int rnd = Random.Range(0,Pain.Count);
                GameObject enemy = Instantiate(Pain[rnd], spawnPoint.transform.position,Quaternion.identity);
                break;
            }
            
        }
    }

}
    
 /*    private float GetSpawnRate(int roomHeight) {
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
    } */

    /* private void UpdateEnemyCounter(int maxEnemies) {
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
 */
