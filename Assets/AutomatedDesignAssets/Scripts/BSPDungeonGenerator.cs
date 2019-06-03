using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BSPDungeonGenerator : MonoBehaviour {

	public int minRoomSize = 10;
	public int maxRoomSize = 40;
	public List<Leaf> subdungeons = new List<Leaf>();
	public int maxAmountOfRooms = 10;
	public int dungeonWidth = 50, dungeonHeight = 50;
	public GameObject prefab;

	private void Start() {
		Leaf test = new Leaf(0,0, dungeonWidth, dungeonHeight);
		subdungeons.Add(test);
		//CreateBSP(test);
		bool did_split = true;
			while(did_split){
				did_split = false;
				 foreach (Leaf leaf in subdungeons.ToList())
				{
					if(leaf.Split(minRoomSize, maxRoomSize)){
						subdungeons.Add(leaf.left);
						subdungeons.Add(leaf.right);
						did_split = true;
					}
				} 
			}
				CreateRooms(test);

	}

	private void CreateRooms(Leaf dungeon){
		foreach (var item in subdungeons)
		{
			if (item.IAmLeaf()){
				Debug.Log("item created");
				item.isLeaf = true;
			
			}
		}
		/* 
		if(dungeon.left != null || dungeon.right != null){
		if(dungeon.left!= null){
			CreateRooms(dungeon.left);
		}
		if(dungeon.right != null){
			CreateRooms(dungeon.right);
		}
		} else {
		GameObject obj = Instantiate(prefab);
		obj.transform.position = new Vector3(dungeon.positionX , dungeon.positionY, 0);
		obj.transform.localScale = new Vector3(dungeon.width, dungeon.height, 1);
		//prefab.transform.localScale = new Vector3(dungeon.height, dungeon.width, obj.transform.localScale.z);
		}
		*/
	}
	public void CreateBSP(Leaf subDungeon)
	{
		//Debug.Log("Splitting sub-dungeon " + subDungeon.debugId + ": " + subDungeon.rect);
		if (subDungeon.IAmLeaf())
		{
			/* if( subdungeons.Count <= maxAmountOfRooms){
				subDungeon.Split();
			} */
			// if the sub-dungeon is too large
			if (subDungeon.rect.width >  maxRoomSize || subDungeon.rect.height > maxRoomSize || Random.Range(0.0f, 1.0f) > 0.25)
			{
				Debug.Log("splitting yeah");
				if (subDungeon.Split(minRoomSize, maxRoomSize))
				{
/* 					Debug.Log("Splitted sub-dungeon " + subDungeon.debugId + " in "
					  + subDungeon.left.debugId + ": " + subDungeon.left.rect + ", "
					  + subDungeon.right.debugId + ": " + subDungeon.right.rect); */

					CreateBSP(subDungeon.left);
					CreateBSP(subDungeon.right);
				}
			}
		}
	}
}
