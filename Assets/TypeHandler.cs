using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> subgrounds;
    [SerializeField] public GameObject currentGound;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentGound);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name+ " Enter");
        if(collider.name == "StartPoint")
            currentGound = collider.transform.parent.gameObject;
        
    }
}
