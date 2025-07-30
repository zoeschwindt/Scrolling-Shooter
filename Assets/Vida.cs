using UnityEngine;

public class Vida : MonoBehaviour
{

    void LateUpdate()
    {
        if (Camera.main != null)
            transform.LookAt(Camera.main.transform);
    }


}
