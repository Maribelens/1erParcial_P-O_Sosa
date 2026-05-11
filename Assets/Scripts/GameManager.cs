using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI clickerCountText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private Button clickerButton;

    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 10f;

    private int _clickCount;
    private int _highScore;
    private float _timeRemaining;
    private bool _gameRunning;

    [Header("Ads")]
    [SerializeField] private AdsManager adsManager;

    private void Awake()
    {
        clickerButton.onClick.AddListener(OnClickButton);
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        ResetUI();
    }

    private void Update()
    {
        if (!_gameRunning) return;

        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            EndGame();
        }

        timerText.text = $"Tiempo: {Mathf.CeilToInt(_timeRemaining)}";
    }

    private void OnClickButton()
    {
        if (!_gameRunning)
        {
            StartGame();
            return;
        }

        _clickCount++;
        clickerCountText.text = $"{_clickCount:00} Clicks";
    }

    private void StartGame()
    {
        _clickCount = 0;
        _timeRemaining = gameDuration;
        _gameRunning = true;
        instructionText.gameObject.SetActive(false);
        clickerCountText.text = "00 Clicks";
    }

    private void EndGame()
    {
        _gameRunning = false;
        instructionText.gameObject.SetActive(true);
        instructionText.text = "¡Juego terminado!";

        bool isNewRecord = _clickCount > _highScore;

        if (_clickCount > _highScore)
        {
            _highScore = _clickCount;
            PlayerPrefs.SetInt("HighScore", _highScore);
            PlayerPrefs.Save();
        }

        highScoreText.text = $"High Score: {_highScore}";
        timerText.text = "Tiempo: 0";

#if UNITY_ANDROID
        if (!isNewRecord)
            adsManager.ShowInterstitial();
#endif
    }

    private void ResetUI()
    {
        timerText.text = $"Tiempo: {Mathf.CeilToInt(gameDuration)}";
        clickerCountText.text = "00 Clicks";
        highScoreText.text = $"High Score: {_highScore}";
        instructionText.text = "Toca el boton para empezar";
    }

    public void OnRewardButtonPressed()
    {
#if UNITY_ANDROID
        adsManager.ShowRewarded(() =>
        {
            gameDuration += 2f;
            Debug.Log($"Reward otorgado. Próxima duración: {gameDuration}s");
        });
#endif
    }

    private void OnDestroy()
    {
        clickerButton.onClick.RemoveAllListeners();
    }
}
