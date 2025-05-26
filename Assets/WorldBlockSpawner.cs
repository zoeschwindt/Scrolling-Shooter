using UnityEngine;

public class WorldBlockSpawner : MonoBehaviour
{
    public static WorldBlockSpawner Instance { get; private set; }

    [SerializeField] private GameObject[] blocks; // Bloques: 0 = inicial, 1+ = aleatorios
    [SerializeField] private int initialBlockCount = 3;
    [SerializeField] private float blockLength = 40f;

    [SerializeField] private GameObject[] enemyPrefabs;

    private Transform lastBlock;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Instanciar el bloque 0 una sola vez al inicio
        GameObject firstBlock = Instantiate(blocks[0], transform.position, Quaternion.identity);
        lastBlock = firstBlock.transform;

        // Generar los siguientes bloques sin incluir el bloque 0
        for (int i = 0; i < initialBlockCount; i++)
        {
            SpawnBlock();
        }
    }

    public void SpawnBlock()
    {
        if (blocks.Length <= 1)
        {
            Debug.LogWarning("Solo hay un bloque (el inicial). Agregá más prefabs al array 'blocks' en el Inspector.");
            return;
        }

        
        int prefabIndex = Random.Range(1, blocks.Length);
        Vector3 spawnPosition = lastBlock.position + Vector3.forward * blockLength;

        GameObject newBlock = Instantiate(blocks[prefabIndex], spawnPosition, Quaternion.identity);
        lastBlock = newBlock.transform;

  
        SpawnEnemies(newBlock);
    }

    private void SpawnEnemies(GameObject block)
    {
        foreach (Transform child in block.transform)
        {
            if (child.name.StartsWith("EnemySpawnPoint"))
            {
                if (enemyPrefabs.Length == 0) return;

                int enemyIndex = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[enemyIndex], child.position, child.rotation);
            }
        }
    }
}
