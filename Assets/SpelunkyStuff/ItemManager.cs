using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour {

	public List<GameObject> itemPrefabs = new List<GameObject>();
	public int chanceOfSpawning = 75;
	public List<GameObject> itemspawnpoints = new List<GameObject>();

	void OnEnable() {
		SpelunkyLevelGen.doneGenerating += Placeitems;
	}

	void OnDisable() {
		SpelunkyLevelGen.doneGenerating -= Placeitems;
	}

	

	void Placeitems(){
		var tt = GameObject.FindGameObjectsWithTag("Item");
		itemspawnpoints = tt.OfType<GameObject>().ToList();
		System.Random rnd = new System.Random();
		foreach(var item in itemspawnpoints){
			int value = rnd.Next(0,100);
			if(chanceOfSpawning > value){
				//Debug.Log("Spawning item: " + itemPrefabs[rnd.Next(0,itemPrefabs.Count)]);
				GameObject lmao = Instantiate(itemPrefabs[rnd.Next(0,itemPrefabs.Count)], item.transform);
				chanceOfSpawning -= rnd.Next(5,20);
			} else {
				chanceOfSpawning += rnd.Next(1,8);
			}
		}
	}
}
