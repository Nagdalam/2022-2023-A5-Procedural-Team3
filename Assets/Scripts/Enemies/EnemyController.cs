using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    private Rigidbody2DMovement movement;
    public Life life;
    private RoomEnemiesManager enemiesManager;
    public bool enemyInvok;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //player = FindObjectOfType<PlayerController>().transform;
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

    //private void OnDisable()
    //{
    //    movement.SetDirection(Vector2.zero);
    //}

    private void FixedUpdate()
    {
        if (player == null)
            return;

        movement.SetDirection(player.position - transform.position);
    }

    private void OnDestroy()
    {
        //GetComponentInParent<RoomEnemiesManager>().RemoveEnemyFromRoom(this);
    }
}
