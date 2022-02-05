using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        if (PlayerPrefs.HasKey("HasData")) 
        OnData();
    }

    

    void OnData()
    {
        PlayerData.AttackForce = PlayerPrefs.GetFloat("AttackForce");

        PlayerData.StaminaMax = PlayerPrefs.GetFloat("StaminaMax");
        PlayerData.StaminaNow = PlayerPrefs.GetFloat("StaminaNow");

        PlayerData.ManaMax = PlayerPrefs.GetFloat("ManaMax");
        PlayerData.ManaNow = PlayerPrefs.GetFloat("ManaNow");

        PlayerData.ExperienceMax = PlayerPrefs.GetFloat("ExperienceMax");
        PlayerData.ExperienceNow = PlayerPrefs.GetFloat("ExperienceNow");

        PlayerData.Level = PlayerPrefs.GetInt("Level");
    }
}
