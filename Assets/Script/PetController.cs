using System;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public Transform player; // Vị trí nhân vật
    public float followDistance = 5f; // Khoảng cách giữ giữa pet và nhân vật
    public float attackRange = 10f; // Khoảng cách pet có thể tấn công quái vật
    public float moveSpeed = 5f; // Tốc độ di chuyển của pet
    public GameObject fireballPrefab; // Prefab quả cầu lửa
    public Transform fireballSpawnPoint; // Vị trí bắn đạn
    public float attackCooldown = 2f; // Thời gian chờ giữa các lần tấn công

    private Transform currentTarget; // Quái vật mà pet đang tấn công
    private Animator animator; // Animator của pet
    private float lastAttackTime = 0; // Thời gian lần tấn công gần nhất

    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator của pet
    }

    void Update()
    {
        if (currentTarget == null || Vector3.Distance
                (currentTarget.position, transform.position) > attackRange)
        {
            FollowPlayer();
            FindClosestEnemy(); // Tìm quái vật gần nhất
        }
        else
        {
            AttackEnemy();
        }
        
        // Giữ trạng thái animation "bay tại chỗ" nếu không làm gì
        if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && currentTarget == null)
        {
            animator.SetBool("isFlyingFast", false); // Animation bay tại chỗ
        }
    }

    void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) > followDistance)
        {
            // Di chuyển theo nhân vật
            transform.position = Vector3.MoveTowards(transform.position, 
                player.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player.position);

            // Animation: bay nhanh
            animator.SetBool("isFlyingFast", true);
        }
        else
        {
            // Animation: bay tại chỗ
            animator.SetBool("isFlyingFast", false);
        }
    }

    void FindClosestEnemy()
    {
        // Tìm quái vật gần nhất
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= attackRange)
            {
                closestDistance = distance;
                currentTarget = enemy.transform;
            }
        }
    }

    void AttackEnemy()
    {
        if (currentTarget == null) return;

        // Nhìn về phía quái vật
        transform.LookAt(currentTarget.position);

        // Tấn công nếu cooldown đã sẵn sàng
        if (Time.time > lastAttackTime + attackCooldown)
        {
            animator.SetTrigger("Attack"); // Animation tấn công
            ShootFireball(); // Bắn đạn
            lastAttackTime = Time.time; // Cập nhật thời gian tấn công
        }
    }

    void ShootFireball()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null && currentTarget != null)
        {
            // Tạo đạn tại vị trí bắn
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

            // Tính hướng từ vị trí bắn đến quái vật
            Vector3 direction = (currentTarget.position - fireballSpawnPoint.position).normalized;

            // Xoay quả cầu lửa về phía quái vật
            fireball.transform.forward = direction;

            // Thêm vận tốc cho đạn
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 10f; // 10f là tốc độ của đạn, bạn có thể điều chỉnh
            }
        }
    }


    public void TakeDamage(int damage)
    {
        // Pet bị quái đánh
        animator.SetTrigger("Hurt"); // Animation bị đánh
    }

    public void Die()
    {
        // Pet chết
        animator.SetTrigger("Die"); // Animation chết
        this.enabled = false; // Ngưng script khi pet chết
    }
}
