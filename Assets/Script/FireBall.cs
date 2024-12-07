using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 10; // Sát thương của đạn
    public float lifeTime = 2f; // Thời gian sống của đạn

    void Start()
    {
        // Hủy đạn sau thời gian lifeTime
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu đạn trúng quái vật
        if (other.CompareTag("Enemy"))
        {
            //EnemyHealthUI enemyHealth = other.GetComponent<EnemyHealthUI>();
            //if (enemyHealth != null)
            //{
            //    enemyHealth.TakeDamage(damage); // Gây sát thương cho quái vật
            //}
            Debug.Log("Hit enemy");
            Destroy(gameObject); // Hủy đạn sau khi trúng mục tiêu
        }
    }
}