using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mtscoptor.CoreSystem
{

    public class CollisionSenses : CoreComponent
    {
        private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
        private Movement movement;

        #region Check Transforms 

        public Transform GroundCheck
        {
            get => GenericNotInplementedError<Transform>.TryGet(groundCheck, transform.parent.name);
            private set => groundCheck = value;
        }
        
        public Transform WallCheck
        {
            get => GenericNotInplementedError<Transform>.TryGet(wallCheck, transform.parent.name);

            private set => wallCheck = value;
        }

        public Transform LedgeCheckHorizontal
        {
            get => GenericNotInplementedError<Transform>.TryGet(ledgeCheckHorizontal, transform.parent.name);
            private set => ledgeCheckHorizontal = value;
        }

        public Transform LedgeCheckVertical
        {
            get => GenericNotInplementedError<Transform>.TryGet(ledgeCheckVertical, transform.parent.name);
            private set => ledgeCheckVertical = value;
        }

        public Transform CeilingCheck
        {
            get => GenericNotInplementedError<Transform>.TryGet(ceilingCheck, transform.parent.name);
            private set => ceilingCheck = value;
        }
        
        public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }

        public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
        
        public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheckHorizontal;
        [SerializeField] private Transform ledgeCheckVertical;
        [SerializeField] private Transform ceilingCheck;

        [SerializeField] private float groundCheckRadius;
        [SerializeField] private float wallCheckDistance;

        [SerializeField] private LayerMask whatIsGround;

        #endregion

        #region Check Functions ��麯��
        
        public bool Ceiling
        {
            get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
        }
        
        public bool Ground
        {
            get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        }
        
        public bool WallFront
        {
            get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
        }

        public bool LedgeHorizontal
        {
            get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
        }

        public bool LedgeVertical
        {
            get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
        }

        public bool WallBack
        {
            get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDistance, whatIsGround);
        }

        #endregion
    }
}
