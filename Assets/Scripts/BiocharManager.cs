using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode; // 使用Netcode进行网络同步

public class BiocharManager : NetworkBehaviour
{
    public TextMeshPro textMeshPro;

    // Netcode同步变量，初始值为5
    private NetworkVariable<int> value = new NetworkVariable<int>(
        5, // 初始值为5
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server // 仅服务器端可写
    );

    public override void OnNetworkSpawn()
    {
        value.OnValueChanged += OnValueChanged;
    }

    void Start()
    {
        if (textMeshPro != null)
        {
            // 隐藏GameObject并重置value为5
            // gameObject.SetActive(false);
            value.Value = 5;
            // 初始化显示值
            UpdateText();
        }
        else
        {
            Debug.LogError("TextMeshPro组件未设置！");
        }
    }

    // 被其他GameObject调用
    public void Work()
    {
        // if (!IsServer)
        // {
        //     Debug.LogWarning("Work函数只能在服务器端调用！");
        //     return;
        // }

        // 每次调用减少value
        if (value.Value > 0)
        {
            Debug.Log($"BiocharManager Work called with value: {value.Value}");
            Debug.Log($"BiocharManager IsServer: {this.IsServer}");
            Debug.Log($"BiocharManager IsClient: {this.IsClient}");
            // value.Value--;
            if(this.IsServer){
                Debug.Log($"BiocharManager IsServer value: {value.Value}");
                value.Value-- ;
            }else if(this.IsClient){
                Debug.Log($"BiocharManager IsClient value: {value.Value}");
                ResetValueServerRpc(value.Value-1);
            }else{
                Debug.Log($"BiocharManager IsServer: {this.IsServer}");
                Debug.Log($"BiocharManager IsClient: {this.IsClient}");
                Debug.Log($"BiocharManager else value: {value.Value}");
                value.Value-- ;
            }
            

            if (value.Value == 0)
            {
                // 隐藏GameObject并重置value为5
                gameObject.SetActive(false);
                // value.Value = 5;
                if(this.IsClient){
                    ResetValueServerRpc(5);
                }else{
                    value.Value=5;
                }
            }
            UpdateText();
        }
    }

    [ServerRpc]
    private void ResetValueServerRpc(int newValue)
    {
        Debug.Log($"BiocharManager ==== ResetValueServerRpc called with newValue: {newValue}");
        value.Value = newValue;

    }

    // 更新TextMeshPro显示
    private void UpdateText()
    {
        if (textMeshPro != null)
        {
            Debug.Log($"BiocharManager UpdateText called with value: {value.Value}");
            textMeshPro.text = $"Biochar: {value.Value}";
        }
    }

    private void OnValueChanged(int oldValue, int newValue)
    {
        Debug.Log($"Biochar  Value发生变化: {oldValue} -> {newValue}");
        UpdateText();
    }

    private void OnEnable()
    {
        // 监听value值的变化
        value.OnValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        // 移除监听器
        value.OnValueChanged -= OnValueChanged;
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// // using Unity.Netcode; // 使用Netcode进行网络同步

// public class BiocharManager : MonoBehaviour
// {

//     public TextMeshPro textMeshPro;

//     // Start is called before the first frame update
//     void Start()
//     {
//         // 默认为隐藏状态
//         // gameObject.SetActive(false);
//         // 一个netcode 变量value 整数，初始值为5，
//     }

//     public void work(){
//         // 这个函数被其他gameobject调用，每次调用则 value-1，当value为0时，隐藏这个gameobject，并且value值改变为5
//         // gameObject.SetActive(false);
//         showTextMeshPro.text = "Biochar: " + value;
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
