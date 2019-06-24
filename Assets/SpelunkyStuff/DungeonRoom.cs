using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom {
		
	public	enum TypeOfRoom
	{
		StartEnd,
		Corridor,
		DropIn,
		DropOut,
		NonCrit
	}
	
	public Transform roomTransform;
	public TypeOfRoom roomType;

	public DungeonRoom(Transform trans){
		roomTransform = trans;
	}

	public void SpawnRoom(GameObject room){
		// Spawn the corresponding type
	//	Instantiate(room, roomTransform);
	}

	public void FillRoom(){

	}


}
