using UnityEngine;
public class CoinScript : MonoBehaviour
{
    public GameObject[] coins;
    public void RemoveLetter()
    {
        if (GameManager.Instance.CurrentCoins < 5)
        {
            coins[0].gameObject.SetActive(true);
        }
        else
        {
            coins[1].gameObject.SetActive(true);
        }
    }
    public void ShowOneLetter()
    {
        if (GameManager.Instance.CurrentCoins < 5)
        {
            coins[0].gameObject.SetActive(true);
        }
        else
        {
            coins[2].gameObject.SetActive(true);
        }
    }
    public void Hint()
    {
        if (GameManager.Instance.CurrentCoins < 10)
        {
            coins[0].gameObject.SetActive(true);
        }
        else
        {
            coins[3].gameObject.SetActive(true);
        }
    }
    public void FB()
    {
        if (GameManager.Instance.CurrentCoins < 5)
        {
            coins[0].gameObject.SetActive(true);
        }
        else
        {
            coins[5].gameObject.SetActive(true);
        }
    }
    public void Twt()
    {
        if (GameManager.Instance.CurrentCoins < 5)
        {
            coins[0].gameObject.SetActive(true);
        }
        else
        {
            coins[6].gameObject.SetActive(true);
        }
    }
    private static CoinScript _instance;

    public static CoinScript Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CoinScript>();
            }
            return _instance;
        }
    }
}