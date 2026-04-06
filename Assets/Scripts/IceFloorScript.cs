
using UnityEngine;

public class IceFloorScript : MonoBehaviour
{
    public float damping;
    public float touchingObjs;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(touchingObjs > 0)
        {
            FindFirstObjectByType<PlayerMovement>().onIce = true;
        }
        else
        {
            FindFirstObjectByType<PlayerMovement>().onIce = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            touchingObjs++;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            touchingObjs--;
        }
    }
}
