using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 CameraPos;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            CameraPos = GameObject.Find("MainCamera").transform.position;
            Debug.Log("Progress saved");
            GameObject.Find("CheckpointController").GetComponent<CheckpointCotroller>().SetCurrentCheckpoint(this.gameObject);
        }
    }

}
