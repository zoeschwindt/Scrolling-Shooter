using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonToggle : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;

    private PlayerInput playerInput;
    private InputAction toggleViewAction;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        toggleViewAction = playerInput.actions.FindAction("ToggleView");

        toggleViewAction.performed += ctx => ToggleCamera();

        
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
    }

    void ToggleCamera()
    {
        bool isFirstPersonActive = firstPersonCamera.activeSelf;

        firstPersonCamera.SetActive(!isFirstPersonActive);
        thirdPersonCamera.SetActive(isFirstPersonActive);
    }
}
