using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int enemyScore = 0;
    public int bombScore = 0;

    public TMP_Text enemyScoreText;  
    public TMP_Text bombScoreText;   

    public static ScoreManager instance;
    public GameObject boss; 
    public float bossTargetZ = 10f; 
    private bool bossMoved = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
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

    
    public void AddEnemyPoint()
    {
        enemyScore++;
        UpdateEnemyScoreUI();

        if (!bossMoved && enemyScore >= 15)
        {
            bossMoved = true;

            
            if (boss != null)
            {
                MoveBoss mover = boss.GetComponent<MoveBoss>();
                if (mover != null)
                {
                    mover.StartMoving(bossTargetZ);
                }
            }

           
            if (WorldBlockSpawner.Instance != null)
            {
                WorldBlockSpawner.Instance.StopSpawning();
            }

            
        }
    }


   
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
