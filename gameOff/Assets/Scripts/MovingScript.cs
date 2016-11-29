using UnityEngine;
using System.Collections;

public class MovingScript : VerticalLines
{
    public float jumpHeight = 4f;

    private bool facingRight = true;

    private bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.3f;
    public LayerMask whatIsGround;
    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if(grounded)
            Jump();
    }

    private void Jump()
    {
        if (Input.GetKey("w"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        if (Input.GetKey("d"))
        {
            if (!facingRight)
                Flip();
            MoveTargetLine(true);
        }
        if (Input.GetKey("a"))
        {
            if (facingRight)
                Flip();
            MoveTargetLine(false);
        }
        Go();
    }

    /// <summary>
    /// flip object by 180 around y axis
    /// </summary>
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
