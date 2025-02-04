using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    /*private Animator animator;
    private Vector3 lastPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 direction = transform.position - lastPosition;
        lastPosition = transform.position;

        if (direction.x > 0)
        {
            animator.SetFloat("Direction", 1f); // 右移動のアニメーション
        }
        else if (direction.x < 0)
        {
            animator.SetFloat("Direction", -1f); // 左移動のアニメーション
        }
        else if (direction.y < 0)
        {
            animator.SetFloat("Direction", -2f); // 下移動のアニメーション
        }
        // 他の方向も同様に設定可能です
    }*/
    public static readonly string[] staticDirections = { "staticN",
 "staticW", "staticS", "staticE"};
    public static readonly string[] runDirections = { "back", "left", "front", "right" };
    string[] directionArray = null;
    Animator animator;
    private Vector3 lastPosition;
    int lastDirection;
    Vector2 directionNum;

    private void Awake()
    {
        //cache the animator component
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }
    void Update()
    {
        Vector3 direction = transform.position - lastPosition;
        lastPosition = transform.position;

        float horizontal = direction.x;
        float vertical = direction.y;

        animator.SetFloat("x", horizontal);
        animator.SetFloat("y", vertical);

        if(direction.magnitude < 0.01f)
        {
            animator.enabled = false;
        }
        else
        {
            animator.enabled = true;
            //animator.SetFloat("Speed", direction.magnitude); // 移動中のアニメーション
        }
    }

    public void SetDirection(Vector2 direction)
    {


        directionNum = direction;

        if (direction.magnitude < .01f)
        {

            directionArray = staticDirections;
        }
        else
        {

            directionArray = runDirections;
            lastDirection = DirectionToIndex(direction, 4);
        }


        animator.Play(directionArray[lastDirection]);
    }


    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {

        Vector2 normDir = dir.normalized;

        float step = 360f / sliceCount;

        float halfstep = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normDir);

        angle += halfstep;

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }
}
