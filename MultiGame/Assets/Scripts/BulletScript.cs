using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletScript : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    int dir;


    void Start() => Destroy(gameObject, 3.5f);

    void Update() => transform.Translate(Vector3.right * 7 * Time.deltaTime * dir);



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground") PV.RPC("DestroyRPC", RpcTarget.AllBuffered);

        if (!PV.IsMine && col.tag == "Player" && col.GetComponent<PhotonView>().IsMine)
        {
            col.GetComponent<PlayerScript>().Hit();
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }   
    }

    [PunRPC]
    void DirRPC(int dir) => this.dir = dir;

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);
}
