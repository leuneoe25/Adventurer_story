using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSystem : MonoBehaviour
{
    private bool InStore = false;
    [SerializeField] private GameObject Store;
    [SerializeField] private GameObject CheckObject;
    [SerializeField] private GameObject Player;
    [SerializeField] private Button Product_1;
    [SerializeField] private Button Product_2;
    [SerializeField] private Button Product_3;

    [SerializeField] private Button Consent;
    [SerializeField] private Button Refusal;
    [SerializeField] private Text CheckText;
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
                    Store.SetActive(true);
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
        Player.GetComponent<InventorySystem>().AcquireItem(name);
        Player.GetComponent<InventorySystem>().coin.SetCoin(-price);
        CheckObject.SetActive(false);
        Consent.onClick.RemoveAllListeners();
    }
    void ButtenRefusal()
    {
        CheckObject.SetActive(false);
    }
}
