using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    //snake manager
    public SnakeMovement SM;
    public float distanceSnakeBarrier;

    //block prefab
    public GameObject BlockPrefab;

    //time to spawndelegate management
    public float minSpawnTime;
    public float maxSpawnTime;
    private float thisTime;
    private float randomTime;

    //snake value for spawning
    public int minSpawnDistance;
    Vector2 previousSnakePosition;
    public List<Vector3> SimpleBoxPositions = new bool.Lang.List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        thisTime = 0;

        SpawnBarrier();
        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
