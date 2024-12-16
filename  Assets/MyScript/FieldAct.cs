using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode; // 使用Netcode进行网络同步

public class FieldAct : NetworkBehaviour
{
    public GameObject textBoard;
    public TextMeshPro textMeshPro;

    // 网络同步变量，初始值为1-5的随机整数
    private NetworkVariable<int> phValue = new NetworkVariable<int>(
        default,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server // 仅服务器端可写
    );

    void Start()
    {
        if (IsServer)
        {
            // 服务器端初始化phValue
            phValue.Value = Random.Range(1, 6);
            UpdateText(); 
            textBoard.SetActive(false);
        }

        // 监听phValue变化
        phValue.OnValueChanged += OnPhValueChanged;
    }

    private void OnDestroy()
    {
        // 移除事件监听器
        phValue.OnValueChanged -= OnPhValueChanged;
    }

    // 当其他Collider进入触发器时调用
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("detector"))
        {
            HandleDetectorTrigger();
        }

        if (other.CompareTag("BocharBox"))
        {
            HandleBocharBoxTrigger(other);
        }
    }
    private void HandleDetectorTrigger()
    {
        if (!textBoard.activeSelf)
        {
            textBoard.SetActive(true); // 如果未激活，激活textBoard
            UpdateText();             // 更新文字内容
        }
        else
        {
            Debug.Log("textBoard 已经激活，无需重复设置内容。");
        }
    }
    private void HandleBocharBoxTrigger(Collider other)
    {
        Debug.Log("BocharBox === Trigger Enter: BocharBox");
        if (textBoard.activeSelf)
        {
            // 检查是否是当前客户端的对象
            if (IsOwner)
            {
                BiocharManager biocharManager = other.GetComponent<BiocharManager>();
                if (biocharManager != null)
                {
                    if (phValue.Value <= 7)
                    {
                        biocharManager.Work();
                        if (IsServer)
                        {
                            // phValue.Value += 1; // 服务器端增加phValue值
                            PHValueServerRpc(phValue.Value+1);
                        }
                        UpdateText(); // 更新显示内容
                    }
                    else
                    {
                        Debug.Log("phValue 超过允许值，未执行Work方法。");
                    }
                }
                else
                {
                    Debug.LogError("未找到 BiocharManager 组件！");
                }
            }
            else
            {
                Debug.Log("不是本客户端的对象，忽略。");
            }
        }
        else
        {
            Debug.Log("textBoard 未激活，无需重复设置内容。");
        }
    }
    [ServerRpc]
    // private void ServerRpcSetPhValueServerRpc(int newValue)
    private void PHValueServerRpc(int newValue)
    {
        phValue.Value = newValue;
    }

    // 更新文字内容
    private void UpdateText()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = $"ph : {phValue.Value}";
        }
        else
        {
            Debug.LogError("未设置TextMeshPro组件！");
        }
    }

    // 当phValue发生变化时调用
    private void OnPhValueChanged(int oldValue, int newValue)
    {
        Debug.Log($"phValue 发生变化: {oldValue} -> {newValue}");
        UpdateText(); // 当值变化时更新文字内容
    }
}
