using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstcles : MonoBehaviour,DestructibleItem
{
    public bool canRespawn;
    public float RespawnTime;
    public float currentTime;
    private BoxCollider2D collider;
    private GameObject obstacleSprite;
    public void DestroyItem()
    {
        // Destroy(this.gameObject);
        collider.enabled = false;
        obstacleSprite.SetActive(false);
    }
        // Start is called before the first frame update
        void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        obstacleSprite = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (!collider.enabled && canRespawn)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= RespawnTime)
            {
                currentTime = 0;
                collider.enabled = true;
                obstacleSprite.SetActive(true);
            }
        }
    }
}
