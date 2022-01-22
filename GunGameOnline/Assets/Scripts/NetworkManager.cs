using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject DisconnectPanel;
    public GameObject RespawnPanel;

    public InputField NickNameInput;

    public Text StatusText;

    public AudioSource OP;



    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();

        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectPanel.SetActive(true);
        RespawnPanel.SetActive(false);
    }

    public void ConnectBTN()
    {
        OP.Stop();
        PhotonNetwork.ConnectUsingSettings(); 
    }
        
        
          



    // 콜백 함수들
    #region 
    public override void OnConnectedToMaster()  // 연결후 처리 콜백
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text; // 닉네임 저장
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null); // 방 만들기 또는 입장
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine("DetroyBullet");
        DisconnectPanel.SetActive(false);
        Spawn();
    }

    #endregion



    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-4f, 35f), 0, 0), Quaternion.identity);
        RespawnPanel.SetActive(false);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Bullet")) GO.GetComponent<PhotonView>().RPC("DestroyRPC", RpcTarget.All);
    }
}
