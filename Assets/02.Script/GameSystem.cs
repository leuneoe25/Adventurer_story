using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class GameSystem : MonoBehaviour
{
    private static GameSystem ins;
    private bool isFirst;
    [SerializeField] GameObject Tutorial;
    [SerializeField] PolygonCollider2D coll;
    [SerializeField] GameObject Guild;
    [SerializeField] GameObject Guide;
    [SerializeField] private CinemachineConfiner Cm;
    private void Awake()
    {
        if (ins == null)
        {
            ins = gameObject.GetComponent<GameSystem>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public static GameSystem instans()
    {
        if (ins != null)
            return ins;
        else
            return null;
    }
    void Start()
    {
        isFirst = true;
        if (isFirst)
        {
            Tutorial.SetActive(true);
            Cm.m_BoundingShape2D = coll;
            Guild.SetActive(false);
            Guide.SetActive(true);
        }
    }
    public void Guidefalse()
    {
        Guide.SetActive(false);
    }    
    public void SetisFirst(bool val)
    {
        isFirst = val;
    }
}
