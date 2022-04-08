using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownController : MonoBehaviour
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
        Debug.Log(collider.name);
        if(collider.tag == "Player")
        {
            CharacterStat playerStat = collider.gameObject.GetComponent<CharacterStat>();
            CharacterMovement playerMovement = collider.gameObject.GetComponent<CharacterMovement>();
            playerMovement.isOut = true;
        }
        
    }
}
