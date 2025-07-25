using UnityEngine;

public class MarbleSpawner : MonoBehaviour
{
    public GameObject marblePrefab;
    public float spawnInterval = 0.4f;   // 25 per 10 s

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            _timer = 0;
            Instantiate(marblePrefab, transform.position, Quaternion.identity);
        }
    }
}