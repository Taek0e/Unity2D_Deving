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
        // ���� � ��������. 
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
    }




    // UI ��ư���� ��Ʈ���� �޼��� ����.
    #region Public Methods 



    public void Connect()
    {
        // ���� ���� �ؾ��ϴ� �ܰ�.
        // Photon Online Server�� �����ϱ�! 
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Disconnect()
    {
        // ���� ����
        PhotonNetwork.Disconnect();
    }

    public void joinLobby()
    {
        // �κ� �����ϱ�
        PhotonNetwork.JoinLobby();
    }

    // �濡 �����Ϸ���, Connect �Ǿ��ְų�, Lobby�� �������־����
    public void CreateRoom()
    {
        // �� �����ϰ�, ����
        // �� �̸�, �ִ� �÷��̾��, ����� ���� ���� ��������. 
        PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 });
    }

    public void joinRoom()
    {
        // �� �����ϱ�
        // �� �̸����� ���尡��
        PhotonNetwork.JoinRoom(roomInput.text);
    }

    public void JoinOrCreateRoom()
    {
        // �� �����ϴµ�, ���� ������ �����ϰ� ����
        PhotonNetwork.JoinOrCreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 }, null);
    }

    public void JoinRandomRoom()
    {
        // �� �������� �����ϱ� 
        PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        // �� ������ 
        PhotonNetwork.LeaveRoom();
    }

    #endregion   // UI ��ư���� ��Ʈ���� �޼��� ����.


    // �ݹ� �޼��� ������ ����.
    #region PunCallbacks

    public override void OnConnectedToMaster()
    {
        // Photon Online Server�� �����ϸ� �Ҹ��� �ݹ� �Լ�.
        // PhotonNetwork.ConnectUsingSettings()�� �����ϸ� �Ҹ���.

        print("���� ���� �Ϸ�");

        // ���� �÷��̾� �г��� ����.
        PhotonNetwork.LocalPlayer.NickName = nameInput.text;
    }   

    public override void OnDisconnected(DisconnectCause cause)
    {
        // ������ ����� �Ҹ��� �ݹ� �Լ�.
        // PhotonNetwork.Disconnect()�� �����ϸ� �Ҹ���. 

        print("���� ���褻");
    }

    public override void OnJoinedLobby()
    {
        // �κ� �����ϸ� �Ҹ��� �ݹ� �Լ�.
        // PhotonNetwork.JoinLobby()�� �����ϸ� �Ҹ���. 

        print("�κ� ���� ����");
    }

    public override void OnCreatedRoom()
    {
        // �� �����ϸ� �Ҹ��� �ݹ� �Լ�.
        // PhotonNetwork.CreateRoom()�� �����ϸ� �Ҹ���.

        print("�� ����� ����");
    }

    public override void OnJoinedRoom()
    {
        // �濡 �����ϸ� �Ҹ��� �ݹ� �Լ�.
        // PhotonNetwork.CreateRoom(), JoinedRoom()�� ���� �ϸ� �Ҹ���.
        print("�� ���� �Ϸ�");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // �� ���� �����ϸ� �Ҹ��� �ݹ� �Լ�. 
        // PhotonNetwork.CreateRoom()�� ȣ���� ��, �� �̸��� ������ ������ ������ �� �ִ�.
        print("�� ����� ����");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // �� ���� �����ϸ� �Ҹ��� �ݹ� �Լ�
        // PhotonNetwork.JoinRoom()�� ȣ���� ��, �� �ο����� ��� á�ų� ���� �������� ������ ������ �� �ִ�.
        print("�� ���� ����");
    }   

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // �� ���� ���� �����ϸ� �Ҹ��� �ݹ� �Լ�.
        // PhotonNetwork.JoinRandomRoom()�� ȣ���� ��, �� �ο����� ��� ���ְų� �������� ������ ������ �� �ִ�.
        // �ٸ� ����� �� ���� ���ų�, ���� �ݾ��� �� �ִ�,.

        print("�� ���� ���� ����");
    }



    #endregion



}
