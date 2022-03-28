using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapSystem : MonoBehaviour
{
    [SerializeField] private GameObject Light;
    [SerializeField] private GameObject Guild;
    [SerializeField] private CinemachineConfiner GuildCm;
    [SerializeField] private GameObject Map1;
    [SerializeField] private PolygonCollider2D stage1Coll;
    [SerializeField] private GameObject Map2;
    [SerializeField] private GameObject Map3;
    [SerializeField] private PolygonCollider2D stage2Coll;
    [SerializeField] private PolygonCollider2D stage3Coll;
    [SerializeField] private GameObject Map1pos;
    [SerializeField] private GameObject Map2pos;
    [SerializeField] private GameObject Map3pos;
    private GameObject Player;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    public void GoMap1Stage1()
    {
        GuildCm.m_BoundingShape2D = stage1Coll;
        Guild.SetActive(false);
        Map1.SetActive(true);
        Player.transform.position = Map1pos.transform.position;
        Light.SetActive(true);
    }
    public void GoMap2Stage1()
    {
        GuildCm.m_BoundingShape2D = stage2Coll;
        Guild.SetActive(false);
        Map2.SetActive(true);
        Player.transform.position = Map2pos.transform.position;
        Light.SetActive(true);
    }
    public void GoMap3Stage1()
    {
        GuildCm.m_BoundingShape2D = stage3Coll;
        Guild.SetActive(false);
        Map3.SetActive(true);
        Player.transform.position = Map3pos.transform.position;
        Light.SetActive(true);
    }
}
