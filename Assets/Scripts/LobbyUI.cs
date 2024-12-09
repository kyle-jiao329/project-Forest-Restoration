using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField]
    private Button LobbyButton;

    [SerializeField]
    private Button JoinButton;

    void Awake(){
        LobbyButton.onClick.AddListener( () => {
            MultiPlayer.Instance.CreateLobby("DefaultLobby", false);
        });

        JoinButton.onClick.AddListener( ()=> {
            MultiPlayer.Instance.QuickJoin();
        });
    }
}
