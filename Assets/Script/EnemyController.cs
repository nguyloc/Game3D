using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public UIManager player;
    public UIManager pet;
    
    public int health = 30; // Máu của quái vật
    public int damage = 10; // Sát thương mà quái vật gây ra
    public int xpReward = 20; // Điểm kinh nghiệm khi giết quái vật
    

    void Start()
    {
        player = FindObjectOfType<UIManager>();  // Tìm kiếm UIManager trong scene
        pet = FindObjectOfType<UIManager>();     // Tìm kiếm UIManager cho pet (nếu pet có script riêng)
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage; // Giảm máu
        Debug.Log("Quái vật bị sát thương: " + damage + " Máu còn lại: " + health);

        if (health <= 0)
        {
            Die();
        }
    }
    
    
    public void Die()
    {
            player.AddXP(xpReward);
            pet.AddXP(xpReward);
            Destroy(gameObject); // Xóa quái vật
    }
}
