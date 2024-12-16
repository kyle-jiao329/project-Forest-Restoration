using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 导入TextMeshPro命名空间
public class ChangeText : MonoBehaviour
{
// 引用TextMeshPro组件
    public TextMeshPro textMeshPro;

    void Start()
    {
        // 确保组件已绑定
        if (textMeshPro != null)
        {
            // 修改文字内容
            textMeshPro.text = "新文字内容";
        }
        else
        {
            Debug.LogError("未设置TextMeshPro组件！");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
