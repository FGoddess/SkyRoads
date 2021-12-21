using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Transform _scoresContainer;

    [SerializeField] private float _fadeTime = 1f;

    private CanvasGroup _canvasGroup;
    private Coroutine _coroutine;

    private void Awake()
    {
        Time.timeScale = 0;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && _coroutine == null)
        {
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        float timeElapsed = 0;

        Time.timeScale = 1f;
        _scoresContainer.gameObject.SetActive(true);

        while (timeElapsed < _fadeTime)
        {
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, 0, timeElapsed / _fadeTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
