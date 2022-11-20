using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    
    public enum doorDirection { Up, Left, Right, Down};
    public enum roomType {Boss, Item, Regular, Empty, Spawn};
    public List<Room.doorDirection> doors = new List<Room.doorDirection>();
    public roomType type;

    public Room()
    {
        //firstDoor = directionFirstDoor;
    }
}
 