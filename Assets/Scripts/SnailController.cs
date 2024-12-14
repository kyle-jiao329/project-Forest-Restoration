using UnityEngine;

public class SnailController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform player;
    private PlayerController playerController;

    private void Start()
    {
        playerController = player.gameObject.GetComponent<PlayerController>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        AdjustHeightToGround();
        if (playerController.hasCake && distanceToPlayer <= 5f)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position);
        direction.y = 0;
        direction.Normalize();

        Vector3 targetDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

        transform.position += direction * moveSpeed * Time.deltaTime;
        AdjustHeightToGround();
    }

    private void AdjustHeightToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position+5*Vector3.up+Vector3.right, Vector3.down, out hit, 10f))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}