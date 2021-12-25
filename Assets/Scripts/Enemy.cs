using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,DestructibleItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Destroy(this.gameObject);
    }
}
