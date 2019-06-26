using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChanceHolder : MonoBehaviour {

	public float[] SpawnChances;
	void Start () {
		
	}
	
	// Update is called once per frame
	 public void BalanceSpawnRate()
    {

		for (int i = 0; i < SpawnChances.Length; i++)
		{
			if(SpawnChances[i] > 70){
				SpawnChances[i] = 70;
			}
			else if(SpawnChances[i] <10){
				SpawnChances[i] = 10;
			}
		}
}
}

