using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroyer : MonoBehaviour
{
    [SerializeField] private Score _score;

    private int _scoreForAsteroid = 5;
    private float _deactivationDelay = 0.5f;

    private int _asteroidsPassed = 0;

    private Queue<IEnumerator> _coroutineQueue = new Queue<IEnumerator>();

    private void Start()
    {
        StartCoroutine(CoroutineCoordinator());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Asteroid asteroid))
        {
            _score.AddScore(_scoreForAsteroid);

            _asteroidsPassed++;
            UIManager.Instance.AsteroidsText = _asteroidsPassed.ToString();

            _coroutineQueue.Enqueue(DeactivateAsteroid(asteroid));
        }
    }

    private IEnumerator DeactivateAsteroid(Asteroid asteroid)
    {
        yield return new WaitForSeconds(_deactivationDelay);
        asteroid.gameObject.SetActive(false);
    }

    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (_coroutineQueue.Count > 0)
            {
                yield return StartCoroutine(_coroutineQueue.Dequeue());
            }
            yield return null;
        }
    }

}
