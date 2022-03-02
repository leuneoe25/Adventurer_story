using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapSystem : MonoBehaviour
{
    [SerializeField] private GameObject Guild;
    [SerializeField] private CinemachineConfiner GuildCm;
    [SerializeField] private GameObject Map1;
    [SerializeField] private PolygonCollider2D stageColl;
    [SerializeField] private GameObject Map1pos;
    private GameObject Player;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    public void GoMap1Stage1()
    {
        GuildCm.m_BoundingShape2D = stageColl;
        Guild.SetActive(false);
        Map1.SetActive(true);
        Player.transform.position = Map1pos.transform.position;
        
    }
    
}
