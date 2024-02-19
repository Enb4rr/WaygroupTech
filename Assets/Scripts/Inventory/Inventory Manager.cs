using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int numberOfSlots;
    [SerializeField] Player player;
    private List<InventorySlot> slots;

    private GameManager gameManager;
    private InventoryUI ui;

    private void Start()
    {
        CreateSlots();

        gameManager = GameManager.Instance;
        ui = InventoryUI.Instance;
        if(player == null ) player = FindObjectOfType<Player>();

        player.OnPickUpItem += AddItem;
        player.OnDropItem += RemoveItem;
        player.OnThrowItem += RemoveItem;
        player.OnEquipItem += ReturnItemInSlot;
        gameManager.OnStart += ResetSlots;
    }

    private void OnDisable()
    {
        player.OnPickUpItem -= AddItem;
        player.OnDropItem -= RemoveItem;
        player.OnThrowItem -= RemoveItem;
        gameManager.OnStart -= ResetSlots;
    }

    private void CreateSlots()
    {
        slots = new List<InventorySlot>();
        var counter = 0;

        while (counter < numberOfSlots)
        {
            var slot = new InventorySlot();
            slot.SlotNumber = counter;
            slot.IsFull = false;
            slots.Add(slot);

            counter++;
        }
    }

    private void ResetSlots()
    {
        foreach ( var slot in slots)
        {
            slot.Item = null;
            slot.ItemCount = 0;
            slot.IsFull = false;
            ui.RemoveLabel(slot.SlotNumber);
        }
    }

    private void AddItem(PickUpItem item)
    {
        foreach (var slot in slots)
        {
            if(slot.IsFull && item.IsStackable && slot.Item.IsStackable && slot.Maximum > slot.ItemCount)
            {
                if(slot.Item.ItemType == item.ItemType)
                {
                    slot.ItemCount++;
                    ui.UpdateLabel(slot.Item, slot.ItemCount, slot.SlotNumber);
                    break;
                } 
            }
            else if (!slot.IsFull)
            {
                slot.IsFull = true;
                slot.Item = item;
                slot.ItemCount++;
                ui.UpdateLabel(slot.Item, slot.ItemCount, slot.SlotNumber);
                break;
            }
        }
    }

    private void RemoveItem(PickUpItem item)
    {
        foreach(var slot in slots)
        {
            if (slot.Item.ItemType == item.ItemType)
            {
                if (slot.ItemCount > 1)
                {
                    slot.ItemCount--;
                    ui.UpdateLabel(slot.Item, slot.ItemCount, slot.SlotNumber);
                    break;
                }
                else
                {
                    slot.Item = null;
                    slot.ItemCount = 0;
                    slot.IsFull = false;
                    ui.RemoveLabel(slot.SlotNumber);
                    break;
                }
            }
        }
    }

    private PickUpItem ReturnItemInSlot(int index)
    {
        if (slots[index].Item == null) return null;
        else return slots[index].Item;
    }
}
