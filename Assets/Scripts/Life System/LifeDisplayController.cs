using UnityEngine;
using UnityEngine.UI;

public class LifeDisplayController : MonoBehaviour
{
    public Image heartPrefab;
    public int maxHeartDisplay;
    
    private void Start()
    {
        for (var i = 0; i < maxHeartDisplay; i++)
        {
            Instantiate(heartPrefab, transform);
        }

        DungeonManager.current.onPlayerSpawn += SetPlayerData;

        /*var playerLife = FindObjectOfType<PlayerController>().GetComponent<Life>();
        ChangeHeartsCount(playerLife.startLife);
        playerLife.onHealthChange.AddListener(ChangeHeartsCount);*/
    }

    private void FixedUpdate()
    {
        /*var player = FindObjectOfType<PlayerController>();
        if (!player)
            return;*/
    }
    private void ChangeHeartsCount(uint newCount)
    {
        var childrenCount = transform.childCount;
        if(newCount == childrenCount)
            return;

        for (var i = 0; i < maxHeartDisplay; i++)
        {
            transform.GetChild(i).gameObject.SetActive(newCount>i);
        }
    }

    void SetPlayerData()
    {
        var playerLife = DungeonManager.current.player.life;
        ChangeHeartsCount(playerLife.startLife);
        playerLife.onHealthChange.AddListener(ChangeHeartsCount);
    }
}
