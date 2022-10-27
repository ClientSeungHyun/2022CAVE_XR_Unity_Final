using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    enum STATE { Idle, Chase };

    public GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    public GameObject playerForwardDir;
    public GameObject PlayerPos;
    public GameObject MonsterHead;
    public AudioSource monsterSource;
    public AudioClip[] monsterClip;

    public Transform turningPoint;
    public Transform destroyPoint;
    public bool isTurn;


    private bool isHit = false;
    public bool isChase;
    private int animState;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (player.GetComponent<PlayerControl>().nowScene == 0)
        {
            
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            monsterSource = GetComponent<AudioSource>();
            animator.SetBool("run", true);
            monsterSource.loop = false;
            monsterSource.clip = monsterClip[0];
            monsterSource.Play();

            animState = 0;
        }
        if (player.GetComponent<PlayerControl>().nowScene == 2)
        {
            turningPoint = GameObject.Find("TurningPoint").transform;
            destroyPoint = GameObject.Find("PlayerPosition0").transform;
            isTurn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<PlayerControl>().nowScene == 0)
        {
            chasePlayer();
            animator.SetInteger("MonsterState", animState);
        }
        else if (player.GetComponent<PlayerControl>().nowScene == 2 && player.GetComponent<PlayerControl>().isHaveKey==true)
        {
            
            Invoke("isAction", 2);
        }

        if (isHit == true)
        {
            Vector3 dir = MonsterHead.transform.position - PlayerPos.transform.position;
            PlayerPos.transform.rotation = Quaternion.Lerp(PlayerPos.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3);
            //Debug.Log(PlayerPos.transform.rotation);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            isHit = true;
            transform.LookAt(PlayerPos.transform.position);
            StartCoroutine(changeScene());
            //Cam.transform.LookAt(transform.position);
            //transform.position = playerForwardDir.transform.position
        }
    }

    public void isAction()
    {
        this.gameObject.SetActive(true);
        
        if(isTurn)
            agent.SetDestination(destroyPoint.position);
        else
            agent.SetDestination(turningPoint.position);
    }
    
    public bool chasePlayer()
    {

        animState = 1;
        agent.speed = 8.0f;
        agent.SetDestination(player.transform.position);
        return true;

        agent.speed = 3.5f;
        animState = 0;

    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(3.0f);//FadeOutµ…µøæ» 3√  µÙ∑π¿Ã 
        LodingSceneControlScr.LoadScene("EndingScene");//æ¿ ∑ŒµÂ 

    }
}
