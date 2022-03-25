using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch : MonoBehaviour
{
    [SerializeField] private float MaxHp;
    [SerializeField] private int amendsCoin;
    [SerializeField] private GameObject HpbarPrefab;
    [SerializeField] private GameObject Attack_1;
    public int Damage;
    private GameObject Camaera;
    private GameObject Hpbar;
    private GameObject HpbarObject;
    private float Hp;


    public GameObject target;
    Rigidbody2D rigid;
    public int nextMove;
    Animator animator;
    bool move;
    private bool ismove;
    bool coru;
    bool isDie = false;
    bool isAttack = false;
    void Awake()
    {
        target = GameObject.Find("Player");
        Camaera = GameObject.Find("Main Camera");
        HpbarObject = Instantiate(HpbarPrefab);
        Hpbar = HpbarObject.transform.GetChild(1).gameObject;
        coru = false;
        move = false;
        ismove = true;
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Invoke("movelange", 3);
    }
    private void Start()
    {
        Hp = MaxHp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerAttack_1"))
        {
            Hp -= target.GetComponent<PlayerState>().GeneralDamage();
            Camaera.GetComponent<CameraShake>().VibrateForTime(0.1f);
        }
        if (collision.transform.CompareTag("PlayerAttack_2"))
        {
            Hp -= target.GetComponent<PlayerState>().GeneralDamage();
            Camaera.GetComponent<CameraShake>().VibrateForTime(0.1f);
        }
        if (collision.transform.CompareTag("Double_Slash"))
        {
            Hp -= target.GetComponent<PlayerState>().ESkillDamage();
            Camaera.GetComponent<CameraShake>().VibrateForTime(0.1f);
        }
        if (collision.transform.CompareTag("QSkill"))
        {
            Hp -= target.GetComponent<PlayerState>().XSkillDamage();
            Camaera.GetComponent<CameraShake>().VibrateForTime(0.1f);
        }
    }
    IEnumerator KnockBack(float dir)
    {
        float ctime = 0;
        float speed = 0.1f;
        while (ctime < 0.2f)
        {
            if (transform.localScale.x == 5)
                transform.Translate(Vector2.left * speed * dir);
            else
                transform.Translate(Vector2.left * speed * -1 * dir);
            ctime += Time.deltaTime;
            yield return null;
        }
    }
    void FixedUpdate()
    {
        if (ismove && !isDie)
        {
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        }
        else
            rigid.velocity = new Vector2(0, rigid.velocity.y);



        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y - 1f);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (raycast.collider == null && !isAttack)
        {
            Debug.Log("Turn");
            Turn();
        }
        if (nextMove != 0 && !isDie)
        {
            if (nextMove == -1)
            {
                transform.localScale = new Vector3(-4f, transform.localScale.y, transform.localScale.z);
            }
            else
                transform.localScale = new Vector3(4f, transform.localScale.y, transform.localScale.z);
            animator.SetBool("walk", true);
        }

    }

    void Update()
    {
        if (HpbarObject != null)
            UpdateHpbar();
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 5 && !isDie)
        {

            move = true;
            Facetarget();
            if (distance <= 2.3)
            {
                animator.SetBool("walk", false);
                nextMove = 0;
                if (!coru)
                    StartCoroutine(Attacktatget());
            }
            else
            {
                Movetarget();
            }
        }
    }
    void movelange()
    {
        animator.SetBool("walk", false);
        nextMove = Random.Range(-1, 2);
        float time = Random.Range(2f, 5f);
        if (!move)
        {
            Invoke("movelange", time);
        }
        else
        {
            animator.SetBool("walk", false);
        }

    }
    void Turn()
    {
        if (isAttack)
            return;
        nextMove = nextMove * (-1);
        if (nextMove == -1)
        {
            transform.localScale = new Vector3(4, transform.localScale.y, transform.localScale.z);
        }
        else
            transform.localScale = new Vector3(-4, transform.localScale.y, transform.localScale.z);
        CancelInvoke();
        Invoke("movelange", 3);
    }
    void Facetarget()
    {
        if (!coru)
        {
            //
            if (target.transform.position.x - transform.position.x < 0)
            {
                transform.localScale = new Vector3(-4f, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(4f, transform.localScale.y, transform.localScale.z);
            }
        }

    }
    void Movetarget()
    {
        if (target.transform.position.x - transform.position.x < 0)
        {
            nextMove = -1;
        }
        else
        {
            nextMove = 1;
        }
    }
    IEnumerator Attacktatget()
    {
        ismove = false;
        coru = true;
        isAttack = true;
        animator.SetBool("attack1", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("attack1", false);
        isAttack = false;

        yield return new WaitForSeconds(2f);
        coru = false;
        ismove = true;
    }
    void Offattack()
    {
        Attack_1.SetActive(false);
    }
    void UpdateHpbar()
    {
        HpbarObject.transform.position = new Vector2(gameObject.transform.position.x - 1f, gameObject.transform.position.y + 1f);
        Hpbar.transform.localScale = new Vector2((Hp / MaxHp * 100 / 100), 0.3715625f);
        if (Hp <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(DieAnimator());
        }
    }
    IEnumerator DieAnimator()
    {
        rigid.velocity = Vector2.zero;
        isDie = true;
        Attack_1.SetActive(false);
        Destroy(HpbarObject);
        gameObject.layer = 0;
        animator.SetBool("isdie", true);
        animator.SetBool("walk", false);
        animator.SetBool("attack1", false);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        rigid.gravityScale = 0;
        target.GetComponent<InventorySystem>().coin.SetCoin(amendsCoin);
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
    public void OnAttack_1()
    {
        Attack_1.SetActive(true);
    }
}
