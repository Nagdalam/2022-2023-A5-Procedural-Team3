using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct ItemStruct
{
    public DataEntity item;
    public int nb;
    public float rarity;
}

[System.Serializable]
public struct ItemGameObjectStruct
{
    public GameObject item;
    public int nb;
    public float rarity;
}

[ExecuteInEditMode]
public class ItemSpawner : MonoBehaviour
{
    public List<ItemStruct> itemList;
    public List<ItemGameObjectStruct> itemGameObjectList;
    BoxCollider2D boxCollider2D;
    public GameObject doorPrefab;
    public GameObject openDoorPrefab;
    GameObject door1, door2, door3, door4;
    Room roomScript;

    List<GameObject> itemSpawn;
    List<GameObject> itemGameObjectSpawn;
    public float distanceMinBetweenObjects, distanceFromDoor;
    public int comptToSpawn;
    public bool showGizmo;
    //public Room _room;
    private Vector3 rightDoorPosition = new Vector3(9.5f, -0.5f, 0);
    private Vector3 leftDoorPosition = new Vector3(-9.5f, -0.5f, 0);
    private Vector3 upDoorPosition = new Vector3(0.5f, 4.5f, 0);
    private Vector3 downDoorPosition = new Vector3(0.5f, -4.5f, 0);

    public bool boss1Spawn;
    public GameObject boss2;

    private void Awake()
    {
        itemSpawn = new List<GameObject>();
        itemGameObjectSpawn = new List<GameObject>();
    }
    private void Start()
    {
        //Refresh();
    }

    private void OnEnable()
    {
        boxCollider2D = this.GetComponent<BoxCollider2D>();
    }
    public void Spawn()
    {
        Quaternion rotationDoor = doorPrefab.transform.rotation;
        if (roomScript.doors.Contains(Room.doorDirection.Up))
        {
            door3 = Instantiate<GameObject>(doorPrefab, upDoorPosition, rotationDoor, this.transform);
            door3.transform.localPosition = upDoorPosition;
        }
        if (roomScript.doors.Contains(Room.doorDirection.Down))
        {
            door4 = Instantiate<GameObject>(doorPrefab, downDoorPosition, rotationDoor, this.transform);
            door4.transform.localPosition = downDoorPosition;
        }
        if (roomScript.doors.Contains(Room.doorDirection.Left))
        {
            door2 = Instantiate<GameObject>(doorPrefab, leftDoorPosition, rotationDoor, this.transform);
            door2.transform.localPosition = leftDoorPosition;
        }
        if (roomScript.doors.Contains(Room.doorDirection.Right))
        {
            door1 = Instantiate<GameObject>(doorPrefab, rightDoorPosition, rotationDoor, this.transform);
            door1.transform.localPosition = rightDoorPosition;
        }

        for (int i = 0; i < itemGameObjectList.Count; ++i)
        {
            for (int j = 0; j < itemGameObjectList[i].nb; ++j)
            {
                if (itemGameObjectList[i].rarity >= Random.Range(1, 101))
                {
                    var newPos = RandomPointInBounds(boxCollider2D.bounds);
                    int compt = 0;
                    int layerMask = ~LayerMask.GetMask("Rooms") & ~LayerMask.GetMask("NoCollision");
                    bool dontSpawn = false;
                    while (Physics2D.OverlapCircle(newPos, distanceMinBetweenObjects, layerMask)
                        || (door1 != null && Vector3.Distance(newPos, door1.transform.position) <= distanceFromDoor)
                        || (door2 != null && Vector3.Distance(newPos, door2.transform.position) <= distanceFromDoor)
                        || (door3 != null && Vector3.Distance(newPos, door3.transform.position) <= distanceFromDoor)
                        || (door4 != null && Vector3.Distance(newPos, door4.transform.position) <= distanceFromDoor))
                    {
                        newPos = RandomPointInBounds(boxCollider2D.bounds);
                        if (compt == comptToSpawn)
                        {
                            Debug.LogError("Not enough place : " + Physics2D.OverlapCircle(newPos, 1, layerMask));
                            dontSpawn = true;
                            break;
                        }
                        compt++;
                    }
                    if (!dontSpawn)
                    {
                        if (roomScript.type == Room.roomType.Boss && boss1Spawn && i == 0)
                        {
                            GameObject temp = Instantiate(boss2, newPos, Quaternion.identity);
                            temp.transform.parent = this.transform;
                            itemGameObjectSpawn.Add(temp);
                        }
                        else
                        {
                            GameObject temp = Instantiate(itemGameObjectList[i].item, newPos, Quaternion.identity);
                            temp.transform.parent = this.transform;
                            itemGameObjectSpawn.Add(temp);
                        }
                    }
                }
            }
        }

        for (int i = 0; i < itemList.Count; ++i)
        {
            for (int j = 0; j < itemList[i].nb; ++j)
            {
                if (itemList[i].rarity >= Random.Range(1, 101))
                {
                    var newPos = RandomPointInBounds(boxCollider2D.bounds);
                    int compt = 0;
                    int layerMask = ~LayerMask.GetMask("Rooms") & ~LayerMask.GetMask("NoCollision");
                    bool dontSpawn = false;
                    while (Physics2D.OverlapCircle(newPos, distanceMinBetweenObjects, layerMask)
                        || (door1 != null && Vector3.Distance(newPos, door1.transform.position) <= distanceFromDoor)
                        || (door2 != null && Vector3.Distance(newPos, door2.transform.position) <= distanceFromDoor)
                        || (door3 != null && Vector3.Distance(newPos, door3.transform.position) <= distanceFromDoor)
                        || (door4 != null && Vector3.Distance(newPos, door4.transform.position) <= distanceFromDoor))
                    {
                        newPos = RandomPointInBounds(boxCollider2D.bounds);
                        if (compt == comptToSpawn)
                        {
                            Debug.LogError("Not enough place : " + Physics2D.OverlapCircle(newPos, 1, layerMask));
                            dontSpawn = true;
                            break;
                        }
                        compt++;
                    }
                    if (!dontSpawn)
                    {
                        GameObject temp = Instantiate(itemList[i].item.entity, newPos, Quaternion.identity);
                        var lootComponent = temp.GetComponent<LootTable>();
                        if (lootComponent)
                            (lootComponent as ILootable).SetData(itemList[i].item);
                        else
                        {
                            lootComponent = temp.AddComponent<LootTable>();
                            (lootComponent as ILootable).SetData(itemList[i].item);
                        }
                        temp.transform.parent = this.transform;
                        itemSpawn.Add(temp);
                    }
                }
            }
        }
    }

