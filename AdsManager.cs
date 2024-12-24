using System;
using UnityEngine;
#if ADS_ENABLED
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
#endif
public class AdsManager : MonoBehaviour
{
    public string UnityAdsAndroid = "0", UnityAdsIOS = "0", AdmobAppIDAndroid, AdmobAppIDIOS, AdmobBannerAndroid, AdmobBannerIOS, AdmobInterstitialAndroid, AdmobInterstitialIOS;
    public bool testMode;
    public static bool loadedOnce = false;
#if ADS_ENABLED

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private void Start()
    {
        InitUnityAds();
#if UNITY_ANDROID
        string appId = AdmobAppIDAndroid;// "ca-app-pub-4418947252701545~6548047274";
#elif UNITY_IPHONE
            string appId = AdmobAppIDIOS;//"ca-app-pub-4418947252701545~6548047274";
#else
            string appId = "unexpected_platform";
#endif
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        this.RequestBanner();
        RequestInterstitial();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = AdmobBannerAndroid;// "ca-app-pub-4418947252701545/1492103660";
#elif UNITY_IPHONE
                string adUnitId = AdmobBannerIOS;//"ca-app-pub-4418947252701545/1492103660";
#else
                string adUnitId = "unexpected_platform";
#endif

        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnBAdLoaded;
        bannerView.OnAdFailedToLoad += HandleOnBAdFailedToLoad;
        bannerView.OnAdClosed += HandleOnBAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
    public void HandleOnBAdLoaded(object sender, EventArgs args)
    {
        bannerView.Show();
    }

    public void HandleOnBAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestBanner();
    }

    public void HandleOnBAdClosed(object sender, EventArgs args)
    {
        RequestBanner();
    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = AdmobInterstitialAndroid;// "ca-app-pub-4418947252701545/1027578548";
#elif UNITY_IPHONE
        string adUnitId = AdmobInterstitialIOS;//"ca-app-pub-4418947252701545/1027578548";
#else
        string adUnitId = "unexpected_platform";
#endif
        interstitial = new InterstitialAd(adUnitId);
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitial.OnAdClosed += HandleOnAdClosed;
        interstitial.LoadAd(new AdRequest.Builder().Build());
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }



    public bool IsRewardAvailable
    {
        get { return Advertisement.isInitialized && Advertisement.IsReady(REWARDED_VIDEO); }
    }
    string REWARDED_VIDEO = "rewardedVideo";
    static int adCount = 0;
    public void ShowAdsGameOver()
    {
        adCount++;
        if (adCount > 5)
        {
            ShowInterstitial();
            adCount = 0;
        }
    }
    public void ShowRewardBasedAd()
    {
        Debug.LogWarning("Reward Video Clicked");
        if (IsRewardAvailable)
            Advertisement.Show(REWARDED_VIDEO, new ShowOptions
            {
                resultCallback = (ShowResult result) =>
                {
                    if (result == ShowResult.Finished)
                    {
                        GameManager.Instance.CurrentCoins += 10;
                    }
                }
            });
    }


    void InitUnityAds()
    {
        if (!loadedOnce)
        {

            loadedOnce = true;
            string gameId = null;
#if UNITY_ANDROID
            gameId = UnityAdsAndroid;
#elif UNITY_IOS
      gameId = UnityAdsIOS;
#endif
            if (Advertisement.isSupported && !Advertisement.isInitialized)
            {
                Advertisement.Initialize(gameId, testMode);
            }
        }
    }

#endif
    private static AdsManager _instance;
    public static AdsManager Instance
    {
        get
        {
            if (_instance == null) _instance = GameObject.FindObjectOfType<AdsManager>();
            return _instance;
        }
    }
    public void ShowInterstitial()
    {
#if ADS_ENABLED
        try
        {
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
            }
            else
            {
                RequestInterstitial();
                MonoBehaviour.print("Interstitial is not ready yet");
            }
        }
        catch (Exception)
        {
            print("Can't Show Ad");
        }
#endif
    }
}