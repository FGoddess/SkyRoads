using UnityEngine;

public class Score : MonoBehaviour
{
    private int _score = 0;
    private int _highscore;
    private float _lastUpdate;

    private int _scorePerSecond = 1;

    private const string _highscoreSaveKey = "Highscore";

    private void Start()
    {
        if (PlayerPrefs.HasKey(_highscoreSaveKey))
        {
            _highscore = PlayerPrefs.GetInt(_highscoreSaveKey);
            UIManager.Instance.HighscoreText = _highscore.ToString();
        }
        else
        {
            _highscore = 0;
            PlayerPrefs.SetInt(_highscoreSaveKey, _highscore);
        }
    }

    private void Update()
    {
        if (Time.time - _lastUpdate >= 1f)
        {
            _score += _scorePerSecond;
            _lastUpdate = Time.time;

            CheckForHighscore();

            UIManager.Instance.ScoreText = _score.ToString();
            UIManager.Instance.TimeText = Mathf.Round(Time.time).ToString();
        }
    }

    public void AddScore(int value)
    {
        _score += value;
        UIManager.Instance.ScoreText = _score.ToString();
        CheckForHighscore();
    }

    private void CheckForHighscore()
    {
        if (_score > _highscore)
        {
            _highscore = _score;
            UIManager.Instance.HighscoreText = _highscore.ToString();
        }
    }

    public void TrySaveHighScore()
    {
        if (PlayerPrefs.GetInt(_highscoreSaveKey, 0) < _highscore)
        {
            PlayerPrefs.SetInt(_highscoreSaveKey, _highscore);
            UIManager.Instance.ActivateCongratulationsText();
        }
    }

    private void OnApplicationQuit()
    {
        TrySaveHighScore();
    }
}
