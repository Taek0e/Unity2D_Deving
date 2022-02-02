using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollow : MonoBehaviour
{
    public Transform trans;
    public SpriteRenderer SR;

    void Awake() => StartCoroutine("CoMove");
    void FixedUpdate()
    {
        MonsterMove();
    }


    int Dir = 0;
    void MonsterMove()
    {
        Vector3 vector = new Vector3();
        if (Dir == 0) vector = Vector3.zero;
        if (Dir == 1) { vector = Vector3.right; SR.flipX = false; }
        if (Dir == 2) { vector = Vector3.left; SR.flipX = true; }

        trans.Translate(vector * 3f * Time.deltaTime);
    }


    bool move = true;
    IEnumerator CoMove()
    {
        while (move)
        {
            yield return new WaitForSeconds(1f);
            Dir = 1;
            yield return new WaitForSeconds(2f);
            Dir = 2;
            yield return new WaitForSeconds(2f);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            move = false;
            if (col.transform.position.x - trans.position.x < 0)
            { 
                if (col.transform.position.x + 1f >= trans.position.x) Dir = 0;
                else Dir = 2;
            }
            else
            {
                if (col.transform.position.x - 1f <= trans.position.x) Dir = 0;
                else Dir = 1;
            }
        }
    }



    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            move = true;
        }
    }



}
