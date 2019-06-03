using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaunayGenerator : MonoBehaviour {

    public GameObject floorTile;
    private SeededRandom randomGenerator;

    public List<GameObject> rooms = new List<GameObject>();
    public int amountOfCells = 20;
    private int xScale, yScale;

    void Start(){
        //randomGenerator = GetComponent<SeededRandom>();
        randomGenerator = GetComponent<SeededRandom>();
    }

    void GenerateRooms(){
         for(int i = 0; i < amountOfCells; i++){
            GameObject room = (GameObject) Instantiate(floorTile);
            rooms.Add(room);

            // using park miller to generate a random range with more smaller rooms than large rooms
            xScale = randomGenerator.Range(3, 15);
            yScale = randomGenerator.Range(3, 15);
            randomGenerator.setSeed((uint)Random.Range(0,99999));
        //    xScale = Random.Range(5,15);
         //   yScale = Random.Range(5,15);

            // ??? what does this do
        //    if(xScale % 2 == 0) {xScale +=1;}
        //    if(yScale % 2 == 0) {yScale +=1;}

            // change the scale of the rooms
            room.transform.localScale = new Vector3(xScale, yScale, room.transform.localScale.z);

            //decide on a random location to put the roomCell in
            int xPos = Random.Range(0, 20);
            int yPos = Random.Range(0, 20);

            room.transform.position = new Vector3(-10 + xPos, -10 + yPos, 0);

            // Change to a random color
            room.transform.GetComponent<SpriteRenderer>().material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));

        }
    }

    void BreakRoomsApart(){
        foreach(GameObject room in rooms){
            
        }
    }

    void ConnectRooms(){

    }

    void DrawCorridors(){

    }
	
}
