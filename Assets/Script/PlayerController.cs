using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển
    public float rotationSpeed = 720f; // Tốc độ xoay (độ/giây)

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Lấy input từ bàn phím hoặc joystick
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Tạo hướng di chuyển theo input
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = moveDirection.normalized; // Chuẩn hóa để đảm bảo tốc độ di chuyển nhất quán

        if (moveDirection.magnitude > 0)
        {
            // Xoay nhân vật theo hướng di chuyển
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Di chuyển nhân vật
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}