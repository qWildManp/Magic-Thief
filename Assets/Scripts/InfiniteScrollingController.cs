using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollingController : MonoBehaviour
{
    float barLength = 9.65f;
    float barDist = 10f;
    [SerializeField]private ScrollBar[] bars;
    private GameObject activeEndPoint;
    public ScrollBar activeBar;
    [SerializeField] private CharacterMovement player;
    //bars[0]

    void Start(){
        activeEndPoint = bars[0].GetEndPoint();
        activeBar = bars[0];
    }
    void Update(){
        if (!player)
        {
            return;
        }
        if(player.transform.position.x > activeEndPoint.transform.position.x){// if current bar reaches the end
            Debug.Log("Exit Bar");
            OnBarExit();
        }
    }

    public void ShiftBars(){//change bar order
        ScrollBar head = bars[0];
        for(int i = 0; i < bars.Length - 1; i++){
            bars[i] = bars[i+1];
        }
        bars[bars.Length - 1] = head;
    }
    public void OnBarExit(){// Actions when the current bar is end
        //shift bars;
        ShiftBars();
        MoveBarToEnd();
        activeBar = bars[0];
        activeEndPoint = bars[0].GetEndPoint();
    }

    public void MoveBarToEnd(){// move the bar to the last bar position
        ScrollBar bar = bars[bars.Length - 1];
        ScrollBar lastBar = bars[bars.Length - 2];
        Vector3 startPos = bar.GetStartPoint().transform.position;
        Vector3 endPos = lastBar.GetEndPoint().transform.position;
        Vector3 diff = endPos - startPos;
        bar.transform.position += diff;
    }
}
