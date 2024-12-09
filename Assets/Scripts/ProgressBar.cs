using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Transform player;
    public Image spriteRenderer;
    public int woniu = 0;
    public float progressValue; // 进度值，范围 0 到 1
    private Vector3 initialOffset;
    void Start()
    {
        initialOffset = transform.position - player.position;
    }
    void Update()

    {
        progressValue = woniu/ 5f;
        //transform.position = player.position + initialOffset;
        spriteRenderer.fillAmount = progressValue; // 设置进度条的填充量

    }
}