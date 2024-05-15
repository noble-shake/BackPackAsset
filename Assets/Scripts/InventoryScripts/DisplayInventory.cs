using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemNodeList 
{
    List<Node> Nodes = new List<Node>();

    public void addNode(Node _node)
    {
        if (Nodes.Count == 0)
        {
            _node.isRoot = true;
        }
        Nodes.Add(_node);
    }

    public Node getNode(string _direction) {
        for (int inum = 0; inum < Nodes.Count; inum++)
        {
            if (Nodes[inum].directionCheck() == _direction) return Nodes[inum];
        }

        return null;
    }
}

[System.Serializable]
public class Node
{
    string direction;
    public GameObject Part;
    public bool isRoot;

    public string directionCheck() {
        return direction;
    }
}

public class DisplayInventory : MonoBehaviour
{
    [Header("BackPack Status")]
    [SerializeField] float X_SPACE;
    [SerializeField] float Y_SPACE;
    [SerializeField] float Z_SPACE;
    [SerializeField] int NUMBER_OF_WIDTH; // x
    [SerializeField] int NUMBER_OF_HEIGHT; //y
    [SerializeField] int NUMBER_OF_DEPTH; //z

    [SerializeField] GameObject BackPackObject;
    [SerializeField] Vector3 BackpackPos;
    [SerializeField] GameObject SlotPrefab;

    [SerializeField] Camera BackpackTracer;
    [SerializeField] float traceSpeed;
    [SerializeField] float traceDistance;
    [SerializeField] float traceTime;
    [SerializeField] float TracerHeight;

    [SerializeField] bool storeMode;

    [SerializeField] Vector3 restoreMeshPos;
    [SerializeField] float restoreMeshTime;

    [Header("Inventory Cube")]
    [SerializeField] int[,,] inventorySlotCube;
    [SerializeField] Vector3 inventoryCursor;


