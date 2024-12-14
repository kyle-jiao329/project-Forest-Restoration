using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float detectionRange = 2f;
    private bool hasNetBag = false;
    public bool hasCake = false;
    public GameObject Netbag;
    public GameObject Cake;

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("NetBag"))
            {
                Destroy(collider.gameObject);
                hasNetBag = true;
                Netbag.SetActive(true);
            }
            else if (collider.CompareTag("Snail") && hasNetBag)
            {
                Destroy(collider.gameObject);
                FindObjectOfType<ProgressBar>().pickCount += 1;
            }
            else if (collider.CompareTag("Cake"))
            {
                Destroy(collider.gameObject);
                Cake.SetActive(true);
                hasCake = true;
            }
        }
    }
}