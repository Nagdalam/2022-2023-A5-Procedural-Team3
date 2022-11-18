using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct ItemStruct
{
    public GameObject item;
    public int nb;
    public float rarity;
}

[ExecuteInEditMode]
public class ItemSpawner : MonoBehaviour
{
    public List<ItemStruct> itemList = new List<ItemStruct>();
    BoxCollider2D boxCollider2D;
    List<GameObject> itemSpawn = new List<GameObject>();
    public float distanceMinBetweenObjects;
    public bool showGizmo;

    private void OnEnable()
    {
        boxCollider2D = this.GetComponent<BoxCollider2D>();
    }
    public void Spawn()
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            for (int j = 0; j < itemList[i].nb; ++j)
            {
                if (itemList[i].rarity >= Random.Range(1, 101))
                {
                    var newPos = RandomPointInBounds(boxCollider2D.bounds);
                    int compt = 0;
                    int layerMask = ~LayerMask.GetMask("Rooms") & ~LayerMask.GetMask("NoCollision");
                    bool dontSpawn=false;
                    while (Physics2D.OverlapCircle(newPos, distanceMinBetweenObjects, layerMask))
                    {
                        newPos = RandomPointInBounds(boxCollider2D.bounds);
                        compt++;
                        if (compt == 10)
                        {
                            Debug.LogError("Not enough place : " + Physics2D.OverlapCircle(newPos, 1, layerMask));
                            dontSpawn = true;
                            break;
                        }
                    }
                    if (!dontSpawn)
                    {
                        Quaternion rotation = itemList[i].item.transform.rotation;
                        GameObject temp = Instantiate<GameObject>(itemList[i].item, newPos, rotation);
                        itemSpawn.Add(temp);
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
                Handles.color = Color.green;
                Handles.DrawWireDisc(itemSpawn[i].transform.position, itemSpawn[i].transform.forward, distanceMinBetweenObjects);
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
        for (int i = 0; i < itemSpawn.Count; ++i)
        {
            DestroyImmediate(itemSpawn[i]);
        }
        itemSpawn.Clear();
    }
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0
        );
    }
}