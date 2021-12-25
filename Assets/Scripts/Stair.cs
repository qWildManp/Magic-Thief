using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    GameObject player;
    float oldVelocity_x;
    float distance_x ;
    float distance_y;
    float t;
    float newVelocity_y;
    [SerializeField]GameObject startPt;
    [SerializeField]GameObject EndPt;
    bool playerAtStair;
    // Start is called before the first frame update
    void Start()
    {
        playerAtStair = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = player.transform.position;
        CharacterMovement movement = player.GetComponent<CharacterMovement>();
        float percentage = 0;
        
        if (player.transform.position.x >= EndPt.transform.position.x && playerAtStair)
        {
            Debug.Log("leave stair");
            playerAtStair = false;
            movement.isOnStair = false;

        }
        else if(player.transform.position.x >= startPt.transform.position.x &&
                player.transform.position.x < EndPt.transform.position.x
                && !playerAtStair)
        {
            playerAtStair = true;
            movement.isOnStair = true;
            Debug.Log("Reach point");
            distance_x = EndPt.transform.position.x - startPt.transform.position.x;
            Debug.Log("dis_x:" + distance_x);
            distance_y = EndPt.transform.position.y - startPt.transform.position.y;
            Debug.Log("dis_y:" + distance_y);
            
            /*
            oldVelocity_x = player.GetComponent<CharacterMovement>().velocity.x;
            Debug.Log("old_velocity_x: " + oldVelocity_x);
            t = distance_x / player.GetComponent<CharacterMovement>().velocity.x;
            Debug.Log("t: " + t);
            newVelocity_y = distance_y / t;
            Debug.Log("new_velocity_y: " + oldVelocity_x);*/
        }

        if (playerAtStair)
        {
            percentage = (player.transform.position.x - startPt.transform.position.x) / distance_x;
            percentage = Mathf.Clamp01(percentage);
            
            position.y = startPt.transform.position.y + percentage * distance_y;
            if(player.transform.position.y < position.y){
                //Debug.Log(position);
                player.transform.position = position;
                movement.velocity.y = 0;
                movement.isGrounded = true;
                movement.isOnStair = true;
            }
            
            /*position.y += newVelocity_y * Time.deltaTime;
            position.x += oldVelocity_x * Time.deltaTime;
            player.GetComponent<CharacterMovement>().velocity.x = oldVelocity_x;
            player.GetComponent<CharacterMovement>().velocity.y = 0;
            player.transform.position = position;*/
            //player.transform.position = EndPt.transform.position;
        }
        //player.transform.position = position;
        //Debug.Log("St:" + startPt.transform.position);
        //Debug.Log("En:" + EndPt.transform.position);
    }
}
