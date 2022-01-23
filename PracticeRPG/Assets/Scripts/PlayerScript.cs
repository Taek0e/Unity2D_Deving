using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D RB;
    public SpriteRenderer SR;

    public GameObject RightAtk;     
    public GameObject LeftAtk;     

    bool isGround;

    float curtime = 0;     // 공격 쿨타임 설정변수
    float cooltime = 1f;   //
    



    
    void Update()
    {
        playerMove();
        playerJump();

        playerAttack();
    }







    void playerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        RB.velocity = new Vector2(horizontal * 7f, RB.velocity.y);

        if (horizontal != 0)
        SR.flipX = horizontal == -1;

        Debug.Log(SR.flipX);
    }

    void playerJump()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.7f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGround)
        {
            RB.velocity = Vector3.zero;
            RB.AddForce(Vector3.up * 14f, ForceMode2D.Impulse);
        }
    }


    void playerAttack()
    {
        if (curtime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LorR_Atk();  // 왼쪽 공격 or 오른쪽 공격
                curtime = cooltime;
            }
        }
        else curtime -= Time.deltaTime;
    }

    void LorR_Atk()
    {
        if (SR.flipX) // true == Left 공격
        {
            LeftAtk.SetActive(true);
            StartCoroutine("AtkTime");
            
        }
        else   // false == Right 공격
        {
            RightAtk.SetActive(true);
            StartCoroutine("AtkTime");
        }
    }

    IEnumerator AtkTime()
    {
        yield return new WaitForSeconds(0.2f);
        LeftAtk.SetActive(false);
        RightAtk.SetActive(false);
    }
    


}
