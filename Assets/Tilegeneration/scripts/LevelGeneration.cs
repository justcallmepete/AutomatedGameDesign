using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    private int direction;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;

    public bool stopGeneration;
    private int downCounter;

    public LayerMask room;

    public Transform startingPosition;
    private Vector3 lastRoomPos;
    public GameObject[] rooms; // 0 = LR, 1 = LRB, 2 = LRT, 3 = LRTB, 4 = Start, 5 = Boss
    public GameObject player;

	// Use this for initialization
	void Start () {
        transform.position = startingPosition.position;
        Instantiate(rooms[4], transform.position, Quaternion.identity);

        direction = 1;
	}

    private void Update() {
        if (timeBtwRoom <= 0 && stopGeneration == false) {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move() {
        if (direction == 1 || direction == 2) { // Move right !

            if (transform.position.x < maxX) {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(0, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                // Makes sure the level generator doesn't move left !
                direction = Random.Range(1, 6);
                if (direction == 3) {
                    direction = 1;
                }
                else if (direction == 4) {
                    direction = 5;
                }
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) { // Move left !

            if (transform.position.x > minX) {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(0, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else {
                direction = 5;
            }

        }
        else if (direction == 5) { // MoveDown
            downCounter++;
            if (transform.position.y > minY) {
                // Now I must replace the room BEFORE going down with a room that has a DOWN opening, so type 3 or 5
                Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, room);
                lastRoomPos = previousRoom.gameObject.transform.position;
                Debug.Log(previousRoom);
                if (previousRoom.GetComponent<RoomType>().type != 1 && previousRoom.GetComponent<RoomType>().type != 3) {
                    // My problem : if the level generation goes down TWICE in a row, there's a chance that the previous room is just 
                    // a LRB, meaning there's no TOP opening for the other room ! 

                    if (downCounter >= 2) {
                        previousRoom.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else {
                        previousRoom.GetComponent<RoomType>().RoomDestruction();
                        int randRoomDownOpening = Random.Range(1, 4);
                        if (randRoomDownOpening == 2) {
                            randRoomDownOpening = 3;
                        }
                        Instantiate(rooms[randRoomDownOpening], transform.position, Quaternion.identity);
                    }

                }



                Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = pos;

                // Makes sure the room we drop into has a TOP opening !
                int randRoom = Random.Range(2, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else {
                Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, room);
                previousRoom.GetComponent<RoomType>().RoomDestruction();
                GameObject endRoom = (GameObject)Instantiate(rooms[5], transform.position, Quaternion.identity);
                if (lastRoomPos.x < endRoom.transform.position.x) {
                    endRoom.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else if (lastRoomPos.x > endRoom.transform.position.x) {
                    endRoom.transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                stopGeneration = true;
                Instantiate(player, startingPosition.transform.position, Quaternion.identity);
            }

        }
    }
}
