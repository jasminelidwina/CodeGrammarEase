using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform parentQSlots, parentAnsSlots;
    internal QSlot[] qSlotLst;
    internal AnsSlot[] ansSlotLst;
    public Image questionDisplayImg;
    List<int> rndLst = new List<int>();
    public Word[] words;
    public static int levelValue = 0;
    public Text level;
    public GameObject[] panel;
    public GameObject[] dialogs;
    public Text anwserText;
    public int index;
    public static GameManager main;
    public string word;
    const string LEVEL = "LEVEL";
    private int _currentLevel;
    private string desiredRandom;
    int CurrentLevel
    {
        get { return _currentLevel; }
        set
        {
            _currentLevel = value;

            level.text = _currentLevel.ToString();
            PlayerPrefs.SetInt(LEVEL, _currentLevel);
        }
    }
    public Text[] score;
    public int _CurrentScore;
    public int scoreValue;
    const string SCORE = "SCORE";
    int CurrentScore
    {
        get { return _CurrentScore; }
        set
        {
            _CurrentScore = value;
            score[0].text = _CurrentScore.ToString();
            score[1].text = _CurrentScore.ToString();
            PlayerPrefs.SetInt(SCORE, _CurrentScore);
            PlayerPrefs.Save();
        }
    }
    public int coinValue = 25;
    public Text coin;
    private int _CurrentCoin;
    const string COIN = "COIN";
    public int CurrentCoins
    {
        get { return _CurrentCoin; }
        set
        {
            _CurrentCoin = value;
            coin.text = "x " + _CurrentCoin.ToString();
            PlayerPrefs.SetInt(COIN, _CurrentCoin);
            PlayerPrefs.Save();
        }
    }
    private void Awake()
    {
        main = this;
        qSlotLst = parentQSlots.GetComponentsInChildren<QSlot>();
        ansSlotLst = parentAnsSlots.GetComponentsInChildren<AnsSlot>();
    }
    void Start()
    {
        PlayerPrefs.DeleteAll();
        CurrentCoins = PlayerPrefs.GetInt(COIN, 25);
        CurrentScore = PlayerPrefs.GetInt(SCORE, 0);
        CurrentLevel = PlayerPrefs.GetInt(LEVEL, 1);
        ShowQues(CurrentLevel);

    }
    public void ShowQues(int index)
    {
        Word questionData = words[index];
        questionDisplayImg.sprite = questionData.Image;

        foreach (var item in qSlotLst)
        {
            item.slotText.text = GetRandomCh();
        }
        rndLst.Clear();
        for (int i = 0; i < words[index].QuesWord.Length; i++)
        {
            qSlotLst[GetRandomIndex()].slotText.text = words[index].QuesWord[i].ToString();
        }
        for (int i = 0; i < ansSlotLst.Length; i++)
        {
            ansSlotLst[i].gameObject.SetActive(i < words[index].QuesWord.Length);
        }
    }
    int GetRandomIndex()
    {
        int v;
        do
        {
            v = UnityEngine.Random.Range(0, 20);
        } while (rndLst.Contains(v));
        rndLst.Add(v);
        return v;
    }
    public void CheckWord()
    {
        string word = "";
        foreach (AnsSlot charObject in ansSlotLst)
        {
            word += charObject.slotText.text;
        }
        if (word == words[CurrentLevel].QuesWord)
        {
            panel[0].gameObject.SetActive(true);
            dialogs[0].gameObject.SetActive(true);
        }
        else
        {
            if (word.Length == words[CurrentLevel].QuesWord.Length)
            {
                dialogs[1].gameObject.SetActive(true);
                panel[1].gameObject.SetActive(true);

                for (int i = 0; i < qSlotLst.Length; i++)
                {
                    qSlotLst[i].isPresent = false;
                }
            }
        }
    }
    public void Next()
    {
        Retry();
        CurrentLevel++;
        CurrentCoins += 10;
        levelValue += 1;
        CurrentScore += 100;
        if (CurrentLevel >= words.Length)
        {
            dialogs[2].gameObject.SetActive(true);
       } else 
		{
			ShowQues(CurrentLevel);
		}
    }
    //public void Reset()
    //{
    //    foreach (var obj in qSlotLst)
    //    {
    //        obj.gameObject.SetActive(true);
    //        obj.isPresent = true;
    //    }
    //    foreach (AnsSlot ans in ansSlotLst)
    //    {
    //        ans.gameObject.SetActive(true);
    //        ans.slotText.text = "";
    //        ans.isVacant = true;
    //    }
    //}
    public void RemoveLetter()
    {
        CurrentCoins -= 5;
        for (int i = 0; i <qSlotLst.Length; i++)
        {
            qSlotLst [i].gameObject.SetActive (rndLst.Contains (i));
        }

            for (int j = 0; j < words [CurrentLevel].QuesWord.Length; j++) 
		{
			for (int k = 0; k < qSlotLst.Length; k++) 
			{
				if (ansSlotLst [j].slotText.text.Contains (qSlotLst [k].slotText.text)) 
				{
					qSlotLst [k].gameObject.SetActive (false);
					break;
				}
			}
		}
    }
    public void ShowLetter()
    {
         CurrentCoins -= 5;
		for(int i=0;i<words[CurrentLevel].QuesWord.Length;i++)
		{
			if (ansSlotLst [i].isVacant==true) {
				ansSlotLst [i].slotText.text = "?";
			}
		}
        for (int i = 0; i < qSlotLst.Length; i++)
        {
			qSlotLst[i].isPresent = false;
        }
    }
     public void Click(int index)
	{
        foreach (var slot in ansSlotLst)
        {
			if (slot.slotText.text == "?") 
			{
				slot.slotText.text = "";
			}
        }
        ansSlotLst[index].slotText.text = words[CurrentLevel].QuesWord[index].ToString();
		for (int i = 0; i < qSlotLst.Length; i++)
        {
			if (ansSlotLst[index].slotText.text.Contains(qSlotLst[i].slotText.text.ToString()))
			{
				ansSlotLst[index].isVacant=false;
				qSlotLst [i].gameObject.SetActive (false);
				break;
            }
        }
		foreach (var qSlot in qSlotLst) 
		{
			qSlot.isPresent=true;
		}
		CheckWord ();
    }
    private int hintCount;
    public void Hint()
    {
        if (hintCount == 0)
        {
            CurrentCoins -= 10;
            anwserText.text = words[CurrentLevel].QuesWord;
            hintCount++;
        }
        else
        {
            anwserText.text = words[CurrentLevel].QuesWord;
        }
    }
    public void ResetGam()
    {
        try
        {
            PlayerPrefs.DeleteAll();
            CurrentScore = 0;
            CurrentLevel = 1;
            CurrentCoins = 25;
            hintCount = 0;
            UIManager.Instance.count = 0;
            UIManager.Instance.twtCount = 0;
            ShowQues(_currentLevel);
            Retry();
        }
        catch (NullReferenceException)
        {
        }
    }
    internal void FillQSlot(string s, int rInd)
    {
        int vC = 0;
        foreach (var item in ansSlotLst)
        {
            if (item.isVacant)
            {
                vC++;
                item.FillSlot(s, rInd);
                break;
            }
        }
    }
    public string GetRandomCh()
    {
        return ((char)UnityEngine.Random.Range(65, 91)).ToString();
    }
    public void Retry()
    {
        for (int i = 0; i < qSlotLst.Length; i++)
        {
            qSlotLst[i].gameObject.SetActive(true);
            qSlotLst[i].isPresent = true;

        }
        for (int i = 0; i < words[CurrentLevel].QuesWord.Length; i++)
        {
            ansSlotLst[i].isVacant = true;
            ansSlotLst[i].slotText.text = "";
        }
    }
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }
    public void Wikipedia()
    {
        Application.OpenURL("https://www.wikipedia.org/wiki/" + words[CurrentLevel].QuesWord);
    }
    //private void OnDisable()
    //{
    //    AdsManager.Instance.ShowInterstitial();
    //}
}
[System.Serializable]
public class Word
{
    public string QuesWord;
    public Sprite Image;
}
