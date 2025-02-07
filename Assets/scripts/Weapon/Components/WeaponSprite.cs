using Mtscoptor.Weapons.Components;

using Mtscoptor.Weapons.Components.ComponentData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{

    public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
    {

        private SpriteRenderer baseSpriteRenderer;

        private SpriteRenderer weaponSpriteRenderer;

        private int currentWeaponSpriteIndex;


        private Sprite[] currentPhaseSprites;

        public int manaCost;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            currentWeaponSpriteIndex = 0;
        }


        private void HandleEnterAttackPhase(AttackPhases phase)
        {
            Debug.Log("com");
            currentWeaponSpriteIndex = 0;
            Debug.Log("Com");

            currentPhaseSprites = currentAttackData.PhaseSprites.FirstOrDefault(data => data.Phase == phase).Sprites;

            manaCost=currentAttackData.ManaCost;
        }


        private void HandleBaseSpriteChange(SpriteRenderer sr)
        {
            if (sr == null)
            {
                Debug.LogError("SpriteRenderer is null in HandleBaseSpriteChange!");
                return;
            }
            Debug.Log("SpriteRenderer is in HandleBaseSpriteChange!");

            if (!isAttackActive)
            {
                weaponSpriteRenderer.sprite = null;
                Debug.Log("HandleBaseSpriteChange!");
                return;
            }
            Debug.Log("Pass");
            Debug.Log("in"+currentWeaponSpriteIndex);
            Debug.Log("Ph"+currentPhaseSprites.Length);

            if (currentWeaponSpriteIndex >= currentPhaseSprites.Length)
            {
                Debug.LogWarning($"{weapon.name} �������鳤�Ȳ�ƥ��");
            }
            Debug.Log("Here!");

            weaponSpriteRenderer.sprite = currentPhaseSprites[currentWeaponSpriteIndex];

            currentWeaponSpriteIndex++;
        }

        protected override void Start()
        {
            base.Start();

            baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            if (baseSpriteRenderer != null) Debug.Log("baseSpriteRenderer");
            weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
            if (weaponSpriteRenderer != null) Debug.Log("weaponSpriteRenderer");
            weapon.weaponManaCost = manaCost;

            data = weapon.Data.GetData<WeaponSpriteData>();

            baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
            Debug.Log("+");
            eventHandler.OnEnterAttackPhases += HandleEnterAttackPhase;
            Debug.Log("++");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);

            eventHandler.OnEnterAttackPhases -= HandleEnterAttackPhase;
        }
    }
}
