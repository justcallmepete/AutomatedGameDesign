using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public EnemySpawner spawn;
    public DifficultyManager difman;
    public GameObject player;
    private void Start() {
}

  
	
	// Update is called once per frame
	void Update () {
            player = GameObject.FindGameObjectWithTag("Player");

        if (Input.GetKey(KeyCode.R))
        {
            GenerateNewLevel();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Reset();
        }

    }
    public void GenerateNewLevel()
    {
        difman.changeSpawnChancesOfEnemy();

        PlayerPrefs.SetFloat("CurrentTime", difman.CurrentOverallTime);

        PlayerPrefs.SetFloat("currentFloatTimePerLevel", difman.currentTimePerLevel);

        PlayerPrefs.SetInt("CurrentHP", player.GetComponent<Player>().currentHp);
        PlayerPrefs.SetInt("MaxEnemy", spawn.maxEnemy);
        PlayerPrefs.SetInt("Difficulty", difman.difficulty);
    
        Application.LoadLevel(Application.loadedLevel);

    }
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("CurrentHP", 100);
        Application.LoadLevel(Application.loadedLevel);
       

    }
}