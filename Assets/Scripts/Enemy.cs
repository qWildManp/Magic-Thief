using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,DestructibleItem
{
    [SerializeField] List<Animator> EnemyAnimators;
    [SerializeField] AudioSource enemyDieSound;
    public float gravity = -100f;
    public Vector2 velocity;
    public bool isGrounded = false;
    public bool isOut = false;
    float ticker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOut)
        {
            DestroyItem();
        }
        EnemyUpdate();
        if (EnemyAnimators[0].GetBool("isDead"))
        {
            ticker += Time.deltaTime;
            if(ticker >= 1.5)
            {
                ticker = 0;
                DestroyItem();
            }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name == "fireball")
        {
            Debug.Log("hit enemy");
            
        }
        
        if(collider.tag == "Player")
        {
            collider.gameObject.GetComponent<CharacterStat>().ChangeHealth(-1);
        }
    }
    public void DestroyItem()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void EnemeyDie()
    {
        foreach(Animator EnemyAnimator in EnemyAnimators)
        {
            EnemyAnimator.SetBool("isDead", true);
        }
        GetComponent<BoxCollider2D>().enabled = false;
        enemyDieSound.Play();
    }


    bool RaycastToGround(out Vector2 pushback)
    {// detect ground collision
        Vector2 pos = transform.position;
        Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y - 0.5f);
        Vector2 rayDir = Vector2.up;
        float rayDistance = velocity.y * Time.deltaTime;
        int hitMask = (1 << 7) | (1 << 8);
        hitMask = ~hitMask;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, -16.0f, hitMask);
        pushback = new Vector2(0, 0);

        isGrounded = false;
        if (hit2D.collider != null)
        {
            Ground ground = hit2D.collider.GetComponent<Ground>();
            fire fire = hit2D.collider.GetComponent<fire>();
            if (hit2D.distance <= 1f)
            {
                if (ground)
                {
                    //Debug.Log(hit2D.distance);
                    isGrounded = true;
                   
                    
                    if (hit2D.distance < 0.9f)
                    {
                        pushback = Vector2.up * (0.9f - hit2D.distance);
                        velocity.y = 0;
                    }
                }
               
            }

        }
        Debug.DrawRay(rayOrigin, rayDir * (-hit2D.distance), isGrounded ? Color.green : Color.red);
        return isGrounded;
    }

    void EnemyUpdate()
    {

        float dt = Time.deltaTime;
        Vector2 pos = transform.parent.position;
        Vector2 pushback;
        // when collision is detected , then push back the player
        if (velocity.y <= 0)
        {
            if (RaycastToGround(out pushback))
            {
                pos += pushback;
                if (velocity.y < 0)
                {
                    velocity.y = 0;
                }
            }
        }
       
        pos.y += velocity.y * dt;
        velocity.y += gravity * dt;
        transform.parent.position = pos;
    }
}
