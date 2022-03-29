using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    [SerializeField] GameObject Attack_Combo_1;
    [SerializeField] GameObject Attack_Combo_2;
    private Boolean isAttack;
    private System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
    Animator animator;
    public bool AttackIsPossible = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Attack_Combo_1.transform.position = transform.position;
        Attack_Combo_2.transform.position = transform.position;
        if (watch.ElapsedMilliseconds > 1000)
        {
            watch.Stop();
            watch.Reset();
            isAttack = false;
        }
        if(Input.GetMouseButtonDown(0)&&!isAttack&& gameObject.GetComponent<PlayerBehaviour>().isMove&& AttackIsPossible&& gameObject.GetComponent<PlayerBehaviour>().isJumpable && !gameObject.GetComponent<PlayerBehaviour>().isDash)
        {
            StartCoroutine(AttackComboOne());
            watch.Start();
        }
        if(Input.GetMouseButtonDown(0) && isAttack&& gameObject.GetComponent<PlayerBehaviour>().isMove&& AttackIsPossible&& gameObject.GetComponent<PlayerBehaviour>().isJumpable&&!gameObject.GetComponent<PlayerBehaviour>().isDash)
        {
            StartCoroutine(AttackComboTwo());
            watch.Stop();
            watch.Reset();
        }
        if(gameObject.GetComponent<PlayerBehaviour>().isDash)
        {
            StopAllCoroutines();
            animator.SetBool("isAttack_1", false);
            animator.SetBool("isAttack_2", false);
            Attack_Combo_2.SetActive(false);
            Attack_Combo_1.SetActive(false);
        }
    }
    IEnumerator AttackComboOne()
    {
        gameObject.GetComponent<PlayerBehaviour>().Setdefaultspeed(3);
        gameObject.GetComponent<PlayerBehaviour>().isAttack = true;
        animator.SetBool("isAttack_1", true);
        
        yield return new WaitForSeconds(0.3f);
        Attack_Combo_1.SetActive(false);
        animator.SetBool("isAttack_1", false);
        isAttack = true;
        gameObject.GetComponent<PlayerBehaviour>().isAttack = false;
        gameObject.GetComponent<PlayerBehaviour>().Setdefaultspeed(10);
    }
    IEnumerator AttackComboTwo()
    {
        gameObject.GetComponent<PlayerBehaviour>().Setdefaultspeed(3);
        gameObject.GetComponent<PlayerBehaviour>().isAttack = true;
        animator.SetBool("isAttack_1", false);
        animator.SetBool("isAttack_2", true);
        
        yield return new WaitForSeconds(0.4f);
        Attack_Combo_2.SetActive(false);
        isAttack = false;
        animator.SetBool("isAttack_2", false);
        gameObject.GetComponent<PlayerBehaviour>().isAttack = false;
        gameObject.GetComponent<PlayerBehaviour>().Setdefaultspeed(10);
    }
    public void Attack_Combo_2_On()
    {
        Attack_Combo_2.SetActive(true);
    }
    public void Attack_Combo_1_On()
    {
        Attack_Combo_1.SetActive(true);
    }
    public void Attack_1()
    {
        StartCoroutine(AttackComboOne());
    }
    public void Attack_2()
    {
        StartCoroutine(AttackComboTwo());
    }
    public void doubleslash(bool val)
    {
        animator.SetBool("isDoubleSlash", val);
    }
}
