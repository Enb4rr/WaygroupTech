using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance {  get { return instance; } }

    private PlayerControls playerControls;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }


        playerControls = new PlayerControls();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playerControls?.Enable();
    }

    private void OnDisable()
    {
        playerControls?.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool PlayerInteracted()
    {
        return playerControls.Player.Interact.triggered;
    }

    public bool PlayerDropped()
    {
        return playerControls.Player.Drop.triggered;
    }

    public bool PlayerThrowed()
    {
        return playerControls.Player.Throw.triggered;
    }

    public bool PlayerSelectedItem1()
    {
        return playerControls.Player.Item1.triggered;
    }

    public bool PlayerSelectedItem2()
    {
        return playerControls.Player.Item2.triggered;
    }

    public bool PlayerSelectedItem3()
    {
        return playerControls.Player.Item3.triggered;
    }
}
