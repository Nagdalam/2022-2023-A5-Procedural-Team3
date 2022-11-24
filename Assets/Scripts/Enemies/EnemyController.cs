using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerController player;
    private Rigidbody2DMovement movement;
    public Life life;
    private RoomEnemiesManager enemiesManager;
    public bool enemyInvok;

    private void Awake()
    {
        
    }

    private void Start()
    {
       
        movement = GetComponent<Rigidbody2DMovement>();
        //if (GetComponentInParent<RoomEnemiesManager>() != null)
        //{
        //    //Debug.Log("Has correct parent");
        //    //GetComponentInParent<RoomEnemiesManager>().AddEnemyToRoom(this);
        //}
        if (enemyInvok)
        {
            enabled = true;
        }
        else
            enabled = false;


    }
    private void Update()
    {
        if (enemyInvok && !player)
            player = FindObjectOfType<PlayerController>();
    }

    //private void OnDisable()
    //{
    //    movement.SetDirection(Vector2.zero);
    //}

    private void FixedUpdate()
    {
        if (player == null)
            return;

        movement.SetDirection(player.transform.position - transform.position);
    }

    private void OnDestroy()
    {
        //GetComponentInParent<RoomEnemiesManager>().RemoveEnemyFromRoom(this);
    }
}