    // Start is called before the first frame update
    void Start()
    {
        inventoryCursor = Vector3.zero;
        inventorySlotCube = new int[NUMBER_OF_WIDTH, NUMBER_OF_HEIGHT, NUMBER_OF_DEPTH];

        traceTime = 0f;
        traceDistance = Mathf.Max(NUMBER_OF_WIDTH, NUMBER_OF_HEIGHT, NUMBER_OF_DEPTH) + 1.5f;
        TracerHeight = Mathf.Min(NUMBER_OF_WIDTH, NUMBER_OF_HEIGHT, NUMBER_OF_DEPTH) + 1.5f;

        createSlots();
        setAnchorInventory();

        BackpackTracer.transform.localPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * traceSpeed * traceTime), TracerHeight / traceDistance, Mathf.Sin(Mathf.Deg2Rad * traceSpeed * traceTime)) * traceDistance + BackpackPos;
        Vector3 vec = BackpackPos - BackpackTracer.transform.position;
        vec.Normalize();
        Quaternion rot = Quaternion.LookRotation(vec);
        BackpackTracer.transform.rotation = rot;
    }

    private void createSlots() {
        BackpackPos = BackPackObject.transform.position;
        for (int i = 0; i < NUMBER_OF_WIDTH; i++)
        {
            for (int j = 0; j < NUMBER_OF_HEIGHT; j++)
            {
                for (int k = 0; k < NUMBER_OF_DEPTH; k++)
                {
                    GameObject slot = Instantiate(SlotPrefab, GetPosition(i, j, k), Quaternion.identity, BackPackObject.transform);
                    
                }
            }
        }
    }

    private void setAnchorInventory() {
        if (NUMBER_OF_WIDTH % 2 == 0)
        {
            BackpackPos.x += NUMBER_OF_WIDTH / 2 - 0.5f;
        }
        else
        {
            BackpackPos.x += (NUMBER_OF_WIDTH - 1) / 2f;
        }

        if (NUMBER_OF_HEIGHT % 2 == 0)
        {
            BackpackPos.y -= NUMBER_OF_HEIGHT / 2 + 0.5f;
        }
        else
        {
            BackpackPos.y -= (NUMBER_OF_HEIGHT - 1) / 2f;
        }

        if (NUMBER_OF_DEPTH % 2 == 0)
        {
            BackpackPos.z += NUMBER_OF_DEPTH / 2f - 0.5f;
        }
        else
        {
            BackpackPos.z += (NUMBER_OF_DEPTH - 1) / 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
       

        Debug.DrawLine(BackpackPos, new Vector3(-NUMBER_OF_WIDTH + BackpackPos.x, BackpackPos.y, NUMBER_OF_DEPTH + BackpackPos.z), Color.red);
        Debug.DrawLine(BackpackPos, new Vector3(NUMBER_OF_WIDTH + BackpackPos.x, BackpackPos.y, NUMBER_OF_DEPTH + BackpackPos.z), Color.red);
        Debug.DrawLine(BackpackPos, new Vector3(NUMBER_OF_WIDTH + BackpackPos.x, BackpackPos.y, -NUMBER_OF_DEPTH + BackpackPos.z), Color.red);
        Debug.DrawLine(BackpackPos, new Vector3(-NUMBER_OF_WIDTH + BackpackPos.x, BackpackPos.y, -NUMBER_OF_DEPTH + BackpackPos.z), Color.red);

        if (!BackPackObject.activeSelf) return;

        BackpackRotating();
        ItemStoreCheck();

    }

    private void BackpackRotating() {

        if (storeMode) return;

        if (Input.GetKey(KeyCode.Q))
        {
            traceTime += Time.unscaledDeltaTime;
            BackpackTracer.transform.localPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * traceSpeed * traceTime), TracerHeight / traceDistance, Mathf.Sin(Mathf.Deg2Rad * traceSpeed * traceTime)) * traceDistance + BackpackPos;
            Vector3 vec = BackpackPos - BackpackTracer.transform.position;
            vec.Normalize();
            Quaternion rot = Quaternion.LookRotation(vec);
            BackpackTracer.transform.rotation = rot;

        }
        if (Input.GetKey(KeyCode.E))
        {
            traceTime -= Time.unscaledDeltaTime;
            BackpackTracer.transform.localPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * traceSpeed * traceTime), TracerHeight / traceDistance, Mathf.Sin(Mathf.Deg2Rad * traceSpeed * traceTime)) * traceDistance + BackpackPos;
            Vector3 vec = BackpackPos - BackpackTracer.transform.position;
            vec.Normalize();
            Quaternion rot = Quaternion.LookRotation(vec);
            BackpackTracer.transform.rotation = rot;
        }

        Debug.DrawLine(BackpackPos, new Vector3(Mathf.Cos(Mathf.Deg2Rad * traceSpeed * traceTime), TracerHeight / traceDistance, Mathf.Sin(Mathf.Deg2Rad * traceSpeed * traceTime)) * traceDistance + BackpackPos, Color.blue);

        if (traceTime > 360f || traceTime < -360f)
        {
            traceTime = 0f;
        }

        if (traceSpeed * traceTime > 360f || traceSpeed * traceTime < -360f)
        {
            traceTime = 0f;
        }
    }

    private void ItemStoreCheck() {
        

        if (Input.GetKeyDown(KeyCode.Z) && !storeMode)
        {
            storeMode = true;
            restoreMeshPos = BackpackTracer.transform.localPosition;
            restoreMeshTime = traceTime;
        }

        if (Input.GetKeyDown(KeyCode.X) && storeMode)
        {
            storeMode = false;
            BackpackTracer.transform.localPosition = restoreMeshPos;
            traceTime = restoreMeshTime;
            meshCanceled();
        }

        if (storeMode)
        {
            meshSelect();
        }
    }

    private void meshCanceled()
    {
        
    }

    private void meshSelect() {
        


        // float deg = traceSpeed * traceTime;
        // if (deg > -180f && deg < -60f)
        // NUMBER_OF_WIDTHz
        // NUMBER_OF_DEPTH

        // Vector3 pt1 = new Vector3(-NUMBER_OF_WIDTH/2, 0, -NUMBER_OF_DEPTH/2);
        // Vector3 pt2 = new Vector3(NUMBER_OF_WIDTH/2, 0, -NUMBER_OF_DEPTH/2);
        // Vector3 pt3 = new Vector3(NUMBER_OF_WIDTH/2, 0, NUMBER_OF_DEPTH/2);
        // Vector3 pt4 = new Vector3(-NUMBER_OF_WIDTH/2, 0, NUMBER_OF_DEPTH/2);



    }

    public void earnItemCheck(ItemObject _item)
    { 
        
    }

    public Vector3 GetPosition(int i, int j, int k) {
        return new Vector3(X_SPACE * (i % NUMBER_OF_WIDTH), (-Y_SPACE * (j % NUMBER_OF_HEIGHT)), (Z_SPACE * (k % NUMBER_OF_DEPTH)));
    }
}
