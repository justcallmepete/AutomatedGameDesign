using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChunkGenerator : MonoBehaviour {

	// open up the pathways
	//
	public bool spawnLeftEntrance;
	public bool spawnRightEntrance;
	public bool spawnBottomEntrance;
	public bool spawnTopEntrance;

	public TypeOfRoom roomType;

	public int chanceOfOpenEntrance = 25;
	public int chanceofOpenNonCrit = 75;

	public List<GameObject> entrances = new List<GameObject>(); // 0,1 left - 2,3 right - 4,5 top - 6,7 bot
	public List<GameObject> chunkSpawnPoints = new List<GameObject>();
	public List<GameObject> roomChunks = new List<GameObject>();
	public List<GameObject> items = new List<GameObject>();

	void Awake() {
		spawnLeftEntrance = false;
		spawnRightEntrance = false;
		spawnTopEntrance = false;
		spawnBottomEntrance = false;
	}

	public void setEntrances(bool top, bool bot, bool right, bool left){
		spawnTopEntrance = top;
		spawnLeftEntrance = left;
		spawnRightEntrance = right;
		spawnBottomEntrance = bot;
	}

	public void SpawnEntrances(int regChance1, int regChance2, int regChance3, int regChance4){
		// fill entrance with block or not?
		if (spawnLeftEntrance){
			entrances[0].SetActive(false);
			entrances[1].SetActive(false);
		} else {
			if(chanceOfOpenEntrance > regChance1){
			entrances[0].SetActive(false);
			entrances[1].SetActive(false);
			}
		}

		if (spawnRightEntrance){
			entrances[2].SetActive(false);
			entrances[3].SetActive(false);
		}else {
			if(chanceOfOpenEntrance > regChance2){
			entrances[0].SetActive(false);
			entrances[1].SetActive(false);
			}
		}
		

		if(spawnTopEntrance){
			entrances[4].SetActive(false);
			entrances[5].SetActive(false);
		}else {
			if(chanceOfOpenEntrance > regChance3){
			entrances[0].SetActive(false);
			entrances[1].SetActive(false);
			}
		}
		if(spawnBottomEntrance){
			entrances[6].SetActive(false);
			entrances[7].SetActive(false);
		}else {
			if(chanceOfOpenEntrance > regChance4){
			entrances[0].SetActive(false);
			entrances[1].SetActive(false);
			}
		}
	}

	public void SpawnNonCritEntrances(int crit1, int crit2, int crit3, int crit4){
		if (chanceofOpenNonCrit > crit1){
			entrances[0].SetActive(false);
			entrances[1].SetActive(false);
		}

		if (chanceofOpenNonCrit > crit2){
			entrances[2].SetActive(false);
			entrances[3].SetActive(false);
		}

		if (chanceofOpenNonCrit > crit3){
			entrances[4].SetActive(false);
			entrances[5].SetActive(false);
		}

		if (chanceofOpenNonCrit > crit4){
			entrances[6].SetActive(false);
			entrances[7].SetActive(false);
		}
	}

	public void SpawnChunks(){
		// fill the room with all the random types of chunks
		GameObject tempChunk = null;
		System.Random rnd = new System.Random();
		foreach (var item in chunkSpawnPoints)
		{
			int chunkNumber = rnd.Next(0, roomChunks.Count);
			if(roomChunks.Count > 0){
			tempChunk = Instantiate(roomChunks[chunkNumber], item.transform);
			roomChunks.Remove(roomChunks[chunkNumber]);
			}
		}
	}

	public void SpawnItems(){
		// chance of spawning items in level??
	}
}
