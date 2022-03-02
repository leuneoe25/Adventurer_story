using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    public Quest quest;
    public Text questText;
    public Button questImage;
    public GameObject Lock;
    [SerializeField] private GameObject MapSystem;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject QuestObject;

    [SerializeField] private GameObject questObject;
    [SerializeField] private Text questObjectTitle;
    [SerializeField] private Text questObjectExp;
    [SerializeField] private Button consent;
    [SerializeField] private Image SkillImage;
    [SerializeField] private Text SkillExpText;
    private bool Proceeding = false;
    public void AddQuest(Quest _quest)
    {
        quest = _quest;
        questText.text = _quest.Gettitle();
        
        questImage.onClick.RemoveAllListeners();
        questImage.onClick.AddListener(Execute);
        questImage.gameObject.SetActive(true);
    }
    private void Update()
    {
        if(questObject.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Esc();
            }
        }
    }
    void Execute()
    {
        
        questObject.SetActive(true);
        if(quest.Gettype()==Quest.Type.first)
        {
            if (QuestObject.GetComponent<QuestSystem>().GetGameLevel() >= 1)
            {
                questObjectExp.text = "이미 완성한 퀘스트입니다";
                consent.gameObject.SetActive(false);
                return;
            }
        }
        if (quest.Gettype() == Quest.Type.Second)
        {
            if (QuestObject.GetComponent<QuestSystem>().GetGameLevel() >= 2)
            {
                questObjectExp.text = "이미 완성한 퀘스트입니다";
                consent.gameObject.SetActive(false);
                return;
            }
        }
        Debug.Log(quest.Gettitle());
        consent.gameObject.SetActive(true);
        questObjectTitle.text = quest.Gettitle(); 
        questObjectExp.text = quest.GetExplanation();
        consent.onClick.RemoveAllListeners();
        consent.onClick.AddListener(Questconsent);
    }
    public void Questconsent()
    {
        Proceeding = true;
        questObject.SetActive(false);
        QuestObject.GetComponent<QuestSystem>().SetCanvers(false);

        MapSystem.GetComponent<MapSystem>().GoMap1Stage1();
        
    }
    public void Esc()
    {
        questObject.SetActive(false);
    }

}
