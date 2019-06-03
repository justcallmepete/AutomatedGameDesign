using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public float tekentijd = .5f;
	public Dungeon testDungeon;
	public GameObject floorTile, wallTile, corridorTile;

	public int dungeonWidth, dungeonHeight;
	public int maxRoomSize, minRoomSize;
	public int maxAmountOfRooms = 10;

	public List<Dungeon> subdungeons = new List<Dungeon>();
    private GameObject[,] boardPositionsFloor;

	
	private void Start() {
		StartCoroutine(MaakLevel());
	}

	IEnumerator MaakLevel(){
		// maak dungeon
		testDungeon = new Dungeon(new Rect(0,0,dungeonWidth, dungeonHeight));

		yield return new WaitForSeconds(tekentijd);
		CreateBSP(testDungeon);
		// teken rooms
		boardPositionsFloor = new GameObject[dungeonWidth, dungeonHeight];
		testDungeon.CreateRooms();
		yield return new WaitForSeconds(tekentijd);
		
		DrawRooms(testDungeon);
		DrawCorridors(testDungeon);


		// maak corridor

	}

		public void CreateBSP(Dungeon subDungeon)
	{
		Debug.Log("Splitting sub-dungeon " + subDungeon.debugId + ": " + subDungeon.rect);
		if (subDungeon.IAmLeaf())
		{
			// if the sub-dungeon is too large
			if (subDungeon.rect.width > maxRoomSize || subDungeon.rect.height > maxRoomSize   || Random.Range(0.0f, 1.0f) > 0.25)
			{
				if (subDungeon.Split(minRoomSize, maxRoomSize))
				{
					CreateBSP(subDungeon.left);
					CreateBSP(subDungeon.right);
				}
			}
		}
	}

// create the rooms with this function
	public void CreatePremadeRooms(Dungeon subDungeon){
		if(subDungeon.IAmLeaf()){
			// gebruik dit 
			var iets = subDungeon.room;
		} else {
			CreatePremadeRooms(subDungeon.left);
			CreatePremadeRooms(subDungeon.right);
		}
	}

	public void DrawRooms(Dungeon subDungeon)
	{
		if (subDungeon == null)
		{
			Debug.LogError("No dungeon found");
			return;
		}

	    if (subDungeon.IAmLeaf())
	    {
	        subdungeons.Add(subDungeon);
	        for (int i = (int) subDungeon.room.min.x; i < subDungeon.room.xMax; i++)
	        {
	            for (int j = (int) subDungeon.room.min.y; j < subDungeon.room.yMax; j++)
	            {
	                if (boardPositionsFloor[i, j] == null)
	                {
	                    GameObject instance =
	                        Instantiate(floorTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
	                    instance.transform.SetParent(transform);
	                    boardPositionsFloor[i, j] = instance;
	                }

	                // subDungeon.corridors.Remove(Rect[i,j]);
	            }
	        }
				
            for (int i = (int)subDungeon.room.x; i < subDungeon.room.xMax; i++)
            {
                int test = (int)subDungeon.room.yMin;
                    GameObject wall =
                        Instantiate(wallTile, new Vector3(i, test, 0f), Quaternion.identity) as GameObject;
                    wall.transform.SetParent(transform);
                    boardPositionsFloor[i, test] = wall;
            }

            for (int i = (int)subDungeon.room.x; i <= subDungeon.room.xMax; i++)
            {
                int test = (int)subDungeon.room.yMax;
                    GameObject wall =
                        Instantiate(wallTile, new Vector3(i, test, 0f), Quaternion.identity) as GameObject;
                    wall.transform.SetParent(transform);
                    boardPositionsFloor[i, test] = wall;
            }

            for (int i = (int)subDungeon.room.y; i < subDungeon.room.yMax; i++)
            {
                int test = (int)subDungeon.room.xMin;
                GameObject wall = Instantiate(wallTile, new Vector3(test, i, 0f), Quaternion.identity) as GameObject;
                wall.transform.SetParent(transform);
                boardPositionsFloor[test, i] = wall;
            }

            for (int i = (int)subDungeon.room.y; i < subDungeon.room.yMax; i++)
            {
                int test = (int)subDungeon.room.xMax;
                GameObject wall = Instantiate(wallTile, new Vector3(test, i, 0f), Quaternion.identity) as GameObject;
                wall.transform.SetParent(transform);
                boardPositionsFloor[test, i] = wall;
            }

            // turn wall part that collides with corridor back into a normal ground tile
            for (int i = (int) subDungeon.room.xMin -1; i <= subDungeon.room.xMax; i++)
            {
                for (int j = (int) subDungeon.room.yMin -1; j <= subDungeon.room.yMax; j++)
                {
                    if (boardPositionsFloor[i, j] != null && boardPositionsFloor[i, j].tag == "Corridor")
                    {
                           Debug.LogError("FOUND ONE");
                        GameObject instance = Instantiate(floorTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
						//subDungeon.tilesInDungeon.Add(instance);
                        instance.transform.SetParent(transform);
                        boardPositionsFloor[i+1, j-1] = instance;
                    }
                }
            }
		
        }
	    else
		{
			DrawRooms(subDungeon.left);
			DrawRooms(subDungeon.right);
		}
	}

	void DrawCorridors(Dungeon subDungeon)
	{
		if (subDungeon == null)
		{
			return;
		}

		DrawCorridors(subDungeon.left);
		DrawCorridors(subDungeon.right);
		

		foreach (Rect corridor in subDungeon.corridors)
		{
			for (int i = (int)corridor.x; i < corridor.xMax; i++)
			{
				for (int j = (int)corridor.y; j < corridor.yMax; j++)
				{
					if (boardPositionsFloor[i, j] == null || boardPositionsFloor[i,j].tag == wallTile.tag)
					{
					    if (boardPositionsFloor[i, j] != null && boardPositionsFloor[i, j].tag == wallTile.tag)
					    {
                            if (boardPositionsFloor[i, j].GetComponent<Collider2D>())
                            {
                                Destroy(boardPositionsFloor[i, j].GetComponent<Collider2D>());
                            }
                        }
						GameObject instance = Instantiate(corridorTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent(transform);
						boardPositionsFloor[i, j] = instance;
					}
				}
			}
		}
	}

}
