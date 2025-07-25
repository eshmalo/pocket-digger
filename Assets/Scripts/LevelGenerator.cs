//  Assets/Scripts/LevelGenerator.cs
using UnityEngine;
using Destructible2D;

public class LevelGenerator : MonoBehaviour
{
    [Header("Core Prefabs")]
    [Tooltip("Sprite (with D2dDestructible) that becomes the diggable ground")]
    public GameObject terrainPrefab;

    [Tooltip("Prefab with Marble.cs attached")]
    public GameObject marblePrefab;

    [Tooltip("Goal bucket prefab (BoxCollider2D trigger, Tag = Goal)")]
    public GameObject bucketPrefab;

    [Header("Level Parameters")]
    [Range(10, 200)] public int marblesToWin = 20;
    public float terrainWidth  = 10f;
    public float terrainHeight = 8f;
    public Vector2 bucketPosition = new Vector2(0, -3.8f);
    public Vector2 marbleSpawnPosition = new Vector2(0, 4.5f);
    public float marbleSpawnInterval = 0.4f;

    // Internal refs
    private MarbleSpawner _spawner;
    private GameManager   _gm;

    private void Awake()
    {
        BuildTerrain();
        BuildBucket();
        BuildMarbleSpawner();

        // Hook GameManager to generated objects
        _gm = FindObjectOfType<GameManager>();
        if (_gm != null)
        {
            _gm.SetMarblesToWin(marblesToWin);
        }
    }

    /* ------------------------------------------------------------ */

    private void BuildTerrain()
    {
        var terrain = Instantiate(terrainPrefab, Vector3.zero, Quaternion.identity);
        terrain.name = "Terrain";

        // Scale terrain to desired size
        terrain.transform.localScale = new Vector3(terrainWidth, terrainHeight, 1);

        // Ensure physics collider syncs with Destructible2D alpha
        if (!terrain.TryGetComponent(out D2dCollider _))
            terrain.AddComponent<D2dCollider>();
    }

    private void BuildBucket()
    {
        var bucket = Instantiate(bucketPrefab, bucketPosition, Quaternion.identity);
        bucket.name = "GoalBucket";
        bucket.tag  = "Goal";
    }

    private void BuildMarbleSpawner()
    {
        var spawnerGO = new GameObject("MarbleSpawner");
        _spawner = spawnerGO.AddComponent<MarbleSpawner>();
        _spawner.transform.position = marbleSpawnPosition;
        _spawner.marblePrefab  = marblePrefab;
        _spawner.spawnInterval = marbleSpawnInterval;
    }
}