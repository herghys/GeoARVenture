using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cubex.Characters
{
    public class Box : Collectable
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
        [SerializeField]
        protected int levelScene;
        [SerializeField]
        protected int levelMateri;
        [SerializeField]
        public int jumlahMisi;

        private int misi;

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

        private void Start()
        {
            PlayerPrefs.SetInt("Jumlah Misi", jumlahMisi);
            PlayerPrefs.Save();
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            Character character = other.GetComponent<Character>();
            if (m_UseOnTriggerEnter2D && character != null)
            {
                if (CekMisi())
                    Collect();
            }
        }

        public override void OnCollisionEnter2D(Collision2D collision2D)
        {
            Character character = collision2D.collider.GetComponent<Character>();
            if (!m_UseOnTriggerEnter2D && character != null)
            {
                if (CekMisi())
                    Collect();
            }
        }

        private bool CekMisi()
        {
            misi = PlayerPrefs.GetInt("Misi", 0);
            if (misi == jumlahMisi)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Collect()
        {
            /*GameManager.Singleton.m_Coin.Value++;
            m_Animator.SetTrigger(COLLECT_TRIGGER);
            m_ParticleSystem.Play();
            m_SpriteRenderer.enabled = false;
            m_Collider2D.enabled = false;
            Destroy(gameObject, m_ParticleSystem.main.duration);
            AudioManager.Singleton.PlayCoinSound(transform.position);*/
            m_ParticleSystem.Play();
            Debug.Log("BOX");

            //Set Level Materi
            var currentBentuk = PlayerPrefs.GetString("Bentuk", "Kubus");
            //PlayerPrefs.SetInt(currentBentuk, levelMateri);
            //PlayerPrefs.Save();

            //Set Scene AR
            string level;
            switch (levelScene)
            {
                case 1:
                    level = "Cube 01";
                    break;
                case 2:
                    level = "Balok";
                    break;
                case 3:
                    level = "Prisma";
                    break;
                case 4:
                    level = "Limas";
                    break;
                default:
                    level = "Main Menu";
                    break;
            }
            SceneManager.LoadScene(level);
        }

    }
}