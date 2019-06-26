using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpelunkyLevelGen : MonoBehaviour {
    public delegate void Ongenerating();
    public static event Ongenerating doneGenerating;
    public float waitTime = 1f;
    public RoomTypeManager roomTypeManager;
    public List<GameObject> RoomPositions = new List<GameObject>(); // maybe make room class?
    public List<DungeonRoom> ActualRooms = new List<DungeonRoom>();
    public List<GameObject> spawnedRooms = new List<GameObject>();

    private int[,] possibleRooms = new int[4,4];
    public int[,] boardPosition;
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
    public int lastGeneratedRoomNumber;

public GameObject spawnObject;

public GameObject player;
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
       // Instantiate(spawnObject, StartRoom.roomTransform); 
        
        boardPosition = new int[startPos, 0];
        currentX = startPos;
        currentY = 0;
        possibleRooms[startPos, 0] = 1;
        //RoomDictionary.Add(boardPosition, StartRoom);
      //  Instantiate(spawnObject, StartRoom.roomTransform); 
         

     //   StartRoom.SpawnRoom(spawnObject);
     StartCoroutine(CreateACoolDungeon());
             SpawnPlayer(player);

    }

    void Update(){
        /*
        if(isGenerating){
            GenerateNextPart();
        } else {
            if (isTesting){
                DecideOnRoomType();
            }
        }
        */
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
                //    Instantiate(spawnObject, tempRoom.roomTransform); 
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
                    //Instantiate(spawnObject, tempRoom.roomTransform); 
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
                   // Instantiate(spawnObject, tempRoom.roomTransform); 
                    lastSpawnedRoom = tempRoom;
                    ActualRooms.Add(tempRoom);
                   
                } else {
                    //end reached, put down end room
                    Debug.Log("End reached");
                    lastSpawnedRoom.roomType = TypeOfRoom.End;
                    isGenerating = false;
                    //stop generating
                }
                 lastGeneratedRoomNumber = ActualRooms.Count;
    }

    private void DecideOnRoomType(){
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

        if(ActualRooms[lastGeneratedRoomNumber-1].yPos > ActualRooms[lastGeneratedRoomNumber-2].yPos){
            ActualRooms[lastGeneratedRoomNumber-1].upSide = true;
        }else {
            if(ActualRooms[lastGeneratedRoomNumber-1].xPos < ActualRooms[lastGeneratedRoomNumber-2].xPos){
                ActualRooms[lastGeneratedRoomNumber-1].rightSide = true;
            }else{
                ActualRooms[lastGeneratedRoomNumber-1].leftSide = true;
            }
        }
    }

    private void FillGridWithNonCriticalRooms(){
        for(int i = 0; i < 4; i++){
            for (int j = 0; j< 4; j++){
                if(possibleRooms[i,j] == -1){
                    int tempX = i;
                    int tempY = j;
                    int roomnumber = tempX + (tempY *4);
                    DungeonRoom temp = new DungeonRoom(RoomPositions[roomnumber].transform, tempX, tempY);
                    temp.roomType = TypeOfRoom.NonCrit;
                    ActualRooms.Add(temp);
                }
            }
        }
    }

    private void ActuallyCreateTheRooms(){
            foreach (var item in ActualRooms)
            {
                Instantiate(spawnObject, item.roomTransform);
                switch(item.roomType){
                    case TypeOfRoom.Start:{
                        
                        break;
                    }
                    case TypeOfRoom.End:{
                        
                        break;
                    }
                    case TypeOfRoom.DropIn:{
                        
                        break;
                    }
                    case TypeOfRoom.DropOut:{
                        
                        break;
                    }
                    case TypeOfRoom.Corridor:{
                        
                        break;
                    }
                    case TypeOfRoom.NonCrit:{
                        
                        break;
                    }
                }
            }
    }

        IEnumerator CreateTheDungeonRooms(){
        
         foreach (var item in ActualRooms)
            {
                GameObject prefab = null;
                yield return new WaitForSeconds(waitTime);
                 prefab = roomTypeManager.GetRoom(item.roomType);
                 Debug.Log("Getting roomtype: "+ item.roomType);
               GameObject spawnedThing = Instantiate(prefab, item.roomTransform);
                spawnedThing.GetComponent<ChunkGenerator>().roomType = item.roomType;
                spawnedRooms.Add(spawnedThing);
            } 
    }

    private void GenerateRandomChunksPerRoom(){
        System.Random rnd = new System.Random();
        for(int i = 0; i < ActualRooms.Count; i++){
            int regChance1 = rnd.Next(0,100);
             int regChance2 = rnd.Next(0,100);
              int regChance3 = rnd.Next(0,100);
               int regChance4 = rnd.Next(0,100);
            int nonCritChance1 = rnd.Next(0,100);
            int nonCritChance2 = rnd.Next(0,100);
            int nonCritChance3 = rnd.Next(0,100);
            int nonCritChance4 = rnd.Next(0,100);
            ChunkGenerator room = spawnedRooms[i].GetComponent<ChunkGenerator>();
         //   Debug.Log("booleans are: " +ActualRooms[i].leftSide+ActualRooms[i].rightSide+ActualRooms[i].upSide+ActualRooms[i].downSide);
            room.setEntrances(ActualRooms[i].upSide, ActualRooms[i].downSide, ActualRooms[i].rightSide, ActualRooms[i].leftSide);
           // room.spawnBottomEntrance = true;
           if(room.roomType == TypeOfRoom.NonCrit){
               room.SpawnNonCritEntrances(nonCritChance1, nonCritChance2, nonCritChance3, nonCritChance4);
           } else {
            room.SpawnEntrances(regChance1, regChance2, regChance3, regChance4);
           }
            room.SpawnChunks();
        }

        //ToDO: non critical room - random openings and closed off borders
    }
    
    private void SpawnPlayer(GameObject po){
       Instantiate(po, ActualRooms[0].roomTransform.position,Quaternion.identity);
    }

    IEnumerator CreateACoolDungeon(){
        while(isGenerating){
            GenerateNextPart();
            yield return new WaitForSeconds(waitTime);
        }
        
        yield return new WaitForSeconds(waitTime);
        DecideOnRoomType();
        yield return new WaitForSeconds(waitTime);
        // fill the rest of the level (non-critical-rooms)
        FillGridWithNonCriticalRooms();
        yield return new WaitForSeconds(waitTime);
        //actually create the rooms
       // ActuallyCreateTheRooms();
        yield return StartCoroutine(CreateTheDungeonRooms());
        yield return new WaitForSeconds(waitTime);
        //generate random chunks and exits per room
        GenerateRandomChunksPerRoom();
        yield return new WaitForSeconds(waitTime);
        //fill level with enemies
        //ToDo: BILAL ADD YOUR ENEMY SPAWN STUFF HERE POR FAVOR
        doneGenerating.Invoke();
        yield return new WaitForSeconds(waitTime);
        //start with player in start room
    }



} 