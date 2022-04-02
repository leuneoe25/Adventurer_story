using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject CheckObject;
    [SerializeField] private GameObject Canvers;
    [SerializeField] private GameObject SlotParent;

    [SerializeField] private GameObject SecondLock;
    [SerializeField] private GameObject ThirdLock;
    public static int GameLeval = 1;
    private QuestSlot[] questSlots;
    private bool isOnQuest = false;
    private bool OnQuest = false;
    [SerializeField] private GameObject yesObject;
    [SerializeField] private GameObject NoObject;
    [SerializeField] private GameObject Quest2Object;
    [SerializeField] private GameObject Quest3Object;
    void Start()
    {
        questSlots = SlotParent.GetComponentsInChildren<QuestSlot>();
        Canvers.SetActive(false);
        AcquireQuest();
    }
    void Update()
    {
        if(OnQuest)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                if (!isOnQuest)
                {

                    Player.GetComponent<PlayerBehaviour>().isMove = false;
                    if (GameLeval == 1)
                    {
                        SecondLock.SetActive(false);
                    }
                    if (GameLeval == 2)
                    {
                        SecondLock.SetActive(false);
                        ThirdLock.SetActive(false);
                    }
                    OffNo();
                    Offyes();
                    //questSlots[0].gameObject.SetActive(false);
                    SetCanvers(true);
                    isOnQuest = !isOnQuest;
                }
                else if(!CheckObject.activeSelf)
                {
                    
                    SetCanvers(false);
                    isOnQuest = !isOnQuest;
                }
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(isOnQuest)
                {
                    if (!CheckObject.activeSelf)
                    {

                        SetCanvers(false);
                        isOnQuest = !isOnQuest;
                    }
                }
            }
            
        }
        
    }
    public void OnQuest2Object()
    {
        Quest2Object.SetActive(true);
    }
    public void OffQuest2Object()
    {
        Quest2Object.SetActive(false);
    }
    public void OnQuest3Object()
    {
        Quest3Object.SetActive(true);
    }
    public void OffQuest3Object()
    {
        Quest3Object.SetActive(false);
    }
    public void AcquireQuest()
    {
        WarriorQuest warriorQuest = new WarriorQuest();
        shaderQuest shaderQuest = new shaderQuest();
        fireQuest fireQuest = new fireQuest();
        Debug.Log("asd");
        questSlots[0].AddQuest(warriorQuest);
        questSlots[1].AddQuest(shaderQuest);
        questSlots[2].AddQuest(fireQuest);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            OnQuest = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            OnQuest = false;
        }
    }
    public void SetCanvers(bool val)
    {
        Canvers.SetActive(val);
        if(val == false)
        {
            InventorySystem.OnUI = 0;
            Player.GetComponent<PlayerBehaviour>().isMove = true;
        }
        else
        {
            InventorySystem.OnUI++;
            Player.GetComponent<PlayerBehaviour>().isMove = false;
        }
            
    }
    public int GetGameLevel()
    {
        return GameLeval;
    }
    public void Onyes()
    {
        yesObject.SetActive(true);
    }
    public void Offyes()
    {
        yesObject.SetActive(false);
    }
    public void OnNo()
    {
        NoObject.SetActive(true);
    }
    public void OffNo()
    {
        NoObject.SetActive(false);
    }
}
