using Mtscoptor.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ս�����Լ���
/// </summary>
public class CombatTestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticles;

    private Animator anim;
    //[SerializeField] private Transform groundCheckPivot;
    //[SerializeField] private float groundCheckDistance;
    //[SerializeField] LayerMask groundLayer;

    //public bool IsGroundDetected() => Physics2D.Raycast(groundCheckPivot.position, Vector2.down,
    //groundCheckDistance, groundLayer);

    public void Damage(float amount)
    {
        Debug.Log($"{amount} Damage taken");

        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        anim.SetTrigger("damage");
        Destroy(gameObject);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}