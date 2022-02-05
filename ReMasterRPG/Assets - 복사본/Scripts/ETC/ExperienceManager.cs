using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public Image ExperienceImage;
    public Text LevelText;
    


    void Update()
    {
        ExperienceImage.fillAmount = PlayerData.ExperienceNow / PlayerData.ExperienceMax;
        LevelText.text = $"LV.{PlayerData.Level}";
    }




    public void PlusEx(float ExValue)
    {
        PlayerData.ExperienceNow += ExValue;

        if (PlayerData.ExperienceNow >= PlayerData.ExperienceMax) PlayerLevelUp();
    }

    


    public void PlayerLevelUp()
    {
        PlayerData.ExperienceNow = PlayerData.ExperienceNow - PlayerData.ExperienceMax;
        PlayerData.ExperienceMax += 50f * PlayerData.Level;

        PlayerData.Level += 1;
        PlayerData.StaminaMax += 50f;
        PlayerData.AttackForce += 15f;
        PlayerData.ManaMax += 20f;

        PlayerData.StaminaNow = PlayerData.StaminaMax;
        PlayerData.ManaNow = PlayerData.ManaMax;
    }



    
    public void SaveData()
    {
        PlayerPrefs.SetInt("HasData", 0);

        PlayerPrefs.SetFloat("AttackForce", PlayerData.AttackForce);

        PlayerPrefs.SetFloat("StaminaMax", PlayerData.StaminaMax);
        PlayerPrefs.SetFloat("StaminaNow", PlayerData.StaminaNow);

        PlayerPrefs.SetFloat("ManaMax", PlayerData.ManaMax);
        PlayerPrefs.SetFloat("ManaNow", PlayerData.ManaNow);

        PlayerPrefs.SetFloat("ExperienceMax", PlayerData.ExperienceMax);
        PlayerPrefs.SetFloat("ExperienceNow", PlayerData.ExperienceNow);

        PlayerPrefs.SetInt("Level", PlayerData.Level);
        PlayerPrefs.Save();
    }




    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
