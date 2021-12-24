using System;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private float _difficultyPerSeconds = 0.1f;
    [SerializeField] private float _timeToIncreaseDifficulty = 5f;

    public event Action<float> MultiplierChanged;

    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _timeToIncreaseDifficulty)
        {
            MultiplierChanged?.Invoke(_difficultyPerSeconds);
            _timer = 0f;
        }
    }
}
