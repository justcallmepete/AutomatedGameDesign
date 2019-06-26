using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {

	// open up the pathways
	//
	public bool spawnLeftEntrance;
	public bool spawnRightEntrance;
	public bool spawnBottomEntrance;
	public bool spawnTopEntrance;

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

	public void SpawnEntrances(){
		// fill entrance with block or not?
		if (!spawnLeftEntrance){
			entrances[0].SetActive(true);
			entrances[1].SetActive(true);
		}

		if (!spawnRightEntrance){
			entrances[2].SetActive(true);
			entrances[3].SetActive(true);
		}

		if(!spawnTopEntrance){
			entrances[4].SetActive(true);
			entrances[5].SetActive(true);
		}
		if(!spawnBottomEntrance){
			entrances[6].SetActive(true);
			entrances[7].SetActive(true);
		}
	}

	public void SpawnChunks(){
		// fill the room with all the random types of chunks
		GameObject tempChunk = null;
		foreach (var item in chunkSpawnPoints)
		{
			int chunkNumber = Random.Range(0, roomChunks.Count);
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
