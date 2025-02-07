using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    // 玩家属性
    public float maxHealth = 10f; // 最大血量
    public float currentHealth; // 当前血量
    public float maxMana = 50f; // 最大法力值
    static public float currentMana = 20f; // 当前法力值
    public float attackPower; // 攻击力
    public float defensePower; // 防御力
    public int coins; // 持有金币
    public List<string> collectedWeapons = new List<string>() { "Whip","Sword","Spear"} ;
    static public bool[] isWeaponCollected { get;private set; }= new bool[] {false,false,false};
    public int weaponCollected = 0;
    private int currentWeaponIndex = 0; // 当前装备的武器索引
    public string currentEquipedWeapon; // 当前装备的武器名字
    public bool haveGoldenKey = false; // 是否持有前往下一关的金色钥匙
    public bool haveRedKey = false; // 是否持有奖励房红色钥匙
    public bool isDead = false;
    public bool isInvicible = false; // 无敌状态方便测试
    public bool isStuck = false;    // 是否卡关
    public RespawnSystem respawnSystem;
    protected PlayerState playerState;


    private void Start()
    {
        // 初始化血量和法力值
        currentHealth = maxHealth;
        currentMana = 20f;
        Debug.Log("Stats Start");
        attackPower = 0f;
        defensePower = 0f;
        coins = 0;

    }

    private void Update()
    {
        if (!isDead && currentHealth == 0)
        {
            respawnSystem.Die();
            isDead = true;
        }
    }


    public void TakeDamage(float damage)
    {
        if (isInvicible)
        {
            return;
        }

        float finalDamage = damage - defensePower;
        if (finalDamage<=0)  
        {
            finalDamage = 0.5f;
        }

        currentHealth -= finalDamage;
        
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("玩家当前血量: " + currentHealth);
    }
    
    public void RestoreHealth(float amount)
    {
        currentHealth+=amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    
    public void RestoreMana(float amount)
    {
        currentMana += amount;
        currentMana = Mathf.Min(currentMana, maxMana); 

        Debug.Log("玩家当前法力: " + currentMana);
    }
    
    public void UseMana(float amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Max(currentMana, 0);
        Debug.Log("玩家当前法力: " + currentMana);
    }

    
    public void AddGold(int amount)
    {
        coins += amount;
        coins=Mathf.Clamp(coins,0,999);
    }
    
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
    }
    
    public void IncreaseAttackPower(float amount)
    {
        attackPower+=amount;
    }

    public void IncreaseDefensePower(float amount)
    {
        defensePower+=amount;
    }
    
    public void GetWeapon(string weaponName)
    {
        int index = -1;
        if(weaponName =="Whip") index = 0;
        if(weaponName =="Sword")index = 1;
        if(weaponName =="Spear")index = 2;

        if (!isWeaponCollected[index])
        {

            isWeaponCollected[index]=true;
            weaponCollected++;
            Debug.Log("Added weapon: " + weaponName);
            
            if(weaponCollected==1)
            {
                currentWeaponIndex = index;
                EquipWeapon(weaponName);
                SwitchWeapon(index);
                PlayerState.attackInputs[(int)PlayerState.CombatInputs.Whip]= true;
            }
        }
    }
    public bool GetIsWeaponCollected(int index) { 
        return isWeaponCollected[index];
    }
    
    public bool SwitchWeapon(int direction)
    {
        Debug.Log("canSwitchWeapon = " + isWeaponCollected[direction]);
        if (!isWeaponCollected[direction]) return false;
        currentWeaponIndex = direction;
        
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = collectedWeapons.Count - 1; 
        }
        else if (currentWeaponIndex >= collectedWeapons.Count)
        {
            Debug.Log("?");
            currentWeaponIndex = 0; 
        }
        
        EquipWeapon(collectedWeapons[currentWeaponIndex]);
        return true;
    }
    
    public void EquipWeapon(string weaponName)
    {
        Debug.Log("Equipped weapon: " + weaponName);
        currentEquipedWeapon = weaponName;
        
    }

}
