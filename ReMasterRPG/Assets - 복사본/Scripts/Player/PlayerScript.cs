using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
   
    public Rigidbody2D RB;
    public SpriteRenderer SR;

    public Image staminaUI;
    public Text staminaText;
    public Image manaUI;
    public Text manaText;

    public GameObject LeftAttack;
    public GameObject RightAttack;





    void FixedUpdate()
    {
        PlayerMove();
        PlayerAttack();

        PlayerStaminaManaUI();
        PlayerStaminaManaRegen();
    }



    void PlayerStaminaManaUI() 
    {
        staminaText.text = $"{PlayerData.StaminaNow} / {PlayerData.StaminaMax}";
        staminaUI.fillAmount = (PlayerData.StaminaNow + PlayerData.PlusStamina) / (PlayerData.StaminaMax + PlayerData.PlusStamina);

        manaText.text = $"{PlayerData.ManaNow} / {PlayerData.ManaMax}";
        manaUI.fillAmount = (PlayerData.ManaNow + PlayerData.PlusMana) / (PlayerData.ManaMax + PlayerData.PlusMana);
    }

    public void Hit()
    {
        PlayerData.StaminaNow -= MonsterData.AttackForce;

        if (PlayerData.StaminaNow <= 0) PlayerDie();
    }


    #region 체력, 마나 재생
    float curtime = 0;
    float regenCool = 0.5f;
    void PlayerStaminaManaRegen()
    {
        if (curtime <= 0)
        {
            if (PlayerData.StaminaMax - PlayerData.StaminaNow > 0)   PlayerData.StaminaNow += 3f; // 0.5초당 3f 체젠

            if (PlayerData.StaminaMax < PlayerData.StaminaNow)   PlayerData.StaminaNow += PlayerData.StaminaMax - PlayerData.StaminaNow;


            if (PlayerData.ManaMax - PlayerData.ManaNow > 0)   PlayerData.ManaNow += 2f;  // 0,5초당 2f 마젠

            if (PlayerData.ManaMax < PlayerData.ManaNow)   PlayerData.ManaNow += PlayerData.ManaMax - PlayerData.ManaNow;

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
        RB.velocity = (new Vector2(Dir * PlayerData.MoveForce, RB.velocity.y));
    }

    bool isGround;
    public void JumptBtn() 
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.7f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));

        if (isGround)
        {
            RB.velocity = Vector2.zero;
            RB.AddForce(Vector2.up * PlayerData.JumpForce, ForceMode2D.Impulse);
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



    void PlayerDie()
    {
        PlayerData.StaminaNow = 0;
        staminaText.text = $"0 / {PlayerData.StaminaMax}";
        staminaUI.fillAmount = 0f;
        Destroy(gameObject);
    }
}


public class PlayerData  // 플레이어 스탯관리 데이터 클래스
{
    static public float StaminaNow = 100f;
    static public float StaminaMax = 100f;
    static public float PlusStamina = 0f;

    static public float ManaNow = 100f;
    static public float ManaMax = 100f;
    static public float PlusMana = 0f;

    static public float AttackForce = 30f;
    static public float PlusAttackForce = 0f;

    static public float JumpForce = 14f;
    static public float MoveForce = 7f;
}
