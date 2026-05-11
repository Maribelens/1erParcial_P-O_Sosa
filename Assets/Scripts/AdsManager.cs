using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [Header("Game ID")]
    private string gameId;
    [SerializeField] private bool isTestMode = true;

    [Header("Ad Managers")]
    [SerializeField] private BannerManager bannerManager;
    [SerializeField] private InterstinitialManager interstinitialManager;
    [SerializeField] private RewardedManager rewardedManager;

    private void Awake()
    {
#if UNITY_ANDROID
        gameId = "6111710";
#elif UNITY_IOS
    gameId = "6111711";
#elif UNITY_EDITOR
        gameId = "6111710";
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(gameId, isTestMode, this);
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads inicialization completed");
        bannerManager.LoadBanner();
        interstinitialManager.LoadInterstinitial();
        rewardedManager.LoadRewarded();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads inicialization error: {error.ToString()} - {message}");
    }

    public void ShowInterstitial() => interstinitialManager.ShowInterstinitial();
    public void ShowRewarded(Action onRewarded) => rewardedManager.ShowRewarded(onRewarded);
}