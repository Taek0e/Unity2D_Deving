using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float AttackForce = 0;

    void Start() => StartCoroutine("AttackTime");

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") col.GetComponent<PlayerScript>().Hit(AttackForce);
    }

    
    IEnumerator AttackTime()
    {
        while (true)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            yield return new WaitForSeconds(0.02f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
