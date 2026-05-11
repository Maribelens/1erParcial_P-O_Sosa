using UnityEngine;

public class LayoutAdapter : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private RectTransform timerText;
    [SerializeField] private RectTransform clickButton;
    [SerializeField] private RectTransform clickCountText;
    [SerializeField] private RectTransform highScoreText;

    [Header("Portrait settings")]
    [SerializeField] private Vector2 portraitSize = new Vector2(980, 1800);
    [SerializeField] private Vector2 buttonPortraitPos = new Vector2(0, 0);

    [Header("Landscape settings")]
    [SerializeField] private Vector2 landscapeSize = new Vector2(1800, 900);
    [SerializeField] private Vector2 buttonLandscapePos = new Vector2(-400, 0);

    private RectTransform _rectTransform;
    private ScreenOrientation _lastOrientation;
    private bool _lastWasPortrait;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        ApplyLayout();
    }

    void Update()
    {
        bool isPortrait = Screen.height > Screen.width;
        if (isPortrait != _lastWasPortrait)
            ApplyLayout();
    }

    private void ApplyLayout()
    {
        bool isPortrait = Screen.height > Screen.width;
        _lastWasPortrait = isPortrait;

        if (isPortrait)
            ApplyPortrait();
        else
            ApplyLandscape();
    }

    private void ApplyPortrait()
    {
        // Background ocupa casi toda la pantalla verticalmente
        _rectTransform.sizeDelta = portraitSize;

        // Timer arriba al centro
        timerText.anchorMin = new Vector2(0.5f, 1f);
        timerText.anchorMax = new Vector2(0.5f, 1f);
        timerText.anchoredPosition = new Vector2(0, -100);

        // Botón al centro
        clickButton.anchorMin = new Vector2(0.5f, 0.5f);
        clickButton.anchorMax = new Vector2(0.5f, 0.5f);
        clickButton.anchoredPosition = buttonPortraitPos;

        // Clicks debajo del botón
        clickCountText.anchorMin = new Vector2(0.5f, 0.5f);
        clickCountText.anchorMax = new Vector2(0.5f, 0.5f);
        clickCountText.anchoredPosition = new Vector2(0, -280);

        // High score debajo de clicks
        highScoreText.anchorMin = new Vector2(0.5f, 0.5f);
        highScoreText.anchorMax = new Vector2(0.5f, 0.5f);
        highScoreText.anchoredPosition = new Vector2(0, -400);
    }

    private void ApplyLandscape()
    {
        // Background más ancho que alto
        _rectTransform.sizeDelta = landscapeSize;

        // Timer arriba al centro
        timerText.anchorMin = new Vector2(0.5f, 1f);
        timerText.anchorMax = new Vector2(0.5f, 1f);
        timerText.anchoredPosition = new Vector2(0, -80);

        // Botón a la izquierda del centro
        clickButton.anchorMin = new Vector2(0.5f, 0.5f);
        clickButton.anchorMax = new Vector2(0.5f, 0.5f);
        clickButton.anchoredPosition = buttonLandscapePos;

        // Clicks a la derecha del botón
        clickCountText.anchorMin = new Vector2(0.5f, 0.5f);
        clickCountText.anchorMax = new Vector2(0.5f, 0.5f);
        clickCountText.anchoredPosition = new Vector2(300, 60);

        // High score debajo de clicks en landscape
        highScoreText.anchorMin = new Vector2(0.5f, 0.5f);
        highScoreText.anchorMax = new Vector2(0.5f, 0.5f);
        highScoreText.anchoredPosition = new Vector2(300, -60);
    }
}
