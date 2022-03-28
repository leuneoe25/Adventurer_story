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
    [SerializeField] private GameObject consentImage;
    [SerializeField] private Button No;
    [SerializeField] private Image SkillImage;
    [SerializeField] private Sprite ESkillImage;
    [SerializeField] private Sprite QSkillImage;
    [SerializeField] private Text SkillExpText;
    private bool Proceeding = false;
    public void AddQuest(Quest _quest)
    {
        quest = _quest;
        //questText.text = _quest.Gettitle();
        
        questImage.onClick.RemoveAllListeners();
        questImage.onClick.AddListener(Execute);
        questImage.gameObject.SetActive(true);
        No.onClick.RemoveAllListeners();
        No.onClick.AddListener(Exit);
    }
    void Exit()
    {
        questObject.SetActive(false);
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
        consentImage.SetActive(false);
        questObject.SetActive(true);
        if(quest.Gettype()==Quest.Type.first)
        {
            if (QuestObject.GetComponent<QuestSystem>().GetGameLevel() >= 1)
            {
                consentImage.SetActive(true);
                questObjectTitle.text = "";
                questObjectExp.text = "이미 완성한 퀘스트입니다";
                consent.gameObject.SetActive(false);
                SkillImage.gameObject.SetActive(false);
                SkillExpText.text = "없음";

                return;
            }
        }
        if (quest.Gettype() == Quest.Type.Second)
        {
            if (QuestObject.GetComponent<QuestSystem>().GetGameLevel() >= 2)
            {
                consentImage.SetActive(true);
                questObjectTitle.text = "";
                questObjectExp.text = "이미 완성한 퀘스트입니다";
                consent.gameObject.SetActive(false);
                SkillImage.gameObject.SetActive(false);
                SkillExpText.text = "없음";
                return;
            }
        }
        Debug.Log(quest.Gettitle());
        consent.gameObject.SetActive(true);
        questObjectTitle.text = quest.Gettitle(); 
        questObjectExp.text = quest.GetExplanation();
        consent.onClick.RemoveAllListeners();
        consent.onClick.AddListener(Questconsent);
        if(quest.Gettype() == Quest.Type.first)
        {
            if (QuestObject.GetComponent<QuestSystem>().GetGameLevel() == 0)
            {
                SkillImage.gameObject.SetActive(true);
                SkillImage.sprite = ESkillImage;
                SkillExpText.text = "- E스킬 연속검귀\n연속으로 두개의 검귀를 날려 공격합니다\n재사용 대기시간 5초";
            }
            
        }
        else if (quest.Gettype() == Quest.Type.Second)
        {
            if (QuestObject.GetComponent<QuestSystem>().GetGameLevel() ==1)
            {
                SkillImage.gameObject.SetActive(true);
                SkillImage.sprite = QSkillImage;
                SkillExpText.text = "- Q스킬 연속검귀\n하나의 큰 강기를 내리찍어 공격합니다\n재사용 대기시간 10초";
            }
        }
        else
        {
            SkillImage.gameObject.SetActive(false);
            SkillExpText.text = "없음";
        }

    }
    public void Questconsent()
    {
        Proceeding = true;
        questObject.SetActive(false);
        QuestObject.GetComponent<QuestSystem>().SetCanvers(false);
        if (quest.Gettype() == Quest.Type.first)
            MapSystem.GetComponent<MapSystem>().GoMap1Stage1();
        else if (quest.Gettype() == Quest.Type.Second)
            MapSystem.GetComponent<MapSystem>().GoMap2Stage1();
        else if (quest.Gettype() == Quest.Type.third)
            MapSystem.GetComponent<MapSystem>().GoMap3Stage1();

    }
    public void Esc()
    {
        questObject.SetActive(false);
    }

}
