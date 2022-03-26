using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reachGoalBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Animator PlayerAnimator;
    [SerializeField] GameObject displayUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Debug.Log("Reach Goal");
            PlayerAnimator.SetBool("reachGoal", true);
            displayUI.SetActive(true);
            GameObject.Find("Player").GetComponent<CharacterMovement>().velocity.x = 0;
            GameObject.Find("Player").GetComponent<CharacterMovement>().velocity.y = 0;
            GameObject.Find("Player").GetComponent<CharacterMovement>().maxAcceleration = 0;
        }
    }
}
