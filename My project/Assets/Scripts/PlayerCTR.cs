using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerCTR : MonoBehaviourPunCallbacks
{
    private Transform trans;

    public float speed = 0;
    public Text nameText;
    public PhotonView PV;
    
    

    
    void Start()
    {
        trans = GetComponent<Transform>();

        nameText.text = PlayerPrefs.GetString("Name");
    }

   
    void Update()
    {
        if (PV.IsMine)
        {
            float Horizontal = speed * Input.GetAxisRaw("Horizontal") * Time.deltaTime;
            float Vertical = speed * Input.GetAxisRaw("Vertical") * Time.deltaTime;
            trans.Translate(new Vector3(Horizontal, Vertical, trans.position.z));
        }
    }
}
