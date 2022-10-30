using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    enum STATE { Idle, Chase, Attack };

    GamaManager gm;
    FloorSetting floorSetting;

    public GameObject player;
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject playerForwardDir;
    public GameObject PlayerPos;
    public GameObject MonsterHead;
    public AudioSource monsterSource;
    public AudioClip[] monsterClip;

    public Transform turningPoint;
    public Transform destroyPoint;
    public bool isTurn;

    public bool isGone;
    public bool isHit;
    public bool isChase;
    public bool isWalkStart;
    private int animState;
    private int onceTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gm = GameObject.Find("GameManager").GetComponent<GamaManager>();
        isHit = false;
        isChase = false;
        isWalkStart = false;
        onceTime = 1;
        isGone = false;

        if (player.GetComponent<PlayerControl>().nowScene == 0 || player.GetComponent<PlayerControl>().nowScene == 1)
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            monsterSource = GetComponent<AudioSource>();
            animator.SetBool("run", true);
            monsterSource.loop = false;
            monsterSource.clip = monsterClip[0];
            monsterSource.Play();

            animState = 1;
        }
        if (player.GetComponent<PlayerControl>().nowScene == 2)
        {
            turningPoint = GameObject.Find("TurningPoint").transform;
            destroyPoint = GameObject.Find("PlayerPosition1").transform;
            isTurn = false;
        }

        PlayerPos = GameObject.Find("Player");
        playerForwardDir = GameObject.Find("ForwardDirection");
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)gm.gameState > 0)
        {
            if (player.GetComponent<PlayerControl>().nowScene == 0 || player.GetComponent<PlayerControl>().isLastDoorOpen)
            {
                chasePlayer();
                monsterSource.pitch = 2;
                animator.SetInteger("MonsterState", animState);
            }
            else if (player.GetComponent<PlayerControl>().nowScene == 2 && player.GetComponent<PlayerControl>().isHiding == true)
            {
                isAction();
            }

            if (isHit == true)
            {
                
                Vector3 dir = MonsterHead.transform.position - PlayerPos.transform.position;
                PlayerPos.transform.rotation = Quaternion.Lerp(PlayerPos.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3);

            }
        }

        if(animState == 1)
        {

            if (!monsterSource.isPlaying)
            {
                monsterSource.loop = true;
                monsterSource.clip = monsterClip[1];
                monsterSource.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && player.GetComponent<PlayerControl>().nowScene != 2)
        {
            isHit = true;
            monsterSource.Stop();
            animState = 2;
            animator.SetInteger("MonsterState", animState);
            transform.LookAt(PlayerPos.transform.position);
            StartCoroutine(changeScene());
            player.GetComponent<OVRPlayerController>().Acceleration = 0;  //플레이어 못 움직이게
        }
    }

    public void isAction()
    {
        animState = 1;
        
        if(Vector3.Distance(transform.position, turningPoint.position) <= 2.0f)
            isTurn = true;
        if (Vector3.Distance(transform.position, destroyPoint.position) <= 5.0f && isTurn == true)
        {
            floorSetting = GameObject.Find("FloorSetting").GetComponent<FloorSetting>();
            floorSetting.playerControl.isHiding = false;
            isGone = true;
        }

        if (isTurn)
        {
            animState = 0;
            Invoke("setTarget", 5f);
        }
        else
        {
            animState = 1;
            agent.destination = turningPoint.position;
        }
        animator.SetInteger("MonsterState", animState);
    }
    
    public void setTarget()
    {
        animState = 1;
        agent.destination = destroyPoint.position;
    }

    public bool chasePlayer()
    {
        isWalkStart = true;
        onceTime = -1;
        agent.speed = 8.0f;
        agent.SetDestination(player.transform.position);
        return true;

        agent.speed = 3.5f;
        animState = 0;

    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(2.0f);//FadeOut될동안 3초 딜레이 
        LodingSceneControlScr.LoadScene("EndingScene");//씬 로드 

    }
}
