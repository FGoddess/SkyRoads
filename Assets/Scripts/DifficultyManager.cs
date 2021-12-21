using System;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private static DifficultyManager _instance;
    public static DifficultyManager Instance => _instance;

    [SerializeField] private float _difficultyPerSeconds = 0.1f;
    [SerializeField] private float _timeToIncreaseDifficulty = 5f;

    public event Action<float> MultiplierChanged;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

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
