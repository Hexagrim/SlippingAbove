using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    public GameObject Pivot;
    public GameObject Indicator;
    public float MaxLength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawIndicator(Vector2 dir, float str)
    {
        float Deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Pivot.transform.rotation = Quaternion.Euler(0f, 0f, Deg + 90);
        Indicator.transform.localScale = new Vector2(Pivot.transform.localScale.y, str * MaxLength);
    }

}
