using UnityEngine;

public class CameraControlManager : MonoBehaviour
{
    public static CameraControlManager Instance;

    public bool puedeRotar = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
