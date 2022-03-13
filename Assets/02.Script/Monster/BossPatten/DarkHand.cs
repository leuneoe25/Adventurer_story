using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkHand : MonoBehaviour
{
    BoxCollider2D box;
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }
    public void OnAttackArea()
    {
        box.enabled = true;
    }

    public void OffAttackArea()
    {
        box.enabled = false;
    }
}
