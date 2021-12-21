using System.Collections;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    private float _zSpawnPosition = 120f;
    private float _boundsXPosition = 3.4f;

    private float _timeBetweenSpawn = 3f;
    private float _minTimeBetweenSpawn = 0.5f;

    [SerializeField] private float _asteroidsSpeed = 25f;
    [SerializeField] private float _asteroidsMaxSpeed = 40f;

    [SerializeField] private float _speedPerDifficultyTick = 1f;

    private void OnEnable()
    {
        DifficultyManager.Instance.MultiplierChanged += OnDifficultyMultiplierChanged;
    }

    private void OnDisable()
    {
        DifficultyManager.Instance.MultiplierChanged -= OnDifficultyMultiplierChanged;
    }

    private void OnDifficultyMultiplierChanged(float value)
    {
        _timeBetweenSpawn -= value;
        _asteroidsSpeed += _speedPerDifficultyTick;

        if(_timeBetweenSpawn <= _minTimeBetweenSpawn)
        {
            _timeBetweenSpawn = _minTimeBetweenSpawn;
        }

        if(_asteroidsSpeed > _asteroidsMaxSpeed)
        {
            _asteroidsSpeed = _asteroidsMaxSpeed;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnAsteroid());
    }

    private IEnumerator SpawnAsteroid()
    {
        while(true)
        {
            yield return new WaitForSeconds(_timeBetweenSpawn);

            var asteroid = ObjectPool.Instance.GetPooledObject();

            if (asteroid != null)
            {
                asteroid.transform.position = new Vector3(Random.Range(-_boundsXPosition, _boundsXPosition), 0.5f, _zSpawnPosition);

                var temp = asteroid.GetComponent<Asteroid>();

                temp.MoveSpeed = _asteroidsSpeed;

                var randomScale = Random.Range(0.3f, 0.6f);

                asteroid.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                asteroid.SetActive(true);
            }
        }
    }
}
