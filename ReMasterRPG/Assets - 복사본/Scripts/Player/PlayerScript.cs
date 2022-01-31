using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // 플레이어 스탯
    static public float StaminaNow = 100f;
    static public float StaminaMax = 100f;
    static public float PlusStamina = 0f;
    static public float AttackForce = 30f;



    public Rigidbody2D RB;
    public SpriteRenderer SR;
    public Image staminaUI;
    public Text staminaText;

    public GameObject LeftAttack;
    public GameObject RightAttack;

    
    bool isGround;




    void FixedUpdate()
    {
        PlayerMove();
        PlayerAttack();

        PlayerStaminaUI();
        PlayerStaminaRegen();
    }



    void PlayerStaminaUI() 
    {
        staminaText.text = $"{StaminaNow} / {StaminaMax}";
        staminaUI.fillAmount = (StaminaNow + PlusStamina) / (StaminaMax + PlusStamina);
    }

    public void Hit()
    {
        StaminaNow -= MonsterScript.AttackForce;

        if (StaminaNow <= 0) 
        {
            StaminaNow = 0;
            staminaText.text = $"0 / {StaminaMax}";
            staminaUI.fillAmount = 0f;
            Destroy(gameObject);
        }
        
    }


    #region 체력 재생
    float curtime = 0;
    float regenCool = 0.5f;
    void PlayerStaminaRegen()
    {
        if (curtime <= 0)
        {
            if (StaminaMax - StaminaNow > 0) StaminaNow += 3f;

            if (StaminaMax < StaminaNow) StaminaNow += StaminaMax - StaminaNow;

            curtime = regenCool;
        }
        else curtime -= Time.deltaTime;
    }
    #endregion


    #region 플레이어 이동 메서드
    public void MoveBtnUp() => Dir = 0;
    public void RightBtn() => Dir = 1;
    public void LeftBtn() => Dir = -1;

    int Dir = 0;
    
    void PlayerMove()
    {
        if (Dir != 0) SR.flipX = (Dir == -1);
        RB.velocity = (new Vector2(Dir * 7f, RB.velocity.y));
    }

    public void JumptBtn() 
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.7f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));

        if (isGround)
        {
            RB.velocity = Vector2.zero;
            RB.AddForce(Vector2.up * 14f, ForceMode2D.Impulse);
        }
    }
    
        
    
    #endregion


    #region 플레이어 공격 메서드
    public void AttackBtn() => attackCheck = true;

    
    bool attackCheck = false;
    float curTime = 0;         // 공격 쿨타임 적용
    float coolTime = 0.5f;     //
    void PlayerAttack()
    {
        if (curTime <= 0)
        {
            if (attackCheck)
            {
                if (SR.flipX) LeftAttack.SetActive(true);
                else RightAttack.SetActive(true);

                StartCoroutine("AttackTime");
                curTime = coolTime;
            }
        }
        else 
        {
            attackCheck = false;
            curTime -= Time.deltaTime;
        }
    }
    
    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(0.02f);
        LeftAttack.SetActive(false);
        RightAttack.SetActive(false);
    }
    #endregion


}
