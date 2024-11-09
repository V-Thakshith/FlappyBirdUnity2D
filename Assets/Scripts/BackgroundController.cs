using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float startPos;
    public GameObject cam;
    public float parallaxEffect;
    public GameObject pipe;
    public float spawnDistance = 10f;
    public float obstacleSpacing = 2f;
    public float despawnDistance = 15f;
    private Queue<GameObject> obstacles = new Queue<GameObject>();
    [SerializeField] private Bird bird;
    [SerializeField] private GameObject counterPrefab;

    private float nextSpawnPosition;
    void Start()
    {
        startPos = transform.position.x;

        nextSpawnPosition = bird.transform.position.x + spawnDistance;

        // Spawn initial obstacles
        for (int i = 0; i < 5; i++)
        {
            SpawnObstacle();
            nextSpawnPosition += obstacleSpacing;
        }
    }

    private void Update()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        float birdX = bird.transform.position.x;

        // Spawn new obstacles ahead of the bird
        if (birdX + spawnDistance > nextSpawnPosition)
        {
            SpawnObstacle();
            nextSpawnPosition += obstacleSpacing;
        }

        // Despawn obstacles behind the bird
        if (obstacles.Count > 0 && obstacles.Peek().transform.position.x < birdX - despawnDistance)
        {
            Destroy(obstacles.Dequeue());
        }
    }

    private void SpawnObstacle()
    {
        Quaternion uprightRotation = Quaternion.Euler(0, 0, -180);
        Vector3 spawnPositionUp = new Vector3(nextSpawnPosition, Random.Range(4f, 6f), 0); //3
        Vector3 spawnPositionDown = new Vector3(nextSpawnPosition, Random.Range(-6f, -3.5f), 0); //-3
        GameObject newObstacleUp = Instantiate(pipe, spawnPositionUp, uprightRotation);
        GameObject newObstacleDown = Instantiate(pipe, spawnPositionDown, Quaternion.identity);
        GameObject counterInstance = Instantiate(counterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        BoxCollider2D _boxCollider = counterInstance.GetComponent<BoxCollider2D>();
        _boxCollider.size = new Vector2(0.25f, 50);
        _boxCollider.offset = new Vector2(nextSpawnPosition, -20);
        obstacles.Enqueue(newObstacleUp);
        obstacles.Enqueue(newObstacleDown);
        obstacles.Enqueue(counterInstance);
    }
}
