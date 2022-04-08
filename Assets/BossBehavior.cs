using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour, DestructibleItem
{
    [SerializeField] GameObject enemyPrefabe;
    [SerializeField] GameObject lavaBallPrefabe;
    [SerializeField] GameObject ballPrefabe;
    [SerializeField] GameObject player;
    [SerializeField] GameObject genPoint;
    [SerializeField] Animator bossAnimator;
    [SerializeField] List<AudioClip> soundEffects;
    public int currentMode;
    public float currentCount;
    public float modeCount;
    public List<Vector2> modeRelativeDistance;
    public List<Vector2> prefabeGenPoints;
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
        bool isPlayerOut = player.GetComponent<CharacterMovement>().isOut;
        if (!isPlayerOut)
        {
            BossAttack(currentMode);
        }
        else if (isPlayerOut)
        {
            this.currentCount = 0; 
        }
        

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

    void BossAttack(int currentMode)
    {
        genPoint.transform.localPosition = prefabeGenPoints[currentMode - 1];
        switch (currentMode) {
            case 1:
                GetComponent<BoxCollider2D>().size = new Vector2(3.2f, 2.2f);
                bossAnimator.SetInteger("Mode", 1);
                bossAnimator.SetBool("ShootShell", false);
                this.modeCount = 3.5f;
                if (currentCount >= this.modeCount)
                {
                    currentCount = 0;
                    bossAnimator.SetBool("ShootLavaBall", true);
                }
                    //ThrowLavaBall();
                    
                break;
            case 2:
                this.modeCount = 2f;
                bossAnimator.SetBool("ShootLavaBall", false);
                if (currentCount >= this.modeCount)
                {
                    currentCount = 0;
                    bossAnimator.SetBool("GenEnemy", true);
                    //GenerateEnemy();
                }
                break;
            case 3:
                GetComponent<BoxCollider2D>().size = new Vector2(1.6f, 2.2f);
                this.modeCount = 2.5f;
                bossAnimator.SetBool("GenEnemy", false);
                bossAnimator.SetInteger("Mode", 3);
                if (currentCount >= this.modeCount)
                {
                    currentCount = 0;
                    bossAnimator.SetBool("ShootShell", true);
                }
                break;
            default:
                return;
        }
        currentCount += Time.deltaTime;

    }
    void ThrowLavaBall()
    {
            GameObject lavaball = Instantiate(lavaBallPrefabe);
            GetComponent<AudioSource>().clip = soundEffects[currentMode-1];
            GetComponent<AudioSource>().volume = 0.05f;
            GetComponent<AudioSource>().Play();
            lavaball.transform.position = genPoint.transform.position;
            bossAnimator.SetBool("ShootLavaBall", false);

    }

    void GenerateEnemy()
    {
        
            GameObject enemy = Instantiate(enemyPrefabe);
            GetComponent<AudioSource>().clip = soundEffects[currentMode - 1];
            GetComponent<AudioSource>().volume = 0.1f;
            GetComponent<AudioSource>().Play();
            enemy.transform.position = genPoint.transform.position;
            bossAnimator.SetBool("GenEnemy", false);

    }

    void ShootBall()
    {

        GameObject canonball = Instantiate(ballPrefabe);
        GetComponent<AudioSource>().clip = soundEffects[currentMode - 1];
        GetComponent<AudioSource>().volume = 0.05f;
        GetComponent<AudioSource>().Play();
        canonball.transform.position = genPoint.transform.position;
        bossAnimator.SetBool("ShootShell", false);

    }
    public void DestroyItem()
    {
        throw new NotImplementedException();
    }
}
