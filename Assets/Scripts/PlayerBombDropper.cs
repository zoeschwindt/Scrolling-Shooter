using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerBombDropper : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform bombDropPoint;
    public int bombsAvailable = 0;

    private InputAction dropBombAction;
    private bool bombPressed = false;

    public TMP_Text bombCountText;

    private void OnEnable()
    {
        var playerInput = GetComponent<PlayerInput>();
        dropBombAction = playerInput.actions["DropBomb"];

        dropBombAction.started -= OnDropBomb;
        dropBombAction.started += OnDropBomb;

        dropBombAction.canceled -= OnDropBombRelease;
        dropBombAction.canceled += OnDropBombRelease;

        UpdateBombUI();
    }

    private void OnDisable()
    {
        if (dropBombAction != null)
        {
            dropBombAction.started -= OnDropBomb;
            dropBombAction.canceled -= OnDropBombRelease;
        }
    }

    private void OnDropBomb(InputAction.CallbackContext context)
    {
        if (bombPressed) return; // evita múltiples lanzamientos por el mismo clic
        if (bombsAvailable <= 0) return;

        Instantiate(bombPrefab, bombDropPoint.position, bombDropPoint.rotation);
        bombsAvailable--;
        bombPressed = true;

        UpdateBombUI();
    }

    private void OnDropBombRelease(InputAction.CallbackContext context)
    {
        // Se libera la bandera cuando se suelta el botón
        bombPressed = false;
    }

    public void AddBomb()
    {
        bombsAvailable++;
        UpdateBombUI();
    }

    private void UpdateBombUI()
    {
        if (bombCountText != null)
        {
            bombCountText.text = "" + bombsAvailable;
        }
    }
}
