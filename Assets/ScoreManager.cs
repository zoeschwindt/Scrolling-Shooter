using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int enemyScore = 0;
    public int bombScore = 0;

    public TMP_Text enemyScoreText;  // Texto para puntaje enemigos
    public TMP_Text bombScoreText;   // Texto para puntaje bombas

    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            UpdateEnemyScoreUI();
            UpdateBombScoreUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Suma punto de enemigo
    public void AddEnemyPoint()
    {
        enemyScore++;
        UpdateEnemyScoreUI();
    }

    // Suma punto de bomba
    public void AddBombPoint()
    {
        bombScore++;
        UpdateBombScoreUI();
    }

    void UpdateEnemyScoreUI()
    {
        if (enemyScoreText != null)
            enemyScoreText.text = enemyScore.ToString();
    }

    void UpdateBombScoreUI()
    {
        if (bombScoreText != null)
            bombScoreText.text = bombScore.ToString();
    }
}
