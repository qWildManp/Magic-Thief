using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{

    private float startSpeedX; 
    private float startSpeedY;
    private float gravity = -100f;
    private int hitMask;
    // Start is called before the first frame update
    private CharacterMovement player;
    void Start()
    {
        startSpeedX = -10.0f;
        startSpeedY = 30.0f;
        hitMask = (1 << 7)|(1 << 8);
        hitMask = ~hitMask;
        player = FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 oldPos = transform.position;
        startSpeedY = startSpeedY + gravity * Time.deltaTime;
        oldPos += new Vector2(startSpeedX * Time.deltaTime, startSpeedY * Time.deltaTime);
        
        Vector2 pushback;
        if(startSpeedY < 0){
            RaycastToGround(out pushback);
            oldPos += pushback;
        }
        
        transform.position = oldPos;
        //transform.position//
        if(hitPlayer()){
            Destroy(gameObject);
        }
    }

    bool hitPlayer(){
        if(!player){
            player = FindObjectOfType<CharacterMovement>();
        }
        else{
            Vector3 difference = transform.position - player.transform.position;
            difference.z = 0;
            float distance = difference.magnitude;
            if(distance < 1.5){
                Debug.Log("Hit player!");
                return true;
            }
        }
        return false;
    }

    
    bool RaycastToGround(out Vector2 pushback){// detect ground collision
        Vector2 pos = transform.position;
        Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y - 0.5f);
        Vector2 rayDir = Vector2.up;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, -16.0f, hitMask);
        pushback = new Vector2(0, 0);

        if (hit2D.collider != null)
        {
            Ground ground = hit2D.collider.GetComponent<Ground>();
            if (ground && (hit2D.distance <= 0.11f))
            {
                //Debug.Log(hit2D.distance);
                if(hit2D.distance < 0.1f){
                    pushback = Vector2.up * (0.1f - hit2D.distance);
                    startSpeedY = - startSpeedY * 0.5f;
                }
            }
           
        }
        return true;
    }
}
