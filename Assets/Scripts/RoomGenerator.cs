using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ListSpawner
{
    public List<GameObject> _spawner;
}

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
    public Vector2 roomDimension;
    bool error = false;

    public List<ListSpawner> listSpawner;
    public List<GameObject> spawnerChoosed;
    public GameObject openDoorPrefab;
    public GameObject lockedDoorPrefab;

    private bool boss1Spawn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CreateDungeon();
        }
    }
    public void CreateDungeon()
    {
        boss1Spawn = false;
        spawnerChoosed = listSpawner[UnityEngine.Random.Range(0, listSpawner.Count)]._spawner;
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        roomLayout = new Room[layoutHeight, layoutWidth];
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
        roomLayout[(int)firstRoom.x, (int)firstRoom.y] = new Room(Room.roomType.Boss, 0);
        roomLayout[(int)firstRoom.x, (int)firstRoom.y].doors.Add(Room.doorDirection.Up);
        roomLayout[(int)firstRoom.x, (int)firstRoom.y].doors.Add(Room.doorDirection.Down);
        roomLayout[(int)firstRoom.x, (int)firstRoom.y].doors.Add(Room.doorDirection.Left);
        roomLayout[(int)firstRoom.x, (int)firstRoom.y].doors.Add(Room.doorDirection.Right);
        roomLayout[(int)firstRoom.x + 1, (int)firstRoom.y] = new Room(GenerateRandomRoomType(), 1);
        roomLayout[(int)firstRoom.x - 1, (int)firstRoom.y] = new Room(GenerateRandomRoomType(), 2);
        roomLayout[(int)firstRoom.x, (int)firstRoom.y + 1] = new Room(GenerateRandomRoomType(), 3);
        roomLayout[(int)firstRoom.x, (int)firstRoom.y - 1] = new Room(Room.roomType.Boss, 4);

        //Instantiate(testStart, new Vector3(10, 10, 0), Quaternion.identity);
        //G�n�ration vers Spawn

        TracePath(new Vector2((int)firstRoom.x + 1, (int)firstRoom.y), pathLength, Room.roomType.Spawn, 1);
        TracePath(new Vector2((int)firstRoom.x - 1, (int)firstRoom.y), pathLength, Room.roomType.Item, 2);
        TracePath(new Vector2((int)firstRoom.x, (int)firstRoom.y + 1), pathLength, Room.roomType.Item, 3);
        if (error == true)
        {
            error = false;
            CreateDungeon();
            return;
        }
        //TracePath(firstRoom, pathLength, Room.roomType.Item, 3);
        //TracePath(firstRoom, pathLength, Room.roomType.Item, 4);
        bool secretRoomCreated = false;
        for (int i = 0; i < layoutWidth; i++)
        {
            for (int j = 0; j < layoutHeight; j++)
            {
                if (roomLayout[i, j] != null && CheckIfNeighboursFull(new Vector2(i, j)) == false && secretRoomCreated == false)
                {
                    if (roomLayout[i, j].type == Room.roomType.Regular || roomLayout[i, j].type == Room.roomType.Miniboss || roomLayout[i, j].type == Room.roomType.Recovery)
                    {
                        secretRoomCreated = true;
                        roomLayout[i, j].type = Room.roomType.SecretEntrance;
                        TracePath(new Vector2(i, j), 1, Room.roomType.Secret, 12);
                    }
                }
            }
        }

        for (int i = 0; i < layoutWidth; i++)
        {

            for (int j = 0; j < layoutHeight; j++)
            {

                if (roomLayout[i, j] != null)
                {
                    if (i - 1 >= 0 && roomLayout[i - 1, j] != null && (roomLayout[i, j].pathID == roomLayout[i - 1, j].pathID || roomLayout[i - 1, j].pathID == 0))
                    {
                        roomLayout[i, j].doors.Add(Room.doorDirection.Left);
                    }
                    if (i + 1 < layoutWidth - 1 && roomLayout[i + 1, j] != null && (roomLayout[i, j].pathID == roomLayout[i + 1, j].pathID || roomLayout[i + 1, j].pathID == 0))
                    {
                        roomLayout[i, j].doors.Add(Room.doorDirection.Right);
                    }
                    if (j - 1 >= 0 && roomLayout[i, j - 1] != null && (roomLayout[i, j].pathID == roomLayout[i, j - 1].pathID || roomLayout[i, j - 1].pathID == 0))
                    {
                        roomLayout[i, j].doors.Add(Room.doorDirection.Down);
                    }
                    if (j + 1 < layoutHeight - 1 && roomLayout[i, j + 1] != null && (roomLayout[i, j].pathID == roomLayout[i, j + 1].pathID || roomLayout[i, j + 1].pathID == 0))
                    {
                        roomLayout[i, j].doors.Add(Room.doorDirection.Up);
                    }
                    if (roomLayout[i, j].type == Room.roomType.Secret)
                    {
                        if (i - 1 >= 0 && roomLayout[i - 1, j] != null && roomLayout[i - 1, j].type == Room.roomType.SecretEntrance)
                        {
                            roomLayout[i, j].doors.Add(Room.doorDirection.Left);
                            roomLayout[i - 1, j].doors.Add(Room.doorDirection.Right);
                        }
                        if (i + 1 < layoutWidth - 1 && roomLayout[i + 1, j] != null && roomLayout[i + 1, j].type == Room.roomType.SecretEntrance)
                        {
                            roomLayout[i, j].doors.Add(Room.doorDirection.Right);
                            roomLayout[i + 1, j].doors.Add(Room.doorDirection.Left);
                        }
                        if (j - 1 >= 0 && roomLayout[i, j - 1] != null && roomLayout[i, j - 1].type == Room.roomType.SecretEntrance)
                        {
                            roomLayout[i, j].doors.Add(Room.doorDirection.Down);
                            roomLayout[i, j - 1].doors.Add(Room.doorDirection.Up);
                        }
                        if (j + 1 < layoutHeight - 1 && roomLayout[i, j + 1] != null && roomLayout[i, j + 1].type == Room.roomType.SecretEntrance)
                        {
                            roomLayout[i, j].doors.Add(Room.doorDirection.Up);
                            roomLayout[i, j + 1].doors.Add(Room.doorDirection.Down);
                        }
                    }
                    //Debug.Log("Generate room");
                    GameObject newRoom = Instantiate(defaultRoom, new Vector3(i * roomDimension.x, j * roomDimension.y, 0), Quaternion.identity);
                    newRoom.GetComponent<RoomContent>().myRoom = roomLayout[i, j];
                    newRoom.transform.parent = gameObject.transform;
                    if (roomLayout[i, j].type != Room.roomType.Spawn && roomLayout[i, j].type != Room.roomType.Secret)
                    {
                        GameObject itemSpawn;
                        if (roomLayout[i, j].type == Room.roomType.Regular)
                            itemSpawn = Instantiate(spawnerChoosed[0], newRoom.transform);
                        else if (roomLayout[i, j].type == Room.roomType.Recovery)
                            itemSpawn = Instantiate(spawnerChoosed[1], newRoom.transform);
                        else if (roomLayout[i, j].type == Room.roomType.Item)
                            itemSpawn = Instantiate(spawnerChoosed[2], newRoom.transform);
                        else if (roomLayout[i, j].type == Room.roomType.Miniboss)
                            itemSpawn = Instantiate(spawnerChoosed[3], newRoom.transform);
                        else if (roomLayout[i, j].type == Room.roomType.Boss)
                        {
                            itemSpawn = Instantiate(spawnerChoosed[4], newRoom.transform);
                            itemSpawn.GetComponent<ItemSpawner>().boss1Spawn = boss1Spawn;
                            boss1Spawn = true;
                        }
                        else if (roomLayout[i, j].type == Room.roomType.SecretEntrance)
                            itemSpawn = Instantiate(spawnerChoosed[5], newRoom.transform);
                        else
                        {

                            Debug.LogError("WTF");
                            break;
                        }

                        itemSpawn.transform.localPosition = new Vector3(0, -2, 0);
                        newRoom.GetComponent<RoomContent>().SetSpawner(itemSpawn.GetComponent<ItemSpawner>());
                        if (i - 1 >= 0 && roomLayout[i - 1, j] != null && (roomLayout[i, j].pathID == roomLayout[i - 1, j].pathID || roomLayout[i - 1, j].pathID == 0) && roomLayout[i - 1, j].type == Room.roomType.Spawn)
                            itemSpawn.GetComponent<ItemSpawner>().DeleteSpawnDoor(Room.doorDirection.Left);
                        if (i + 1 < layoutWidth - 1 && roomLayout[i + 1, j] != null && (roomLayout[i, j].pathID == roomLayout[i + 1, j].pathID || roomLayout[i + 1, j].pathID == 0) && roomLayout[i + 1, j].type == Room.roomType.Spawn)
                            itemSpawn.GetComponent<ItemSpawner>().DeleteSpawnDoor(Room.doorDirection.Right);
                        if (j - 1 >= 0 && roomLayout[i, j - 1] != null && (roomLayout[i, j].pathID == roomLayout[i, j - 1].pathID || roomLayout[i, j - 1].pathID == 0) && roomLayout[i, j - 1].type == Room.roomType.Spawn)
                            itemSpawn.GetComponent<ItemSpawner>().DeleteSpawnDoor(Room.doorDirection.Down);
                        if (j + 1 < layoutHeight - 1 && roomLayout[i, j + 1] != null && (roomLayout[i, j].pathID == roomLayout[i, j + 1].pathID || roomLayout[i, j + 1].pathID == 0) && roomLayout[i, j + 1].type == Room.roomType.Spawn)
                            itemSpawn.GetComponent<ItemSpawner>().DeleteSpawnDoor(Room.doorDirection.Up);

                        if (roomLayout[i, j].type == Room.roomType.SecretEntrance)
                        {
                            if (roomLayout[i, j].doors.Contains(Room.doorDirection.Left) && roomLayout[i - 1, j].type == Room.roomType.Secret)
                                itemSpawn.GetComponent<ItemSpawner>().DeleteDoor(Room.doorDirection.Left);
                            if (roomLayout[i, j].doors.Contains(Room.doorDirection.Right) && roomLayout[i + 1, j].type == Room.roomType.Secret)
                                itemSpawn.GetComponent<ItemSpawner>().DeleteDoor(Room.doorDirection.Right);
                            if (roomLayout[i, j].doors.Contains(Room.doorDirection.Down) && roomLayout[i, j - 1].type == Room.roomType.Secret)
                                itemSpawn.GetComponent<ItemSpawner>().DeleteDoor(Room.doorDirection.Down);
                            if (roomLayout[i, j].doors.Contains(Room.doorDirection.Up) && roomLayout[i, j + 1].type == Room.roomType.Secret)
                                itemSpawn.GetComponent<ItemSpawner>().DeleteDoor(Room.doorDirection.Up);
                        }
                    }
                    else if (roomLayout[i, j].type == Room.roomType.Spawn)
                    {
                        GameObject tempGameObject;
                        if (roomLayout[i, j].doors.Contains(Room.doorDirection.Left))
                        {
                            tempGameObject = Instantiate(openDoorPrefab, newRoom.GetComponent<RoomContent>().transform);
                            tempGameObject.transform.SetLocalPositionAndRotation(new Vector3(-9.5f, -2.5f, 0), Quaternion.identity);
                        }

                        if (roomLayout[i, j].doors.Contains(Room.doorDirection.Right))
                        {
                            tempGameObject = Instantiate(openDoorPrefab, newRoom.GetComponent<RoomContent>().transform);
                            tempGameObject.transform.SetLocalPositionAndRotation(new Vector3(9.5f, -2.5f, 0), Quaternion.identity);
                        }

                        if (roomLayout[i, j].doors.Contains(Room.doorDirection.Down))
                        {
                            tempGameObject = Instantiate(openDoorPrefab, newRoom.GetComponent<RoomContent>().transform);
                            tempGameObject.transform.SetLocalPositionAndRotation(new Vector3(0.5f, -6.5f, 0), Quaternion.identity);
                        }
                        if (roomLayout[i, j].doors.Contains(Room.doorDirection.Up))
                        {
                            tempGameObject = Instantiate(openDoorPrefab, newRoom.GetComponent<RoomContent>().transform);
                            tempGameObject.transform.SetLocalPositionAndRotation(new Vector3(0.5f, 2.5f, 0), Quaternion.identity);
                        }
                    }

                }
            }
        }

        LockedDoor[] _door = GetComponentsInChildren<LockedDoor>();
        List<int> indexCheck = new List<int>();
        for (int i = 0; i < _door.Length; ++i)
        {
            for (int j = 0; j < _door.Length; ++j)
            {
                if (!indexCheck.Contains(i) && !indexCheck.Contains(j) && i != j)
                {
                    if (Vector3.Distance(_door[i].transform.position, _door[j].transform.position) <= 1)
                    {
                        indexCheck.Add(i);
                        indexCheck.Add(j);
                        _door[i].SetConnectedDoor(_door[j]);
                        _door[j].SetConnectedDoor(_door[i]);
                    }
                }
            }
        }
    }

    void TracePath(Vector2 origin, int length, Room.roomType lastRoomType, int pathID)
    {
        Vector2 originBackup = origin;
        int chances = 0;
        List<Vector2> previousRoomLocations = new List<Vector2>();
        for (int i = 0; i < length; i++)
        {
            bool foundSuitableNeighbour = false;
            List<int> direction = new List<int>();
            direction.Add(pathID);
            direction.Add(pathID);
            direction.Add(pathID);
            direction.Add(1); //Left
            direction.Add(2); //Right
            direction.Add(3); //Up
            direction.Add(4); //Down
            while (foundSuitableNeighbour == false && direction.Count > 0)
            {
                chances++;
                if (chances > length * 5)
                {
                    Debug.Log("Abort");
                    roomLayout[(int)origin.x, (int)origin.y].type = lastRoomType;
                    error = true;
                    return;

                }
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
                                if (CheckIfNeighboursFull(origin) == false)
                                    previousRoomLocations.Add(new Vector2(origin.x, origin.y));
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
                                if (CheckIfNeighboursFull(origin) == false)
                                    previousRoomLocations.Add(new Vector2(origin.x, origin.y));
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
                                if (CheckIfNeighboursFull(origin) == false)
                                    previousRoomLocations.Add(new Vector2(origin.x, origin.y));
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
                                if (CheckIfNeighboursFull(origin) == false)
                                    previousRoomLocations.Add(new Vector2(origin.x, origin.y));
                            }
                        }
                        else
                        {
                            direction.Remove(4);
                        }
                        break;
                }
            }
            if (direction.Count <= 0)
            {
                //if (previousRoomLocations.Count > 0)
                //{
                //    int newOriginInt = UnityEngine.Random.Range(0, previousRoomLocations.Count);
                //    origin = previousRoomLocations[newOriginInt];
                //    previousRoomLocations.RemoveAt(newOriginInt);
                //    //direction.Add(1); //Left
                //    //direction.Add(2); //Right
                //    //direction.Add(3); //Up
                //    //direction.Add(4); //Down
                //}
                //else
                //{
                roomLayout[(int)origin.x, (int)origin.y].type = lastRoomType;
                foundSuitableNeighbour = true;
                return;
                //}
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
        return Room.roomType.Regular;
    }

    bool CheckIfNeighboursFull(Vector2 tile)
    {
        if (tile.x - 1 >= 0 && (roomLayout[(int)tile.x - 1, (int)tile.y]) == null)
            return false;
        else if (tile.x + 1 < layoutWidth - 1 && roomLayout[(int)tile.x + 1, (int)tile.y] == null)
            return false;
        else if (tile.y - 1 >= 0 && (roomLayout[(int)tile.x, (int)tile.y - 1]) == null)
            return false;
        else if (tile.y + 1 < layoutHeight - 1 && roomLayout[(int)tile.x, (int)tile.y + 1] == null)
            return false;
        return true;
    }



    //bool CheckIfNextToTile(Vector2 tileToExamine, Vector2 tileToAvoid)
    //{
    //    if (tileToExamine.x - 1 >= 0 && tileToAvoid == new Vector2(tileToExamine.x-1, tileToExamine.y))
    //        return true;
    //    else if (tileToExamine.x + 1 < layoutWidth && tileToAvoid == new Vector2(tileToExamine.x+1, tileToExamine.y))
    //        return true;
    //    else if (tileToExamine.y - 1 >= 0 && tileToAvoid == new Vector2(tileToExamine.x - 1, tileToExamine.y))
    //        return true;
    //    else if (tileToExamine.y + 1 < layoutHeight && tileToAvoid == new Vector2(tileToExamine.x - 1, tileToExamine.y))
    //        return true;
    //    return false;
    //}
}