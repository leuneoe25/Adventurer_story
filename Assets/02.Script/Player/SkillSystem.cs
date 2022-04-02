using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//리시버
public class Skill
{
    protected int damage;
    protected float coolTime;
    protected float StartTIme;
    protected float time_current = 0;
}
public class DoubleSlash : Skill
{
    private float coolTime = 5f;
    private bool isCoolTime = false;
    public IEnumerator StartSkill(GameObject one, GameObject two,GameObject Palyer)
    {
        return skill(one,two, Palyer);
    }
    IEnumerator skill(GameObject one, GameObject two, GameObject Palyer)
    {
        if(!isCoolTime)
        {
            Palyer.GetComponent<PlayerAttackSystem>().doubleslash(true);
            isCoolTime = true;
            SkillSystem.OnSkill = true;
            yield return new WaitForSeconds(0.2f);
            one.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            one.SetActive(false);
            two.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            two.SetActive(false);
            Palyer.GetComponent<PlayerAttackSystem>().doubleslash(false);
            SkillSystem.OnSkill = false;
            StartTIme = Time.time;
            time_current = 0;
            yield return new WaitForSeconds(coolTime);
            isCoolTime = false;
        }
        if(isCoolTime)
        {
            Debug.Log("DoubleSlash Skill is Cool Time"); 
            yield break;
        }
    }
    public float GetSkillTime()
    {
        if(isCoolTime)
        {
            time_current = Time.time - StartTIme;
            if(time_current< coolTime)
            {
                return (time_current/ coolTime*100/100);
            }
            else
            {
                time_current = coolTime;
                return 1;
            }
        }
        return 1;
    }
    public float GetTimeText()
    {
        if (isCoolTime)
        {
            time_current = Time.time - StartTIme;
            if (time_current < coolTime)
            {
                return coolTime - time_current;
            }
            else
            {
                time_current = coolTime;
                return -1;
            }
        }
        return -1;
    }
}
public class Slaughter : Skill
{
    private float coolTime = 6f;
    
    private bool isCoolTime = false;
    private System.Diagnostics.Stopwatch SkillTime = new System.Diagnostics.Stopwatch();
    public IEnumerator StartSkill(GameObject SwordEffect_Combo_1, GameObject SwordEffect_Combo_2)
    {
        return skill(SwordEffect_Combo_1, SwordEffect_Combo_2);
    }
    public float GetSkillTime()
    {
        if (isCoolTime)
        {
            time_current = Time.time - StartTIme;
            if (time_current < coolTime)
            {
                return time_current / coolTime * 100 / 100;
            }
            else
            {
                time_current = coolTime;
                return 1;
            }
        }
        return 1;

    }
    public float GetTimeText()
    {
        if (isCoolTime)
        {
            time_current = Time.time - StartTIme;
            if (time_current < coolTime)
            {
                return coolTime-time_current;
            }
            else
            {
                time_current = coolTime;
                return -1;
            }
        }
        return -1;
    }
    IEnumerator skill(GameObject one, GameObject two)
    {
        if(!isCoolTime)
        {
            isCoolTime = true;
            one.transform.localScale = new Vector3(2, 1, 2);
            two.transform.localScale = new Vector3(2, 2, 2);
            yield return new WaitForSeconds(3f);
            one.transform.localScale = new Vector3(1, 1, 1);
            two.transform.localScale = new Vector3(1, 1, 1);
            StartTIme = Time.time;
            time_current = 0;
            yield return new WaitForSeconds(coolTime);
            isCoolTime = false;
        }
        if(isCoolTime)
        {
            Debug.Log("Slaughter Skill is Cool Time");
            yield break;
        }
        
    }

}
public class SwordOfWill : Skill
{
    private float coolTime = 10f;
    private bool isCoolTime = false;
    private float DownSpeed = 0.9f;
    private System.Diagnostics.Stopwatch SkillTime = new System.Diagnostics.Stopwatch();
    public IEnumerator StartSkill(GameObject Sword, GameObject Player)
    {
        return skill(Sword, Player);
    }
    public float GetSkillTime()
    {
        if (isCoolTime)
        {
            time_current = Time.time - StartTIme;
            if (time_current < coolTime)
            {
                return time_current / coolTime * 100 / 100;
            }
            else
            {
                time_current = coolTime;
                return 1;
            }
        }
        return 1;

    }
    public float GetTimeText()
    {
        if (isCoolTime)
        {
            time_current = Time.time - StartTIme;
            if (time_current < coolTime)
            {
                return coolTime-time_current;
            }
            else
            {
                time_current = coolTime;
                return -1;
            }
        }
        return -1;
    }
    IEnumerator skill(GameObject one, GameObject Player)
    {
        if(!isCoolTime)
        {
            Player.GetComponent<PlayerAttackSystem>().Attack_2();
            isCoolTime = true;
            SkillSystem.OnSkill = true;
            one.SetActive(true);
            yield return new WaitForSeconds(1f);
            SkillSystem.OnSkill = false;
            one.SetActive(false);
            StartTIme = Time.time;
            time_current = 0;
            yield return new WaitForSeconds(coolTime);
            isCoolTime = false;
        }
        if(isCoolTime)
        {
            Debug.Log("SwordOfWill Skill is Cool Time");
            yield break;
        }
    }
}

