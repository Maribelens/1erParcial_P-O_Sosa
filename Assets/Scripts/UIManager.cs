using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private CanvasGroup gameCanvasGroup;
    [SerializeField] private CanvasGroup creditsCanvasGroup;

    [Header("Game Elements")]
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button creditsCloseButton;

    [Header("Layout")]
    [SerializeField] private RectTransform background;

    private bool _isPortrait;

    private void Awake()
    {
        AddButtonsListeners();
        SetStateCanvasGroup(gameCanvasGroup, true);
        SetStateCanvasGroup(creditsCanvasGroup, false);
    }

    private void AddButtonsListeners()
    {
        creditsButton.onClick.AddListener(ShowCredits);
        creditsCloseButton.onClick.AddListener(HideCredits);
    }

    void Update()
    {
        bool portrait = Screen.height > Screen.width;

        // Solo recalcula si cambió la orientación
        if (portrait != _isPortrait)
        {
            _isPortrait = portrait;
            AdaptLayout(portrait);
        }
    }

    private void AdaptLayout(bool portrait)
    {
        if (portrait)
        {
            // Márgenes para portrait
            background.offsetMin = new Vector2(60, 60);
            background.offsetMax = new Vector2(-60, -60);
        }
        else
        {
            // Márgenes para landscape (más espacio lateral)
            background.offsetMin = new Vector2(120, 40);
            background.offsetMax = new Vector2(-120, -40);
        }
    }

    private void SetStateCanvasGroup(CanvasGroup canvasGroup, bool state)
    {
        // Activa o desactiva visibilidad e interacción de un panel
        canvasGroup.alpha = state ? 1 : 0;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }

    private void ShowCredits()
    {
        SetStateCanvasGroup(creditsCanvasGroup, true);
        creditsButton.interactable = false;
    }

    private void HideCredits()
    {
        SetStateCanvasGroup(creditsCanvasGroup, false);
        creditsButton.interactable = true;
    }

    private void OnDestroy()
    {
        RemoveButtonsListeners();
    }

    private void RemoveButtonsListeners()
    {
        creditsButton.onClick.RemoveAllListeners();
        creditsCloseButton.onClick.RemoveAllListeners();
    }
}


//using UnityEngine;
//using UnityEngine.UI;

//public class AppController : MonoBehaviour
//{
//    [Header("Home")]
//    [SerializeField] private CanvasGroup homeScreen;

//    [Header("App Buttons")]
//    [SerializeField] private Button app1Button;
//    [SerializeField] private Button app2Button;
//    [SerializeField] private Button app3Button;
//    [SerializeField] private Button app4Button;

//    [Header("Windows")]
//    [SerializeField] private CanvasGroup app1Window;
//    [SerializeField] private CanvasGroup app2Window;
//    [SerializeField] private CanvasGroup app3Window;
//    [SerializeField] private CanvasGroup app4Window;

//    [Header("Close Buttons")]
//    [SerializeField] private Button closeButton1;
//    [SerializeField] private Button closeButton2;
//    [SerializeField] private Button closeButton3;
//    [SerializeField] private Button closeButton4;

//    private CanvasGroup currentApp = null;

//    private void Awake()
//    {
//        AddButtonsListeners();
//        SetStateCanvasGroup(homeScreen, true);
//        CloseAllWindows();
//    }

//    private void AddButtonsListeners()
//    {
//        app1Button.onClick.AddListener(() => OpenApp(app1Window));
//        app2Button.onClick.AddListener(() => OpenApp(app2Window));
//        app3Button.onClick.AddListener(() => OpenApp(app3Window));
//        app4Button.onClick.AddListener(() => OpenApp(app4Window));

//        closeButton1.onClick.AddListener(CloseCurrentApp);
//        closeButton2.onClick.AddListener(CloseCurrentApp);
//        closeButton3.onClick.AddListener(CloseCurrentApp);
//        closeButton4.onClick.AddListener(CloseCurrentApp);
//    }

//    private void OpenApp(CanvasGroup app)
//    {
//        if (currentApp == app) return;

//        SetStateCanvasGroup(homeScreen, false);
//        SetStateCanvasGroup(app, true);
//        currentApp = app;

//        app.GetComponent<IApp>()?.OnAppOpen();
//    }

//    public void CloseCurrentApp()
//    {
//        if (currentApp == null) return;

//        currentApp.GetComponent<IApp>()?.OnAppClose();

//        SetStateCanvasGroup(currentApp, false);
//        SetStateCanvasGroup(homeScreen, true);
//        currentApp = null;
//    }

//    private void CloseAllWindows()
//    {
//        SetStateCanvasGroup(app1Window, false);
//        SetStateCanvasGroup(app2Window, false);
//        SetStateCanvasGroup(app3Window, false);
//        SetStateCanvasGroup(app4Window, false);
//    }

//    private void SetStateCanvasGroup(CanvasGroup canvasGroup, bool state)
//    {
//        // Activa o desactiva visibilidad e interacción de un panel
//        canvasGroup.alpha = state ? 1 : 0;
//        canvasGroup.interactable = state;
//        canvasGroup.blocksRaycasts = state;
//    }

//    private void OnDestroy()
//    {
//        RemoveButtonsListeners();
//    }

//    private void RemoveButtonsListeners()
//    {
//        app1Button.onClick.RemoveAllListeners();
//        app2Button.onClick.RemoveAllListeners();
//        app3Button.onClick.RemoveAllListeners();
//        app4Button.onClick.RemoveAllListeners();

//        closeButton1.onClick.RemoveAllListeners();
//        closeButton2.onClick.RemoveAllListeners();
//        closeButton3.onClick.RemoveAllListeners();
//        closeButton4.onClick.RemoveAllListeners();
//    }
//}
