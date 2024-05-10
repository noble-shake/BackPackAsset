using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public float X_SPACE;
    public float Y_SPACE;
    public float Z_SPACE;
    public int NUMBER_OF_WIDTH; // x
    public int NUMBER_OF_HEIGHT; //y
    public int NUMBER_OF_DEPTH; //z
    public GameObject BackPackObject;
    public Vector3 BackpackPos;
    public GameObject SlotPrefab;

    public Camera BackpackTracer;
    public float traceSpeed;
    public float traceDistance;
    public float traceTime;
    public float TracerHeight;


    // Start is called before the first frame update
    void Start()
    {
        traceTime = 0f;
        traceSpeed = 100f;
        traceDistance = Mathf.Max(NUMBER_OF_WIDTH, NUMBER_OF_HEIGHT, NUMBER_OF_DEPTH);
        TracerHeight = Mathf.Min(NUMBER_OF_WIDTH, NUMBER_OF_HEIGHT, NUMBER_OF_DEPTH);

        BackpackPos = BackPackObject.transform.position;
        for (int i = 0; i < NUMBER_OF_WIDTH; i++) {
            for (int j = 0; j < NUMBER_OF_HEIGHT; j++) {
                for (int k = 0; k < NUMBER_OF_DEPTH; k++) {
                    GameObject slot = Instantiate(SlotPrefab, GetPosition(i, j, k), Quaternion.identity, BackPackObject.transform);
                }
            }
        }

        BackpackPos.x += NUMBER_OF_WIDTH / 2;
        BackpackPos.y -= NUMBER_OF_HEIGHT/ 2;
        BackpackPos.z += NUMBER_OF_DEPTH/ 2;

        BackpackTracer.transform.localPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * traceSpeed * traceTime), TracerHeight / traceDistance, Mathf.Sin(Mathf.Deg2Rad * traceSpeed * traceTime)) * traceDistance + BackpackPos;
        Vector3 vec = BackpackPos - BackpackTracer.transform.position;
        vec.Normalize();
        Quaternion rot = Quaternion.LookRotation(vec);
        BackpackTracer.transform.rotation = rot;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (traceTime > 360f || traceTime < -360f) {
            traceTime = 0f;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            traceTime += Time.deltaTime;
            BackpackTracer.transform.localPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * traceSpeed * traceTime), TracerHeight / traceDistance,  Mathf.Sin(Mathf.Deg2Rad * traceSpeed * traceTime)) * traceDistance + BackpackPos;
            Vector3 vec = BackpackPos - BackpackTracer.transform.position;
            vec.Normalize();
            Quaternion rot = Quaternion.LookRotation(vec);
            BackpackTracer.transform.rotation = rot;

        }
        if (Input.GetKey(KeyCode.E))
        {
            traceTime -= Time.deltaTime;
            BackpackTracer.transform.localPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * traceSpeed * traceTime), TracerHeight / traceDistance, Mathf.Sin(Mathf.Deg2Rad * traceSpeed * traceTime)) * traceDistance + BackpackPos;
            Vector3 vec = BackpackPos - BackpackTracer.transform.position;
            vec.Normalize();
            Quaternion rot = Quaternion.LookRotation(vec);
            BackpackTracer.transform.rotation = rot;
        }
    }

    public Vector3 GetPosition(int i, int j, int k) {
        return new Vector3(X_SPACE * (i % NUMBER_OF_WIDTH), (-Y_SPACE * (j % NUMBER_OF_HEIGHT)), (Z_SPACE * (k % NUMBER_OF_DEPTH)));
    }
}
