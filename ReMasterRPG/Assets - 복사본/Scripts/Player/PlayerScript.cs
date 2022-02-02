using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // �÷��̾� ����
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
        staminaText.text = $"{StaminaNow} / {StaminaMax}";
        staminaUI.fillAmount = (StaminaNow + PlusStamina) / (StaminaMax + PlusStamina);

        manaText.text = $"{ManaNow} / {ManaMax}";
        manaUI.fillAmount = (ManaNow + PlusMana) / (ManaMax + PlusMana);
    }

    public void Hit()
    {
        StaminaNow -= MonsterScript.AttackForce;

        if (StaminaNow <= 0) PlayerDie();
    }


    #region ü��, ���� ���
    float curtime = 0;
    float regenCool = 0.5f;
    void PlayerStaminaManaRegen()
    {
        if (curtime <= 0)
        {
            if (StaminaMax - StaminaNow > 0) StaminaNow += 3f; // 0.5�ʴ� 3f ü��

            if (StaminaMax < StaminaNow) StaminaNow += StaminaMax - StaminaNow;

            if (ManaMax - ManaNow > 0) ManaNow += 2f;  // 0,5�ʴ� 2f ����

            if (ManaMax < ManaNow) ManaNow += ManaMax - ManaNow;

            curtime = regenCool;
        }
        else curtime -= Time.deltaTime;
    }
    #endregion


    #region �÷��̾� �̵� �޼���
    public void MoveBtnUp() => Dir = 0;
    public void RightBtn() => Dir = 1;
    public void LeftBtn() => Dir = -1;

    int Dir = 0;
    
    void PlayerMove()
    {
        if (Dir != 0) SR.flipX = (Dir == -1);
        RB.velocity = (new Vector2(Dir * MoveForce, RB.velocity.y));
    }

    bool isGround;
    public void JumptBtn() 
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.7f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));

        if (isGround)
        {
            RB.velocity = Vector2.zero;
            RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
    
        
    
    #endregion


    #region �÷��̾� ���� �޼���
    public void AttackBtn() => attackCheck = true;

    
    bool attackCheck = false;
    float curTime = 0;         // ���� ��Ÿ�� ����
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
        StaminaNow = 0;
        staminaText.text = $"0 / {StaminaMax}";
        staminaUI.fillAmount = 0f;
        Destroy(gameObject);
    }
}
