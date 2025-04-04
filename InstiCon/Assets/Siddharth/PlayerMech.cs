using UnityEngine;

public class PlayerMech : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
    }
}
