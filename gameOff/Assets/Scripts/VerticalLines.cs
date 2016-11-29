using UnityEngine;
using System.Collections;

public class VerticalLines : MonoBehaviour {
    private const float boundriesX = 2.5f;

    public float speed = 4f;
    public LayerMask whatIsMoveable;
    public float sideRadius = 0.3f;

    protected float width;
    private float nowLine;
    private float targetLine;

    private int pushCollision = 0;
    Collider2D[] pushColliders = new Collider2D[1];

    // Use this for initialization
    public virtual void Start () {
        width = GetComponent<Renderer>().bounds.size.x;
        nowLine = transform.position.x;
        targetLine = nowLine;
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    // Update is called once per frame
    public virtual void Update () {
        Go();
	}

    public virtual void FixedUpdate()
    {
    }

    /// <summary>
    /// isMovingToByX to next target line
    /// </summary>
    protected void Go()
    {
        if (targetLine != nowLine)
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(targetLine, transform.position.y, transform.position.z),
                speed * Time.deltaTime);
        if (targetLine == transform.position.x)
            nowLine = targetLine;
    }

    /// <summary>
    /// Check neighbours in the moving direction and if is possible change the target line, the method Go will move object
    /// </summary>
    protected bool MoveTargetLine(bool toRight)
    {
        bool isPossibleToMove = moveSideElements(toRight);
        if (targetLine <= boundriesX && toRight && isPossibleToMove) //right
        {
            targetLine = nowLine + width;
            return true;
        }

        if (targetLine >= -boundriesX && !toRight && isPossibleToMove) //left
        {
            targetLine = nowLine - width;
            return true;
        }
        return false;
    }

    /// <summary>
    /// check colliders if exist it use movetargetline on this object
    /// return possibilty to move object in the direction
    /// </summary>
    protected bool moveSideElements(bool toRight)
    {
        int layer = gameObject.layer;
        gameObject.layer = 1;
        if (toRight)
            pushCollision = Physics2D.OverlapCircleNonAlloc(
                new Vector3(transform.position.x + width / 2, transform.position.y, transform.position.z)
                , sideRadius, pushColliders, whatIsMoveable);
        else
            pushCollision = Physics2D.OverlapCircleNonAlloc(
                new Vector3(transform.position.x - width / 2, transform.position.y, transform.position.z)
                , sideRadius, pushColliders, whatIsMoveable);
        gameObject.layer = layer;

        if (pushCollision > 0)
        {
            return pushColliders[0].GetComponent<VerticalLines>().MoveTargetLine(toRight);
        }
        return true;
    }
}
