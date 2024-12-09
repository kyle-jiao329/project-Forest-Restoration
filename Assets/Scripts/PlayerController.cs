using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float detectionRange = 2f;
    private bool hasNetBag = false;
    public GameObject Netbag;

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
                //hasNetBag = false;
                                FindObjectOfType<ProgressBar>().woniu += 1;
                //Netbag.SetActive(false);

            }
        }
    }
}