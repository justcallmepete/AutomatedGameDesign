using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> Enemies;
    public List<float> percentage;
    public GameObject[] spawnPoints;
    int enemyCount;
    public int maxEnemy;

    float remaining;
    //weighted spawn change
    float standardSpawnChance;
    //used to create the spawn change.
    float ahold;
    float upRate = 5f;
    float downRate = 5f;
   
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        maxEnemy = PlayerPrefs.GetInt("MaxEnemy");
        if(maxEnemy == 0)
        {
            maxEnemy = 100;
        }
        remaining = maxEnemy;
        
        foreach (GameObject en in Enemies)
        {   en.GetComponent<Enemy>().BalanceSpawnRate(en.GetComponent<Enemy>().chanceOfSpawning);
            ahold = ahold + en.GetComponent<Enemy>().chanceOfSpawning / 100;
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
                    ChooseEnemy(spawnPoints[i].transform);
                    remaining--;
                    enemyCount++;
                }

            }
            
            i++;
        }
        Debug.Log(enemyCount);
    }
    void ChooseEnemy(Transform pos)
    {
        float a = Random.value;

        if (a < percentage[0])
        {
            Instantiate(Enemies[0], pos.position, Quaternion.identity);
           // Debug.Log("The enemy chance is 0");
        }
        else if (a < percentage[1])
        {
            Instantiate(Enemies[1], pos.position, Quaternion.identity);
           // Debug.Log("The enemy chance is 1");

        }
        else if (a < percentage[2])
        {
            Instantiate(Enemies[2], pos.position, Quaternion.identity);
            //Debug.Log("The enemy chance is 2");

        }
        else if (a < percentage[3])
        {
            Instantiate(Enemies[3], pos.position, Quaternion.identity);
           // Debug.Log("The enemy chance is 3");

        }
    }
    public void EnemySpawnRatesChanger(int ChangeRates)
    {
        float dividedRate = downRate/ (Enemies.Count-1);
        switch (ChangeRates)
        {
            // more basic enemies
            case 1:
                {
                    Mathf.Round(Enemies[0].GetComponent<Enemy>().chanceOfSpawning += upRate);
                    
                    Mathf.Round(Enemies[1].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    Mathf.Round(Enemies[2].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    Mathf.Round(Enemies[3].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    return;
                }
            // more slow enemies
            case 2:
                {
                    Mathf.Round(Enemies[0].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    Mathf.Round(Enemies[1].GetComponent<Enemy>().chanceOfSpawning += upRate);
                    Mathf.Round(Enemies[2].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    Mathf.Round(Enemies[3].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    return;
                }
            // more tank enemies
            case 3:
                {
                    Mathf.Round(Enemies[0].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    Mathf.Round(Enemies[1].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    Mathf.Round(Enemies[2].GetComponent<Enemy>().chanceOfSpawning += upRate);
                    Mathf.Round(Enemies[3].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    return;
                }
            // more faster enemies.
            case 4:
                {
                    Mathf.Round(Enemies[0].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    Mathf.Round(Enemies[1].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    Mathf.Round(Enemies[2].GetComponent<Enemy>().chanceOfSpawning -= dividedRate);
                    Mathf.Round(Enemies[3].GetComponent<Enemy>().chanceOfSpawning += upRate);
                    return;
                }

        }
    }


    
}
