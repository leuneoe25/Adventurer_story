﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Training : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject TrainingObject;
    [SerializeField] private Text LevelText;
    [SerializeField] private Text CoinText;
    [SerializeField] private Text NeedCoinText;
    [SerializeField] private Button LevelUpButten;
    [SerializeField] private Button ExitButten;
    private bool Inplayer = false;

    [SerializeField] private GameObject LevelUpObject;
    [SerializeField] private GameObject ExitObject;
    void Start()
    {
        //Player.GetComponent<InventorySystem>().coin.SetCoin(350);

        LevelUpButten.onClick.RemoveAllListeners();
        LevelUpButten.onClick.AddListener(LevelUp);
        ExitButten.onClick.RemoveAllListeners();
        ExitButten.onClick.AddListener(Exit);
        NeedCoinText.text = "레벨 업 "
            + Player.GetComponent<InventorySystem>().coin.NeedLevelUpCoin(
                Player.GetComponent<PlayerState>().GetLevelVal())
            + "G";
        LevelText.text = "Lv. " + Player.GetComponent<PlayerState>().GetLevelVal();
        CoinText.text = Player.GetComponent<InventorySystem>().coin.GetCoin() + "G";
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Inplayer)
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(TrainingObject.activeSelf)
                {
                    Exit();
                    
                }
                else
                {
                    CoinText.text = Player.GetComponent<InventorySystem>().coin.GetCoin() + "G";
                    Player.GetComponent<PlayerAttackSystem>().AttackIsPossible = false;
                    Player.GetComponent<PlayerBehaviour>().isMove = false;
                    Player.GetComponent<SkillSystem>().SkillIsPossible = false;
                    OffExitObject();
                    OffLevelUpObject();
                    TrainingObject.SetActive(true);
                    InventorySystem.OnUI++;
                }
                
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Exit();
            }
        }
    }
    private void FixedUpdate()
    {
        LevelText.text = "Lv. " + Player.GetComponent<PlayerState>().GetLevelVal();
    }
    private void LevelUp()
    {
        SoundManager.instance.button();
        LevelText.text = "Lv. " + Player.GetComponent<PlayerState>().GetLevelVal();
        CoinText.text = Player.GetComponent<InventorySystem>().coin.GetCoin() + "G";
        if (Player.GetComponent<PlayerState>().GetLevelVal() >=40)
        {
            NeedCoinText.text = "MaxLeval";
            return;
        }
        if(Player.GetComponent<InventorySystem>().coin.GetCoin() 
            >= Player.GetComponent<InventorySystem>().coin.NeedLevelUpCoin(
                Player.GetComponent<PlayerState>().GetLevelVal()))
        {
            Player.GetComponent<InventorySystem>().coin.SetCoin(-
                Player.GetComponent<InventorySystem>().coin.NeedLevelUpCoin(
                Player.GetComponent<PlayerState>().GetLevelVal()));
            Player.GetComponent<PlayerState>().GetExp();
        }
        Debug.Log("Up");


        NeedCoinText.text = "레벨 업 "
            + Player.GetComponent<InventorySystem>().coin.NeedLevelUpCoin(
                Player.GetComponent<PlayerState>().GetLevelVal())
            + "G";
    }
    private void Exit()
    {
        SoundManager.instance.button();
        InventorySystem.OnUI = 0;
        TrainingObject.SetActive(false);
        Player.GetComponent<PlayerAttackSystem>().AttackIsPossible = true;
        Player.GetComponent<PlayerBehaviour>().isMove = true;
        Player.GetComponent<SkillSystem>().SkillIsPossible = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.transform.CompareTag("Player"))
        {
            Inplayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Inplayer = false;
        }
    }
    public void OnLevelUpObject()
    {
        LevelUpObject.SetActive(true);
    }
    public void OffLevelUpObject()
    {
        LevelUpObject.SetActive(false);
    }
    public void OnExitObject()
    {
        ExitObject.SetActive(true);
    }
    public void OffExitObject()
    {
        ExitObject.SetActive(false);
    }

}
