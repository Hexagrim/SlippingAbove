using System.Collections;

using UnityEngine;

using UnityEngine.UI;

using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerMovement : MonoBehaviour
{

    public float jumpSpeed;
    private Rigidbody2D[] softBodyRBs;
    private SpringJoint2D[] softBodySprings;
    bool grounded;
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayer;



    Vector2 mouseUpPos, mouseDownPos;
    bool canJump = true;
    public float jumpCooldownTime;

    public float maxDistance;

    public float KeyPressedFrequency;
    public float OriginalFrequency;

    bool frLow = false;

    public Image BarFill;

    public bool onIce;
    private void Start()
    {
        softBodyRBs = GetComponentsInChildren<Rigidbody2D>();
        softBodySprings = GetComponentsInChildren<SpringJoint2D>();

    }

    private void Update()
    {


        if (FindFirstObjectByType<GameUI>().escaped) return;

        //yea i need to call this so that the player moves slower in the ground cause of linear damping so they cant slide. pretty self explainatory
        
        //check if we are on ICE
        if(!onIce) SetFrictionOnGround();

        //call this is for distance, i like making voids cause they are cleaner for this stuff but i guess making it a float thing is better;
        //CalcDistanceBetweenMouse();

        //calcs the strenght which is like the distance between the mosue but like a slider, idk why im commenting ngl
        float strength = Mathf.Clamp01(CalcDistanceBetweenMouse() / maxDistance);

        BarFill.fillAmount = strength;
        if (Input.GetKeyDown(KeyCode.Mouse0) && canJump)
        {
            mouseDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetKey(KeyCode.Mouse0) && canJump && grounded)
        {
            if (!frLow)
            {
                LowerFrequency();
            }
            Vector2 dir = mouseDownPos - mouseUpPos;
            mouseUpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GetComponent<IndicatorScript>().DrawIndicator(dir, strength);


        }
        else
        {
            if (frLow)
            {
                NormalFrequency();
            }
            GetComponent<IndicatorScript>().DrawIndicator(Vector2.zero, 0);
        }



        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (Input.GetKeyUp(KeyCode.Mouse0) && grounded && canJump)
        {


            Vector2 dir = (mouseUpPos - mouseDownPos).normalized;
            Jump(-dir * jumpSpeed * strength , strength);
            StartCoroutine(BeginJumpCooldown(jumpCooldownTime));


        }


        //here i will clamp velocity:

        //foreach(Rigidbody2D rb in softBodyRBs)
        //{
        //    rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -22f, float.MaxValue);
        //}
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

    void Jump(Vector2 d , float str)
    {
        FindFirstObjectByType<CamShake>().Shake(5f * str, 0.15f);

        foreach (Rigidbody2D rb in softBodyRBs)
        {
            rb.AddForce(d);
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
    void LowerFrequency()
    {
        frLow = true;
        foreach(SpringJoint2D sj in softBodySprings)
        {
            sj.frequency = KeyPressedFrequency;
        }
    }
    void NormalFrequency()
    {
        frLow = false;
        foreach (SpringJoint2D sj in softBodySprings)
        {
            sj.frequency = OriginalFrequency;
        }
    }
}
