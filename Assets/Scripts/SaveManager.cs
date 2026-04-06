using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject MidBone;

    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //this makes it so good instead of a big ahh line
        Player.transform.position = new Vector2(
        PlayerPrefs.GetFloat("PlayerX", 0),
        PlayerPrefs.GetFloat("PlayerY", 0)
        );

    }
    void Update()
    {
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
        PlayerPrefs.SetFloat("PlayerX", MidBone.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", MidBone.transform.position.y);
        PlayerPrefs.Save();
    }
}
