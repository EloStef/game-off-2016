using UnityEngine;
using System.Collections;

public class Element : VerticalLines {

    public LayerMask whatIsGround;

    private bool grounded = false;
    private Transform groundCheck;
    
    // Use this for initialization
    void Start()
    {
        base.Start();
        groundCheck = new GameObject("groundCheck").transform;
        groundCheck.position = new Vector3(transform.position.x, transform.position.y - width/2, transform.position.z);
        groundCheck.parent = gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate () {
        int layer = gameObject.layer;
        gameObject.layer = 1;
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);
        gameObject.layer = layer;
    }

    public void DestroyElement()
    {
        transform.name = "none";
        GetComponentInChildren<TextureShowHide>().runDisapearing(800);
    }

    public bool isGrounded()
    {
        return grounded;
    }
}
