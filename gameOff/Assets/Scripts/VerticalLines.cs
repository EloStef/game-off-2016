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
    Collider2D[] pushColliders = new Collider2D[3];

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
    protected void MoveTargetLine(bool toRight)
    {
        bool isPossibleToMove = IsPossibleMoveTargetLine(toRight);

        if (!isPossibleToMove)
            return;

        moveSideElements(toRight);

        if (targetLine <= boundriesX && toRight && isPossibleToMove) //right
        {
            targetLine = nowLine + width;
        }

        if (targetLine >= -boundriesX && !toRight && isPossibleToMove) //left
        {
            targetLine = nowLine - width;
        }
    }

    /// <summary>
    /// Check neighbours in the moving direction and if is possible change the target line, the method Go will move object
    /// </summary>
    protected bool IsPossibleMoveTargetLine(bool toRight)
    {
        bool isPossibleToMove = IsPossibleToMoveSideElements(toRight);

        if (targetLine <= boundriesX && toRight && isPossibleToMove) //right
        {
            return true;
        }

        if (targetLine >= -boundriesX && !toRight && isPossibleToMove) //left
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// check if is possible to move object in the direction
    /// </summary>
    protected bool IsPossibleToMoveSideElements(bool toRight)
    {
        int layer = gameObject.layer;
        gameObject.layer = 1;
        if (toRight)
            pushCollision = Physics2D.OverlapAreaNonAlloc(
                new Vector2(transform.position.x - sideRadius + width / 2, transform.position.y + width / 2 - 0.1f),
                new Vector2(transform.position.x + sideRadius + width / 2, transform.position.y - width / 2 + 0.1f),
                pushColliders, whatIsMoveable);
        else
            pushCollision = Physics2D.OverlapAreaNonAlloc(
                new Vector2(transform.position.x - sideRadius - width / 2, transform.position.y + width / 2 - 0.1f),
                new Vector2(transform.position.x + sideRadius - width / 2, transform.position.y - width / 2 + 0.1f),
                pushColliders, whatIsMoveable);
        gameObject.layer = layer;

        for(int i = 0; i < pushCollision; i++)
        {
            if (!pushColliders[i].GetComponent<VerticalLines>().IsPossibleMoveTargetLine(toRight))
                return false;
        }
        return true;
    }

    /// <summary>
    /// check colliders if exist it use movetargetline on this object
    /// return possibilty to move object in the direction
    /// </summary>
    protected void moveSideElements(bool toRight)
    {
        int layer = gameObject.layer;
        gameObject.layer = 1;
        if (toRight)
            pushCollision = Physics2D.OverlapAreaNonAlloc(
                new Vector2(transform.position.x - sideRadius + width / 2, transform.position.y + width / 2 - 0.1f),
                new Vector2(transform.position.x + sideRadius + width / 2, transform.position.y - width / 2 + 0.1f),
                pushColliders, whatIsMoveable);
        else
            pushCollision = Physics2D.OverlapAreaNonAlloc(
                new Vector2(transform.position.x - sideRadius - width / 2, transform.position.y + width / 2 - 0.1f),
                new Vector2(transform.position.x + sideRadius - width / 2, transform.position.y - width / 2 + 0.1f),
                pushColliders, whatIsMoveable);
        gameObject.layer = layer;

        for (int i = 0; i < pushCollision; i++)
        {
            pushColliders[i].GetComponent<VerticalLines>().MoveTargetLine(toRight);
        }
        return;
    }

    public int getCurrentLine()
    {
        if(nowLine != targetLine)
            return -1;
        return (int)System.Math.Round(nowLine / width, 0) + 3;
    }
}
