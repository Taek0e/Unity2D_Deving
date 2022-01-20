using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject startPanel;
    public InputField nameInput;
    public GameObject leaveRoomBtn;
    public Text statusText;



    void Awake()
    {
        Screen.SetResolution(960, 540, false);

        PhotonNetwork.ConnectUsingSettings();

        PlayerPrefs.DeleteKey("Name");

    }


    void Update()
    {
        statusText.text = PhotonNetwork.NetworkClientState.ToString();
    }



    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby() => PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 10 }, null);

    


    // 버튼으로 사용할 메서드들
    public void LeaveRoomBTN()
    {
        PhotonNetwork.LeaveRoom();

        startPanel.SetActive(true);
        leaveRoomBtn.SetActive(false);
    }
    
    public void StartBTN()
    {
        startPanel.SetActive(false);
        leaveRoomBtn.SetActive(true);

        PlayerPrefs.SetString("Name", nameInput.text);
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }



}
