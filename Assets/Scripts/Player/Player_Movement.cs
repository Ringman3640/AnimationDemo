using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player_Movement : MonoBehaviour
{
    public float movementSpeed = 6f;

    private SpriteRenderer sr;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(sr);

        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateDirection();
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

        direction.Normalize();

        Vector3 speed = direction * movementSpeed;
        transform.position += speed * Time.deltaTime;
        animator.SetFloat("Speed", speed.magnitude);

    }

    private void UpdateDirection() 
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.y *= -1;
        direction.z = 0f;

        if (direction.x >= 0)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

        if (direction.y >= 0)
        {
            animator.SetBool("Facing Forward", true);
        }
        else
        {
            animator.SetBool("Facing Forward", false);
        }
    }

}
