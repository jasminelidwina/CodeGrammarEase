using UnityEngine;

public class SampleSceneController : MonoBehaviour
{
    public GameObject cover; // Panel Cover
    public GameObject menu; // Panel Menu
   
    void Start()
    {
        // Ambil informasi dari PlayerPrefs
        string targetPanel = PlayerPrefs.GetString("TargetPanel", "menu"); // Nilai default 'menu'

        Debug.Log("TargetPanel: " + targetPanel); // Debug log untuk melihat apa yang disimpan di PlayerPrefs

        // Menentukan panel yang akan ditampilkan berdasarkan nilai dari PlayerPrefs
        if (targetPanel == "Menu")
        {
            menu.SetActive(true);
            cover.SetActive(false);
        }
        else
        {
            menu.SetActive(false);
            cover.SetActive(true); // Tampilkan panel Cover jika tidak ada nilai yang valid
        }

        // Hapus key PlayerPrefs setelah digunakan
        PlayerPrefs.DeleteKey("TargetPanel");
    }
}