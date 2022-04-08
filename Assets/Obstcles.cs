using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstcles : MonoBehaviour,DestructibleItem
{
    public bool canRespawn;
    public float RespawnTime;
    public float currentTime;
    private BoxCollider2D collider;
    [SerializeField] private List<GameObject> obstacleSprites;
    public void DestroyItem()
    {
        // Destroy(this.gameObject);
        collider.enabled = false;
        foreach(GameObject obstacleSprite in obstacleSprites)
            obstacleSprite.SetActive(false);
    }

    public void RespawnItem()
    {
        // Destroy(this.gameObject);
        collider.enabled = true;
        foreach (GameObject obstacleSprite in obstacleSprites)
            obstacleSprite.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
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
                RespawnItem();
            }
        }
    }
}
