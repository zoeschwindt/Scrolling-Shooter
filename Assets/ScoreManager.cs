using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText; // Arrastrás el TMP Text desde la escena

    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoint()
    {
        score++;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = score.ToString();
    }
}
