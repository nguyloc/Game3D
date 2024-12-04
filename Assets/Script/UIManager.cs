using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider xpBar;
    public TMP_Text levelText;
    public int maxHealth = 100;
    public int maxXP = 100;
    
    private int currentHealth;
    private int currentXP;
    private int currentLevel = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        currentXP = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        // cập nhật máu, xp, level
        healthBar.value = currentHealth;
        xpBar.value = currentXP;
        levelText.text = "Lv. " + currentLevel;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateUI();
    }
    
    public void AddXP(int xp)
    {
        currentXP += xp;
        if (currentXP >= maxXP)
        {
            currentXP = 0;
            currentLevel++;
        }
        UpdateUI();
    }
}
