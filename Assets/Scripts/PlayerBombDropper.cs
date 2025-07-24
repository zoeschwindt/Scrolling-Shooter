using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 

public class PlayerBombDropper : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform bombDropPoint;
    public int bombsAvailable = 0;

    private InputAction dropBombAction;

    
    public TMP_Text bombCountText;

    private void OnEnable()
    {
        var playerInput = GetComponent<PlayerInput>();
        dropBombAction = playerInput.actions["DropBomb"];
        dropBombAction.performed += OnDropBomb;

        UpdateBombUI();
    }

    private void OnDisable()
    {
        if (dropBombAction != null)
            dropBombAction.performed -= OnDropBomb;
    }

    public void OnDropBomb(InputAction.CallbackContext context)
    {
        if (bombsAvailable <= 0) return;

        Instantiate(bombPrefab, bombDropPoint.position, bombDropPoint.rotation);
        bombsAvailable--;
        UpdateBombUI();
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
