using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D rigidbody;
    private static readonly float MAX_RAY_DISTANCE = 1f;
    private static readonly float MAX_SEARCH_DISTANCE = 1f;
    [SerializeField] private Transform searchPos;
    public Transform wallChk;
    public float wallChkDistance;
    public float SlidingSpeed;
    public float wallJumpPower;
    public LayerMask w_Layer;

    public bool isWallJump;
    bool isWall;

    [SerializeField] private Single speed;
    [SerializeField] private Single jumpPower;
    [SerializeField] private Single DashCoolTime = 3;
    [SerializeField] private Image Dashbar;


    public Boolean isJumpable;
    public bool isMove;
    public bool isAttack = false;

    public bool isdash;
    public float dashTime;
    public float defaultTime;
    public float dashspeed;
    private float defaultspeed;

    private Animator animator;
    private float x;

    private bool isCoolTimeDash = false;
    private float StartTIme;
    private float time_current;
    private bool isDown = false;
    public bool isDash = false;
    GameObject block = null;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        isMove = true;
        defaultspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Block"));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-0.5f), Vector2.down, MAX_RAY_DISTANCE, layerMask);
        isJumpable = hit.transform != null;
        if (Input.GetKeyDown(KeyCode.Space) && isJumpable&& isMove)
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumpable = false;
            animator.SetBool("isJump", true);
            animator.SetBool("isRun", false);
        }
        if(hit.transform == null)
        {
            animator.SetBool("isJump", true);
            animator.SetBool("isRun", false);
            
        }
        else
        {
            animator.SetBool("isJump", false);
        }

        RaycastHit2D sea = Physics2D.Raycast(searchPos.position, Vector2.down, MAX_SEARCH_DISTANCE, LayerMask.GetMask("Block"));
        
        if (sea.transform != null)
        {
            
             block = sea.transform.gameObject;
            
            if(Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(GoDown(block));
            }
            else if(!isDown)
            {
                block.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
        else
        {
            if(block!=null)
            {
                block.GetComponent<BoxCollider2D>().isTrigger = true;
                block = null;
            }
            
        }



        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isdash = true;
            StartCoroutine(dash());
        }
        Dashbar.fillAmount = GetDashTime();
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(searchPos.position, Vector2.down * MAX_SEARCH_DISTANCE);
    }
    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        if (x == -1&& isMove&&!isAttack&&!isWallJump)
        {
            gameObject.transform.localScale = new Vector3(-5, 5, 1);
        }
        else if (x == 1&& isMove&&!isAttack && !isWallJump)
        {
            gameObject.transform.localScale = new Vector3(5, 5, 1);
        }
        if (isMove && !isWallJump)
        {
            rigidbody.velocity = new Vector2(x * defaultspeed, rigidbody.velocity.y);
            animator.SetBool("isRun", true);
        }
        else
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

        }
        if (rigidbody.velocity.x == 0)
        {
            animator.SetBool("isRun", false);
        }
    }
    public void Setdefaultspeed(float val)
    {
        defaultspeed = val;
    }
    IEnumerator GoDown(GameObject block)
    {
        if(!isDown)
        {
            isDown = true;
            block.GetComponent<BoxCollider2D>().isTrigger = true;
            yield return new WaitForSeconds(1f);
            isDown = false;
        }
        
    }
    IEnumerator dash()
    {
        if(!isCoolTimeDash)
        {
            isDash = true;
            isCoolTimeDash = true;
            animator.SetBool("isDash", true);
            gameObject.layer = 11;
            defaultspeed = dashspeed;
            yield return new WaitForSeconds(defaultTime);
            gameObject.layer = 9;
            defaultspeed = speed;
            isdash = false;
            animator.SetBool("isDash", false);
            StartTIme = Time.time;
            time_current = 0;
            isDash = false;
            yield return new WaitForSeconds(DashCoolTime);
            isCoolTimeDash = false;
        }
        else
        {
            Debug.Log("dash is cooltime");
            yield break;
        }
        
    }
    public float GetDashTime()
    {
        if (isCoolTimeDash)
        {
            time_current = Time.time - StartTIme;
            if (time_current < DashCoolTime)
            {
                return (time_current / DashCoolTime * 100 / 100);
            }
            else
            {
                time_current = DashCoolTime;
                return 1;
            }
        }
        return 1;
    }
}
