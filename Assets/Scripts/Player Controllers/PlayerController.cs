using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(Player))]
public class PlayerController : MonoBehaviour
{
    #region Events

    public delegate void ControllerInteractions();
    public delegate void InventoryControllerInteractions(int index);

    public ControllerInteractions OnControllerInteractionKey;
    public ControllerInteractions OnControllerDropKey;
    public ControllerInteractions OnControllerThrowKey;
    public InventoryControllerInteractions OnControllerItem1Key;
    public InventoryControllerInteractions OnControllerItem2Key;
    public InventoryControllerInteractions OnControllerItem3Key;

    #endregion

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private bool canPickUp;
    private bool canDrop;
    private bool canThrow;

    private CharacterController controller;
    private Player player;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform camTransform;

    private void Start()
    {
        inputManager = InputManager.Instance;
        controller = GetComponent<CharacterController>();
        player = GetComponent<Player>();
        camTransform = Camera.main.transform;

        player.OnPlayerCollidedState += UpdateCanPickUp;
        player.OnItemEquippedState += UpdateCanDrop;
        player.OnItemEquippedState += UpdateCanThrow;
    }

    private void OnDisable()
    {
        player.OnPlayerCollidedState -= UpdateCanPickUp;
        player.OnItemEquippedState -= UpdateCanDrop;
        player.OnItemEquippedState -= UpdateCanThrow;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = camTransform.forward * move.z + camTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (inputManager.PlayerInteracted() && canPickUp) OnControllerInteractionKey?.Invoke();
        if (inputManager.PlayerDropped() && canDrop) OnControllerDropKey?.Invoke();
        if (inputManager.PlayerThrowed() && canThrow) OnControllerThrowKey?.Invoke();
        if (inputManager.PlayerSelectedItem1()) OnControllerItem1Key?.Invoke(0);
        if (inputManager.PlayerSelectedItem2()) OnControllerItem2Key?.Invoke(1);
        if (inputManager.PlayerSelectedItem3()) OnControllerItem3Key?.Invoke(2);
    }

    private void UpdateCanPickUp(bool state)
    {
        canPickUp = state;
    }

    private void UpdateCanDrop(bool state)
    {
        canDrop = state;
    }

    private void UpdateCanThrow(bool state)
    {
        canThrow = state;
    }
}
