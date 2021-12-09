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
    //[SerializeField] GameObject Line;
    [SerializeField] GameObject mouseCollider;
    [SerializeField] GameObject particlePrefab;
    private List<ParticleSystem> activeParticles;
    private ParticleSystem drawingParticle;
    private bool isAllParticlePlaying = false;
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

    private Collider2D lastCollider;
    // Start is called before the first frame update
    void Start()
    {
        inputVectors = new List<Vector2>();
        playerInputs = new List<PlayerInput>();
        activeParticles = new List<ParticleSystem>();
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
                transform.position = Input.mousePosition;
                isTouchDown = true;
            }
            else{
                isTouchDown = false;
            }
        #endif
        //Debug.Log(touchPosition);
        if(isTouchDown){
            if(!wasTouchDown && isTouchDown){
                CheckAndCreateParticle();
            }
            BeginDraw(touchPosition);
            PushToInputBuffer(touchPosition);
        }
        else if(wasTouchDown && !isTouchDown){
            string pattern = "";
            if(PatternMatchStair()){
                Debug.Log("Is Stair");
                pattern = "Stair";
            }
            else if(PatternMatchCircle())
            {
                Debug.Log("Is Fire Ball");
                pattern = "Fireball";
            }
            inputVectors.Clear();
            playerInputs.Clear();
            EndDraw(pattern);
        }

        wasTouchDown = isTouchDown;
    }

    ParticleSystem InstantiateLine(){
        GameObject newLine = Instantiate(particlePrefab);
        return newLine.GetComponent<ParticleSystem>();
    }

    ParticleSystem GetAnyActiveLine(){
        foreach(ParticleSystem p in activeParticles){
            if(p.particleCount == 0){
                return p;
            }
        }
        return null;
    }

    void CheckAndCreateParticle(){
        if(GetAnyActiveLine() == null){
            drawingParticle = InstantiateLine();
            activeParticles.Add(drawingParticle);
        }
        else{
            drawingParticle = GetAnyActiveLine();
        }
    }
    void BeginDraw(Vector2 touchPosition)
    {

        //Instantiate(Line);
        var screenPoint = new Vector3(touchPosition.x, touchPosition.y, 10);
        drawingParticle.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        drawingParticle.Play();
        //Line.transform.position = touchPosition;
        mouseCollider.transform.position = drawingParticle.transform.position;

    }

    void EndDraw(string pattern){
        if(GetAnyActiveLine() != null){
            isAllParticlePlaying = false;
        }
        else{
            isAllParticlePlaying = true;
        }

        if(lastCollider){
            //
            if(pattern == "Stair"){
                //do something...
                lastCollider.GetComponent<ColliderRegion>().TriggerStair();
            }
        }
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
    bool PatternMatchCircle()
    {
        int currentPhase = 0;
        int currentThreshold = wrongInputThreshold;
        for (int i = 0; i < playerInputs.Count; i++)
        {
            PlayerInput current = playerInputs[i];
            switch (currentPhase)
            {
                case 0:
                case 4:
                    if(current.x == InputDirections.LEFT && current.y == InputDirections.NONE)
                    {
                        currentPhase += 1;
                        currentThreshold = wrongInputThreshold;
                    }
                    if(current.x == InputDirections.RIGHT)
                    {
                        currentThreshold -= 1;
                        if (currentThreshold < 0)
                        {
                            return false;
                        }
                    }
                    break;
                case 1:
                case 5:
                    if(current.x == InputDirections.NONE && current.y == InputDirections.DOWN)
                    {
                        if(currentPhase == 5)
                        {
                            return true;
                        }
                        currentPhase += 1;
                        currentThreshold = wrongInputThreshold;
                    }
                    if(current.y == InputDirections.UP)
                    {
                        currentThreshold -= 1;
                        if (currentThreshold < 0)
                        {
                            return false;
                        }
                    }
                    break;
                case 2:
                case 6:
                    if (current.x == InputDirections.RIGHT && current.y == InputDirections.NONE)
                    {
                        currentPhase += 1;
                        currentThreshold = wrongInputThreshold;
                    }
                    if (current.x == InputDirections.LEFT)
                    {
                        currentThreshold -= 1;
                        if (currentThreshold < 0)
                        {
                            return false;
                        }
                    }
                    break;
                case 3:
                    if (current.x == InputDirections.NONE && current.y == InputDirections.UP)
                    {
                        currentPhase += 1;
                        currentThreshold = wrongInputThreshold;
                    }
                    if (current.y == InputDirections.DOWN)
                    {
                        currentThreshold -= 1;
                        if (currentThreshold < 0)
                        {
                            return false;
                        }
                    }
                    break;
            }
        }
        return false;
    }

    public void SetLastCollider(Collider2D collider){
        lastCollider = collider;
    }
}
