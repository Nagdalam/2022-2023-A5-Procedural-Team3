using UnityEngine;

public class BulletController : InflictDamage
{
    private Rigidbody2DMovement movement;
    public bool bulletSecretRoom;
    public LayerMask _parent;
    public string _parentName;

    private void Awake()
    {
        movement = GetComponent<Rigidbody2DMovement>();
    }

    private void Start()
    {
       movement.SetDirection(transform.up); 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (!collision.gameObject.CompareTag(_parentName))
        {
            if (bulletSecretRoom && collision.gameObject.layer == LayerMask.GetMask("SecretDoor"))
            {
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.TryGetComponent<Life>(out Life life))
            {
                life.TakeDamage(damageOnTouch);
            }
            

            Destroy(gameObject);
        }
        
        
    }
        
    

}
