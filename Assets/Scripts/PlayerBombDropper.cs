using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBombDropper : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform bombDropPoint;
    public int maxBombs = 3;

    private int bombsRemaining;
    private InputAction dropBombAction;

    private void OnEnable()
    {
        bombsRemaining = maxBombs;
        var playerInput = GetComponent<PlayerInput>();
        dropBombAction = playerInput.actions["DropBomb"];
        dropBombAction.performed += OnDropBomb;
    }

    private void OnDisable()
    {
        if (dropBombAction != null)
            dropBombAction.performed -= OnDropBomb;
    }

    private void OnDropBomb(InputAction.CallbackContext context)
    {
        if (bombsRemaining <= 0) return;

        Instantiate(bombPrefab, bombDropPoint.position, bombDropPoint.rotation);
        bombsRemaining--;
    }
}
