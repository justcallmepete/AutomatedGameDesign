using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntranceExit : MonoBehaviour {

	bool shouldSpawn;
	
	private void Start() {
		if(shouldSpawn){
			Debug.Log("Entrance should be spawned");
		}
	}
}
