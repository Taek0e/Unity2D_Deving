using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public float StaminaNow = 0f;
    public float StaminaMax = 0f;


    public GameObject ExManager;
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
        StaminaNow -= PlayerData.AttackForce;
        if (dir) RB.AddForce(left, ForceMode2D.Impulse);
        else RB.AddForce(right, ForceMode2D.Impulse);

        if (StaminaNow <= 0) 
        {
            StaminaNow = 0;
            staminaText.text = "0";
            staminaUI.fillAmount = 0f;
            Destroy(gameObject);
            ExManager.GetComponent<ExperienceManager>().PlusEx(20f);
        }
    }

    public void skillHit()
    {
        StaminaNow -= 50f;
        if (StaminaNow <= 0)
        {
            StaminaNow = 0;
            staminaText.text = "0";
            staminaUI.fillAmount = 0f;
            Destroy(gameObject);
            ExManager.GetComponent<ExperienceManager>().PlusEx(20f);
        }
    }
}


  
