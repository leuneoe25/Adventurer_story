using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormerWarriorBoss : MonoBehaviour
{
    bool isattack;
    
    private GameObject Hpbar; // ���ݹ���
     private GameObject HpbarObject; // ���ݹ���
    [SerializeField] private GameObject Portal; // ���ݹ���
    [SerializeField] private GameObject Attack1Area; // ���ݹ���
    [SerializeField] private GameObject Attack2Area; // ���ݹ���
    [SerializeField] private GameObject AttackRange_1; // ���ݹ���
    [SerializeField] private GameObject AttackRange_2; // ���ݹ���
    [SerializeField] private GameObject SkillEffect_1; // ��ų ����Ʈ
    [SerializeField] private GameObject SkillEffect_2; // ��ų ����Ʈ
    [SerializeField] private float MaxHp; // ��ų ����Ʈ
    private bool isAction;
    private GameObject Player;
    public int Damage;
    public int nrl;
    public int next;
    public int nextfo;
    private float Hp;
    private bool isDie = false;
    private BoxCollider2D BoxCollider2D;
    Animator animator;
    Rigidbody2D rigidbody;
    bool isAccionPatten = false;
    // Start is called before the first frame update
    void Awake()
    {
        Hp = MaxHp;
        Player = GameObject.Find("Player");
        HpbarObject = GameObject.Find("BossCanvas").transform.Find("FomerWarriorHpbar").gameObject;
        HpbarObject.SetActive(true);
        Hpbar = GameObject.Find("FormerWarrior_Hpbar");
        rigidbody = GetComponent<Rigidbody2D>();
           nrl = 1;
        nextfo = 0;
        isattack = false;
        animator = GetComponent<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        
        HpbarObject.SetActive(true);
        
    }
    void Start()
    {
        isAction = true;
        Invoke( "RandomBossAction",4);
    }
    void Update()
    {
        
        BossAction(nextfo);
        UpdateHpbar();
    }

    void BossAction(int next)
    {
        if (!isAction)
        {
            switch (next)
            {
                case 0:
                    return;
                case 1:
                    StartCoroutine(Attackm1());
                    return;
                case 2:
                    if (nrl == 1)
                    {
                        nrl++;
                        StartCoroutine(Attackm2());
                        break;
                    }
                    else
                    {
                        nrl--;
                        StartCoroutine(Attackm22());
                        break;
                    }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerAttack_1"))
        {
            Hp -= Player.GetComponent<PlayerState>().GeneralDamage();
        }
        if (collision.transform.CompareTag("PlayerAttack_2"))
        {
            Hp -= Player.GetComponent<PlayerState>().GeneralDamage();
        }
        if (collision.transform.CompareTag("Double_Slash"))
        {
            Hp -= Player.GetComponent<PlayerState>().ESkillDamage();
        }
    }
    void RandomBossAction()
    {
        while (true)
        {
            next = Random.Range(1, 3);
            if (next != nextfo)
            {
                break;
            }
        }
        nextfo = next;
        isAccionPatten = false;
        isAction = false;
    }

    public void str1on()
    {
        if (SkillEffect_1 == null)
            return;
        SkillEffect_1.SetActive(true);
    }
    public void str1off()
    {
        if (SkillEffect_1 == null)
            return;
        SkillEffect_1.SetActive(false);
    }
    public void Attackarea1on()
    {
        if (Attack1Area == null)
            return;
        Attack1Area.SetActive(true);
    }
    public void Attackarea2on()
    {
        if (Attack2Area == null)
            return;
        Attack2Area.SetActive(true);
    }
    void UpdateHpbar()
    {
        Hpbar.GetComponent<Image>().fillAmount = (Hp / MaxHp * 100 / 100);
        if (Hp <= 0)
        {
            isAction = true;
            StopAllCoroutines();
            Destroy(SkillEffect_1);
            Destroy(SkillEffect_2);
            Destroy(AttackRange_1);
            Destroy(AttackRange_2);
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack1", false);
            animator.SetBool("isDie", true);
            Invoke("Die", 3);
            HpbarObject.SetActive(false);
        }
    }
    void Die()
    {
        Player.GetComponent<SkillSystem>().AddCommandESkill();
        QuestSystem.GameLeval++;
        Destroy(gameObject);
    }
    IEnumerator Attackm1()
    {
        if(!isAccionPatten)
        {
            isAccionPatten = true;
            isAction = true;
            isattack = true;
            AttackRange_1.SetActive(true);
            yield return new WaitForSeconds(1f);
            AttackRange_1.SetActive(false);
            Debug.Log("asd");

            animator.SetBool("Attack1", true);
            yield return new WaitForSeconds(0.8f);
            animator.SetBool("Attack1", false);
            Attack2Area.SetActive(false);
            next = 0;
            isattack = false;

            Invoke("RandomBossAction", 4);
        }
        
    }

    IEnumerator Attackm2()
    {
        if(!isAccionPatten)
        {
            isAction = true;
            rigidbody.gravityScale = 0;
            BoxCollider2D.isTrigger = true;

            isattack = true;
            AttackRange_2.SetActive(true);
            animator.SetBool("Attack2", true);
            yield return new WaitForSeconds(1.5f);
            AttackRange_2.SetActive(false);
            Attackarea2on();
            while (true)
            {
                SkillEffect_2.SetActive(true);
                if (transform.position.x <= 18)
                {
                    break;
                }
                yield return new WaitForSeconds(0.01f);
                transform.position += Vector3.left * 0.6f;
            }
            SkillEffect_2.SetActive(false);
            animator.SetBool("Attack2", false);
            Attack2Area.SetActive(false);
            transform.localScale = new Vector3(12, 12, 12);
            isattack = false;

            BoxCollider2D.isTrigger = false;
            rigidbody.gravityScale = 1;
            next = 0;
            Invoke("RandomBossAction", 4);
        }
        
    }

    IEnumerator Attackm22()
    {
        if(!isAccionPatten)
        {
            isAction = true;
            rigidbody.gravityScale = 0;
            BoxCollider2D.isTrigger = true;

            isattack = true;
            AttackRange_2.SetActive(true);
            animator.SetBool("Attack2", true);
            yield return new WaitForSeconds(1.5f);
            AttackRange_2.SetActive(false);
            Attackarea2on();
            while (true)
            {
                SkillEffect_2.SetActive(true);
                if (transform.position.x >= 36)
                {
                    break;
                }
                yield return new WaitForSeconds(0.01f);
                transform.position += Vector3.right * 0.6f;
            }
            SkillEffect_2.SetActive(false);
            animator.SetBool("Attack2", false);
            Attack2Area.SetActive(false);
            transform.localScale = new Vector3(-12, 12, 12);
            BoxCollider2D.isTrigger = false;
            isattack = false;
            rigidbody.gravityScale = 1;
            next = 0;

            Invoke("RandomBossAction", 4);
        }
        
    }
}
