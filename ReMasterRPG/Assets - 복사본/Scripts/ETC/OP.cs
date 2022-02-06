using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OP : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("HasData")) SceneManager.LoadScene("GameScene");
    }
}
