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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
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
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void Continue()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
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
