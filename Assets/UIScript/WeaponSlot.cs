using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUIManager : MonoBehaviour
{
    // 玩家对象的 PlayerStats 脚本
    public PlayerStats playerStats;

    // 存储武器名称和对应的图标对象
    private Dictionary<string, GameObject> weaponIcons = new Dictionary<string, GameObject>();

    // 预设的武器图标对象
    [Header("Weapon Icons")]
    public GameObject whipIcon;
    public GameObject swordIcon;
    public GameObject spearIcon;

    void Start()
    {
        // 初始化字典，存储武器名称与对应的图标
        weaponIcons.Add("Whip", whipIcon);
        weaponIcons.Add("Sword", swordIcon);
        weaponIcons.Add("Spear", spearIcon);

        // 禁用所有图标，确保只有正确的武器图标显示
        DisableAllIcons();
    }

    void Update()
    {
        if (playerStats != null && playerStats.collectedWeapons.Count > 0)
        {
            // 获取玩家当前装备的武器名称
            string currentWeaponName = playerStats.currentEquipedWeapon;

            // 更新显示的武器图标
            UpdateWeaponIcon(currentWeaponName);
        }
    }

    // 更新武器图标显示
    void UpdateWeaponIcon(string weaponName)
    {
        // 禁用所有图标
        DisableAllIcons();

        // 启用当前武器对应的图标
        if (weaponIcons.ContainsKey(weaponName))
        {
            weaponIcons[weaponName].SetActive(true);
        }
    }

    // 禁用所有图标
    void DisableAllIcons()
    {
        foreach (var icon in weaponIcons.Values)
        {
            icon.SetActive(false);
        }
    }
}
