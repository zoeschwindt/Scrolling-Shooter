using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public float tiempo = 60f; 
    public TextMeshProUGUI textoTiempo;

    void Update()
    {
       
        tiempo -= Time.deltaTime;

        
        if (tiempo < 0)
        {
            tiempo = 0;
        }

        
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        textoTiempo.text = string.Format("{0}:{1:00}", minutos, segundos);


        if (tiempo <= 0)
        {
            SceneManager.LoadScene("Muerte"); 
    }
}
}
