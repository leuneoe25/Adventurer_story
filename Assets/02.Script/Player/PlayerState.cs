﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private Image AttackedImage;
    [SerializeField] private Image Hpbar;
    [SerializeField] private Text HpText;

    [SerializeField] private Image Expbar;
    [SerializeField] private Text ExpText;
    [SerializeField] private Text LevelText;

    [SerializeField] private Text DamageText;

    [SerializeField] private GameObject LevelUp;

    private static readonly Single START_MAX_HEALTH_POINT = 100;
    private static readonly Single START_EXPERIENCE = 20;
    private static readonly Single START_ATTACK = 10;
    private static readonly Single START_DAMAGE = 10;
    
    
    private static readonly Single General_DAMAGE_Coefficient = 0.8f;
    private static readonly Single ESkill_DAMAGE_Coefficient = 1f;
    private static readonly Single QSkill_DAMAGE_Coefficient = 2f;
    private int HealthPoint;
    LevelSystem levelSystem;
    HealthPointSystem healthPointSystem;

    public int Damage;

    private int isLevel;
    private bool isLevelUp = false;
    private GameObject Camaera;
    bool isOn = false;
    SpriteRenderer sprite;

    bool isred = false;
    private void Awake()
    {
        levelSystem = new LevelSystem();
        healthPointSystem = new HealthPointSystem();
        Camaera = GameObject.Find("Main Camera");
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
        if (PlayerPrefs.HasKey("Level"))
        {
            for(int i=0;i<PlayerPrefs.GetInt("Level")-1;i++)
            {
                levelSystem.AddExp();
            }
        }
        isLevel = levelSystem.GetLevel();
        Damage = (levelSystem.GetLevel()) * (int)START_DAMAGE;
        Heal(100);

    }

    void Update()
    {
        UpDataUI();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("SkeletonAttack"))
        {
            Attacked(collision.transform.parent.GetComponent<Skeleton>().Damage);
        }
        if (collision.transform.CompareTag("BanditAttack"))
        {
            Debug.Log("BanditAttack");
            Attacked(collision.transform.parent.GetComponent<Bandit_>().Damage);
        }
        if (collision.transform.CompareTag("FomerWarriorAttack"))
        {
            Attacked(collision.transform.parent.GetComponent<FormerWarriorBoss>().Damage);
        }
        if (collision.transform.CompareTag("EyeAttack"))
        {
            Attacked(collision.transform.parent.GetComponent<eye>().Damage);
        }
        if (collision.transform.CompareTag("MushroomAttack"))
        {
            Attacked(collision.transform.parent.GetComponent<Mushroom>().Damage);
        }
        if (collision.transform.CompareTag("GoblinAttack"))
        {
            Attacked(collision.transform.parent.GetComponent<Goblin>().Damage);
        }
        if (collision.CompareTag("BringerofDeathAttack"))
        {
            GameObject boss = GameObject.Find("BringerofDeath(Clone)");
            Attacked(boss.transform.GetComponent<BringerofDeath>().Damage);
        }
        if (collision.CompareTag("EvilWizardAttack"))
        {
            GameObject boss = GameObject.Find("EvilWizard");
            Attacked(boss.transform.GetComponent<EvilWizard>().Damage);
        }
    }
    
    void UpDataUI()
    {        
        if(!isLevelUp)
            UpDataExp();
        UpDataHp();
        UpDataDamage();
    }
    void UpDataExp()
    {
        LevelText.text = levelSystem.GetLevel().ToString();
        //if (levelSystem.GetExp() < levelSystem.GetLevel() * START_EXPERIENCE)
        //{
        //    Expbar.fillAmount = levelSystem.GetExp() / (levelSystem.GetLevel() * START_EXPERIENCE) * 100 / 100;
        //}
        //else
        //{
        //    Expbar.fillAmount = 1;
        //}
        //ExpText.text = "" + levelSystem.GetExp().ToString() + " / " + (levelSystem.GetLevel() * START_EXPERIENCE).ToString();
    }
    void UpDataHp()
    {
        if (healthPointSystem.GetHp() < (START_MAX_HEALTH_POINT + ((levelSystem.GetLevel()-1) * 20) + healthPointSystem.GetAddition()))
        {
            Hpbar.fillAmount = healthPointSystem.GetHp() / (START_MAX_HEALTH_POINT + ((levelSystem.GetLevel()-1) * 20+ healthPointSystem.GetAddition()) ) * 100 / 100;
        }
        else
        {
            Hpbar.fillAmount = 1;
        }
        HpText.text = "" + healthPointSystem.GetHp() + " / " + (START_MAX_HEALTH_POINT + ((levelSystem.GetLevel()-1) * 20)+ healthPointSystem.GetAddition()).ToString();
        if(healthPointSystem.GetHp() <=0)
        {
            GameSystem.instans().AddDie();
            SceneManager.LoadScene("Die");
            PlayerPrefs.SetInt("Level", levelSystem.GetLevel());
        }
    }
    void UpDataDamage()
    {
        Damage = (levelSystem.GetLevel()) * (int)START_DAMAGE;
        //DamageText.text = "Damage : " + Damage;
    }
    public void GetExp()
    {
        if (levelSystem.GetLevel() < 40)
            levelSystem.AddExp();
        else
            return;
        Heal(100);
    }
    public void Attacked(int damage)
    {
        if (!isOn)
        {

            StartCoroutine(AttackedEvent());
        }
        Camaera.GetComponent<CameraShake>().VibrateForTime(0.05f);
        healthPointSystem.Attacked(damage);
        StartCoroutine(Attactedimage());
    }
    IEnumerator Attactedimage()
    {
        if(!isred)
        {
            isred = true;
            sprite.color = Color.red;
            //Time.timeScale = 0.8f;
            yield return new WaitForSeconds(0.1f);
            //Time.timeScale = 1;
            sprite.color = Color.white;
            isred = false;
        }
        
    }
    IEnumerator AttackedEvent()
    {
        isOn = true;
        AttackedImage.gameObject.SetActive(true);
        Color color = AttackedImage.color;
        while (true)
        {
            
            color.a += 0.01f;
            AttackedImage.color = color;
            if (AttackedImage.color.a >= 0.2f)
            {
                break;
            }
            yield return new WaitForSeconds(0.00005f);
        }
        while (true)
        {
            color.a -= 0.01f;
            AttackedImage.color = color;
            if(AttackedImage.color.a <= 0)
            {
                break;
            }
            yield return new WaitForSeconds(0.00005f);
        }
        AttackedImage.gameObject.SetActive(false);
        isOn = false;
    }
    public void Heal(int healamount)
    {
        healthPointSystem.Portion(levelSystem.GetLevel(), healamount);
    }
    public void HpAdd(int add)
    {
        healthPointSystem.SetAddition(add);
    }
    public int GetLevelVal()
    {
        return levelSystem.GetLevel();
    }
    public int GetMaxHpVal()
    {
        return ((int)START_MAX_HEALTH_POINT + ((levelSystem.GetLevel() - 1) * 20) + healthPointSystem.GetAddition());
    }
    public int GetPowerVal()
    {
        return (Damage);
    }
    public int GeneralDamage()
    {
        return (int)(Damage * General_DAMAGE_Coefficient);
    }
    public int ESkillDamage()
    {
        return (int)(Damage * ESkill_DAMAGE_Coefficient);
    }
    public int XSkillDamage()
    {
        return (int)(Damage * QSkill_DAMAGE_Coefficient);
    }
    IEnumerator LevelUpEvent()
    {
        LevelUp.SetActive(true);
        yield return new WaitForSeconds(1f);
        LevelUp.SetActive(false);
    }
}
