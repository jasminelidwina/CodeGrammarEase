using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Button ansButton;
    public Button ShowOneButton;
    public GameObject truebtn1;
    public GameObject truebtn2;

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
    public void OpenUlr()
    {
        Application.OpenURL("https://assetstore.unity.com/publishers/22181");
    }
    public int count;
    public void FbUrl()
    {
        if (count == 0)
        {
            GameManager.Instance.CurrentCoins -= 5;
            Application.OpenURL("https://en-gb.facebook.com/login/");
            count++;
        }
        else
        {
            Application.OpenURL("https://en-gb.facebook.com/login/");
        }
    }
    public int twtCount;
    public void TwitterUrl()
    {
        if (twtCount == 0)
        {
            GameManager.Instance.CurrentCoins -= 5;
            Application.OpenURL("https://twitter.com/login");
            twtCount++;
        }
        else
        {
            Application.OpenURL("https://twitter.com/login");
        }
    }
    public void ShareLink()
    {
        Application.OpenURL("https://www.google.com/");
    }
    public void PlayStore()
    {
        Application.OpenURL("https://play.google.com/store?hl=en");
    }

    public void ButtonEvent()
    {
        Handheld.Vibrate();
        Debug.Log("ok");
    }
    public void DisableButton()
    {
        ansButton.interactable = false;
    }
    public void EnableButton()
    {
        ansButton.interactable = true;
    }
    public void ShowDiable()
    {
        ShowOneButton.interactable = false;
    }
    public void ShowEnable()
    {
        ShowOneButton.interactable = true;
    }
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }
}