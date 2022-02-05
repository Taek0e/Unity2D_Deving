using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public Text Talktext;
    public GameObject TalkImage;

    public GameObject TalkButton;
    public GameObject PlayerButton;




    string[] talkAbout;
    int talkLength;
    public void StartTalk(string[] text, int length)
    {
        talkAbout = text;
        talkLength = length;
        i = -1;
    }



    public void TalkBtn()
    {
        Accept = true;
        TalkButton.SetActive(false);
        PlayerButton.SetActive(false);
        Time.timeScale = 0;
    }


    void Update()
    {
        if (Accept) Talking();
    }




    bool Accept = false;
    int i;

    void Talking()
    {
        if (Input.GetMouseButtonDown(0) || i == -1)
        {
            i++;
            if (talkLength == i)
            {
                Time.timeScale = 1;
                TalkImage.SetActive(false);
                PlayerButton.SetActive(true);
                Accept = false;
            }

            Talktext.text = talkAbout[i];
            
            if (i == 0) TalkImage.SetActive(true);

        }
    }


    

   
    


}
