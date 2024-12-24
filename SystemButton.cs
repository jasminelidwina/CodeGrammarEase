using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemButton : MonoBehaviour
{
    public GameObject btnsound;
    public GameObject btnmute;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      btnsound.SetActive(true);
      btnmute.SetActive(false);
    }

    public void Mute()
    {
        SystemBacksound.instance.SuaraMusik.Pause();
        btnmute.SetActive(true);
        btnsound.SetActive(false);
    }

    public void Aktifkan()
    {
        SystemBacksound.instance.SuaraMusik.UnPause();
        btnmute.SetActive(false);
        btnsound.SetActive(true);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
