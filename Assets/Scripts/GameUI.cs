using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public Animator MenuAnim;
    public bool escaped;
    bool unpausing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escaped)
            {
                MenuAnim.SetTrigger("pause");
                escaped = true;
            }
        }
    }
    
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void Continue()
    {
        MenuAnim.SetTrigger("continue");
        if(!unpausing) StartCoroutine(NotPaused());
    }

    public IEnumerator NotPaused()
    {
        unpausing = true;
        yield return new WaitForSeconds(0.5f);
        escaped = false;
        unpausing = false;
    }
}
