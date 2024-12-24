using UnityEngine;

public class SystemBacksound : MonoBehaviour
{
    public static SystemBacksound instance;
    public AudioSource SuaraMusik;
    public void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    //}
}
