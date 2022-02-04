using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        if (PlayerPrefs.HasKey("Has")) 
        OnData();
    }

    

    void OnData()
    {
        PlayerData.AttackForce = PlayerPrefs.GetFloat("AttackForce");
        PlayerData.StaminaMax = PlayerPrefs.GetFloat("StaminaMax");
        PlayerData.ManaMax = PlayerPrefs.GetFloat("ManaMax");
        PlayerData.ExperienceMax = PlayerPrefs.GetFloat("ExperienceMax");
        PlayerData.ExperienceNow = PlayerPrefs.GetFloat("ExperienceNow");
        PlayerData.Level = PlayerPrefs.GetInt("Level");
    }
}
