using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MoveMap : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject PlayerLihgt;
    [SerializeField] private GameObject NextStage;
    [SerializeField] private GameObject NowStage;
    [SerializeField] private CinemachineConfiner Cm;
    [SerializeField] private CinemachineVirtualCamera Cmcam;
    [SerializeField] private Camera camera;
    [SerializeField] private bool isBossStage;
    [SerializeField] private bool isOutBossStage;
    [SerializeField] private bool isEnd;
    [SerializeField] private PolygonCollider2D stageColl;
    [SerializeField] GameObject pos;
    private bool InMove;
    private void Start()
    {
        
    }
    private void Update()
    {
        if(InMove)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                if(isEnd)
                {
                    SceneManager.LoadScene("EndingScene");

                }
                if(!isBossStage&&!isOutBossStage)
                {
                    NextStage.SetActive(true);
                    NowStage.SetActive(false);
                    gameObject.SetActive(false);
                    Player.transform.position = pos.transform.position;
                }
                else if(isOutBossStage&& !isBossStage)
                {
                    //Cm.SetActive(true);
                    Player.GetComponent<PlayerState>().Heal(100);
                    Player.GetComponent<SkillSystem>().isG = true;
                    PlayerLihgt.SetActive(false);
                    Player.transform.position = pos.transform.position;
                    Cm.m_BoundingShape2D = stageColl;
                    Cmcam.m_Lens.OrthographicSize = 5;
                    NextStage.SetActive(true);
                    NowStage.SetActive(false);
                    
                }
                else if(!isOutBossStage && isBossStage)
                {
                    //Cm.SetActive(false);
                    Player.transform.position = pos.transform.position;
                    Cm.m_BoundingShape2D = stageColl;
                    Cmcam.m_Lens.OrthographicSize = 9;
                    NextStage.SetActive(true);
                    NowStage.SetActive(false);

                    gameObject.SetActive(false);
                    
                }
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            InMove = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            InMove = false;
        }
    }
}
