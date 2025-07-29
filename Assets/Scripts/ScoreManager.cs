using UnityEngine;
using TMPro;
using System.Collections;

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

    [Header("Feedback visual y sonoro")]
    public GameObject enemyPointImage; // Imagen que titila
    public AudioSource enemyPointSound; // Sonido al sumar punto
    public float flashDuration = 0.3f;

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

        // Mostrar imagen que titila y reproducir sonido
        if (enemyPointImage != null)
            StartCoroutine(FlashImage());

        if (enemyPointSound != null)
            enemyPointSound.Play();

        if (!bossMoved && enemyScore >= 15)
        {
            bossMoved = true;

            if (boss != null)
            {
                MoveBoss mover = boss.GetComponent<MoveBoss>();
                if (mover != null)
                    mover.StartMoving(bossTargetZ);
            }

            if (WorldBlockSpawner.Instance != null)
                WorldBlockSpawner.Instance.StopSpawning();
        }
    }

    IEnumerator FlashImage()
    {
        enemyPointImage.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        enemyPointImage.SetActive(false);
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
