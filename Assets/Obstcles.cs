using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstcles : MonoBehaviour,DestructibleItem
{
    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "fireball")
        {
            DestroyItem();

        }
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
