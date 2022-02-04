using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public Image Experience;
    public Text LevelText;
    GameManager GM;


    void Update()
    {
        Experience.fillAmount = PlayerData.ExperienceNow / PlayerData.ExperienceMax;
        LevelText.text = $"LV.{PlayerData.Level}";
    }

    public void PlusEx(float ExValue)
    {
        PlayerData.ExperienceNow += ExValue;

        if (PlayerData.ExperienceNow >= PlayerData.ExperienceMax)
        {
            PlayerData.ExperienceNow = 0f;
            PlayerData.ExperienceMax += 100f;
            PlayerLevelUp();
        }
    }

    public void PlayerLevelUp()
    {
        PlayerData.Level += 1;
        PlayerData.StaminaMax += 50f;
        PlayerData.AttackForce += 10f;
        PlayerData.ManaMax += 20f;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Has", 0);
        PlayerPrefs.SetFloat("AttackForce", PlayerData.AttackForce);
        PlayerPrefs.SetFloat("StaminaMax", PlayerData.StaminaMax);
        PlayerPrefs.SetFloat("ManaMax", PlayerData.ManaMax);
        PlayerPrefs.SetFloat("ExperienceMax", PlayerData.ExperienceMax);
        PlayerPrefs.SetFloat("ExperienceNow", PlayerData.ExperienceNow);
        PlayerPrefs.SetInt("Level", PlayerData.Level);
        PlayerPrefs.Save();
    }
}
