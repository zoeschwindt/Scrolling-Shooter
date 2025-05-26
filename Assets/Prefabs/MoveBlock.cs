using UnityEngine;

public class MoveBlock : MonoBehaviour
{

    public float speed;
    public float offsetCleaner;

    void Start()
    {

    }


    void Update()
    {
        /*if (GameManager.gameOver) return;*/ // Si el juego terminó, no mover más

        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < offsetCleaner)
        {
            if (WorldBlockSpawner.Instance != null)
            {
                WorldBlockSpawner.Instance.SpawnBlock();
            }
            Destroy(gameObject);
        }
    }


}

