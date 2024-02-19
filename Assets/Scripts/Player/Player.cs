using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(PlayerController))]
public class Player : MonoBehaviour
{
    #region Events

    public delegate void PlayerInteractionEvents(PickUpItem item);
    public delegate void PlayerStateEvents(bool active);
    public delegate PickUpItem EquipEvent(int index);

    public PlayerInteractionEvents OnPickUpItem;
    public PlayerInteractionEvents OnDropItem;
    public PlayerInteractionEvents OnThrowItem;

    public PlayerStateEvents OnPlayerCollidedState;
    public PlayerStateEvents OnItemEquippedState;
    public PlayerStateEvents OnPlayerDead;

    public EquipEvent OnEquipItem;

    #endregion

    [SerializeField] private int healthPoints = 100;
    private PlayerController playerController;
    private SphereCollider capsuleCollider;
    private EquipmentUI equipmentUI;
    private bool canPickItem;
    private bool itemEquipped;
    private PickUpItem item;
    private PickUpItem equippedItem = null;
    private int equippedItemIndex = -1;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        capsuleCollider = GetComponent<SphereCollider>();
        equipmentUI = EquipmentUI.Instance;

        playerController.OnControllerInteractionKey += PickUpItem;
        playerController.OnControllerDropKey += DropItem;
        playerController.OnControllerThrowKey += ThrowItem;
        playerController.OnControllerItem1Key += EquipItem;
        playerController.OnControllerItem2Key += EquipItem;
        playerController.OnControllerItem3Key += EquipItem;
    }

    private void OnDisable()
    {
        playerController.OnControllerInteractionKey -= PickUpItem;
        playerController.OnControllerDropKey -= DropItem;
        playerController.OnControllerThrowKey -= ThrowItem;
        playerController.OnControllerItem1Key -= EquipItem;
        playerController.OnControllerItem2Key -= EquipItem;
        playerController.OnControllerItem3Key -= EquipItem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            canPickItem = true;
            OnPlayerCollidedState?.Invoke(true);
            item = other.gameObject.GetComponent<PickUpItem>();
            equipmentUI.HighlightPickUp(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            canPickItem = false;
            OnPlayerCollidedState?.Invoke(false);
            equipmentUI.HighlightPickUp(false);
        }
    }

    private void PickUpItem()
    {
        OnPickUpItem(item);
        item.PickUpInteracion();

        canPickItem = false;
        OnPlayerCollidedState?.Invoke(false);
        equipmentUI.HighlightPickUp(false);
    }

    private void DropItem()
    {
        if (itemEquipped)
        {
            OnDropItem(item);
            item.DropInteraction();
            equippedItem = OnEquipItem?.Invoke(equippedItemIndex);
            if(equippedItem == null)
            {
                itemEquipped = false;
                equippedItemIndex = -1;
                equipmentUI.UnnequipWeapon();
            }
        }
    }

    private void ThrowItem()
    {
        Debug.Log("Throw item");
    }

    private void EquipItem(int index)
    {
        equippedItem = OnEquipItem?.Invoke(index);
        if(equippedItem != null)
        {
            itemEquipped = true;
            equippedItemIndex = index;
            equipmentUI.EquipWeapon(equippedItem.ItemType.ToString());
            OnItemEquippedState?.Invoke(true);
        }
        else
        {
            itemEquipped = false;
            equippedItemIndex = -1;
            equipmentUI.UnnequipWeapon();
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints < 0)
        {
            healthPoints = 0;
            OnPlayerDead?.Invoke(true);
        }
    }
}
