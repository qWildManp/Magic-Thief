using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public float depth = 1;
    public bool loop;
    CharacterMovement player;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<CharacterMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player)
        {
            float realVelocity = player.velocity.x / depth;
            Vector2 pos = transform.position;

            pos.x -= realVelocity * Time.fixedDeltaTime;

            if (pos.x <= -12 && loop)
            {
                pos.x = 20;
            }
            transform.position = pos;
        }
    }
}
