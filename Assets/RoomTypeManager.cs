using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTypeManager : MonoBehaviour {
	//just a holder for all the room types
	public GameObject startRoom;
	public GameObject endRoom;
	public List<GameObject> corridorRooms = new List<GameObject>();
	public List<GameObject> dropInRooms = new List<GameObject>();
	public List<GameObject> dropThroughRooms = new List<GameObject>();
	public List<GameObject> nonCriticalRooms = new List<GameObject>();

	int shopChanceint = 0;

	public GameObject GetRoom(TypeOfRoom type){
		switch(type){
			case TypeOfRoom.Start:{
				return startRoom;
			}
			case TypeOfRoom.Corridor:{
				int randomNumber = Random.Range(0, corridorRooms.Count-1);
				return corridorRooms[randomNumber];
				
			}
			case TypeOfRoom.DropIn:{
				int randomNumber = Random.Range(0, dropInRooms.Count-1);
				return dropInRooms[randomNumber];
			}
			case TypeOfRoom.DropOut:{
				int randomNumber = Random.Range(0, dropThroughRooms.Count-1);
				return dropThroughRooms[randomNumber];
			}
			case TypeOfRoom.End:{
				return endRoom;
			}
			case TypeOfRoom.NonCrit:{
				int randomNumber = Random.Range(shopChanceint, nonCriticalRooms.Count-1);
				if (randomNumber == 0){
					shopChanceint = 1;
					return nonCriticalRooms[0];
				}else {
				return nonCriticalRooms[randomNumber];
				}
			}		
		}
		return null;
	}
}
