using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

using Cubex.Utilities;

namespace Cubex.Characters
{
    public class Cubex : Character
    {
        #region Fields

        [Header("Character Details")]
        [Space]
        [SerializeField]
        protected float m_MaxRunSpeed = 8f;
        [SerializeField]
        protected float m_RunSmoothTime = 5f;
        [SerializeField]
        protected float m_RunSpeed = 5f;
        [SerializeField]
        protected float m_WalkSpeed = 1.75f;
        [SerializeField]
        protected float m_JumpStrength = 10f;

        //Old
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;
        
        [SerializeField]
        public Button buttonLeft;
        [SerializeField]
        public Button buttonRight;

        private SpriteRenderer spriteRenderer;

        //End Old

        [Header("Character Reference")]
        [Space]
        [SerializeField]
        protected GroundCheck m_GroundCheck;
        [SerializeField]
        protected Rigidbody2D m_Rigidbody2D;
        [SerializeField]
        protected Collider2D m_Collider2D;
        [SerializeField]
        protected Animator m_Animator;
        [SerializeField]
        protected Skeleton m_Skeleton;

        [SerializeField]
        protected Text teksNyawa;
        [SerializeField]
        protected Text teksMisi;

        private int nyawa;
        private int misi;
        private int jumlahMisi;

        #endregion

        #region Private Variables

        protected Vector2 m_Speed = Vector2.zero;
        protected float m_CurrentRunSpeed = 0f;
        protected float m_CurrentSmoothVelocity = 0f;
        protected Vector3 m_InitialScale;
        protected Vector3 m_InitialPosition;

        #endregion

        #region Properties

        public override float MaxRunSpeed
        {
            get
            {
                return m_MaxRunSpeed;
            }
        }

        public override float RunSmoothTime
        {
            get
            {
                return m_RunSmoothTime;
            }
        }

        public override float RunSpeed
        {
            get
            {
                return m_RunSpeed;
            }
        }

        public override float WalkSpeed
        {
            get
            {
                return m_WalkSpeed;
            }
        }

        public override float JumpStrength
        {
            get
            {
                return m_JumpStrength;
            }
        }

        public override Vector2 Speed
        {
            get
            {
                return m_Speed;
            }
        }

        public override GroundCheck GroundCheck
        {
            get
            {
                return m_GroundCheck;
            }
        }

        public override Rigidbody2D Rigidbody2D
        {
            get
            {
                return m_Rigidbody2D;
            }
        }

        public override Collider2D Collider2D
        {
            get
            {
                return m_Collider2D;
            }
        }

        public override Animator Animator
        {
            get
            {
                return m_Animator;
            }
        }

        public override Skeleton Skeleton
        {
            get
            {
                return m_Skeleton;
            }
        }

        #endregion

        #region MonoBehaviour

        // Use this for initialization
        void Awake()
        {
            PlayerPrefs.SetInt("Lives", 3);
            PlayerPrefs.SetInt("Misi", 0);
            nyawa = 3;
            misi = 0;
            teksNyawa.text = "" + nyawa;
            teksMisi.text = "" + misi;

            m_InitialPosition = transform.position;
            m_InitialScale = transform.localScale;
            m_GroundCheck.OnGrounded += GroundCheck_OnGrounded;
            m_Skeleton.OnActiveChanged += Skeleton_OnActiveChanged;
            IsDead = new Property<bool>(false);
            m_Animator = GetComponent<Animator>();

            spriteRenderer = GetComponent<SpriteRenderer>();
            GameManager.OnReset += GameManager_OnReset;

            if(this == null)
            {
                Debug.Log("KOSOOONGG");
            }
        }

        private void OnDestroy()
        {
            GameManager.OnReset -= GameManager_OnReset;
        }

        void FixedUpdate ()
        {
            CekNyawa();
            CekMisi();
            if (transform.position.y < -20f)
            {
                Die();
                //Debug.Log("DIE");
            }

            // Speed
            m_Speed = new Vector2(Mathf.Abs(m_Rigidbody2D.velocity.x), Mathf.Abs(m_Rigidbody2D.velocity.y));

            // Speed Calculations
            m_CurrentRunSpeed = m_RunSpeed;
            if (m_Speed.x >= m_RunSpeed)
            {
                m_CurrentRunSpeed = Mathf.SmoothDamp(m_Speed.x, m_MaxRunSpeed, ref m_CurrentSmoothVelocity, m_RunSmoothTime);
            }

            // Input Processing
            //Move(joystick.Horizontal);
            Move(CrossPlatformInputManager.GetAxis("Horizontal"));

            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Jump();
            }

        }

        void CekNyawa()
        {
            nyawa = PlayerPrefs.GetInt("Lives", 3);
            teksNyawa.text = "" + nyawa;
            if(nyawa == 0)
            {
                Die();
            }
        }

