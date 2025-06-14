using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorRenderer : MonoBehaviour
{
    public static readonly string[] staticDirections = { "staticN",
 "staticW", "staticS", "staticE"};
    public static readonly string[] runDirections = { "back", "left", "front", "right" };

    Animator animator;
    int lastDirection;

    private void Awake()
    {
        //cache the animator component
        animator = GetComponent<Animator>();
    }


    public void SetDirection(Vector2 direction)
    {


        string[] directionArray = null;

        if(direction.magnitude < .01f)
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

        if(angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }
}
