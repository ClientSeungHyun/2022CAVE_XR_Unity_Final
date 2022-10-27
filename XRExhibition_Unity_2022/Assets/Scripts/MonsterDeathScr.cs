using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathScr : MonoBehaviour
{

    public Animator animator;
    public GameObject playerForwardDir;
    public GameObject PlayerPos;
    public GameObject MonsterHead;
    private bool isHit = false;

    // Start is called before the first frame update

    void Start()
    {
        animator.SetBool("run", true);

    }



    // Update is called once per frame

    void Update()
    {
        if (isHit == true)
        {
            Vector3 dir = MonsterHead.transform.position - PlayerPos.transform.position;
            PlayerPos.transform.rotation = Quaternion.Lerp(PlayerPos.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3);
            //Debug.Log(PlayerPos.transform.rotation);

        }
        //Debug.Log("start")
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            isHit = true;
            transform.LookAt(PlayerPos.transform.position);
            //Cam.transform.LookAt(transform.position);
            //transform.position = playerForwardDir.transform.position
        }
    }
}