        void CekMisi()
        {
            misi = PlayerPrefs.GetInt("Misi", 0);
            jumlahMisi = PlayerPrefs.GetInt("Jumlah Misi", 0);
            teksMisi.text = misi + " / " + jumlahMisi;
        }

        void LateUpdate()
        {
            m_Animator.SetFloat("Speed", m_Speed.x);
            m_Animator.SetFloat("VelocityX", Mathf.Abs(m_Rigidbody2D.velocity.x));
            m_Animator.SetFloat("VelocityY", m_Rigidbody2D.velocity.y);
            m_Animator.SetBool("IsGrounded", m_GroundCheck.IsGrounded);
            m_Animator.SetBool("IsDead", IsDead.Value);
        }

        #endregion

        
        /*
        protected void ComputeVelocity()
        {
            Vector2 move = Vector2.zero;

            move.x = joystick.Horizontal;

            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            //bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
            if (flipSprite)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            animator.SetBool("grounded", grounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        

            targetVelocity = move * maxSpeed;

            animator.SetFloat("Speed", Mathf.Abs(move.x));

            if (joystick.Horizontal > 0f)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(joystick.Horizontal);
                transform.localScale = scale;
            }
            else
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(joystick.Horizontal);
                transform.localScale = scale;
            }

            if (transform.position.y < -20f)
            {
                Die();
                //Debug.Log("DIE");
            }
        }
        */
        

        #region Public Methods

        public override void Move(float horizontalAxis)
        {
            if (!IsDead.Value)
            {
                float speed = m_CurrentRunSpeed;
                //				if ( CrossPlatformInputManager.GetButton ( "Walk" ) )
                //				{
                //					speed = m_WalkSpeed;
                //				}
                Vector2 velocity = m_Rigidbody2D.velocity;
                velocity.x = speed * horizontalAxis;
                m_Rigidbody2D.velocity = velocity;
                //m_Rigidbody2D.AddForce(Vector2.right * speed);

                if (horizontalAxis > 0f)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = Mathf.Sign(horizontalAxis);
                    transform.localScale = scale;
                    
                    //m_Rigidbody2D.AddForce(Vector2.right * speed);
                }
                else if (horizontalAxis < 0f)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = Mathf.Sign(horizontalAxis);
                    transform.localScale = scale;
                }
            }
        }

        public override void Jump()
        {
            //Debug.Log("AWW");
            Vector2 velocity = m_Rigidbody2D.velocity;
            if (m_GroundCheck.IsGrounded)
            {
                velocity.y = m_JumpStrength;
                m_Rigidbody2D.velocity = velocity;

                Debug.Log("AWW");
            }
            else if (Speed.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
                Debug.Log("WOO");
            }
            /**if (m_GroundCheck.IsGrounded)
             {
                 Debug.Log("AWW");
                 Vector2 velocity = m_Rigidbody2D.velocity;
                 velocity.y = m_JumpStrength;
                 m_Rigidbody2D.velocity = velocity;
                 //m_Animator.ResetTrigger("Jump");
                 //m_Animator.SetTrigger("Jump");
                 //m_JumpParticleSystem.Play();
                 //AudioManager.Singleton.PlayJumpSound(m_JumpAndGroundedAudioSource);
             }**/

        }

        public override void Die()
        {
            PlayerPrefs.SetInt("Lives", 3);
            Die(false);
        }

        public override void Die(bool blood)
        {
            if (!IsDead.Value)
            {
                Debug.Log("DEAAAADDD");
                IsDead.Value = true;
                m_Skeleton.SetActive(true, m_Rigidbody2D.velocity);

                GameManager.Singleton.Reset();
                GameManager.Singleton.StartGame();
                /*if (blood)
                {
                    ParticleSystem particle = Instantiate<ParticleSystem>(
                                                  m_BloodParticleSystem,
                                                  transform.position,
                                                  Quaternion.identity);
                    Destroy(particle.gameObject, particle.main.duration);
                }*/
                //CameraController.Singleton.fastMove = true;
            }
        }

        public override void Reset()
        {
            IsDead.Value = false;
            transform.localScale = m_InitialScale;
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Skeleton.SetActive(false, m_Rigidbody2D.velocity);
        }

        #endregion

        #region Events

        void GameManager_OnReset()
        {
            transform.position = m_InitialPosition;
            Reset();
        }

        void Skeleton_OnActiveChanged(bool active)
        {
            m_Animator.enabled = !active;
            m_Collider2D.enabled = !active;
            m_Rigidbody2D.simulated = !active;
        }

        void GroundCheck_OnGrounded()
        {
            if (!IsDead.Value)
            {
                //m_JumpParticleSystem.Play();
                //AudioManager.Singleton.PlayGroundedSound(m_JumpAndGroundedAudioSource);
            }
        }

        #endregion

        [System.Serializable]
        public class CharacterDeadEvent : UnityEvent
        {

        }

    }
}