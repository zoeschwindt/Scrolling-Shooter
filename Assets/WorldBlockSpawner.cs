using UnityEngine;

public class WorldBlockSpawner : MonoBehaviour
{

    public static WorldBlockSpawner Instance { get; private set; }

    [SerializeField] private GameObject[] blocks;
    [SerializeField] private int initialBlockCount = 3;
    [SerializeField] private float blockLength = 40f;

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
        GameObject first = Instantiate(blocks[0], transform.position, Quaternion.identity);
        lastBlock = first.transform;

        for (int i = 0; i < initialBlockCount; i++)
        {
            SpawnBlock();
        }

    }

    public void SpawnBlock()
    {

        int prefabIndex = Random.Range(1, blocks.Length);


        Vector3 spawnPosition = lastBlock.position + Vector3.forward * blockLength;

        Transform newBlock = Instantiate(blocks[prefabIndex], spawnPosition, Quaternion.identity).transform;

        lastBlock = newBlock;
    }
}
