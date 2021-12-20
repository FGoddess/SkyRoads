using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;

    private float _zSpawnPosition = 30f;
    private float _boundsXPosition = 3.4f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 1f, 2f);
    }

    private void SpawnAsteroid()
    {
        var asteroid = ObjectPool.Instance.GetPooledObject();

        if (asteroid != null)
        {
            asteroid.transform.position = new Vector3(Random.Range(-_boundsXPosition, _boundsXPosition), 0.5f, _zSpawnPosition);

            var randomScale = Random.Range(0.3f, 0.6f);

            asteroid.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            asteroid.SetActive(true);
        }
    }
}
