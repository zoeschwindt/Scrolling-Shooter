using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.Cinemachine;

public class FirstPersonToggle : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;

    public CinemachineInputAxisController axisController;

    private PlayerInput playerInput;
    private InputAction toggleViewAction;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        toggleViewAction = playerInput.actions.FindAction("ToggleView");
        toggleViewAction.performed += ctx => ToggleCamera();

        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
        axisController.enabled = true;
    }

    void ToggleCamera()
    {
        StartCoroutine(CambiarCamaraConDelay());
    }

    private IEnumerator CambiarCamaraConDelay()
    {
        playerInput.DeactivateInput();

        bool isFirstPersonActive = firstPersonCamera.activeSelf;

        // Desactiva los ejes de Cinemachine al salir de 3ra persona
        axisController.enabled = isFirstPersonActive;

        firstPersonCamera.SetActive(!isFirstPersonActive);
        thirdPersonCamera.SetActive(isFirstPersonActive);

        yield return new WaitForSeconds(0.3f);

        playerInput.ActivateInput();
    }
}
