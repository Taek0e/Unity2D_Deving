using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    public string[] TalkText;
    public TalkManager talkManager;


    public GameObject TalkButton;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            TalkButton.SetActive(true);
            talkManager.StartTalk(TalkText, TalkText.Length);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") TalkButton.SetActive(false);
    }
}
