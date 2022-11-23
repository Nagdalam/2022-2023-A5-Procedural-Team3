using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room  
{
    
    public enum doorDirection { Up, Left, Right, Down};
    public enum roomType {Boss, Item, Regular, Empty, Spawn, Miniboss, Recovery};
    public List<Room.doorDirection> doors = new List<Room.doorDirection>();
    public roomType type;
    public int pathID;

    public Room(roomType myType, int id)
    {
        type = myType;
        pathID = id;
    }
}
 