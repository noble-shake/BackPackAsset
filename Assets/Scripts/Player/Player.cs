using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rigid;
    public InventoryObject inventory;

    public Camera inventoryTracer;
    public Camera mainCam;


    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("item")) {
            grabItem(other);

        }

    }

    private void grabItem(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var item = other.GetComponent<Item>();
            if (item)
            {
                // inventory.AddItem(item.item, 1);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        moving();
        
        invenCheck();
    }

    private void invenCheck()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.instance.InventoryCheck();
        }
    }



    private void moving()
    {
        if (Input.GetKey(KeyCode.W)) {
            
        }
        if (Input.GetKey(KeyCode.S))
        {

        }
        if (Input.GetKey(KeyCode.A))
        {

        }
        if (Input.GetKey(KeyCode.D))
        {

        }
    }
}
