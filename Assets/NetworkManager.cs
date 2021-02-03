using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public InputField IDInput;
  
    public override void OnJoinedRoom()
    {
        Spawn();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        Debug.Log("ÅÂ¾î³µ´Ù");
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
