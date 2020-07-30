using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cubex.UI;

namespace Cubex.Characters
{
    public class Hint : Collectable
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
        protected UIManager myUIManager;
        [SerializeField]
        protected UIWindow myUIWindow;
        [SerializeField]
        protected Text textHint;
        [SerializeField]
        protected string teksPetunjuk;


        [SerializeField]
        protected GameObject imageHintLayout;
        [SerializeField]
        protected Image imageHint;
        [SerializeField]
        protected Sprite mGambar;

        [SerializeField]
        protected GameObject teksIden;

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

        public override void Collect()
        {
            Debug.Log("Hints");
            if(teksIden != null)
            {
                teksIden.SetActive(true);
                Debug.Log("Identifikasi");
            }
            else
            {
                Debug.Log("Identifikasi NULL");
            }
            myUIManager.OpenWindow(myUIWindow);
            if (teksPetunjuk == "Verifikasi")
            {

            }
            else
            {

                textHint.text = teksPetunjuk;

                if (imageHint != null)
                {
                    imageHintLayout.SetActive(true);
                    imageHint.sprite = mGambar;
                }
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

        public override void OnTriggerEnter2D(Collider2D other)
        {
            Character character = other.GetComponent<Character>();
            if (m_UseOnTriggerEnter2D && character != null)
            {
                Collect();
            }
        }

        public void OnTriggerExit2D(Collider2D collision2D)
        {
            //Exit();
            Character character = collision2D.GetComponent<Character>();
            if (character != null)
            {
                myUIManager.CloseActiveWindow();
            }
        }
    }
}