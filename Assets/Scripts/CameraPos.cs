using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    // Start is called before the first frame update
    Transform playerTransform;
    GameObject player;
    void Start()
    {
        player = FindObjectOfType<CharacterMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = player.transform;
        //Vector2 updateCameraPos = new Vector3(playerTransform.position.x+6.4f, playerTransform.position.y + 2.0f);
        //transform.position = Vector3.Lerp(transform.position, updateCameraPos, 2f * Time.deltaTime);
        Vector3 updateCameraPos = transform.position;
        float distance_y = Mathf.Abs(transform.position.y - playerTransform.position.y);
        if (player.GetComponent<CharacterMovement>().isGrounded)
            if(distance_y < 0.5 || distance_y >= 10)
                updateCameraPos.y = Mathf.Lerp(transform.position.y, playerTransform.position.y + 5f, 5f * Time.deltaTime);
        if (player.GetComponent<CharacterMovement>().isGrounded == false)
        {
                if(!player.GetComponent<CharacterMovement>().isJumping){
                    updateCameraPos.y = Mathf.Lerp(transform.position.y, playerTransform.position.y , 7f * Time.deltaTime);
            }
        }
        updateCameraPos.x = playerTransform.position.x + 13f;
        //Debug.Log(playerTransform.position.y + 3f);
        //Debug.Log(updateCameraPos);
        transform.position = updateCameraPos;
    }
}
