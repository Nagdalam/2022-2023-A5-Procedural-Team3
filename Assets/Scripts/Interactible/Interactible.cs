using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public EEntities entity = EEntities.CHEST_1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Interact()
    {
        Debug.Log($"Interacted with {name}");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Inventory colliderInventory))
        {
            Debug.Log($"In range of {name} waiting to be opened");
        }
    }
}
