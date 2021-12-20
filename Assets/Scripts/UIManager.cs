using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _highScoreText;
    [SerializeField] private Text _asteroidsText;
    [SerializeField] private Text _timeText;

    public string ScoreText
    {
        get => _scoreText.text; set => _scoreText.text = $"Score: {value}";
    }

    public string HighscoreText
    {
        get => _highScoreText.text; set => _highScoreText.text = $"Highscore: {value}";
    }

    public string AsteroidsText
    {
        get => _asteroidsText.text; set => _asteroidsText.text = $"Asteroids passed: {value}";
    }

    public string TimeText
    {
        get => _timeText.text; set => _timeText.text = $"Time passed: {value}";
    }

    private void Awake()
    {
        _instance = this;
    }
}
