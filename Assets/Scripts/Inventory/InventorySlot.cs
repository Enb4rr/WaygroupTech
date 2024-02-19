using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    private int slotNumber;
    private bool isFull;
    private PickUpItem item;
    private int itemCount = 0;
    private int maximum = 10;

    public int SlotNumber { get => slotNumber; set => slotNumber = value; }
    public bool IsFull { get => isFull; set => isFull = value; }
    public PickUpItem Item { get => item; set => item = value; }
    public int ItemCount { get => itemCount; set => itemCount = value; }
    public int Maximum { get => maximum;}
}
