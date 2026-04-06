using System.Collections;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float activeTime, regenTime;
    bool decayed;
    Color originalColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Decay()
    {
        decayed = true;
        yield return new WaitForSeconds(activeTime);
        GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a * 0.5f);
        GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(regenTime);
        GetComponent<SpriteRenderer>().color = originalColor;
        GetComponent<Collider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() && !decayed)
        {
            StartCoroutine(Decay());
        }
    }
}