public interface ISkillCommand
{
    IEnumerator Execute();
    float SkillTime();
    float SkillText();
}
public class DoubleSlashCommand : ISkillCommand
{
    private DoubleSlash skill;
    private GameObject firstAttack;
    private GameObject secondAttack;
    private GameObject Player;
    public IEnumerator Execute()
    {
        return skill.StartSkill(firstAttack, secondAttack, Player);
    }
    public float SkillTime()
    {
        return skill.GetSkillTime();
    }
    public float SkillText()
    {
        return skill.GetTimeText();
    }
    public DoubleSlashCommand(DoubleSlash val,GameObject one,GameObject two,GameObject player)
    {
        skill = val;
        firstAttack = one;
        secondAttack = two;
        Player = player;
    }
}
public class SlaughterCommand : ISkillCommand
{
    private Slaughter slaughter;
    private GameObject SwordEffect;
    private GameObject SwordEffect_2;
    public IEnumerator Execute()
    {
        return slaughter.StartSkill(SwordEffect, SwordEffect_2);
    }
    public float SkillTime()
    {
        return slaughter.GetSkillTime();
    }
    public float SkillText()
    {
        return slaughter.GetTimeText();
    }
    public SlaughterCommand(Slaughter val,GameObject Effect_1, GameObject Effect_2)
    {
        slaughter = val;
        SwordEffect = Effect_1;
        SwordEffect_2 = Effect_2;
    }
}
public class SwordOfWillCommand : ISkillCommand
{
    private SwordOfWill swordOfWill;
    private GameObject sword;
    private GameObject Player;
    public IEnumerator Execute()
    {
        return swordOfWill.StartSkill(sword, Player);
    }
    public float SkillTime()
    {
        return swordOfWill.GetSkillTime();
    }
    public float SkillText()
    {
        return swordOfWill.GetTimeText();
    }
    public SwordOfWillCommand(SwordOfWill val, GameObject Sword, GameObject Palyer)
    {
        swordOfWill = val;
        sword = Sword;
        Player = Palyer;
    }
}
public class CommandManager
{
    private Dictionary<string, ISkillCommand> commandDic = new Dictionary<string, ISkillCommand>();
    public void SetCommand(string name, ISkillCommand command)
    {
        if(commandDic.ContainsValue(command))
        {
            Debug.Log("이미 커맨드가 리스트에 포함되어 있음");
            return;
        }
        commandDic.Add(name, command);
    }
    public bool FindCommand(string name)
    {
        if (commandDic.ContainsKey(name))
        {
            
            return true;
        }
        return false;
    }
    public IEnumerator InvokeExecute(string name)
    {
        return commandDic[name].Execute();
    }
    public float InvokeGetSkillTime(string name)
    {
        return commandDic[name].SkillTime();
    }
    public float InvokeGetSkillText(string name)
    {
        return commandDic[name].SkillText();
    }
}
public class SkillSystem : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    CommandManager commandMgr = null;
    [SerializeField] private GameObject slash;
    [SerializeField] private GameObject slash_2;

    [SerializeField] private GameObject Sword_Effect_Combo_1;
    [SerializeField] private GameObject Sword_Effect_Combo_2;

    [SerializeField] private GameObject Sword;
    public static bool OnSkill;
    bool first = true;

    [SerializeField] private Image ESkillImage;
    [SerializeField] private Image QSkillImage;
    [SerializeField] private Image XSkillImage;

    [SerializeField] private Image ESkillBackGroundImage;
    [SerializeField] private Image XSkillBackGroundImage;

    [SerializeField] private Text ESkillText;
    [SerializeField] private Text QSkillText;
    [SerializeField] private Text XSkillText;

    [SerializeField] private GameObject E;
    [SerializeField] private GameObject Q;
    public bool SkillIsPossible = true;
    public bool isG = false;
    // Start is called before the first frame update
    void Start()
    {
        //인보커 생성
        commandMgr = new CommandManager();

        //리시버 생성
        
        Slaughter slaughter = new Slaughter();
        //커맨드를 생성하고 리시버와 연결
        
        SlaughterCommand slaughterCommand = new SlaughterCommand(slaughter, Sword_Effect_Combo_1, Sword_Effect_Combo_2);

        //commandMgr.SetCommand("QKey", slaughterCommand);
        //AddCommandESkill();
        
        OnSkill = false;
        
        if (!PlayerPrefs.HasKey("ESkill"))
        {
            PlayerPrefs.SetInt("ESkill", 0);
        }
        else
        {
            if(PlayerPrefs.GetInt("ESkill")==1)
            {
                AddCommandESkill();
            }
        }
        if (!PlayerPrefs.HasKey("QSkill"))
        {
            PlayerPrefs.SetInt("QSkill", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("QSkill") == 1)
            {
                AddCommandQSkill();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(OnSkill)
        {
            SetisMove(false);
        }
        else if(!OnSkill&&first)
        {
            SetisMove(true);    
        }
        SetImage();
        SetText();
        if (Input.GetKeyDown(KeyCode.E)&&!OnSkill&& SkillIsPossible&& commandMgr.FindCommand("EKey")&&!isG)
        {
            
            StartCoroutine(commandMgr.InvokeExecute("EKey"));
            
            first = true;
        }
        //if(Input.GetKeyDown(KeyCode.Q) && !OnSkill)
        //{
        //    StartCoroutine(commandMgr.InvokeExecute("QKey"));
        //}
        if(Input.GetKeyDown(KeyCode.Q) && !OnSkill&& gameObject.GetComponent<PlayerBehaviour>().isJumpable&& SkillIsPossible&& commandMgr.FindCommand("QKey") && !isG)
        {
            
            StartCoroutine(commandMgr.InvokeExecute("QKey"));
            
            first = true;
        }
    }
    public void AddCommandESkill()
    {
        DoubleSlash doubleSlash = new DoubleSlash();
        DoubleSlashCommand skillCommand = new DoubleSlashCommand(doubleSlash, slash, slash_2, Player);
        commandMgr.SetCommand("EKey", skillCommand);
        if (PlayerPrefs.GetInt("ESkill") == 0)
        {
            StartCoroutine(OnObject(E));
        }
        PlayerPrefs.SetInt("ESkill", 1);
    }
    public void AddCommandQSkill()
    {
        SwordOfWill swordOfWill = new SwordOfWill();
        SwordOfWillCommand swordOfWillCommand = new SwordOfWillCommand(swordOfWill, Sword, Player);
        commandMgr.SetCommand("QKey", swordOfWillCommand);
        if (PlayerPrefs.GetInt("ESkill") == 0)
        {
            StartCoroutine(OnObject(Q));
        }
        PlayerPrefs.SetInt("QSkill", 1);
    }
    private void SetisMove(bool val)
    {
        if(val==false)
        {
            gameObject.GetComponent<PlayerBehaviour>().isMove = false;
            gameObject.GetComponent<PlayerBehaviour>().isAttack = true;
        }
        else
        {
            gameObject.GetComponent<PlayerBehaviour>().isMove = true;
            gameObject.GetComponent<PlayerBehaviour>().isAttack = false;
            //Debug.Log("move is true");
            first = false;
        }
    }
    private void SetText()
    {
        if(commandMgr.FindCommand("EKey"))
        {
            ESkillText.gameObject.SetActive(true);
            if (commandMgr.InvokeGetSkillText("EKey") == -1)
            {

                ESkillText.text = "";
            }
            else
            {
                ESkillText.text = $"{commandMgr.InvokeGetSkillText("EKey"):N1}";
            }
        }
        else
        {
            ESkillText.gameObject.SetActive(false);
        }
        //if(commandMgr.InvokeGetSkillText("QKey")==-1)
        //{
        //    QSkillText.text = "";
        //}
        //else
        //{
        //    QSkillText.text = $"{commandMgr.InvokeGetSkillText("QKey"):N1}";
        //}
        if(commandMgr.FindCommand("QKey"))
        {
            XSkillText.gameObject.SetActive(true);
            if (commandMgr.InvokeGetSkillText("QKey") == -1)
            {
                XSkillText.text = "";
            }
            else
            {
                XSkillText.text = $"{commandMgr.InvokeGetSkillText("QKey"):N1}";
            }
        }
        else
        {
            XSkillText.gameObject.SetActive(false);
        }
        
    }
    private void SetImage()
    {
        if(commandMgr.FindCommand("EKey"))
        {
            ESkillBackGroundImage.gameObject.SetActive(true);
            ESkillImage.gameObject.SetActive(true);
            ESkillImage.fillAmount = commandMgr.InvokeGetSkillTime("EKey");
        }
        else
        {
            ESkillBackGroundImage.gameObject.SetActive(false);
            ESkillImage.gameObject.SetActive(false);
        }
        //QSkillImage.fillAmount = commandMgr.InvokeGetSkillTime("QKey");
        if(commandMgr.FindCommand("QKey"))
        {
            XSkillBackGroundImage.gameObject.SetActive(true);
            XSkillImage.gameObject.SetActive(true);
            XSkillImage.fillAmount = commandMgr.InvokeGetSkillTime("QKey");
        }
        else
        {
            XSkillBackGroundImage.gameObject.SetActive(false);
            XSkillImage.gameObject.SetActive(false);
        }
        
    }

    void dashSound()
    {
        SoundManager.instance.Dash();
    }

    void EskSound()
    {
        SoundManager.instance.esk();
    }

    

    IEnumerator OnObject(GameObject val)
    {
        val.SetActive(true);
        yield return new WaitForSeconds(1f);
        val.SetActive(false);
    }
}
