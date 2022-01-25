using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class MonsterScript : MonoBehaviour
{
    public Image Health;
    public Rigidbody2D RB;

    public float HitForce;
    Vector3 Hitdir_R = new Vector3(1f, 2f, 0);
    Vector3 Hitdir_L = new Vector3(-1f, 2f, 0);


    public void MonsterHit(bool DirAtk)
    {
        Health.fillAmount -= 0.1f;
        if (DirAtk)  RB.AddForce(Hitdir_L * HitForce, ForceMode2D.Impulse);
        else  RB.AddForce(Hitdir_R * HitForce, ForceMode2D.Impulse);

        if (Health.fillAmount <= 0)
            Destroy(gameObject);
    }
}
