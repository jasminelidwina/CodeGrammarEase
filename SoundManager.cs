using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public GameObject audioControlButton;
    public Sprite audioOffSprite;
    public Sprite audioOnSprite;

    void Start()
    {
        if (AudioListener.pause == true)
        {
            audioControlButton.GetComponent<Image>().sprite = audioOffSprite;
        }
        else
        {
            audioControlButton.GetComponent<Image>().sprite = audioOnSprite;
        }
    }
    public void SoundControl()
    {
        if (AudioListener.pause == true)
        {
            AudioListener.pause = false;
            audioControlButton.GetComponent<Image>().sprite = audioOnSprite;
        }
        else
        {
            AudioListener.pause = true;
            audioControlButton.GetComponent<Image>().sprite = audioOffSprite;
        }
    }
}