    //void OnDrawGizmosSelected()
    ////{
    ////    if (showGizmo)
    ////    {
    ////        for (int i = 0; i < itemSpawn.Count; ++i)
    ////        {
    ////            Handles.color = Color.blue;
    ////            Handles.DrawWireDisc(itemSpawn[i].transform.position, itemSpawn[i].transform.forward, distanceMinBetweenObjects);
    ////        }

    ////        for (int i = 0; i < itemGameObjectSpawn.Count; ++i)
    ////        {
    ////            Handles.color = Color.green;
    ////            Handles.DrawWireDisc(itemGameObjectSpawn[i].transform.position, itemGameObjectSpawn[i].transform.forward, distanceMinBetweenObjects);
    ////        }

    ////        Handles.color = Color.red;
    ////        if (door1)
    ////            Handles.DrawWireDisc(door1.transform.position, door1.transform.forward, distanceFromDoor);
    ////        if (door2)
    ////            Handles.DrawWireDisc(door2.transform.position, door2.transform.forward, distanceFromDoor);
    ////        if (door3)
    ////            Handles.DrawWireDisc(door3.transform.position, door3.transform.forward, distanceFromDoor);
    ////        if (door4)
    ////            Handles.DrawWireDisc(door4.transform.position, door4.transform.forward, distanceFromDoor);
    ////    }
    //}

    public void Refresh()
    {
        Delete();
        Spawn();
    }
    public void Delete()
    {
        if (door1)
            DestroyImmediate(door1);
        if (door2)
            DestroyImmediate(door2);
        if (door3)
            DestroyImmediate(door3);
        if (door4)
            DestroyImmediate(door4);

        if (itemSpawn.Count != 0)
        {
            for (int i = 0; i < itemSpawn.Count; ++i)
            {
                DestroyImmediate(itemSpawn[i]);
            }
            itemSpawn.Clear();
        }
        if (itemGameObjectSpawn.Count != 0)
        {
            for (int i = 0; i < itemGameObjectSpawn.Count; ++i)
            {
                DestroyImmediate(itemGameObjectSpawn[i]);
            }
            itemGameObjectSpawn.Clear();
        }
    }
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0);
    }

    public void SetRoomScript(Room room)
    {
        roomScript = room;
        Spawn();
    }

    public void DeleteSpawnDoor(Room.doorDirection direction)
    {
        if (direction == Room.doorDirection.Up)
        {
            if (door3)
            {
                var temp = Instantiate(openDoorPrefab, door3.transform.position, door3.transform.rotation, this.transform);
                temp.transform.localPosition = upDoorPosition;
                Destroy(door3);
            }
        }
        if (direction == Room.doorDirection.Down)
        {
            if (door4)
            {
                var temp = Instantiate(openDoorPrefab, door4.transform.position, door4.transform.rotation, this.transform);
                temp.transform.localPosition = downDoorPosition;
                Destroy(door4);
            }
        }
        if (direction == Room.doorDirection.Left)
        {
            if (door2)
            {
                var temp = Instantiate(openDoorPrefab, door2.transform.position, door2.transform.rotation, this.transform);
                temp.transform.localPosition = leftDoorPosition;
                Destroy(door2);
            }
        }
        if (direction == Room.doorDirection.Right)
        {
            if (door1)
            {
                var temp = Instantiate(openDoorPrefab, door1.transform.position, door1.transform.rotation, this.transform);
                temp.transform.localPosition = rightDoorPosition;
                Destroy(door1);
            }
        }
    }
    public void DeleteDoor(Room.doorDirection direction)
    {
        if (direction == Room.doorDirection.Up)
        {
            if (door3)
                Destroy(door3);
        }
        if (direction == Room.doorDirection.Down)
        {
            if (door4)
                Destroy(door4);
        }
        if (direction == Room.doorDirection.Left)
        {
            if (door2)
                Destroy(door2);
        }
        if (direction == Room.doorDirection.Right)
        {
            if (door1)
                Destroy(door1);
        }
    }
}