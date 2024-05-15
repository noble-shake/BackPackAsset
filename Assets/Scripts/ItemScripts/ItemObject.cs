using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType 
{
    Food,
    Equipment,
    Refill,
    Default,
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;

    int gridW;
    int gridH;
    int gridD;
    Vector3 itemSize;

    int[,,] itemGrid;

    protected virtual void ItemInit() {
        itemGrid = new int[gridW, gridH, gridD];
    }

    public void itemClockRotate(Vector3 _axis)
    {
        prefab.transform.Rotate(_axis, 90f);
    }

    public void itemInverseClockRotate(Vector3 _axis)
    {
        prefab.transform.Rotate(_axis, -90f);
    }

    public int[,,] getItemSize() {
        return itemGrid;
    }
}
