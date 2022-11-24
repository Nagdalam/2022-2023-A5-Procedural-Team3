using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContent : MonoBehaviour
{
    [SerializeField] public Room myRoom;
    public SpriteRenderer mySprite;
    public GameObject doorLeft, doorRight, doorUp, doorDown;
    public GameObject secretReward;
    public GameObject player;

    private ItemSpawner spawner;

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
        foreach (Room.doorDirection door in myRoom.doors)
        {
            if (door == Room.doorDirection.Left)
            {
                doorLeft.SetActive(false);
            }
            else if (door == Room.doorDirection.Right)
            {
                doorRight.SetActive(false);
            }
            else if (door == Room.doorDirection.Up)
            {
                doorUp.SetActive(false);
            }
            else if (door == Room.doorDirection.Down)
            {
                doorDown.SetActive(false);
            }
        }

        if(myRoom.type == Room.roomType.Secret)
        {
            secretReward.SetActive(true);
        }
        if(myRoom.type == Room.roomType.Spawn)
        {
            GameObject newPlayer = Instantiate(player, secretReward.transform.position, Quaternion.identity);
            newPlayer.transform.parent = gameObject.transform;
        }
        if (myRoom.type == Room.roomType.Boss)
        {
            Debug.Log("Boss");
        }
        if (myRoom.type == Room.roomType.Empty)
        {
            Debug.Log("Empty");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpawner(ItemSpawner spawn)
    {
        spawner = spawn;
        spawner.SetRoomScript(myRoom);
    }
}
