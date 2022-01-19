using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text StatusText;
    public InputField roomInput, nameInput;


    private void Awake() => Screen.SetResolution(960,540,false);
    

    void Update()
    {
        // 현재 어떤 상태인지. 
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
    }




    // UI 버튼으로 컨트롤할 메서드 모음.
    #region Public Methods 



    public void Connect()
    {
        // 가장 먼저 해야하는 단계.
        // Photon Online Server에 접속하기! 
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Disconnect()
    {
        // 연결 끊기
        PhotonNetwork.Disconnect();
    }

    public void joinLobby()
    {
        // 로비에 접속하기
        PhotonNetwork.JoinLobby();
    }

    // 방에 참가하려면, Connect 되어있거나, Lobby에 참가해있어야함
    public void CreateRoom()
    {
        // 방 생성하고, 참가
        // 방 이름, 최대 플레이어수, 비공개 여부 등을 지정가능. 
        PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 });
    }

    public void joinRoom()
    {
        // 방 참가하기
        // 방 이름으로 입장가능
        PhotonNetwork.JoinRoom(roomInput.text);
    }

    public void JoinOrCreateRoom()
    {
        // 방 참가하는데, 방이 없으면 생성하고 참가
        PhotonNetwork.JoinOrCreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 }, null);
    }

    public void JoinRandomRoom()
    {
        // 방 랜덤으로 참가하기 
        PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        // 방 떠나기 
        PhotonNetwork.LeaveRoom();
    }

    #endregion   // UI 버튼으로 컨트롤할 메서드 모음.


    // 콜백 메서드 재정의 모음.
    #region PunCallbacks

    public override void OnConnectedToMaster()
    {
        // Photon Online Server에 접속하면 불리는 콜백 함수.
        // PhotonNetwork.ConnectUsingSettings()가 성공하면 불린다.

        print("서버 접속 완료");

        // 현재 플레이어 닉네임 설정.
        PhotonNetwork.LocalPlayer.NickName = nameInput.text;
    }   

    public override void OnDisconnected(DisconnectCause cause)
    {
        // 연결이 끊기면 불리는 콜백 함수.
        // PhotonNetwork.Disconnect()가 성공하면 불린다. 

        print("연결 끊김ㅋ");
    }

    public override void OnJoinedLobby()
    {
        // 로비에 접속하면 불리는 콜백 함수.
        // PhotonNetwork.JoinLobby()가 성공하면 불린다. 

        print("로비 접속 성공");
    }

    public override void OnCreatedRoom()
    {
        // 방 생성하면 불리는 콜백 함수.
        // PhotonNetwork.CreateRoom()가 성공하면 불린다.

        print("방 만들기 성공");
    }

    public override void OnJoinedRoom()
    {
        // 방에 참가하면 불리는 콜백 함수.
        // PhotonNetwork.CreateRoom(), JoinedRoom()가 성공 하면 불린다.
        print("방 참가 완료");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // 방 생성 실패하면 불리는 콜백 함수. 
        // PhotonNetwork.CreateRoom()를 호출할 때, 방 이름이 같은게 있으면 실패할 수 있다.
        print("방 만들기 실패");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // 방 참가 실패하면 불리는 콜백 함수
        // PhotonNetwork.JoinRoom()를 호출할 때, 방 인원수가 모두 찼거나 방이 존재하지 않으면 실패할 수 있다.
        print("방 참가 실패");
    }   

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 방 랜덤 참가 실패하면 불리는 콜백 함수.
        // PhotonNetwork.JoinRandomRoom()를 호출할 때, 방 인원수가 모두 차있거나 존재하지 않으면 실패할 수 있다.
        // 다른 사람이 더 빨리 들어갔거나, 방을 닫았을 수 있다,.

        print("방 랜덤 참가 실패");
    }



    #endregion



}
