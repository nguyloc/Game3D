using System;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2.0f;
    public float moveSpeed = 2.0f;
    
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // Tính toán vị trí mới mà pet muốn di chuyển tới (giữ nguyên giá trị Y)
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

        // Nếu khoảng cách giữa pet và người chơi lớn hơn followDistance, pet sẽ di chuyển
        if (Vector3.Distance(transform.position, player.position) > followDistance)
        {
            // Xoay pet về phía người chơi
            Vector3 direction = (player.position - transform.position).normalized; // Hướng từ pet đến player
            Quaternion lookRotation = Quaternion.LookRotation(direction); // Tính toán góc quay
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 500f); // Xoay pet về hướng đó (500 là tốc độ xoay, có thể điều chỉnh)

            // Di chuyển pet về phía người chơi
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

}
