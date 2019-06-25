using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy: MonoBehaviour {
 
//ublic Transform target;
public float speed;
public float damage;
public float HP;
public float chanceOfSpawning;

    public void BalanceSpawnRate(float spawnChangeOfEnemy)
    {
        if(chanceOfSpawning < 10)
        {
            chanceOfSpawning = 10;
        }
        else if (chanceOfSpawning > 70)
        {
            chanceOfSpawning = 70;

        }
    }
}