using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cubex.Characters
{
    public class Target : Collectable
    {
        [SerializeField]
        protected ParticleSystem m_ParticleSystem;
        [SerializeField]
        protected SpriteRenderer m_SpriteRenderer;
        [SerializeField]
        protected Collider2D m_Collider2D;
        [SerializeField]
        protected Animator m_Animator;
        [SerializeField]
        protected bool m_UseOnTriggerEnter2D = true;

        #region Setter Getter
        public override SpriteRenderer SpriteRenderer
        {
            get
            {
                return m_SpriteRenderer;
            }
        }

        public override Animator Animator
        {
            get
            {
                return m_Animator;
            }
        }

        public override Collider2D Collider2D
        {
            get
            {
                return m_Collider2D;
            }
        }

        public override bool UseOnTriggerEnter2D
        {
            get
            {
                return m_UseOnTriggerEnter2D;
            }
            set
            {
                m_UseOnTriggerEnter2D = value;
            }
        }
        #endregion

        public override void OnTriggerEnter2D(Collider2D other)
        {
            Character character = other.GetComponent<Character>();
            if (m_UseOnTriggerEnter2D && character != null)
            {
                Collect();
            }
        }

        public override void OnCollisionEnter2D(Collision2D collision2D)
        {
            Character character = collision2D.collider.GetComponent<Character>();
            if (!m_UseOnTriggerEnter2D && character != null)
            {
                Collect();
            }
        }

        public override void Collect()
        {
            m_ParticleSystem.Play();
            int objectMisi = PlayerPrefs.GetInt("Misi", 0);
            objectMisi++;
            
            PlayerPrefs.SetInt("Misi", objectMisi);
            PlayerPrefs.Save();
            Destroy(gameObject);
        }
    }
}