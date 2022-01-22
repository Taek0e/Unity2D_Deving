using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;


public class PlayerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    public PhotonView PV;
    public Text NickNameText;
    public Image HealthImage;

    bool isGround;
    Vector3 curPos;


    void Awake()
    {
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;

        if (PV.IsMine)
        {
            var CM = GameObject.Find("CMcamera").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;
        }
    }


    void Update()
    {
        if (PV.IsMine)
        {
            float axis = Input.GetAxisRaw("Horizontal");
            RB.velocity = new Vector2(6 * axis, RB.velocity.y);


            if (axis != 0) PV.RPC("FlipXRPC", RpcTarget.AllBuffered, axis);

            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.5f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGround) PV.RPC("JumpRPC", RpcTarget.All);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PhotonNetwork.Instantiate("Bullet", transform.position + new Vector3(SR.flipX ? -0.4f : 0.4f, -0.11f, 0), Quaternion.identity)
                .GetComponent<PhotonView>().RPC("DirRPC", RpcTarget.All, SR.flipX ? -1 : 1);
            }
        }

        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;

        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);

    }



    public void Hit()
    {
        HealthImage.fillAmount -= 0.1f;
        if (HealthImage.fillAmount <= 0)
        {
            GameObject.Find("Canvas").transform.Find("RespawnPanel").gameObject.SetActive(true);
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }




    // RPC ¸ðÀ½
    [PunRPC]
    void FlipXRPC(float axis) => SR.flipX = (axis == -1);

    [PunRPC]
    void JumpRPC()
    {
        RB.velocity = Vector2.zero;
        RB.AddForce(Vector2.up * 700);
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);




    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(HealthImage.fillAmount);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            HealthImage.fillAmount = (float)stream.ReceiveNext();
        }
    }
}
