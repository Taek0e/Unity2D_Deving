using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SpriteRenderer PlayerSR;
    public Transform PlayerTrans;
    public GameObject BladeObject;


    public void SkillButton()
    {
        if (PlayerData.ManaNow >= 20f)
        {
            PlayerData.ManaNow -= 20f;

            if (PlayerSR.flipX)
            {
                BladeObject.GetComponent<SpriteRenderer>().flipX = true;
                Instantiate(BladeObject, new Vector3(PlayerTrans.position.x - 2, PlayerTrans.position.y, PlayerTrans.position.z), Quaternion.identity);
                
            }
            else
            {
                BladeObject.GetComponent<SpriteRenderer>().flipX = false;
                Instantiate(BladeObject, new Vector3(PlayerTrans.position.x + 2, PlayerTrans.position.y, PlayerTrans.position.z), Quaternion.identity);
            }
        }
    }
}
