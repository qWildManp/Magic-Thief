using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchChecker : MonoBehaviour
{
    private enum InputDirections{
        LEFT,
        RIGHT,
        DOWN,
        UP,
        NONE
    }

    private struct PlayerInput{
        public InputDirections x;
        public InputDirections y;
    }

    private List<Vector2> inputVectors;
    private List<PlayerInput> playerInputs;
    private bool wasTouchDown;
    private bool isTouchDown;
    private const int MAX_BUFFER = 128;
    private int wrongInputThreshold = 10;
    // Start is called before the first frame update
    void Start()
    {
        inputVectors = new List<Vector2>();
        playerInputs = new List<PlayerInput>();
    }


    PlayerInput GetInputFromDelta(Vector2 delta){
        PlayerInput p;
        p.x = InputDirections.NONE;
        p.y = InputDirections.NONE;
        if(delta.magnitude != 0){
            //delta = delta.normalized;
            Vector2 nrmDelta = delta.normalized;
            
            if(Mathf.Abs(nrmDelta.x) > 0.707f){
                p.x = nrmDelta.x > 0? InputDirections.RIGHT : InputDirections.LEFT;
            }
            if(Mathf.Abs(nrmDelta.y) > 0.707f){
                p.y = nrmDelta.y > 0? InputDirections.UP : InputDirections.DOWN;
            }
            //Debug.Log(nrmDelta + ", res: " + p.x + " " + p.y);
        }
        return p;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 touchPosition = new Vector2(0, 0);
        #if UNITY_IOS || UNITY_ANDROID
            if(Input.touchCount > 0){
                //Debug.Log("touched!");
                Touch touch = Input.GetTouch(0);
                touchPosition = touch.position;
                isTouchDown = true;
            }
            else{
                isTouchDown = false;
            }
        #else
            if(Input.GetMouseButton(0)){
                //Debug.Log("touched!");
                touchPosition = Input.mousePosition;
                isTouchDown = true;
            }
            else{
                isTouchDown = false;
            }
        #endif
        //Debug.Log(touchPosition);
        if(isTouchDown){
            PushToInputBuffer(touchPosition);
        }
        else if(wasTouchDown && !isTouchDown){
            if(PatternMatchStair()){
                Debug.Log("Is Stair");
            }
            inputVectors.Clear();
            playerInputs.Clear();
        }

        wasTouchDown = isTouchDown;
    }

    void PushToInputBuffer(Vector2 touchPosition){
        
        if(inputVectors.Count > 0){
            Vector2 delta = touchPosition - inputVectors[inputVectors.Count - 1];
            
            PlayerInput p = GetInputFromDelta(delta);
            //Debug.Log(p.x + " " + p.y);
            playerInputs.Add(p);
        }
        
        inputVectors.Add(touchPosition);
        if(inputVectors.Count > MAX_BUFFER){
            inputVectors.RemoveAt(0);
        }
    }

        
    bool PatternMatchStair(){
        int currentPhase = 0;
        int currentThreshold = wrongInputThreshold;
        for(int i = 0; i < playerInputs.Count; i++){
            PlayerInput current = playerInputs[i];
            if(current.x == InputDirections.LEFT || current.y == InputDirections.DOWN){
                    currentThreshold -= 1;
                    if(currentThreshold < 0){
                        Debug.Log("Over Threshold");
                        return false;
                    }
            }

            switch(currentPhase){
            case 0:
                if(current.x == InputDirections.RIGHT && current.y == InputDirections.NONE){
                    currentPhase = 1;
                    //Debug.Log("Phase 0 Done");
                }
                break;
            case 1:
                if(current.y == InputDirections.UP && current.x == InputDirections.NONE){
                    currentPhase = 2;
                    //Debug.Log("Phase 1 Done");
                }
                break;
            case 2:
                if(current.x == InputDirections.RIGHT && current.y == InputDirections.NONE){
                    //Debug.Log("Phase 2 Done");
                    return true;
                }
                break;
            default:
                break;
            }
        }
        return false;
    }
}
