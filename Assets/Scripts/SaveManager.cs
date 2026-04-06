using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject MidBone;
    public TrailRenderer Tr;
    float timer;
    public Toggle easyToggle;
    public bool easyMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tr.emitting = false;
        //this makes it so good instead of a big ahh line
        Player.transform.position = new Vector2(
        PlayerPrefs.GetFloat("PlayerX", -107),
        PlayerPrefs.GetFloat("PlayerY", -18)
        );

        easyMode = (PlayerPrefs.GetString("Easy", "Yes") == "Yes");

        easyToggle.isOn = easyMode;

    }
    void Update()
    {
        easyMode = easyToggle.isOn;

        //annoyed of coroutines, wanna switch it up a bit here ://
        timer += Time.deltaTime;

        while (timer >= 0.05f)
        {
            timer -= 0.1f;
            Save();
        }
    }
    void Save()
    {
        Tr.emitting = true;
        if (easyMode) PlayerPrefs.SetString("Easy", "Yes"); else PlayerPrefs.SetString("Easy", "No");
        PlayerPrefs.SetFloat("PlayerX", MidBone.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", MidBone.transform.position.y);
        PlayerPrefs.Save();
    }


}
