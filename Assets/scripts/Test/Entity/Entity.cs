using Mtscoptor.CoreSystem;
using Mtscoptor.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Knockback info")]
    public Vector2 knockbackPow;
    public float knockbackDuration;
    protected bool isKnocked;

    [Header("AttackJudge info")]

    [Header("Slope")]
    [SerializeField] protected Transform slopeCheckPivot;
    [SerializeField] protected float slopeCheckDistance;
    public PhysicsMaterial2D noFrictionMat;
    public PhysicsMaterial2D fullFrictionMat;
    public bool isSlope { get; private set; }
    public Vector2 slopeVec { get; private set; }
    public float slopeAngle { get; private set; }

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheckPivot;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform wallCheckPivot;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask wallLayer;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    public Weapon Whip;
    public Weapon Knife;
    public Weapon Spear;
    public Weapon Whip_Heavy;
    public Weapon Knife_Heavy;
    public Weapon Spear_Heavy;

    #region Components
    public Animator anim { get; private set; }
    public Core Core { get; protected set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {

    }

    public virtual void Damaged(int attackerFacingDir)
    {
        // fx.StartCoroutine("FlashFx");
        StartCoroutine("HitKnockbacked", attackerFacingDir);
    }

    protected virtual IEnumerator HitKnockbacked(int attackerFacingDir)
    {
        isKnocked = true; // When isKnocked equals to true, function SetVelocity() break

        rb.velocity = new Vector2(knockbackPow.x * attackerFacingDir, knockbackPow.y);
        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }

    #region Collision
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheckPivot.position, Vector2.down,
        groundCheckDistance, groundLayer);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheckPivot.position, Vector2.right * facingDir,
        wallCheckDistance, groundLayer);

    public void SlopeCheck()
    {
        SlopeCheckHorizontal();
        SlopeCheckVertical();
    }

    public void SlopeCheckVertical()
    {

        RaycastHit2D hit;
        hit = Physics2D.Raycast(slopeCheckPivot.position,
            Vector2.down, slopeCheckDistance, groundLayer);
        if (hit)
        {
            slopeVec = -Vector2.Perpendicular(hit.normal).normalized;
            slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            Debug.DrawRay(hit.point, hit.normal * 5, Color.red);

            if (Vector2.Distance(hit.normal, Vector2.up) > 0.1)
                isSlope = true;
            else
                isSlope = false;
        }
    }

    public void SlopeCheckHorizontal()
    {
        RaycastHit2D hitFront = Physics2D.Raycast(slopeCheckPivot.position,
            Vector2.right, slopeCheckDistance, groundLayer);
        RaycastHit2D hitBack = Physics2D.Raycast(slopeCheckPivot.position,
            Vector2.left, slopeCheckDistance, groundLayer);

        //if(hitFront)
        //    isSlope = true;
        //else if(hitBack)
        //    isSlope = true;
        //else
        //    isSlope = false;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheckPivot.position,
            new Vector3(groundCheckPivot.position.x, groundCheckPivot.position.y - groundCheckDistance));

        Gizmos.DrawLine(wallCheckPivot.position,
            new Vector3(wallCheckPivot.position.x + wallCheckDistance * facingDir, wallCheckPivot.position.y));

    }

    



    #endregion

    #region Flip
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (facingDir * _x < 0)
        {
            Flip();
        }
    }
    #endregion

    #region Velocity
    public void SetVelocity(float _xVelocity, float _yVelocity) // Include the functionality of Flip
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
}
