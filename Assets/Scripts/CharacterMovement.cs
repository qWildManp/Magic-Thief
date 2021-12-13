using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public Vector2 velocity;
    public float acceleration = 10f;
    public float maxAcceleration = 10f;
    public float maxVelocity = 100;
    public float jumpForce = 100f;
    public float gravity = -100f;
    public bool isGrounded = false;
    public bool isSliding = false;
    public float goundheight = -3.5f;
    public float distance;
    Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isSliding", isSliding);
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isSliding = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isSliding = false;
            }
            if (Input.GetKeyDown(KeyCode.Space)&& isSliding == false)
            {
                isGrounded = false;
                velocity.y = jumpForce;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if (!isGrounded)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;
        }

        Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
        Vector2 rayDir = Vector2.up;
        float rayDistance = velocity.y * Time.deltaTime;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, -0.6f);
        if (hit2D.collider != null)
        {
            Ground ground = hit2D.collider.GetComponent<Ground>();
            if (ground)
            {
                    velocity.y = 0;
                    isGrounded = true;
            }
           
        }
       
        else
        {
            isGrounded = false;
        }
        Debug.DrawRay(rayOrigin, rayDir * -0.6f, Color.red);
        distance += velocity.x * Time.fixedDeltaTime;
        if (isGrounded)
        {
            //maxAcceleration = isSliding ? -maxAcceleration : maxAcceleration;
            float velocity_Ratio = velocity.x / maxVelocity;
            acceleration = maxAcceleration * (1 - velocity_Ratio);
            velocity.x += acceleration * Time.fixedDeltaTime;
            if(velocity.x >= maxVelocity)
            {
                velocity.x = maxVelocity;
            }
        }
        //rigidbody2D.velocity = new Vector2(velocity.x, rigidbody2D.velocity.y);
        transform.position = pos;
    }
}
