using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DDA
{
    

public class DifficultyManager : MonoBehaviour
{

    public int difficulty;
    public EnemySpawner Spawner;
    public float CurrentOverallTime;
    public float currentTimePerLevel;
    public Text text;
    public float currentTimeGoal;
    float timeDiff = 30;
    void Start()
    {
        currentTimeGoal =  PlayerPrefs.GetFloat("currentTimeGoal");
        CurrentOverallTime = PlayerPrefs.GetFloat("CurrentTime");
        difficulty = PlayerPrefs.GetInt("Difficulty");

    }

    // Update is called once per frame
    void Update()
    { 
                text = GameObject.Find("Time").GetComponent<Text>();

        if(text != null){
        DifficultyTimer(text); 
        currentTimePerLevel+= Time.deltaTime;
        }
    }
    void DifficultyTimer(Text text)
    {
        CurrentOverallTime += Time.deltaTime;
        
            text.text = ((int)CurrentOverallTime).ToString();
        if(CurrentOverallTime > currentTimeGoal)
        {
            currentTimeGoal += timeDiff;
            PlayerPrefs.SetFloat("currentTimeGoal",currentTimeGoal);
            if (difficulty < 6)
            {
                difficulty++;
                DifficultySlider();
            }
        }

    }
    void DifficultySlider()
    {
        switch (difficulty)
        {
             case 1:
                Spawner.GetComponent<EnemySpawner>().maxEnemy += 10;
                return;
            case 2:
                Spawner.GetComponent<EnemySpawner>().maxEnemy += 10;
                return;
            case 3:
                Spawner.GetComponent<EnemySpawner>().maxEnemy += 10;
                return;
            case 4:
                Spawner.GetComponent<EnemySpawner>().maxEnemy += 10;
                return;
            case 5:
                Spawner.GetComponent<EnemySpawner>().maxEnemy += 10;
                return;
            case 6:
                Spawner.GetComponent<EnemySpawner>().maxEnemy += 10;
                return;

        }
    }
    public void changeSpawnChancesOfEnemy()
    {
        if(currentTimePerLevel < PlayerPrefs.GetFloat("currentFloatTimePerLevel") && PlayerPrefs.GetFloat("currentFloatTimePerLevel") != 0)
        {
            //balance by spawning in enemies that slow the player
            Spawner.EnemySpawnRatesChanger(2);
                Debug.Log("OneFastBoi");
        }
        else if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().currentHp < PlayerPrefs.GetInt("CurrentHP"))
        {
                //balance by spawning enemies that are tankier but little bit slower
                Spawner.EnemySpawnRatesChanger(3);
                Debug.Log("auwie boy");
        }
        else  if(currentTimePerLevel > PlayerPrefs.GetFloat("currentFloatTimePerLevel") && PlayerPrefs.GetFloat("currentFloatTimePerLevel") != 0)
        {
            //balance by spawning in more aggresive enemies that are faster and deal more damage
                Spawner.EnemySpawnRatesChanger(4);
                Debug.Log("SlowBoi");
        }
        else{
            Spawner.EnemySpawnRatesChanger(1); 
            Debug.Log("What a neutral run >_>");
    }
    }


}
}