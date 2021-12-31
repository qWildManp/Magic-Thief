using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeSurvived = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos += Vector2.right * 6.0f * Time.deltaTime;
        transform.position = pos;
        
        if(RaycastToRight()){
            //Destroy(gameObject);
        }
        timeSurvived += Time.deltaTime;
        if(timeSurvived > 5.0f){
            Destroy(gameObject);
        }
    }

    
    bool RaycastToRight(){
        Vector2 pos = transform.position;
        Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
        Vector2 rayDir = Vector2.right;
        float rayDistance = 10.0f;
        int mask = (1 << 7);
        mask = ~mask;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, rayDistance, mask);
        bool hit = false;
        if (hit2D.collider != null)
        {
            Debug.Log("Hit" + hit2D.collider.name);
            if(hit2D.distance <= 0.1f){
                hit = true;
                if (hit2D.collider.gameObject.GetComponent<DestructibleItem>()!=null)//Kill Enemy
                {
                    hit2D.collider.gameObject.GetComponent<DestructibleItem>().DestroyItem();
                    Destroy(gameObject);
                }
            }
            
        }
        Debug.DrawRay(rayOrigin, rayDir * hit2D.distance, hit? Color.green : Color.red);
        return hit;
    }
}
