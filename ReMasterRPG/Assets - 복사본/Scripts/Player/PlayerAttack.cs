using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public SpriteRenderer SR;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Monster")
        {
            col.GetComponent<MonsterScript>().Hit(SR.flipX);
        }
    }
}
