using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringerofDeath : MonoBehaviour
{
    private float Hp;
    private GameObject Player;
    [SerializeField] private float MaxHp;
    [SerializeField] private GameObject Darkhand;
    [SerializeField] private GameObject Patten1;
    public int Damage;
    private GameObject Hpbar;
    private GameObject HpbarObject;
    private int next;
    private int nextfo = 0;
    private bool isAction = false;
    Animator animator;
    GameObject obj = null;
    void Start()
    {
        Hp = MaxHp;
        Player = GameObject.Find("Player");
        HpbarObject = GameObject.Find("BossCanvas").transform.Find("BringerofDeathHpbar").gameObject;
        HpbarObject.SetActive(true);
        Hpbar = GameObject.Find("BringerofDeath_Hpbar");
        animator = GetComponent<Animator>();
        Invoke("RandomBossAction", 1f);
    }

    // Update is called once per frame
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
                    StartCoroutine(BringerofDeath_Patten1());
                    break;
                case 2:
                    StartCoroutine(EvilWizard_Patten3());
                    break;
            }
        }
    }
    IEnumerator BringerofDeath_Patten1()
    {
        isAction = true;
        animator.SetBool("Patten1", true);
        yield return new WaitForSeconds(0.5f);
        if(Player.transform.localScale.x == 5)
        {
            gameObject.transform.localScale = new Vector2(-7, 7);
        }
        else
        {
            gameObject.transform.localScale = new Vector2(7, 7);
        }
        transform.position = new Vector2(Player.transform.position.x, gameObject.transform.position.y);
        animator.SetBool("Patten1", false);
        StartCoroutine(EvilWizard_Patten2());
    }
    IEnumerator EvilWizard_Patten2()
    {
        isAction = true;
        animator.SetBool("Patten2", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Patten2", false);
        Invoke("RandomBossAction", 2);
    }
    IEnumerator EvilWizard_Patten3()
    {
        isAction = true;
        animator.SetBool("Patten3", true);
        for (int i =0;i<3;i++)
        {
            if(i!=0)
            {
                Destroy(obj);
            }
            obj = (GameObject)Instantiate(Darkhand, new Vector2(Player.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        Destroy(obj);
        animator.SetBool("Patten3", false);
        Invoke("RandomBossAction", 4);
    }
    void RandomBossAction()
    {
        if((MaxHp/2) >=Hp)
        {
            while (true)
            {
                next = Random.Range(1, 3);
                if (next != nextfo)
                {
                    break;
                }
            }
        }
        else
        {
            nextfo = 1;
            isAction = false;
            return;
        }
        nextfo = next;
        isAction = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerAttack_1"))
        {
            SoundManager.instance.hit();
            Hp -= Player.GetComponent<PlayerState>().GeneralDamage();
        }
        if (collision.transform.CompareTag("PlayerAttack_2"))
        {
            SoundManager.instance.hit();
            Hp -= Player.GetComponent<PlayerState>().GeneralDamage();
        }
        if (collision.transform.CompareTag("Double_Slash"))
        {
            Hp -= Player.GetComponent<PlayerState>().ESkillDamage();
        }
    }
    public void OnPatten1()
    {
        Patten1.SetActive(true);
    }
    public void OffPatten1()
    {
        Patten1.SetActive(false);
    }
    void UpdateHpbar()
    {
        Hpbar.GetComponent<Image>().fillAmount = (Hp / MaxHp * 100 / 100);
        if (Hp <= 0)
        {
            
            isAction = true;
            StopAllCoroutines();
            Destroy(Patten1);
            Destroy(obj);
            animator.SetBool("Patten1", false);
            animator.SetBool("Patten2", false);
            animator.SetBool("Patten3", false);
            animator.SetBool("isDie", true);
            Invoke("Die", 1f);
            HpbarObject.SetActive(false);
        }
    }
    void Die()
    {
        Player.GetComponent<SkillSystem>().AddCommandQSkill();
        QuestSystem.GameLeval++;
        Destroy(gameObject);
    }
}
