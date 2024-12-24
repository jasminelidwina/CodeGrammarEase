using System.Collections;
using UnityEngine;
public class ShareAndRate : MonoBehaviour
{
    public string subject = "Guess Flags";
    public string body = "https://play.google.com/store/apps/details?";
    public void OnAndroidTextSharingClick()
    {
        StartCoroutine(ShareAndroidText());
    }
    IEnumerator ShareAndroidText()
    {
        yield return new WaitForEndOfFrame();
#if UNITY_ANDROID
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
        //get the current activity
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //start the activity by sending the intent data
        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
        currentActivity.Call("startActivity", jChooser);
#endif
    }
    public void RateUs()
    {
#if UNITY_ANDROID
        Application.OpenURL(body);
#endif
    }
}
