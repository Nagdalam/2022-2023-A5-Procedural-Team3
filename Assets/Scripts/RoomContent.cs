using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomContent : MonoBehaviour
{
    [SerializeField] public Room myRoom;
    public GameObject doorLeft, doorRight, doorUp, doorDown;
    public GameObject secretReward;
    public GameObject player;
    public GameObject crackedWalls;

    public CinemachineVirtualCamera vcam;

    private ItemSpawner spawner;
    public GameObject bigDoor;
    bool hasBigDoor = false;

    // Start is called before the first frame update
    void Start()
    {
       
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
            else if (door == Room.doorDirection.BigDoor)
            {
                bigDoor.SetActive(true);
                hasBigDoor = true;
            }
        }

        if(hasBigDoor == false)
        {
            Destroy(bigDoor);
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
        if (myRoom.type == Room.roomType.Secret || myRoom.type == Room.roomType.SecretEntrance)
        {
            crackedWalls.SetActive(true);
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
