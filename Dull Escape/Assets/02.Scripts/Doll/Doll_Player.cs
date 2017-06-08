using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Doll_Player : MonoBehaviour
{

    private float h = 0.0f;
    private float v = 0.0f;

    private Transform tr;
    private Rigidbody rig;
    
    //private int state = 0; // 0. nomal, 1.groggy, 2. dead, 3.damaged, 4. Jump, 5.fall, 6. getup
    public enum Doll_State { idle, move, jump, attack, damage, die, groggy}
    public Doll_State doll_state = Doll_State.idle;

    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;

    public int HP = 100;

    private bool CheckJump = false;
    public bool attack = false;

    private Animator animator;
    GameObject dead_zone;
    GameObject other_player;
    GameObject obj;

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        animator = this.gameObject.GetComponent<Animator>();
        obj = GameObject.FindGameObjectWithTag("Obejct");
        dead_zone = GameObject.FindGameObjectWithTag("deadzone");
        other_player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
        if (CheckBeHave(doll_state))
        { 
            Move();
            Attack();
            rotatePlayer();
        }
        DollAction();
    }

    bool CheckBeHave(Doll_State state)
    {
        if(state == Doll_State.die || state == Doll_State.groggy)
        {
            return false;
        }
        return true;
    }

    void Move() // 이동
    {
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        
        if (CheckJump == false && Input.GetKey(KeyCode.Space))
        {
            CheckJump = true;
            moveDir = (Vector3.up);
            rig.MovePosition(transform.position + (Vector3.up));
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
        }
        float speed = Mathf.Abs(v) + Mathf.Abs(h);
        animator.SetFloat("speed", speed);
       
        tr.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);
    }



    void Attack() // 공격
    {
        if (Input.GetMouseButtonDown(1))
            doll_state = Doll_State.attack;  
        else if(Input.GetMouseButtonUp(1))
            doll_state = Doll_State.idle; 
    }

    void rotatePlayer()
    {
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "deadzone")
        {
            doll_state = Doll_State.die;
        }

        if (other.gameObject.tag == "BULLET")
        {
            HP -= 100;
         
         
            doll_state = Doll_State.groggy;
        }

        if (other.gameObject.tag == "Player")
        {
            
            if (HP > 0 && other.gameObject.GetComponent<Doll>().attack == true)
            {
                HP -= 50;
                other_player.gameObject.GetComponent<Doll>().HP -= 10;
                doll_state = Doll_State.damage;
                
            }
            if (HP <= 0)
            {

                doll_state = Doll_State.groggy; 
              
               
             }
        }     
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Object")
        {
            //doll_state = Doll_State.attack;
            CheckJump = false;
        }

        
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Punch")
        {
            HP -= 10;

            if (HP > 0)
            {

                doll_state = Doll_State.damage;

            }
            if (HP <= 0)
            {

                doll_state = Doll_State.groggy;


            }
        }
    }


    void DollAction()
    {
        switch (doll_state)
        {
            case Doll_State.attack:
                animator.SetBool("ATK", true);
                animator.SetBool("Damage", false);
                break;

            case Doll_State.idle:
                animator.SetBool("ATK", false);
                break;

            case Doll_State.damage:
                animator.SetTrigger("isDamage");
                doll_state = Doll_State.idle;
                break;
            case Doll_State.die:

                animator.SetBool("Die", true);
                animator.SetBool("Dead", true);

                break;
            case Doll_State.groggy:

                animator.SetTrigger("isGroggy");
                doll_state = Doll_State.idle;
                break;
        }  
        
    }

}

