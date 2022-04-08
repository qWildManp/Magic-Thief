using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeSurvived = 0;
    public int dir;

    public float speed;
    public bool byBoss;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos += dir * Vector2.right * speed * Time.deltaTime;
        transform.position = pos;

        BallShoot();
        timeSurvived += Time.deltaTime;
        if(timeSurvived > 3.0f){
            Destroy(gameObject);
        }
    }

    
    void BallShoot(){
        Vector2 pos = transform.position;
        Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
        Vector2 rayDir = dir * Vector2.right;
        float rayDistance = 10.0f;
        int mask = (1 << 7);
        mask = ~mask;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, rayDistance, mask);
        bool hit = false;
        if (hit2D.collider != null)
        {
           
            if(hit2D.distance <= 0.1f){
                hit = true;
                if (hit2D.collider.gameObject.GetComponent<DestructibleItem>()!=null)
                {
                    if (hit2D.collider.gameObject.GetComponent<Enemy>()!= null)//Kill Enemy
                    {
                        hit2D.collider.gameObject.GetComponent<Enemy>().EnemeyDie();
                    }
                    else if(hit2D.collider.gameObject.GetComponent<BossBehavior>() != null)
                    {
                        Debug.Log("Hit BOSS");
                    }
                    else
                    {
                        hit2D.collider.gameObject.GetComponent<DestructibleItem>().DestroyItem();
                    }
                    Destroy(gameObject);
                }
                else if(hit2D.collider.gameObject.GetComponent<CharacterStat>() != null&&byBoss)
                {
                    hit2D.collider.gameObject.GetComponent<CharacterStat>().ChangeHealth(-1);
                }
            }

            
        }
        Debug.DrawRay(rayOrigin, rayDir * hit2D.distance, hit? Color.green : Color.red);
    }

}
