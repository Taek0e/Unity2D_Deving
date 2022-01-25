using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerATK : MonoBehaviour
{
    public SpriteRenderer PlayerSR;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Monster") col.GetComponent<MonsterScript>().MonsterHit(PlayerSR.flipX);
    }
}
