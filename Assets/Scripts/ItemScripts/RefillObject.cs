using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Refill Object", menuName = "Inventroy System/Items/Refill")]
public class RefillObject : ItemObject
{
    public bool isFilled;
    public float restoreHealthValue;
    public void Awake()
    {
        type = ItemType.Refill;
    }
}
