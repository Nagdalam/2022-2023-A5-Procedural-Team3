using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject testEmpty;
    public int layoutWidth;
    public int layoutHeight;
    public Room[,] roomLayout;
    int currentX;
    int currentY;

    void TracePath(Vector2 origin, int length, Room.roomType lastRoomType)
    {
        for (int i = 0; i < length; i++)
        {
            bool foundSuitableNeighbour = false;
            List<int> direction = new List<int>();
            direction.Add(1); //Left
            direction.Add(2); //Right
            direction.Add(3); //Up
            direction.Add(4); //Down
            while(foundSuitableNeighbour == false && direction.Count>0)
            {
                int directionChosen = UnityEngine.Random.Range(0, direction.Count);
                switch (directionChosen)
                {
                    case 0:
                        if (origin.x - 1 >= 0 && (roomLayout[(int)origin.y, (int)origin.x -1 ] == null)
                        {
                            roomLayout[(int)origin.y, (int)origin.x].secondDoor = Room.doorDirection.Right;
                            origin.x = origin.x - 1;
                            roomLayout[(int)origin.y, (int)origin.x] = new Room(Room.doorDirection.Left);
                            Instantiate(testEmpty, new Vector3((int)origin.y, (int)origin.x, 0), Quaternion.identity);
                            foundSuitableNeighbour = true;
                            if(i==length - 1)
                            {
                                roomLayout[(int)origin.y, (int)origin.x].type = lastRoomType;
                            }
                        }
                        else
                        {
                            direction.Remove(1);
                        }
                        break;

                    case 1:
                        if (origin.x + 1 < layoutWidth && roomLayout[(int)origin.y, (int)origin.x + 1] == null)
                        {
                            roomLayout[(int)origin.y, (int)origin.x].secondDoor = Room.doorDirection.Right;
                            origin.x = origin.x + 1;
                            roomLayout[(int)origin.y, (int)origin.x] = new Room(Room.doorDirection.Left);
                            Instantiate(testEmpty, new Vector3((int)origin.y, (int)origin.x, 0), Quaternion.identity);
                            foundSuitableNeighbour = true;
                            if (i == length - 1)
                            {
                                roomLayout[(int)origin.y, (int)origin.x].type = lastRoomType;
                            }
                        }
                        else
                        {
                            direction.Remove(2);
                        }
                        break;

                    case 2:
                        if (origin.y - 1 >= 0 && roomLayout[(int)origin.y-1, (int)origin.x] == null)
                        {
                            roomLayout[(int)origin.y, (int)origin.x].secondDoor = Room.doorDirection.Up;
                            origin.y = origin.y - 1;
                            roomLayout[(int)origin.y, (int)origin.x] = new Room(Room.doorDirection.Down);
                            Instantiate(testEmpty, new Vector3((int)origin.y, (int)origin.x, 0), Quaternion.identity);
                            foundSuitableNeighbour = true;
                            if (i == length - 1)
                            {
                                roomLayout[(int)origin.y, (int)origin.x].type = lastRoomType;
                            }
                        }
                        else
                        {
                            direction.Remove(3);
                        }
                        break;

                    case 3:
                        if (origin.y + 1 < layoutHeight && roomLayout[(int)origin.y + 1, (int)origin.x] == null)
                        {
                            roomLayout[(int)origin.y, (int)origin.x].secondDoor = Room.doorDirection.Down;
                            origin.y = origin.y + 1;
                            roomLayout[(int)origin.y, (int)origin.x] = new Room(Room.doorDirection.Up);
                            Instantiate(testEmpty, new Vector3((int)origin.y, (int)origin.x, 0), Quaternion.identity);
                            foundSuitableNeighbour = true;
                            if (i == length - 1)
                            {
                                roomLayout[(int)origin.y, (int)origin.x].type = lastRoomType;
                            }
                        }
                        else
                        {
                            direction.Remove(4);
                        }
                        break;
                }
            }
            if(direction.Count <= 0) {
                roomLayout[(int)origin.y, (int)origin.x].type = lastRoomType;
                break;
            }
        }
    }
    void Start()
    {
        roomLayout = new Room[layoutHeight, layoutWidth];
        roomLayout[layoutHeight / 2, layoutWidth / 2] = new Room(Room.doorDirection.Null);
        Instantiate(testEmpty, new Vector3(layoutHeight / 2, layoutWidth / 2, 0), Quaternion.identity);
        //Génération vers Spawn
        int r = UnityEngine.Random.Range(3, 11);
        TracePath(new Vector2(layoutHeight / 2, layoutWidth / 2), r, Room.roomType.Spawn);

    }
}
