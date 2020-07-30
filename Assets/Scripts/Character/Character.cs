using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cubex.Utilities;

namespace Cubex.Characters
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Skeleton))]

    public abstract class Character : MonoBehaviour
    {

    #region Properties
        public delegate void DeadHandler();

        public virtual event DeadHandler OnDead;

        public abstract float MaxRunSpeed { get; }

        public abstract float RunSmoothTime { get; }

        public abstract float RunSpeed { get; }

        public abstract float WalkSpeed { get; }

        public abstract float JumpStrength { get; }

        public abstract Vector2 Speed { get; }

        public abstract GroundCheck GroundCheck { get; }

        public abstract Rigidbody2D Rigidbody2D { get; }

        public abstract Collider2D Collider2D { get; }

        public abstract Animator Animator { get; }

        public abstract Skeleton Skeleton { get; }

        public virtual Property<bool> IsDead { get; set; }

        public abstract void Move(float horizontalAxis);

        public abstract void Jump();

        public abstract void Die();

        public abstract void Die(bool blood);

        public abstract void Reset();

    #endregion

        /*
        public float minGroundNormalY = .65f;
        public float gravityModifier = 1f;

        protected Vector2 targetVelocity;
        protected bool grounded;
        protected Vector2 groundNormal;
        protected Rigidbody2D rb2d;
        protected Vector2 velocity;
        protected ContactFilter2D contactFilter;
        protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
        protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


        protected const float minMoveDistance = 0.001f;
        protected const float shellRadius = 0.01f;

        */

        /*

        void OnEnable()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            contactFilter.useLayerMask = true;
        }

        void Update()
        {
            targetVelocity = Vector2.zero;
            ComputeVelocity();
        }

        protected virtual void ComputeVelocity()
        {

        }

        void FixedUpdate()
        {
            velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
            velocity.x = targetVelocity.x;

            grounded = false;

            Vector2 deltaPosition = velocity * Time.deltaTime;

            Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

            Vector2 move = moveAlongGround * deltaPosition.x;

            Movement(move, false);

            move = Vector2.up * deltaPosition.y;

            Movement(move, true);
        }

        void Movement(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;

            if (distance > minMoveDistance)
            {
                int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
                hitBufferList.Clear();
                for (int i = 0; i < count; i++)
                {
                    hitBufferList.Add(hitBuffer[i]);
                }

                for (int i = 0; i < hitBufferList.Count; i++)
                {
                    Vector2 currentNormal = hitBufferList[i].normal;
                    if (currentNormal.y > minGroundNormalY)
                    {
                        grounded = true;
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(velocity, currentNormal);
                    if (projection < 0)
                    {
                        velocity = velocity - projection * currentNormal;
                    }

                    float modifiedDistance = hitBufferList[i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }


            }

            rb2d.position = rb2d.position + move.normalized * distance;
        }
        */
    }

}