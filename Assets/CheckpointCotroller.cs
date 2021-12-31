using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCotroller : MonoBehaviour
{
    public float recordInterval;
    public float currentTime =  0;
    public bool isInfiniteGround;
    [SerializeField] private GameObject currentCheckpoint;
    [SerializeField] GameObject player;
    //[SerializeField] List<GameObject> checkpoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!player)
        {
            return;
        }
        CharacterMovement playerMovement = player.GetComponent<CharacterMovement>();
        if (playerMovement.isOut)
        {
                /*
                if (isInfiniteGround) {
                    player.transform.position = new Vector2(transform.position.x, 6f);

                }
                else
                {
                    player.transform.position = transform.position;
                }*/
                player.transform.position = GetCurrentCheckpointPos();
                playerMovement.velocity.x = 10f;
                playerMovement.isOut = false;
        }
        /*
        else
        {
            if (currentTime < recordInterval)
            {
                currentTime += Time.fixedDeltaTime;
            }
            else
            {
                currentTime = 0;
                transform.position = player.transform.position;
            }
        }
        */
    }

    public void SetCurrentCheckpoint(GameObject checkpoint)
    {
        this.currentCheckpoint = checkpoint;
    }
    public Vector3 GetCurrentCheckpointPos()
    {
        return this.currentCheckpoint.transform.position;
    }
}
