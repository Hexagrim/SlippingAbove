using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float jumpSpeed;
    private Rigidbody2D[] softBodyRBs;

    bool grounded;
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayer;


    Vector2 mouseUpPos, mouseDownPos;
    bool canJump;
    public float jumpCooldownTime;

    private void Start()
    {
        softBodyRBs = GetComponentsInChildren<Rigidbody2D>();
    }

    private void Update()
    {
        //yea i need to call this so that the player moves slower in the ground cause of linear damping so they cant slide. pretty self explainatory
        SetFrictionOnGround();

        //call this is for distance, i like making voids cause they are cleaner for this stuff but i guess making it a float thing is better;
        //CalcDistanceBetweenMouse();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mouseDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.GetKey(KeyCode.Mouse0))
        {
            mouseUpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }


        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (Input.GetKeyUp(KeyCode.Mouse0) && grounded && canJump)
        {
            Vector2 dir = (mouseUpPos - mouseDownPos).normalized;
            Jump(dir * -1);
            StartCoroutine(BeginJumpCooldown(jumpCooldownTime));
        }

    }

    void SetFrictionOnGround()
    {

        if (grounded)
        {
            foreach (Rigidbody2D rb in softBodyRBs)
            {
                rb.linearDamping = 2;
            }
        }
        else
        {
            foreach (Rigidbody2D rb in softBodyRBs)
            {
                rb.linearDamping = 0;
            }
        }
    }

    void Jump(Vector2 d)
    {
        foreach (Rigidbody2D rb in softBodyRBs)
        {
            rb.AddForce(d * jumpSpeed);
            BeginJumpCooldown(jumpCooldownTime);
        }
    }
    IEnumerator BeginJumpCooldown(float t)
    {
        canJump = false;
        yield return new WaitForSeconds(t);
        canJump = true;
    }


    //might sound crazy, but on mouse down get the position relative to the world,
    //then on mouse up get the new position, then reucde one from another to get 
    //the vector and then do the vector/magnitude formula to find unit vector in its 
    //direction then divide them to get the slope and using tan inverse get the angle of rotation
    //:sob:

    float CalcDistanceBetweenMouse()
    {
        return Vector2.Distance(mouseUpPos, mouseDownPos);
    }
}
