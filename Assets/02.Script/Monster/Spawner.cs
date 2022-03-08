using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Dictionary<string, GameObject> Monster = new Dictionary<string, GameObject>();
    [SerializeField] string MonsterName;
    [SerializeField] GameObject Skeleton;
    [SerializeField] GameObject FormerWarrior;
    [SerializeField] GameObject Bandit;
    
    private void Awake()
    {
        Monster.Add("1", Skeleton);
        Monster.Add("2", FormerWarrior);
        Monster.Add("3", Bandit);
    }
    void Start()
    {
        GameObject.Instantiate(Monster[MonsterName], transform.position, Quaternion.identity).transform.parent = gameObject.transform.parent;
    }
}
