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
    GameObject door1, door2, door3, door4;
    Room roomScript;

    List<GameObject> itemSpawn;
    List<GameObject> itemGameObjectSpawn;
    public float distanceMinBetweenObjects;
    public int comptToSpawn;
    public bool showGizmo;
    //public Room _room;
    private Vector3 rightDoorPosition = new Vector3(9.5f, -0.5f, 0);
    private Vector3 leftDoorPosition = new Vector3(-9.5f, -0.5f, 0);
    private Vector3 upDoorPosition = new Vector3(-0.5f, 4.5f, 0);
    private Vector3 downDoorPosition = new Vector3(-0.5f, -4.5f, 0);

    private void Awake()
    {
        itemSpawn = new List<GameObject>();
        itemGameObjectSpawn = new List<GameObject>();
    }
    private void Start()
    {
        Refresh();
    }

    private void OnEnable()
    {
        boxCollider2D = this.GetComponent<BoxCollider2D>();
    }
    public void Spawn()
    {
        Quaternion rotationDoor = doorPrefab.transform.rotation;
        //if (roomScript.doors.Contains(Room.doorDirection.Up))
        //    door3 = Instantiate<GameObject>(door, upDoorPosition, rotationDoor, this.transform);
        //if (roomScript.doors.Contains(Room.doorDirection.Down))
        //    door4 = Instantiate<GameObject>(door, downDoorPosition, rotationDoor, this.transform);
        //if (roomScript.doors.Contains(Room.doorDirection.Left))
        //    door2 = Instantiate<GameObject>(door, leftDoorPosition, rotationDoor, this.transform);
        //if (roomScript.doors.Contains(Room.doorDirection.Right))
        //    door1 = Instantiate<GameObject>(door, rightDoorPosition, rotationDoor, this.transform);

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
                    while (Physics2D.OverlapCircle(newPos, distanceMinBetweenObjects, layerMask))
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
                        //Quaternion rotation = itemList[i].item.transform.rotation;
                        GameObject temp = Instantiate(itemList[i].item.entity, newPos, Quaternion.identity);
                        var lootComponent = temp.GetComponent<LootTable>();
                        if (lootComponent)
                            (lootComponent as ILootable).SetData(itemList[i].item);
                        else
                        {
                            lootComponent = temp.AddComponent<LootTable>();
                            (lootComponent as ILootable).SetData(itemList[i].item);
                        }
                        itemSpawn.Add(temp);
                    }
                }
            }
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
                    while (Physics2D.OverlapCircle(newPos, distanceMinBetweenObjects, layerMask))
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
                        GameObject temp = Instantiate(itemGameObjectList[i].item, newPos, Quaternion.identity);
                        itemGameObjectSpawn.Add(temp);
                    }
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (showGizmo)
        {
            for (int i = 0; i < itemSpawn.Count; ++i)
            {
                Handles.color = Color.blue;
                Handles.DrawWireDisc(itemSpawn[i].transform.position, itemSpawn[i].transform.forward, distanceMinBetweenObjects);
            }

            for (int i = 0; i < itemGameObjectSpawn.Count; ++i)
            {
                Handles.color = Color.green;
                Handles.DrawWireDisc(itemGameObjectSpawn[i].transform.position, itemGameObjectSpawn[i].transform.forward, distanceMinBetweenObjects);
            }
        }
    }

    public void Refresh()
    {
        Delete();
        Spawn();
    }
    public void Delete()
    {
        //if (door1)
        //    DestroyImmediate(door1);
        //if (door2)
        //    DestroyImmediate(door2);
        //if (door3)
        //    DestroyImmediate(door3);
        //if (door4)
        //    DestroyImmediate(door4);
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

}