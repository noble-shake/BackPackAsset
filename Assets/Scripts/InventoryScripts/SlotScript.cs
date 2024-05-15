using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    [SerializeField] int oriW;
    [SerializeField] int oriH;
    [SerializeField] int oriD;

    [SerializeField] bool importMode;

    [SerializeField] bool CursorOn;
    [SerializeField] bool isEmpty;
    [SerializeField] Item ItemObject;

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material DefaultMat;
    [SerializeField] Material DisableMat;
    [SerializeField] Material AbleMat;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = DefaultMat;
    }

    // Update is called once per frame
    void Update()
    {
        if (importMode)
        {
            if (CursorOn && !isEmpty)
            {
                meshRenderer.material = DisableMat;
            }
            else if (CursorOn && isEmpty)
            {
                meshRenderer.material = AbleMat;
            }
            else
            {
                meshRenderer.material = DefaultMat;
            }
        }
        else
        {
            if (CursorOn)
            {
                meshRenderer.material = AbleMat;
            }
            else
            {
                meshRenderer.material = DefaultMat;
            }
        }

    }

    public void cursorOn() {
        CursorOn = true;
    }

    public void cursorOff()
    {
        CursorOn = false;
    }

    public void importObject(Item _item) {
        ItemObject = _item;
        isEmpty = false;
    }

    public Item exportObject() {
        return ItemObject;
    }

    public void deleteObject() {
        ItemObject = null;
        isEmpty = true;
    }

    public Vector3 getPosition()
    {
        return new Vector3(oriW, oriH, oriD);
    }

    public void setPosition(int _w, int _h, int _d)
    {
        oriW = _w;
        oriH = _h;
        oriD = _d;
    }
}
