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
    private int GameLeval = 0;
    private QuestSlot[] questSlots;
    private bool isOnQuest = false;
    private bool OnQuest = false;
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
    public void AcquireQuest()
    {
        WarriorQuest warriorQuest = new WarriorQuest();
        shaderQuest shaderQuest = new shaderQuest();
        fireQuest fireQuest = new fireQuest();
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
            Player.GetComponent<PlayerBehaviour>().isMove = true;
    }
    public int GetGameLevel()
    {
        return GameLeval;
    }
}
