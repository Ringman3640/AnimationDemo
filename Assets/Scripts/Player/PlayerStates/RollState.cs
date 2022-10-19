using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RollState : PlayerState
{
    private Rigidbody2D rb2d;

    private Vector3 direction;
    private Vector3 velocity;
    private float speed;

    public RollState(float rollSpeed)
    {
        rb2d = player.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb2d);

        speed = rollSpeed;
        direction = new Vector3();
    }

    protected override void ActionExecution()
    {
        if (!started)
        {
            GetDirection();
            StartAnimation();
            velocity = direction * speed;
            started = true;
        }

        rb2d.velocity = velocity;
    }

    private void GetDirection()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.z = 0f;
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            return;
        }

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        direction.z = 0f;
        direction.Normalize();
    }

    private void StartAnimation()
    {
        Animator anim = player.GetComponent<Animator>();
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

        anim.Play("player_rolling_front");

        if (direction.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
