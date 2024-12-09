using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Transform player;
    public Image spriteRenderer;
    public int woniu = 0;
    public float progressValue; // ����ֵ����Χ 0 �� 1
    private Vector3 initialOffset;
    void Start()
    {
        initialOffset = transform.position - player.position;
    }
    void Update()

    {
        progressValue = woniu/ 5f;
        //transform.position = player.position + initialOffset;
        spriteRenderer.fillAmount = progressValue; // ���ý������������

    }
}