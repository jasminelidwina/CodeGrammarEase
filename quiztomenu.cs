using UnityEngine;
using UnityEngine.SceneManagement;

public class quiztomenu : MonoBehaviour
{
    public void GoToSampleSceneMenu()
    {
        // Simpan informasi tentang panel target
        PlayerPrefs.SetString("TargetPanel", "Menu");
        PlayerPrefs.Save();

        // Pindah ke scene SampleScene
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}