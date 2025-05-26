using UnityEngine;

public class MoveBoss : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool shouldMove = false;
    private float targetZ;

    [Header("Spawn de enemigos extra")]
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    private GameObject spawnedEnemy1;
    private GameObject spawnedEnemy2;

    [Header("Panel de victoria")]
    public GameObject victoryPanel;

    [Header("Audio")]
    public AudioSource victoryMusic;  

    public void StartMoving(float targetZPosition)
    {
        shouldMove = true;
        targetZ = targetZPosition;

        if (enemyPrefab1 != null && spawnPoint1 != null)
        {
            spawnedEnemy1 = Instantiate(enemyPrefab1, spawnPoint1.position, spawnPoint1.rotation);
        }

        if (enemyPrefab2 != null && spawnPoint2 != null)
        {
            spawnedEnemy2 = Instantiate(enemyPrefab2, spawnPoint2.position, spawnPoint2.rotation);
        }
    }

    [System.Obsolete]
    void Update()
    {
        if (!shouldMove) return;

        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, targetZ);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            shouldMove = false;
        }

        if (spawnedEnemy1 == null && spawnedEnemy2 == null && victoryPanel != null && !victoryPanel.activeSelf)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

          
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audioSrc in allAudioSources)
            {
                if (audioSrc.isPlaying)
                    audioSrc.Stop();
            }

            
            if (victoryMusic != null)
                victoryMusic.Play();
        }
    }
}
