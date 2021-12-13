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
    private int hitMask;
    Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        hitMask = 1 << 6;
        hitMask = ~hitMask;
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
        PlayerUpdate();
    }
    // Update is called once per frame


    bool RaycastToRight(out Vector2 pushback){
        Vector2 pos = transform.position;
        float headOffset = 0.0f;
        if(isSliding){
            headOffset = -0.2f;
        }
        Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y + headOffset);
        Vector2 rayDir = Vector2.right;
        float rayDistance = 10.0f;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, rayDistance, hitMask);
        pushback = new Vector2(0, 0);
        
        bool hit = false;
        if (hit2D.collider != null)
        {
            if(hit2D.distance <= 0.3f){
                hit = true;
                pushback = Vector2.left * (0.3f - hit2D.distance);
                //pos = pos + pushback;
                //transform.position = pos;
                
            }
        }
        Debug.DrawRay(rayOrigin, rayDir * hit2D.distance, hit? Color.green : Color.red);
        return hit;
    }

    bool RaycastToGround(out Vector2 pushback)
    {
        Vector2 pos = transform.position;
        Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y - 0.5f);
        Vector2 rayDir = Vector2.up;
        float rayDistance = velocity.y * Time.deltaTime;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, -16.0f, hitMask);
        pushback = new Vector2(0, 0);

        isGrounded = false;
        if (hit2D.collider != null)
        {
            Ground ground = hit2D.collider.GetComponent<Ground>();
            if (ground && (hit2D.distance <= 0.11f))
            {
                //Debug.Log(hit2D.distance);
                isGrounded = true;
                if(hit2D.distance < 0.1f){
                    pushback = Vector2.up * (0.1f - hit2D.distance);
                    velocity.y = 0;
                }
            }
           
        }
        Debug.DrawRay(rayOrigin, rayDir * (-hit2D.distance), isGrounded? Color.green : Color.red);
        return isGrounded;
    }

    bool RaycastToCeiling(out Vector2 pushback){
        Vector2 pos = transform.position;
        Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
        Vector2 rayDir = Vector2.up;
        float rayDistance = velocity.y * Time.deltaTime;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, 16.0f, hitMask);
        pushback = new Vector2(0, 0);
        bool isOnCeiling = false;
        if (hit2D.collider != null)
        {
            Ground ground = hit2D.collider.GetComponent<Ground>();
            if (ground && (hit2D.distance <= 0.21f))
            {
                //Debug.Log(hit2D.distance);
                isOnCeiling = true;
                if(hit2D.distance < 0.6f){
                    pushback = -Vector2.up * (0.2f - hit2D.distance);
                    velocity.y = 0;
                }
            }
           
        }
        Debug.DrawRay(rayOrigin, rayDir * (hit2D.distance), isOnCeiling? Color.green : Color.red);
        return isOnCeiling;
    }

    void PlayerUpdate()
    {

        float dt = Time.deltaTime;
        Vector2 pos = transform.position;

        Vector2 pushback;
        if(velocity.y <= 0){
            if(RaycastToGround(out pushback)){
                pos += pushback;
            }
        }
        else{
            if(RaycastToCeiling(out pushback)){
                pos += pushback;
            }
        }
        if(RaycastToRight(out pushback)){
            pos += pushback;
            if(velocity.y > 0){
                velocity.y = 0;
            }
            
        }
        distance += velocity.x * dt;
        if (isGrounded)
        {
            //maxAcceleration = isSliding ? -maxAcceleration : maxAcceleration;
            float velocity_Ratio = velocity.x / maxVelocity;
            acceleration = maxAcceleration * (1 - velocity_Ratio);
            velocity.x += acceleration * dt;
            if(velocity.x >= maxVelocity)
            {
                velocity.x = maxVelocity;
            }
        }
        pos.y += velocity.y * dt;
        velocity.y += gravity * dt;
        //rigidbody2D.velocity = new Vector2(velocity.x, rigidbody2D.velocity.y);
        Debug.Log(isGrounded);

        transform.position = pos;
    }


}
