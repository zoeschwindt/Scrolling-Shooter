using UnityEngine;

public class ViewModeSwitcher : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;

    // Las partes del cuerpo a ocultar en primera persona
    public GameObject[] bodyPartsToHide;

    public GameObject armsAndWeapon;

    private bool inFirstPerson = false;

    void Start()
    {
        SwitchToThirdPerson();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            inFirstPerson = !inFirstPerson;
            if (inFirstPerson)
                SwitchToFirstPerson();
            else
                SwitchToThirdPerson();
        }
    }

    void SwitchToFirstPerson()
    {
        firstPersonCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);

        foreach (GameObject part in bodyPartsToHide)
        {
            part.SetActive(false);
        }

        armsAndWeapon.SetActive(true);
    }

    void SwitchToThirdPerson()
    {
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);

        foreach (GameObject part in bodyPartsToHide)
        {
            part.SetActive(true);
        }

        armsAndWeapon.SetActive(false);
    }
}
