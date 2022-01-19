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
    private Camera mainCamera;
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

       ScrollBar currentBar =  ScrollbarController.activeBar;
       currentMode = Convert.ToInt32(currentBar.gameObject.name);
        Debug.Log("Mode: " + currentMode);
        //
        Vector2 TargetPos = mainCamera.transform.position;
        Vector2 CurrentPos = transform.position;
        TargetPos.y += modeRelativeDistance[currentMode - 1].y;
        TargetPos.x += modeRelativeDistance[currentMode - 1].x;
        float speed = 0;
        if(Mathf.Abs(TargetPos.x - CurrentPos.x) > 20)
        {
            speed = 100f;
        }
        else
        {
            speed = 5f;
        }
        transform.position = Vector2.MoveTowards(transform.position, TargetPos, speed * Time.deltaTime);
        /*
        if (Mathf.Abs(TargetPos.x - CurrentPos.x) > 20)
        {
            transform.position = TargetPos;
        }
        else
        {
            StartCoroutine(MoveToPos(TargetPos));
        }
        */
    }

    private IEnumerator MoveToPos(Vector2 TargetPos)
    {
        float currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;
            //transform.position = Vector2.Lerp(transform.position, TargetPos, currentTime / 4);
            Vector2.MoveTowards(transform.position, TargetPos, 10 * Time.deltaTime);
            if (currentTime / 4 >= 1)
                break;
            yield return null;
        }

    }
}
