using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformationUI : MonoBehaviour
{
    public GameObject OnBtn;
    public GameObject OffBtn;

    public GameObject PanelUI;



    public Text Job;
    public Text NickName;
    public Text Strength;
    public Text LVL;
    public Text Mana;
    public Text Stamina;



 
    
    
    void Update()
    {
        Job.text = $"Job : {PlayerData.Job}";
        NickName.text = PlayerData.NickName;
        Strength.text = $"Strength : {PlayerData.AttackForce}";
        LVL.text = $"LVL : {PlayerData.Level}";
        Mana.text = $"Mana : {PlayerData.ManaMax}";
        Stamina.text = $"Stamina : {PlayerData.StaminaMax}";
    }



    public void OnInforBtn()
    {
        PanelUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void OffInforBtn()
    {
        PanelUI.SetActive(false);
        Time.timeScale = 1;
    }

}
