using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float zoomLerpSpeed = 10f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 15f;

    private Jugador3 controls;    
    private CinemachineCamera cam   ;
    private CinemachineOrbitalFollow orbital;   
    private Vector2 scrollDelta;

    private float targetZoom;
    private float currentZoom;

    void Start()
    {
        controls = new Jugador3 ();
        controls.Enable();
        controls.CameraControls.MouseZoom .performed += HandleMouseScroll;

        Cursor.lockState = CursorLockMode.Locked;
        cam=GetComponent<CinemachineCamera>();
        orbital = cam.GetComponent<CinemachineOrbitalFollow>();

        targetZoom = currentZoom = orbital.Radius;
    }

    private void HandleMouseScroll(InputAction.CallbackContext context)
    {
        scrollDelta = context.ReadValue<Vector2>();
        Debug.Log($"Mouse is scrolling. Value: {scrollDelta}"); 
    }


    void Update()
    {
        
        if (scrollDelta.y != 0)
        {
            if (orbital != null)
            {
                targetZoom = Mathf.Clamp(orbital.Radius - scrollDelta.y * zoomSpeed, minDistance, maxDistance);
                scrollDelta = Vector2.zero;
            }
        }

        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
        orbital.Radius = currentZoom;

    }
}
