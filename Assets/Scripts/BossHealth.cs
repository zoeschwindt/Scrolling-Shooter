using UnityEngine;
using UnityEngine.UI; 

public class BossHealth : MonoBehaviour
{
    public int hitsToDie = 4; 
    private int currentHits = 0;

    public GameObject victoryPanel; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bomba"))
        {
            currentHits++;
            Destroy(other.gameObject); 

            if (currentHits >= hitsToDie)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Debug.Log("El jefe fue destruido.");
        Destroy(gameObject); 
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true); 
        }
    }
}