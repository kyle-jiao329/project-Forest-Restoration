using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public Transform player;
    public TextMeshPro textMeshPro;
    public Image spriteRenderer;
    public int woniu = 0;
    public int model = 1;// 1:easy model 2: normal model 3: hard model
    public float progressValue; // ����ֵ����Χ 0 �� 1
    private Vector3 initialOffset;
    void Start()
    {
        initialOffset = transform.position - player.position;
        if( model==1)
        {
            textMeshPro.text = $"easy model";
        }else if (model == 2)
        {
            textMeshPro.text = $"normal model";
        }
        else if (model == 3)
        {
            textMeshPro.text = $"hard model";
        }
    }
    void Update()

    {
        progressValue = woniu/ 5f;
        //transform.position = player.position + initialOffset;
        spriteRenderer.fillAmount = progressValue; // ���ý������������

    }
}