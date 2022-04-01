using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private GameObject Inven;
    [SerializeField] private GameObject SlotParent;
    [SerializeField] private GameObject InvenCh;

    [SerializeField] private Sprite emergencyPotion_image;
    [SerializeField] private Sprite healingPotion_image;
    [SerializeField] private Sprite highhealingpotion_image;

    [SerializeField] private Text LevelText;
    [SerializeField] private Text HpText;
    [SerializeField] private Text PowerText;
    [SerializeField] private Text ItemExp;
    [SerializeField] private Text ItemNum;
    [SerializeField] private Image ItemImage;

    public ItemDictionary itemDictionary = null;
    public  Coin coin;
    private bool isOnInventory = false;
    private Slot[] slots;
    public static int temporaryDamaage = 0;
    public static int temporaryHealamount = 0;
    public static int OnUI = 0;

    
    private void Awake()
    {

        itemDictionary = new ItemDictionary();
        coin = new Coin();
        if (PlayerPrefs.HasKey("Coin"))
        {
            coin.SetCoin(PlayerPrefs.GetInt("Coin"));
        }
        else
        {
            PlayerPrefs.SetInt("Coin", 0);
        }
        EmergencyPotion emergencyPotion = new EmergencyPotion("emergencyPotion", 10, emergencyPotion_image);
        HealingPotion healingPotion = new HealingPotion("healingPotion", 30, healingPotion_image);
        Highhealingpotion highhealingpotion = new Highhealingpotion("highhealingpotion", 60, highhealingpotion_image);

        itemDictionary.AddItem(emergencyPotion.name, emergencyPotion);
        itemDictionary.AddItem(healingPotion.name, healingPotion);
        itemDictionary.AddItem(highhealingpotion.name, highhealingpotion);
    }
    private void Start()
    {
        slots = SlotParent.GetComponentsInChildren<Slot>();
        Inven.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(coin.GetCoin());
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isOnInventory)
            {
                if(OnUI==0)
                {
                    gameObject.GetComponent<PlayerBehaviour>().isMove = false;
                    gameObject.GetComponent<SkillSystem>().isG = true;
                    Inven.SetActive(true);
                    ItemImage.gameObject.SetActive(false);
                    ItemExp.gameObject.SetActive(false);
                    ItemNum.gameObject.SetActive(false);
                }
                
            }
            else
            {
                gameObject.GetComponent<PlayerBehaviour>().isMove = true;
                gameObject.GetComponent<SkillSystem>().isG = false;
                Inven.SetActive(false);
            }
            isOnInventory = !isOnInventory;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Inven.activeSelf)
            {
                if(InvenCh.activeSelf)
                {
                    InvenCh.SetActive(false);
                }
                gameObject.GetComponent<PlayerBehaviour>().isMove = true;
                gameObject.GetComponent<SkillSystem>().isG = false;
                Inven.SetActive(false);
            }
        }

        UpdateText();

    }

    void UpdateText()
    {
        LevelText.text = gameObject.GetComponent<PlayerState>().GetLevelVal().ToString();
        HpText.text = gameObject.GetComponent<PlayerState>().GetMaxHpVal().ToString();
        PowerText.text = gameObject.GetComponent<PlayerState>().GetPowerVal().ToString();
    }
    public void AcquireItem(string _item,int _count = 1)
    {
        if(Item.type.Weapon!= itemDictionary.GetItem(_item).Gettype())
        {
            for(int i=0;i<slots.Length;i++)
            {
                if(slots[i].item!=null)
                {
                    if(slots[i].item.name== itemDictionary.GetItem(_item).name)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        for(int i=0;i<slots.Length;i++)
        {
            if(slots[i].item == null)
            {
                slots[i].Additem(itemDictionary.GetItem(_item), _count);
                return;
            }
        }
    }
    public bool Search(string _item, int _count = 1)
    {
        if (Item.type.Weapon != itemDictionary.GetItem(_item).Gettype())
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.name == itemDictionary.GetItem(_item).name)
                    {
                        if(slots[i].itemCount- _count >=0 )
                            return true;
                    }
                }
            }
        }
        return false;
    }
    
}
