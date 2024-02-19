using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PickUpItem
{
    [SerializeField] private float throwDamage;
    [SerializeField] private bool stackable;

    private SphereCollider _collider;
    private Rigidbody _rb;
    private MeshRenderer _renderer;

    private void Start()
    {
        ItemType = PickUpItemType.Key;
        Damage = throwDamage;
        IsStackable = stackable;

        _collider = gameObject.GetComponent<SphereCollider>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _renderer = gameObject.GetComponent<MeshRenderer>();
    }

    public override void PickUpInteracion()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
        _rb.useGravity = false;
    }

    public override void DropInteraction()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
        _rb.useGravity = true;
    }

    public override void CollisionInteraction(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
