using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExperienceManager : MonoBehaviour
{
    public Image ExperienceImage;
    public Text LevelText;

    public InputField NickName;


    

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

        switch(PlayerData.Level)
        {
            case 4: PlayerData.Job = "Just swordsmen"; break;

            case 8: PlayerData.Job = "Magic swordsmen"; break;

            default: break;
        }
    }



    
    public void SaveData()
    {
        PlayerPrefs.SetInt("HasData", 0);


        PlayerPrefs.SetString("NickName", PlayerData.NickName);
        PlayerPrefs.SetString("Job", PlayerData.Job);

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

    public void GameStartBtn()
    {
        PlayerData.NickName = NickName.text;
        SceneManager.LoadScene("GameScene");
    }
}
