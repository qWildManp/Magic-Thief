using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollingController : MonoBehaviour
{
    float barLength = 9.65f;
    float barDist = 10f;
    [SerializeField]private ScrollBar[] bars;
    private GameObject activeEndPoint;
    [SerializeField] private CharacterMovement player;
    //bars[0]

    void Start(){
        activeEndPoint = bars[0].GetEndPoint();
    }
    void Update(){
        if(player.transform.position.x > activeEndPoint.transform.position.x){
            Debug.Log("Exit Bar");
            OnBarExit();
        }
    }

    public void ShiftBars(){
        ScrollBar head = bars[0];
        for(int i = 0; i < bars.Length - 1; i++){
            bars[i] = bars[i+1];
        }
        bars[bars.Length - 1] = head;
    }
    public void OnBarExit(){
        //shift bars;
        ShiftBars();
        MoveBarToEnd();
        activeEndPoint = bars[0].GetEndPoint();
    }

    public void MoveBarToEnd(){
        ScrollBar bar = bars[bars.Length - 1];
        ScrollBar lastBar = bars[bars.Length - 2];
        Vector3 startPos = bar.GetStartPoint().transform.position;
        Vector3 endPos = lastBar.GetEndPoint().transform.position;
        Vector3 diff = endPos - startPos;
        bar.transform.position += diff;
    }
}
