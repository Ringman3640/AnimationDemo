using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 6f;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Animator animator;

    private Vector3 pointDirection;
    private const float STRAIGHT_RANGE = 0.2f;

    PlayerState moveState;

    public void PlayerRollCompleted()
    {
        if (moveState != null)
        {
            moveState.Completed();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        name = "Player";

        rb2d = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(sr);

        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);

        pointDirection = new();
        moveState = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveState != null)
        {
            if (!moveState.completed)
            {
                moveState.Execute();
                return;
            }

            moveState = null;
        }

        CheckActionInput();
        UpdatePosition();
        UpdateDirection(ref pointDirection);
        PlayerAnimation.UpdateAnimation();
    }

    private void CheckActionInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveState = new RollState(10f);
        }
    }

    private void UpdatePosition()
    {
        Vector3 direction = new(0f, 0f, 0f);
        //direction.x = Input.GetAxis("Horizontal");
        //direction.y = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.A))
        {
            --direction.x;
        }
        if (Input.GetKey(KeyCode.D))
        {
            ++direction.x;
        }
        if (Input.GetKey(KeyCode.S))
        {
            --direction.y;
        }
        if (Input.GetKey(KeyCode.W))
        {
            ++direction.y;
        }

        if (direction == Vector3.zero)
        {
            PlayerAnimation.moveState = PlayerAnimation.MoveState.Idle;
            rb2d.velocity = Vector2.zero;
            return;
        }

        direction.Normalize();

        PlayerAnimation.moveState = PlayerAnimation.MoveState.Running;
        //transform.position += direction * (movementSpeed * Time.deltaTime);

        direction *= movementSpeed;
        rb2d.velocity = direction;
    }

    private void UpdateDirection(ref Vector3 pointDirection) 
    {
        pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        pointDirection.y *= -1;
        pointDirection.z = 0f;
        pointDirection.Normalize();

        // Set Anim Look State
        if (Mathf.Abs(pointDirection.x) <= STRAIGHT_RANGE)
        {
            if (pointDirection.y >- 0)
            {
                PlayerAnimation.lookState = PlayerAnimation.LookState.StraightFront;
            }
            else
            {
                PlayerAnimation.lookState = PlayerAnimation.LookState.StraightBack;
            }
        }
        else
        {
            if (pointDirection.y > -0)
            {
                PlayerAnimation.lookState = PlayerAnimation.LookState.Front;
            }
            else
            {
                PlayerAnimation.lookState = PlayerAnimation.LookState.Back;
            }
        }

        // Set if flipX
        if (pointDirection.x < -STRAIGHT_RANGE)
        {
            PlayerAnimation.flipX = true;
        }
        else
        {
            PlayerAnimation.flipX = false;
        }
    }
}
