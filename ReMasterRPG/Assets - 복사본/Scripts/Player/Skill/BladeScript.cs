using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{ 
    int dir;    


    void Start()
    {
        if (GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX) dir = -1;
        else dir = 1;

        StartCoroutine("DestroyBlade");
    }

    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * dir * 6f * Time.deltaTime;
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
