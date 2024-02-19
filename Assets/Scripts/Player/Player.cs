using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(PlayerController))]
public class Player : MonoBehaviour
{
    #region Events

    public delegate void PlayerInteractionEvents(PickUpItem item);
    public delegate void PlayerStateEvents(bool active);

    public PlayerInteractionEvents OnPickUpItem;
    public PlayerInteractionEvents OnDropItem;
    public PlayerInteractionEvents OnThrowItem;

    public PlayerStateEvents OnPlayerCollidedState;
    public PlayerStateEvents OnItemEquippedState;

    #endregion

    [SerializeField] private float healthPoints = 100f;
    private PlayerController playerController;
    private SphereCollider capsuleCollider;
    private bool canPickItem;
    private bool itemEquipped;
    private PickUpItem item;
    private PickUpItem equippedItem = null;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        capsuleCollider = GetComponent<SphereCollider>();

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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            canPickItem = false;
            OnPlayerCollidedState?.Invoke(false);
        }
    }

    private void PickUpItem()
    {
        OnPickUpItem(item);
        item.PickUpInteracion();
    }

    private void DropItem()
    {
        Debug.Log("Droped item");
    }

    private void ThrowItem()
    {
        Debug.Log("Throw item");
    }

    private void EquipItem(int index)
    {
        Debug.Log("Equipped item " + index);
        OnItemEquippedState?.Invoke(true);
    }
}
