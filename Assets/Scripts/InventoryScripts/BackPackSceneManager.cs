using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackPackSceneManager : MonoBehaviour
{
    // Inventory Data Get.

    [SerializeField] GameObject BackPackObject;

    private void Awake()
    {
        BackPackObject = GameManager.instance.getInventoryObject();
        BackPackObject.SetActive(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


}
