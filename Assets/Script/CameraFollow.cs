using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Nhân vật mà camera sẽ theo dõi
    public Vector3 offset = new Vector3(0, 5, -7); // Độ lệch từ vị trí của nhân vật
    public float followSpeed = 5f; // Tốc độ theo dõi

    void LateUpdate()
    {
        // Tính toán vị trí mới của camera
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Camera luôn nhìn về nhân vật
        transform.LookAt(target);
    }
}

