using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackObject : ScriptableObject
{
    List<Backpack> Pockets = new List<Backpack>();
    [TextArea(15, 20)]
    public string description;

}

[System.Serializable]
public class Backpack
{
    public int NUMBER_WIDTH;
    public int NUMBER_HEIGHT;
    public int NUMBER_DEPTH;

    private float fromDist;
    private float fromHeight;
    public float traceDistance { 
        get { return fromDist; } 
        set { value = Mathf.Max(NUMBER_WIDTH, NUMBER_HEIGHT, NUMBER_DEPTH) + 1; }
    }

    public float traceHieght {
        get { return fromHeight; }
        set { value = Mathf.Min(NUMBER_WIDTH, NUMBER_HEIGHT, NUMBER_DEPTH) - 1; }
    }

    public void BackpackInstantiate() { 
        
    }

}
