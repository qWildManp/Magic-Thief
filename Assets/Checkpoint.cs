using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Progress saved");
            GameObject.Find("CheckpointController").GetComponent<CheckpointCotroller>().SetCurrentCheckpoint(this.gameObject);
        }
    }
}
