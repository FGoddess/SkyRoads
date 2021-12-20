using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 1f, 2f);
    }

    private void SpawnAsteroid()
    {
        Instantiate(_asteroidPrefab, new Vector3(Random.Range(-3.4f, 3.4f), 0, 30f), Quaternion.identity);
    }
}
