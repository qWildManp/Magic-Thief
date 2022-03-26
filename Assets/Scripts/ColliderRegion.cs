using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRegion : MonoBehaviour
{
    public bool isStair;
    [SerializeField] private GameObject Stair;
    void Awake(){

    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider){
        if(isStair&&collider.GetComponent<TouchChecker>() != null){
            collider.GetComponent<TouchChecker>().SetLastCollider(this.gameObject);
            //Debug.Log("Mouse In Range!");
        }
        else if(!isStair)
        {
            Debug.Log(collider.name);
            if (collider.tag == "Player")
            {
                Debug.Log("Generate Enemy");
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Paralax>().depth = 1.5f;
            }
        }
    }

    public void TriggerStair(bool trigger){
        //Debug.Log(transform.GetChild(0));
        Stair.SetActive(trigger);
    }
}
