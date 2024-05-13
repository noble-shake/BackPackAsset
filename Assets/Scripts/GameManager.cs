using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] Camera MainCam;
    [SerializeField] Camera BackPackCam;

    [SerializeField] GameObject inventoryObject;
    [SerializeField] GameObject inventoryUI;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        MainCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InventoryCheck() {

        if (inventoryUI.activeSelf)
        {
            closeInventory();
        }
        else
        {
            openInventory();
        }
    }

    public void openInventory() {
        Time.timeScale = 0f;
        inventoryUI.SetActive(true);
        inventoryObject.SetActive(true);
    }

    public void closeInventory()
    {
        Time.timeScale = 1f;
        inventoryUI.SetActive(false);
        inventoryObject.SetActive(false);
    }

    public GameObject getInventoryObject() {
        return inventoryObject;
    }
}
