using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int enemyScore = 0;
    public int bombScore = 0;

    public TMP_Text enemyScoreText;  // Texto para puntaje enemigos
    public TMP_Text bombScoreText;   // Texto para puntaje bombas

    public static ScoreManager instance;
    public GameObject boss; // Asignalo en el Inspector
    public float bossTargetZ = 10f; // Posición Z hacia la que se moverá el boss
    private bool bossMoved = false;
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

        if (!bossMoved && enemyScore >= 15)
        {
            bossMoved = true;

            // Mover el boss
            if (boss != null)
            {
                MoveBoss mover = boss.GetComponent<MoveBoss>();
                if (mover != null)
                {
                    mover.StartMoving(bossTargetZ);
                }
            }

            // Detener el spawner
            if (WorldBlockSpawner.Instance != null)
            {
                WorldBlockSpawner.Instance.StopSpawning();
            }

            //// Detener el movimiento de todos los bloques
            //MoveBlock[] bloques = FindObjectsOfType<MoveBlock>();
            //foreach (MoveBlock bloque in bloques)
            //{
            //    bloque.enabled = false;
            //}
        }
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
