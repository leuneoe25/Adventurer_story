using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveMap : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject NextStage;
    [SerializeField] private GameObject NowStage;
    [SerializeField] private GameObject Cm;
    [SerializeField] private Camera camera;
    [SerializeField] private bool isBossStage;
    [SerializeField] private bool isOutBossStage;
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
                if(!isBossStage&&!isOutBossStage)
                {
                    NextStage.SetActive(true);
                    NowStage.SetActive(false);
                    gameObject.SetActive(false);
                    Player.transform.position = pos.transform.position;
                }
                else if(isOutBossStage&& !isBossStage)
                {
                    Cm.SetActive(true);
                    Player.transform.position = pos.transform.position;
                    camera.orthographicSize = 5;
                    NextStage.SetActive(true);
                    NowStage.SetActive(false);
                }
                else if(!isOutBossStage && isBossStage)
                {
                    Cm.SetActive(false);
                    Player.transform.position = pos.transform.position;
                    camera.transform.position = new Vector3(26.93f, 0.59f, -16.5f);
                    camera.orthographic = true;
                    camera.orthographicSize = 7;

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
