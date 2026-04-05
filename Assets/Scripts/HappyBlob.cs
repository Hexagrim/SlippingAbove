using TMPro;
using UnityEngine;

public class HappyBlob : MonoBehaviour
{
    public TMP_Text Text;
    public Animator Anim;
    public string Words;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text.text = Words;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Anim.SetBool("playerIN", true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Anim.SetBool("playerIN", false);
        }

    }
}
