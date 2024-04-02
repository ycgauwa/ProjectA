using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoving : MonoBehaviour
{
    public float movementSpeed = 5f;
    public bool isMove;
    Rigidbody2D rbody;
    NPCAnimationController cr;
    public static Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        cr = GetComponentInChildren<NPCAnimationController>();
        isMove = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isMove)
        {
            Vector2 currentPos = rbody.position;

            movement.x = transform.position.x - movement.x;
            movement.y = transform.position.y - movement.y;
            movement = new Vector2(movement.x, movement.y);
            cr.SetDirection(movement);
        }
        movement.x = transform.position.x;
        movement.y = transform.position.y;
        //if (isMove)
        //{
        //    Vector2 currentPos = rbody.position;

        //    float horizontalInput = Input.GetAxis("Horizontal");
        //    float verticalInput = Input.GetAxis("Vertical");
        //    Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        //    inputVector = Vector2.ClampMagnitude(inputVector, 1);
        //    Vector2 movement = inputVector * movementSpeed;

        //    //動く時にこのMuveMentを通して移動させるとアニメーションがつくなお止めるときは速度を0にしないとアニメーションが止まらなくなる。
        //    Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        //    cr.SetDirection(movement);
        //    rbody.MovePosition(newPos);
        //}
    }
}
