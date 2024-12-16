using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    public class ObjectDestroyer : MonoBehaviour
    {
        // 当鼠标在该物体上按下时调用
        void OnMouseDown()
        {
            // 销毁该物体
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // # 触碰到物体时，获取工具1
    // # 触碰到物体是，获取工具2
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if(other.gameObject.tag == "Coin"){
            Debug.Log("Coin");
            // Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy");
            // Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
