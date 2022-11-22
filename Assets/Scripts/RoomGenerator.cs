using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int layoutWidth;
    public int layoutHeight;
    public Room[,] roomLayout;
    public GameObject defaultRoom;
    public Vector2 firstRoom;
    private List<Room.roomType> typeProbList = new List<Room.roomType>();
    public int regularProb, minibossProb, recovProb;
    public int pathLength;
    void Start()
    {
        roomLayout = new Room[layoutHeight, layoutWidth];
        roomLayout[(int)firstRoom.x, (int)firstRoom.y] = new Room(Room.roomType.Boss, 0);
        for (int x = 0; x < regularProb; x++)
        {
            typeProbList.Add(Room.roomType.Regular);
        }
        for (int y = 0; y < minibossProb; y++)
        {
            typeProbList.Add(Room.roomType.Miniboss);
        }
        for (int z = 0; z < recovProb; z++)
        {
            typeProbList.Add(Room.roomType.Miniboss);
        }
        //Instantiate(testStart, new Vector3(10, 10, 0), Quaternion.identity);
        //G�n�ration vers Spawn

        TracePath(firstRoom, pathLength, Room.roomType.Spawn, 1);
        for (int i = 0; i < layoutWidth; i++)
        {

            for (int j = 0; j < layoutHeight; j++)
            {

                if (roomLayout[i, j] != null)
                {
                    //if (i - 1 >= 0 && roomLayout[i - 1, j] != null && (roomLayout[i, j].pathID == roomLayout[i - 1, j].pathID || roomLayout[i - 1, j].pathID == 0))
                    //{
                    //    roomLayout[i, j].doors.Add(Room.doorDirection.Left);
                    //}
                    //else if (i + 1 < layoutWidth - 1 && roomLayout[i + 1, j] != null && (roomLayout[i, j].pathID == roomLayout[i + 1, j].pathID || roomLayout[i + 1, j].pathID == 0))
                    //{
                    //    roomLayout[i, j].doors.Add(Room.doorDirection.Right);
                    //}
                    //else if (j >= 0 && roomLayout[i, j - 1] != null && (roomLayout[i, j].pathID == roomLayout[i, j - 1].pathID || roomLayout[i, j - 1].pathID == 0))
                    //{
                    //    roomLayout[i, j].doors.Add(Room.doorDirection.Left);
                    //}
                    //else if (j + 1 < layoutHeight - 1 && roomLayout[i, j + 1] != null && (roomLayout[i, j].pathID == roomLayout[i, j + 1].pathID || roomLayout[i, j + 1].pathID == 0))
                    //{
                    //    roomLayout[i, j].doors.Add(Room.doorDirection.Right);
                    //}
                    //Debug.Log("Generate room");
                    GameObject newRoom = Instantiate(defaultRoom, new Vector3(i, j, 0), Quaternion.identity);
                    newRoom.GetComponent<RoomContent>().myRoom = roomLayout[i, j];
                }

            }
        }

    }
    void TracePath(Vector2 origin, int length, Room.roomType lastRoomType, int pathID)
    {
        for (int i = 0; i < length; i++)
        {
            bool foundSuitableNeighbour = false;
            List<int> direction = new List<int>();
            direction.Add(1); //Left
            direction.Add(2); //Right
            direction.Add(3); //Up
            direction.Add(4); //Down
            while (foundSuitableNeighbour == false && direction.Count > 0)
            {
                int directionChosen = UnityEngine.Random.Range(0, direction.Count);
                switch (directionChosen)
                {
                    case 0:
                        if (origin.x - 1 >= 0 && (roomLayout[(int)origin.x - 1, (int)origin.y]) == null)
                        {
                            origin.x = origin.x - 1;
                            foundSuitableNeighbour = true;
                            if (i == length - 1)
                            {
                                roomLayout[(int)origin.x, (int)origin.y] = new Room(lastRoomType, pathID);
                            }
                            else
                            {
                                roomLayout[(int)origin.x, (int)origin.y] = new Room(GenerateRandomRoomType(), pathID);
                            }
                        }
                        else
                        {
                            direction.Remove(1);
                        }
                        break;

                    case 1:
                        if (origin.x + 1 < layoutWidth - 1 && roomLayout[(int)origin.x + 1, (int)origin.y] == null)
                        {
                            origin.x = origin.x + 1;
                            foundSuitableNeighbour = true;
                            if (i == length - 1)
                            {
                                roomLayout[(int)origin.x, (int)origin.y] = new Room(lastRoomType, pathID);
                            }
                            else
                            {
                                roomLayout[(int)origin.x, (int)origin.y] = new Room(GenerateRandomRoomType(), pathID);
                            }
                        }
                        else
                        {
                            direction.Remove(2);
                        }
                        break;

                    case 2:
                        if (origin.y - 1 >= 0 && roomLayout[(int)origin.x, (int)origin.y - 1] == null)
                        {
                            origin.y = origin.y - 1;
                            foundSuitableNeighbour = true;
                            if (i == length - 1)
                            {
                                roomLayout[(int)origin.x, (int)origin.y] = new Room(lastRoomType, pathID);
                            }
                            else
                            {
                                roomLayout[(int)origin.x, (int)origin.y] = new Room(GenerateRandomRoomType(), pathID);
                            }
                        }
                        else
                        {
                            direction.Remove(3);
                        }
                        break;

                    case 3:
                        if (origin.y + 1 < layoutHeight - 1 && roomLayout[(int)origin.x, (int)origin.y + 1] == null)
                        {
                            origin.y = origin.y + 1;

                            foundSuitableNeighbour = true;
                            if (i == length - 1)
                            {
                                roomLayout[(int)origin.x, (int)origin.y] = new Room(lastRoomType, pathID);
                            }
                            else
                            {
                                roomLayout[(int)origin.x, (int)origin.y] = new Room(GenerateRandomRoomType(), pathID);
                            }
                        }
                        else
                        {
                            direction.Remove(4);
                        }
                        break;
                }
                if (direction.Count <= 0)
                {
                    roomLayout[(int)origin.x, (int)origin.y].type = lastRoomType;
                    foundSuitableNeighbour = true;
                    break;
                }
            }

        }

        Room.roomType GenerateRandomRoomType()
        {
            int r = UnityEngine.Random.Range(0, typeProbList.Count);
            if (typeProbList[r] == Room.roomType.Regular)
            {
                typeProbList.Add(Room.roomType.Miniboss);
                typeProbList.Add(Room.roomType.Recovery);
                return Room.roomType.Regular;
            }
            else if (typeProbList[r] == Room.roomType.Recovery)
            {
                typeProbList.Add(Room.roomType.Miniboss);
                typeProbList.Remove(Room.roomType.Recovery);
                return Room.roomType.Recovery;
            }
            else if (typeProbList[r] == Room.roomType.Miniboss)
            {
                typeProbList.Add(Room.roomType.Recovery);
                typeProbList.Remove(Room.roomType.Miniboss);
                return Room.roomType.Miniboss;
            }
            return Room.roomType.Empty;
        }
    }
}