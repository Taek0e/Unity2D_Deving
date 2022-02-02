using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    static public float StaminaNow = 200f;
    static public float StaminaMax = 200f;
    static public float AttackForce = 10f;

    public Rigidbody2D RB;
    public Image staminaUI;
    public Text staminaText;

    void FixedUpdate()
    {
        staminaText.text = $"{StaminaNow}";
        staminaUI.fillAmount = (StaminaNow) / (StaminaMax);
    }



    Vector2 right = new Vector2(1, 2) * 5f;
    Vector2 left = new Vector2(-1, 2) * 5f;
    public void Hit(bool dir)
    {
        StaminaNow -= PlayerScript.AttackForce;
        if (dir) RB.AddForce(left, ForceMode2D.Impulse);
        else RB.AddForce(right, ForceMode2D.Impulse);

        if (StaminaNow <= 0) 
        { 
            StaminaNow = 0;
            staminaText.text = "0";
            staminaUI.fillAmount = 0f;
            Destroy(gameObject); 
        }
    }

}
