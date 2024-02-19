using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PickUpItemType { Thrash, EBomb, Key }

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody), typeof(MeshRenderer))]
public abstract class PickUpItem : MonoBehaviour
{
    private PickUpItemType itemType;
    private float damage;
    private bool isStackable;

    internal PickUpItemType ItemType { get => itemType; set => itemType = value; }
    public float Damage { get => damage; set => damage = value; }
    public bool IsStackable { get => isStackable; set => isStackable = value; }


    private void OnCollisionEnter(Collision collision)
    {
        CollisionInteraction(collision);
    }

    public abstract void PickUpInteracion();

    public abstract void DropInteraction();

    public abstract void CollisionInteraction(Collision collision);
}
