using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public	enum TypeOfRoom
	{
		Start,
		End,
		Corridor,
		DropIn,
		DropOut,
		NonCrit
	}
[System.Serializable]
public class DungeonRoom {
		
	

	public DungeonRoom(Transform trans, int x, int y){
		roomTransform = trans;
		xPos = x;
		yPos = y;
	}

	public bool leftSide;
	public bool rightSide;
	public bool upSide;
	public bool downSide;
	
	public Transform roomTransform;
	public int xPos;
	public int yPos;
	public TypeOfRoom roomType;

	

	public void SpawnRoom(GameObject room){
		// Spawn the corresponding type
	//	Instantiate(room, roomTransform);
	}

	public void FillRoom(){

	}


}
