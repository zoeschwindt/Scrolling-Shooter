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

