using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private GameObject Inven;
    [SerializeField] private GameObject SlotParent;

    [SerializeField] private Sprite emergencyPotion_image;
    [SerializeField] private Sprite healingPotion_image;
    [SerializeField] private Sprite highhealingpotion_image;

    [SerializeField] private Text LevelText;
    [SerializeField] private Text HpText;
    [SerializeField] private Text PowerText;
    
    public ItemDictionary itemDictionary = null;
    public Coin coin;
    private bool isOnInventory = false;
    private Slot[] slots;
    public static int temporaryDamaage = 0;
    public static int temporaryHealamount = 0;

    
    private void Awake()
    {
        itemDictionary = new ItemDictionary();
        coin = new Coin();

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
        UpdateText();
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOnInventory = !isOnInventory;
            if (!isOnInventory)
            {
                gameObject.GetComponent<PlayerBehaviour>().isMove = false;
                Inven.SetActive(true);
            }
            else
            {
                gameObject.GetComponent<PlayerBehaviour>().isMove = true;
                Inven.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
           
            AcquireItem("boen", 10);
            Debug.Log("in item");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {

            AcquireItem("hpAddition", 1);
            Debug.Log("in item");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {

            AcquireItem("damageAddition", 1);
            Debug.Log("in item");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {

            AcquireItem("emergencyPotion", 1);
            Debug.Log("in item");
        }
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    W();
        //}

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
