using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpelunkyLevelGen : MonoBehaviour {

    public List<GameObject> RoomPositions = new List<GameObject>(); // maybe make room class?

    public List<DungeonRoom> ActualRooms = new List<DungeonRoom>();

    public int[,] possibleRooms = new int[4,4];
    public int[,] boardPosition;
    public Dictionary<int[,] , DungeonRoom > RoomDictionary = new Dictionary<int [,], DungeonRoom>();
    int currentX = 0;
    int currentY = 0;

    private DungeonRoom lastSpawnedRoom;

    int[] xMove = {0, 1, -1};
    int[] yMove = {1, 0, 0};

    public int startPos;
    public int currentPos;
    public int moveDir;

    private bool isGenerating = true;
    private bool isTesting = true;

    public GameObject spawnObject;

    private void Start() {
        for(int i = 0; i < 4; i++){
            for (int j = 0; j< 4; j++){
                possibleRooms[i,j] = -1;
            }
        }

         startPos = Random.Range(0, 3);
        DungeonRoom StartRoom = new DungeonRoom(RoomPositions[startPos].transform, startPos, currentY);
        ActualRooms.Add(StartRoom);
        ActualRooms[0].roomType = TypeOfRoom.Start;
        Instantiate(spawnObject, StartRoom.roomTransform); 
        
        boardPosition = new int[startPos, 0];
        currentX = startPos;
        currentY = 0;
        possibleRooms[startPos, 0] = 1;
        //RoomDictionary.Add(boardPosition, StartRoom);
      //  Instantiate(spawnObject, StartRoom.roomTransform); 
         

     //   StartRoom.SpawnRoom(spawnObject);
    }

    void Update(){

        if(isGenerating){
            GenerateNextPart();
        } else {
            if (isTesting){
                DecideOnRoomType();
            }
        }
    }

    private bool isMoveSafe(int x, int y){ // if move is within bounds and room is not taken
  /*   return ( x >= 0 && x <= 4 && y >= 0 && 
             y <= 4 && possibleRooms[x,y] == -1);  */

     
    if ( x >= 0 && x < 4 && y >= 0 && y < 4){
        if (possibleRooms[x,y] == -1) return true;
    } else {
        return false;
    }
    return false;
    }

    private bool isLeftEdge(int x, int y){
     if(x <= -1){
            Debug.Log("left edge reached");
                    return  true;
        } else {
            Debug.Log("x is actually:" + x);
        }
        return false;
    } 

    private bool isRightEdge(int x, int y){
        if(x  >= 4){
            Debug.Log("right edge reached");
                    return  true;
        } else {
            Debug.Log("x is actually:" + x);
        }
        return false;
    }

    private void GenerateNextPart(){
        moveDir = Random.Range(1, 6); // 0 under, 1, left, 2 right
        //moveDir = 0;

        switch(moveDir){
            case 1:{
               MoveDown();
                break;
            }
            case 2:{
                MoveDown();
                 
                break;
            } 
            case 3:{
                MoveLeft();
                break;
            }
            case 4:{
                MoveLeft();
                break;
            }
            case 5:{
                MoveRight();
                break;
            }
            case 6:{
                MoveRight();
                break;
            }          
        }
    }


    private void MoveLeft(){
         DungeonRoom tempRoom = null;
                int tempX = currentX + xMove[2];
                int tempY = currentY + yMove[2];
                if(isMoveSafe(tempX, tempY)){
                    currentX = tempX;
                    currentY = tempY;
                    int roomnumber = currentX + (currentY *4);
                     tempRoom = new DungeonRoom(RoomPositions[roomnumber].transform, currentX, currentY);
                     possibleRooms[currentX, currentY] = 0;
                     ActualRooms.Add(tempRoom);
                    Instantiate(spawnObject, tempRoom.roomTransform); 
                    lastSpawnedRoom = tempRoom;
                } else {
                    if(isLeftEdge(tempX, tempY)){
                        MoveDown();
                    } else {
                        //stopgenerating
                      //  Debug.Log("End reached");
                    //    lastSpawnedRoom.roomType = DungeonRoom.TypeOfRoom.StartEnd;
                      //  isGenerating = false;
                    }
                    //end reached, put down end room 
                    //stop generating
                }
    }

    private void MoveRight(){
          DungeonRoom tempRoom = null;
                int tempX = currentX + xMove[1];
                int tempY = currentY + yMove[1];
                if(isMoveSafe(tempX, tempY)){
                    currentX = tempX;
                    currentY = tempY;
                    int roomnumber = currentX + (currentY *4);
                     tempRoom = new DungeonRoom(RoomPositions[roomnumber].transform, currentX, currentY);
                     possibleRooms[currentX, currentY] = 0;
                     ActualRooms.Add(tempRoom);
                    Instantiate(spawnObject, tempRoom.roomTransform); 
                    lastSpawnedRoom = tempRoom;
                } else {
                    if(isRightEdge(tempX, tempY)){
                        MoveDown();
                    }else {
                     //   Debug.Log("End reached");
                  //  lastSpawnedRoom.roomType = DungeonRoom.TypeOfRoom.StartEnd;
                 //   isGenerating = false;
                    }
                }
    }

    private void MoveDown(){
         DungeonRoom tempRoom = null;
                int tempX = currentX + xMove[0];
                int tempY = currentY + yMove[0];
                if(isMoveSafe(tempX, tempY)){
                    currentX = tempX;
                    currentY = tempY;
                    int roomnumber = currentX + (currentY *4);
                     tempRoom = new DungeonRoom(RoomPositions[roomnumber].transform, currentX, currentY);
                     possibleRooms[currentX, currentY] = 0;
                    Instantiate(spawnObject, tempRoom.roomTransform); 
                    lastSpawnedRoom = tempRoom;
                    ActualRooms.Add(tempRoom);
                } else {
                    //end reached, put down end room
                    Debug.Log("End reached");
                    lastSpawnedRoom.roomType = TypeOfRoom.End;
                    isGenerating = false;
                    //stop generating
                }
    }

    void DecideOnRoomType(){
        // start room -> next room (left,right,down)
        if(ActualRooms[1].yPos > ActualRooms[0].yPos){
            // room is below it
            ActualRooms[0].downSide = true;
        } else
        {
            if(ActualRooms[1].xPos < ActualRooms[0].xPos){
                // next room is on the left side
                ActualRooms[0].leftSide = true;
            }else {
                // next room is on the right side
                ActualRooms[0].rightSide = true;
            }
        }

        for (int i = 1; i < ActualRooms.Count -1; i++){
            if(ActualRooms[i-1].yPos < ActualRooms[i].yPos){
                //if previous room is above it
                ActualRooms[i].roomType = TypeOfRoom.DropIn;
                ActualRooms[i].upSide = true;
                if(ActualRooms[i+1].yPos > ActualRooms[i].yPos){
                    // next room is below it
                    ActualRooms[i].roomType = TypeOfRoom.DropOut;
                ActualRooms[i].downSide = true;
                } else
                {
                    if(ActualRooms[i+1].xPos < ActualRooms[i].xPos){
                        // next room is on left side
                        ActualRooms[i].leftSide = true;
                    } else {
                        // next room is right side
                        ActualRooms[i].rightSide = true;
                    }
                }
            } else if(ActualRooms[i-1].xPos < ActualRooms[i].xPos){
                // if previous room is on left side
                ActualRooms[i].roomType = TypeOfRoom.Corridor;
                ActualRooms[i].leftSide = true;
                if (ActualRooms[i+1].yPos > ActualRooms[i].yPos){
                    // next room is below it
                    ActualRooms[i].roomType = TypeOfRoom.DropOut;
                    ActualRooms[i].downSide = true;
                } else{
                     if(ActualRooms[i+1].xPos < ActualRooms[i].xPos){
                        // next room is on left side
                        ActualRooms[i].leftSide = true;
                    } else {
                        // next room is right side
                        ActualRooms[i].rightSide = true;
                    }
                }
            } else if(ActualRooms[i-1].xPos > ActualRooms[i].xPos){
                //if previous room is on right side
                 ActualRooms[i].roomType = TypeOfRoom.Corridor;
                ActualRooms[i].rightSide = true;
                if (ActualRooms[i+1].yPos > ActualRooms[i].yPos){
                    // next room is below it
                    ActualRooms[i].roomType = TypeOfRoom.DropOut;
                    ActualRooms[i].downSide = true;
                } else{
                     if(ActualRooms[i+1].xPos < ActualRooms[i].xPos){
                        // next room is on left side
                        ActualRooms[i].leftSide = true;
                    } else {
                        // next room is right side
                        ActualRooms[i].rightSide = true;
                    }
            }
            }
        }
        isTesting = false;
    }
} 
