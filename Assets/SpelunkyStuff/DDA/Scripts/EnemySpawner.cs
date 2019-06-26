using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyDiff
{
    Baby,
    Easy,
    Medium,
    Pain
}
public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> Enemies;
    public List<float> percentage;
    public GameObject[] spawnPoints;
    public GameObject spawnChanceGO;
    int enemyCount;
    public int maxEnemy;

    float remaining;
    //weighted spawn change
    float standardSpawnChance;
    //used to create the spawn change.
    float ahold;
    float upRate = 5f;
    float downRate = 5f;

    private void OnEnable() {
        SpelunkyLevelGen.doneGenerating += firstSpawn;
    }

    private void OnDisable() {
          SpelunkyLevelGen.doneGenerating -= firstSpawn;
    }

    void firstSpawn(){
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        maxEnemy = PlayerPrefs.GetInt("MaxEnemy");
        if(maxEnemy == 0)
        {
            maxEnemy = 20;
        }
        remaining = maxEnemy;
        
        foreach (float perc in spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances)
        {   spawnChanceGO.GetComponent<SpawnChanceHolder>().BalanceSpawnRate();
            ahold = ahold + perc / 100;
            percentage.Add(ahold);
        }
        SpawnEnemy();
    }
    void SpawnEnemy()
    {
        int i = 0;
        while (i < spawnPoints.Length)
        {
            {
                standardSpawnChance = remaining / maxEnemy;

                if (Random.value < standardSpawnChance)
                {
                    //enemy has been spawned decrease the chance of spawning the next one.
                    ChooseEnemy(spawnPoints[i].transform,spawnPoints[i].GetComponent<spawnEnemy>());
                    remaining--;
                    enemyCount++;
                }
            }
            
            i++;
        }
        Debug.Log(enemyCount);
    }
    void ChooseEnemy(Transform pos,spawnEnemy enemyspawnerScript)
    {
        float a = Random.value;

        if (a < percentage[0])
        {
            enemyspawnerScript.spawnNewEnemy(EnemyDiff.Baby,pos);
           // Debug.Log("The enemy chance is 0");
        }
        else if (a < percentage[1])
        {
            enemyspawnerScript.spawnNewEnemy(EnemyDiff.Easy,pos);
           // Debug.Log("The enemy chance is 1");

        }
        else if (a < percentage[2])
        {
            enemyspawnerScript.spawnNewEnemy(EnemyDiff.Medium,pos);
            //Debug.Log("The enemy chance is 2");

        }
        else if (a < percentage[3])
        {
            enemyspawnerScript.spawnNewEnemy(EnemyDiff.Pain,pos);
           // Debug.Log("The enemy chance is 3");

        }
    }
    
    public void EnemySpawnRatesChanger(int ChangeRates)
    {
        float dividedRate = downRate/ (spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances.Length-1);
        switch (ChangeRates)
        {
            // baby
            case 1:
                {
               spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[0] += upRate;
                spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[1] -= dividedRate;
                spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[2] -= dividedRate;
                spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[3] -= dividedRate;
                    return;
                }
//easy
            case 2:
                {
                    Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[0] -= dividedRate);
                    Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[1] += upRate);
                    Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[2] -= dividedRate);
                    Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[3] -= dividedRate);
                    return;
                }
//medium
            case 3:
                {

                    Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[0] -= dividedRate);
                    Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[1] -= dividedRate);
                    Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[2] += upRate);
                   Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[3] -= dividedRate);
                    return;
                }
            // pain
            case 4:
                {
                    Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[0] -= dividedRate);
                    Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[1] -= dividedRate);
                     Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[2] -= dividedRate);
                    Mathf.Round(spawnChanceGO.GetComponent<SpawnChanceHolder>().SpawnChances[3] += upRate);
                    return;
                }

        }
    }


    
}
