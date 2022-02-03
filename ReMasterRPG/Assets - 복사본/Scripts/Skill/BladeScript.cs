using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    public Rigidbody2D RB;   
    int dir;    


    void Start()
    {
        if (GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX) dir = -1;
        else dir = 1;

        StartCoroutine("DestroyBlade");
    }

    void FixedUpdate()
    {
        RB.velocity = Vector2.right * dir * 6f;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Monster")
        {
            col.GetComponent<MonsterScript>().skillHit();
        }
    }

    IEnumerator DestroyBlade()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
