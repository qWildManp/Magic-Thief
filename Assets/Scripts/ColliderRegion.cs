using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRegion : MonoBehaviour
{
    private Collider2D collider2D;
    void Awake(){
        collider2D = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.GetComponent<TouchChecker>() != null){
            collider.GetComponent<TouchChecker>().SetLastCollider(collider2D);
            Debug.Log("Mouse In Range!");
        }
    }

    public void TriggerStair(){
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
