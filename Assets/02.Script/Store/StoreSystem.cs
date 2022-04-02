﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSystem : MonoBehaviour
{
    private bool InStore = false;
    [SerializeField] private GameObject Store;
    [SerializeField] private GameObject CheckObject;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject One;
    [SerializeField] private GameObject Two;
    [SerializeField] private GameObject three;
    [SerializeField] private Button Product_1;
    [SerializeField] private Button Product_2;
    [SerializeField] private Button Product_3;

    [SerializeField] private Button Consent;
    [SerializeField] private Button Refusal;
    [SerializeField] private Text CheckText;
    [SerializeField] private Text CoinText;
    void Start()
    {
        Product_1.onClick.AddListener(delegate { buyCheck("emergencyPotion",2); } );
        Product_2.onClick.AddListener(delegate { buyCheck("healingPotion",5); } );
        Product_3.onClick.AddListener(delegate { buyCheck("highhealingpotion",8); } );
    }
    void Update()
    {
        if(InStore)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                if (Store.activeSelf&& !CheckObject.activeSelf)
                {
                    Store.SetActive(false);
                    Player.GetComponent<PlayerBehaviour>().isMove = true;
                    InventorySystem.OnUI=0;
                }
                    
                else
                {
                    CoinText.text = "Coin : " + Player.GetComponent<InventorySystem>().coin.GetCoin().ToString();
                    Store.SetActive(true);
                    OffOne();
                    OffThree();
                    OffTwo();
                    Player.GetComponent<PlayerBehaviour>().isMove = false;
                    InventorySystem.OnUI++;
                }
                    
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(Store.activeSelf)
                {
                    if(!CheckObject.activeSelf)
                    {
                        Store.SetActive(false);
                        Player.GetComponent<PlayerBehaviour>().isMove = true;
                    }
                        
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            InStore = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InStore = false;
        }
    }
    void buyCheck(string name, int price)
    {
        CheckObject.SetActive(true);
        Consent.onClick.AddListener(delegate { ButtenConsent(name, price); });
        Refusal.onClick.AddListener(ButtenRefusal);
        if(Player.GetComponent<InventorySystem>().coin.GetCoin()>=price)
        {
            Consent.gameObject.SetActive(true);
            CheckText.text = "아이템을 구매하시겠습니까?";
        }
        else
        {
            Consent.gameObject.SetActive(false);
            CheckText.text = "코인이 부족합니다.";
        }
        
    }
    void ButtenConsent(string name,int price)
    {
        CoinText.text = Player.GetComponent<InventorySystem>().coin.GetCoin().ToString();
        Player.GetComponent<InventorySystem>().AcquireItem(name);
        Player.GetComponent<InventorySystem>().coin.SetCoin(-price);
        CheckObject.SetActive(false);
        Consent.onClick.RemoveAllListeners();
    }
    void ButtenRefusal()
    {
        CheckObject.SetActive(false);
    }

    public void OnOne()
    {
        if(CheckObject.activeSelf)
        {
            return;
        }
        One.SetActive(true);
    }
    public void OnTwo()
    {
        if (CheckObject.activeSelf)
        {
            return;
        }
        Two.SetActive(true);
    }
    public void OnThree()
    {
        if (CheckObject.activeSelf)
        {
            return;
        }
        three.SetActive(true);
    }
    public void OffOne()
    {
        if (CheckObject.activeSelf)
        {
            return;
        }
        One.SetActive(false);
    }
    public void OffTwo()
    {
        if (CheckObject.activeSelf)
        {
            return;
        }
        Two.SetActive(false);
    }
    public void OffThree()
    {
        if (CheckObject.activeSelf)
        {
            return;
        }
        three.SetActive(false);
    }
}
