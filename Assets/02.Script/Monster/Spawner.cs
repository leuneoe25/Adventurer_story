using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Dictionary<string, GameObject> Monster = new Dictionary<string, GameObject>();
    [SerializeField] string MonsterName;
    [SerializeField] GameObject Skeleton;
    [SerializeField] GameObject FormerWarrior;
    
    private void Awake()
    {
        Monster.Add("Skeleton", Skeleton);
        Monster.Add("FormerWarrior", FormerWarrior);
    }
    void Start()
    {
        GameObject.Instantiate(Monster[MonsterName], transform.position, Quaternion.identity).transform.parent = gameObject.transform.parent;
    }
}
