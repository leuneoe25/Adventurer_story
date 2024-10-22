using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvilWizard : MonoBehaviour
{
    private float Hp;
    private GameObject Player;
    [SerializeField] private float MaxHp;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject Circle;
    [SerializeField] private GameObject meteo;
    [SerializeField] private GameObject Patten3;
    public int Damage;
    private GameObject Hpbar;
    private GameObject HpbarObject;
    private int next;
    private int nextfo = 0;
    private bool isAction = false;
    Animator animator;
    SpriteRenderer sprite;
    bool isred = false;
    void Start()
    {
        Hp = MaxHp;
        Player = GameObject.Find("Player");
        HpbarObject = GameObject.Find("BossCanvas").transform.Find("EvilWizardHpbar").gameObject;
        HpbarObject.SetActive(true);
        Hpbar = GameObject.Find("EvilWizard_Hpbar");
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("RandomBossAction", 3);
        //RandomBossAction();
    }

    // Update is called once per frame
    void Update()
    {
        BossAction(nextfo);
        UpdateHpbar();
    }
    void BossAction(int next)
    {
        if(!isAction)
        {
            //if(5>=Vector2.Distance(transform.position,Player.transform.position))
            //{
            //    StartCoroutine(EvilWizard_Patten3());
            //    return;
            //}
            switch (next)
            {
                case 0:
                    return;
                case 1:
                    StartCoroutine(EvilWizard_Patten1());
                    break;
                case 2:
                    StartCoroutine(EvilWizard_Patten2());
                    break;
                case 3:
                    StartCoroutine(EvilWizard_Patten4());
                    
                    break;
                case 4:
                    if (5 >= Vector2.Distance(transform.position, Player.transform.position))
                    {
                        StartCoroutine(EvilWizard_Patten3());
                    }
                    else
                    {
                        RandomBossAction();
                    }
                    break;
            }
        }
    }
    IEnumerator EvilWizard_Patten1()
    {
        isAction = true;
        animator.SetBool("Patten1",true);
        BossSound.instance.EvilDownMeteo();
        for (int i = 0; i < 44; i++)
        {
            GameObject obj;
            obj = (GameObject)Instantiate(meteo, new Vector3(Random.Range(-25.46f, 7.57f), 9, 0), Quaternion.identity);
            obj.transform.Rotate(new Vector3(0f, 0f,0));
            yield return new WaitForSeconds(0.2f);
        }
        BossSound.instance.EvilDownMeteoStop();
        animator.SetBool("Patten1", false);
        Invoke("RandomBossAction", 4);
    }
    IEnumerator EvilWizard_Patten2()
    {
        isAction = true;
        animator.SetBool("wait", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("wait", false);

        animator.SetBool("Patten1", true);
        float oneShoting = 10;

        float speed = 10;
        for (int j = 0; j < 5; j++)
        {
            BossSound.instance.EvilWizardfirepoooooStart();
            float angle = 360 / oneShoting;
            for (int i = 0; i < oneShoting; i++)
            {
                Debug.Log("patten44");
                GameObject obj;
                obj = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * Mathf.Cos(Mathf.PI * 2 * i / oneShoting), speed * Mathf.Sin(Mathf.PI * i * 2 / oneShoting)));
                obj.transform.Rotate(new Vector3(0f, 0f, 360 * i / oneShoting - 90));
            }
            oneShoting++;
            yield return new WaitForSeconds(0.2f);
        }


        yield return new WaitForSeconds(1f);
        animator.SetBool("Patten1", false);
        Invoke("RandomBossAction", 4);
    }
    IEnumerator EvilWizard_Patten4()
    {
        isAction = true;
        animator.SetBool("Patten1", true);
        GameObject player = GameObject.Find("Player");
        
        GameObject obj;
        for (int i = 0; i < 3; i++)
        {
            BossSound.instance.EvilWizardUpMeteoStart();
            obj = (GameObject)Instantiate(meteo, new Vector2(Player.transform.position.x, gameObject.transform.position.y-10), Quaternion.identity);
            obj.transform.Rotate(new Vector3(0, 0, -180));
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            yield return new WaitForSeconds(1f);
        }



        yield return new WaitForSeconds(1f);
        animator.SetBool("Patten1", false);
        Invoke("RandomBossAction", 4);
    }
    IEnumerator EvilWizard_Patten3()
    {
        isAction = true;
        BossSound.instance.EvilFireStart();
        animator.SetBool("Patten3", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Patten3", false);
        BossSound.instance.EvilFireStop();
        Invoke("RandomBossAction", 4);
    }
    void patten3On()
    {
        Patten3.SetActive(true);
    }
    void patten3Off()
    {
        Patten3.SetActive(false);
    }
    void RandomBossAction()
    {
        while (true)
        {
            next = Random.Range(1, 5);
            if (next != nextfo)
            {
                break;
            }
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
            StartCoroutine(Attacked());
        }
        if (collision.transform.CompareTag("PlayerAttack_2"))
        {
            SoundManager.instance.hit();
            Hp -= Player.GetComponent<PlayerState>().GeneralDamage();
            StartCoroutine(Attacked());
        }
        if (collision.transform.CompareTag("Double_Slash"))
        {
            Hp -= Player.GetComponent<PlayerState>().ESkillDamage();
            StartCoroutine(Attacked());
        }
        if (collision.transform.CompareTag("QSkill"))
        {
            Hp -= Player.GetComponent<PlayerState>().XSkillDamage();
            StartCoroutine(Attacked());
        }
    }
    IEnumerator Attacked()
    {
        if (!isred)
        {
            isred = true;
            sprite.color = Color.red;
            Time.timeScale = 0.7f;
            yield return new WaitForSeconds(0.1f);
            Time.timeScale = 1;
            sprite.color = Color.white;
            isred = false;
        }
    }
    void UpdateHpbar()
    {
        Hpbar.GetComponent<Image>().fillAmount = (Hp / MaxHp * 100 / 100);
        if (Hp <= 0)
        {
            Time.timeScale = 1;
            sprite.color = Color.white;
            isAction = true;
            StopAllCoroutines();
            Destroy(Patten3);
            animator.SetBool("Patten1", false);
            animator.SetBool("Patten2", false);
            animator.SetBool("isDie", true);
            animator.SetBool("Patten1", false);
            animator.SetBool("Patten2", false);
            Invoke("Die", 3);
            HpbarObject.SetActive(false);
        }
    }
    void Die()
    {
        //Player.GetComponent<SkillSystem>().AddCommandQSkill();
        //QuestSystem.GameLeval++;
        animator.SetBool("Patten1", false);
        animator.SetBool("Patten2", false);
        Destroy(gameObject);
    }
}
