using UnityEngine;
using UnityEngine.UI;

public class Cursorm : MonoBehaviour
{
    RectTransform rectTransform;
    Image img;

    public Color ColorNormal, ColorPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            img.color = ColorPressed;
        }
        else
        {
            img.color = ColorNormal;
        }
            rectTransform.position = Input.mousePosition;
    }
}
