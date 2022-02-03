using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public Rigidbody2D RB;
    public Image staminaUI;
    public Text staminaText;

    void FixedUpdate()
    {
        staminaText.text = $"{MonsterData.StaminaNow}";
        staminaUI.fillAmount = (MonsterData.StaminaNow) / (MonsterData.StaminaMax);
    }



    Vector2 right = new Vector2(1, 2) * 5f;
    Vector2 left = new Vector2(-1, 2) * 5f;
    public void Hit(bool dir)
    {
        MonsterData.StaminaNow -= PlayerData.AttackForce;
        if (dir) RB.AddForce(left, ForceMode2D.Impulse);
        else RB.AddForce(right, ForceMode2D.Impulse);

        if (MonsterData.StaminaNow <= 0) 
        {
            MonsterData.StaminaNow = 0;
            staminaText.text = "0";
            staminaUI.fillAmount = 0f;
            Destroy(gameObject); 
        }
    }

    public void skillHit()
    {
        MonsterData.StaminaNow -= 50f;
        if (MonsterData.StaminaNow <= 0)
        {
            MonsterData.StaminaNow = 0;
            staminaText.text = "0";
            staminaUI.fillAmount = 0f;
            Destroy(gameObject);
        }
    }
}

public class MonsterData  // 몬스터 스탯관리 데이터 클래스 
{
    static public float StaminaNow = 200f;
    static public float StaminaMax = 200f;
    static public float AttackForce = 10f;
}
