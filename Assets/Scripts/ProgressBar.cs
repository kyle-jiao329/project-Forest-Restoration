using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Transform player;
    public Image spriteRenderer;
    [FormerlySerializedAs("woniu")] public int pickCount = 0;
    public float progressValue;
    private Vector3 initialOffset;
    void Start()
    {
        initialOffset = transform.position - player.position;
    }
    void Update()

    {
        progressValue = pickCount/ 5f;
        //transform.position = player.position + initialOffset;
        spriteRenderer.fillAmount = progressValue;

    }
}