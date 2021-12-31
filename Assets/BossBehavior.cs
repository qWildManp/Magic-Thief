using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public int currentMode;
    public List<Vector2> modeRelativeDistance;
    public InfiniteScrollingController ScrollbarController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       ScrollBar currentBar =  ScrollbarController.activeBar;
       currentMode = Convert.ToInt32(currentBar.gameObject.name);
        Debug.Log("Mode: " + currentMode);
    }
}
