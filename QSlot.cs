using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QSlot : MonoBehaviour
{
    public bool isPresent;
    public Text slotText;

    public void OnClick()
    {
        if (isPresent)
        {
            isPresent = false;
            GameManager.Instance.FillQSlot(slotText.text, transform.GetSiblingIndex());
            this.gameObject.SetActive(false);
        }
    }

    internal void ReturnSlot(int returningIndex, string text)
    {
        GameManager.Instance.qSlotLst[returningIndex].Reset(text);
    }
    public void Reset(string va)
    {
        isPresent = true;
        slotText.text = va;
        this.gameObject.SetActive(true);

    }
    private static QSlot _instance;

    public static QSlot Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<QSlot>();
            }
            return _instance;
        }
    }
}
