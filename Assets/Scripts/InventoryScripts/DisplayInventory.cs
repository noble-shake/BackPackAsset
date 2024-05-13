using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] Material DefaultMat;
    [SerializeField] Material DisableMat;
    [SerializeField] Material AbleMat;


    // Start is called before the first frame update
    void Start()
    {
        traceTime = 0f;
        traceSpeed = 50f;
        traceDistance = Mathf.Max(NUMBER_OF_WIDTH, NUMBER_OF_HEIGHT, NUMBER_OF_DEPTH) + 1;
        TracerHeight = Mathf.Min(NUMBER_OF_WIDTH, NUMBER_OF_HEIGHT, NUMBER_OF_DEPTH) - 1;

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
            traceTime += Time.unscaledTime;
            BackpackTracer.transform.localPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * traceSpeed * traceTime), TracerHeight / traceDistance,  Mathf.Sin(Mathf.Deg2Rad * traceSpeed * traceTime)) * traceDistance + BackpackPos;
            Vector3 vec = BackpackPos - BackpackTracer.transform.position;
            vec.Normalize();
            Quaternion rot = Quaternion.LookRotation(vec);
            BackpackTracer.transform.rotation = rot;

        }
        if (Input.GetKey(KeyCode.E))
        {
            traceTime -= Time.unscaledTime;
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
