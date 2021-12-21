using System.Collections.Generic;
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

    [SerializeField] private Image _deathPanel;
    [SerializeField] private Image _startPanel;

    private List<Text> _scores;

    private int _deathScreenMinTextSize = 30;
    private int _deathScreenMaxTextSize = 60;

    private float _ySpasing = 100f;

    private Vector2 _newTextPosition = new Vector2(800f, 100f);

    private void Start()
    {
        _scores = new List<Text>() { _scoreText, _highScoreText, _asteroidsText, _timeText };
        if (!_startPanel.gameObject.activeInHierarchy)
        {
            _startPanel.gameObject.SetActive(true);
        }
    }

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
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ShowDeathScreen()
    {
        _deathPanel.gameObject.SetActive(true);

        float y = 0;

        foreach (var text in _scores)
        {
            text.rectTransform.sizeDelta = _newTextPosition;

            text.resizeTextMinSize = _deathScreenMinTextSize;
            text.resizeTextMaxSize = _deathScreenMaxTextSize;

            text.alignment = TextAnchor.MiddleCenter;

            text.rectTransform.localPosition = new Vector3(0, y, 0);

            y += _ySpasing;

        }
    }
}
