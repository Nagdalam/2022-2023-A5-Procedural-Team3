using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContent : MonoBehaviour
{
    [SerializeField] public Room myRoom;
    public SpriteRenderer mySprite;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = gameObject.GetComponent<SpriteRenderer>();

        switch (myRoom.type)
        {
            case (Room.roomType.Regular):
                mySprite.color = Color.white;
                break;
            case (Room.roomType.Boss):
                mySprite.color = Color.red;
                break;
            case (Room.roomType.Recovery):
                mySprite.color = Color.green;
                break;
            case (Room.roomType.Item):
                mySprite.color = Color.cyan;
                break;
            case (Room.roomType.Miniboss):
                mySprite.color = Color.magenta;
                break;
            case (Room.roomType.Spawn):
                mySprite.color = Color.blue;
                break;
        }
    }

    

    // Update is called once per frame
    void Update()
    {

    }

}
