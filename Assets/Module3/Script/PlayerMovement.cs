using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UI;

namespace Platformer
{
    public class PlayerController3 : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;

        private bool facingRight = false;

        private bool isGrounded;
        public Transform groundCheck;

        private Rigidbody2D rb;
        private Animator animator;
        public Image bubbleImage;
        private bool getBubble = false;
        [SerializeField] private AudioSource jumpSoundEffect;
        [SerializeField] private AudioSource boostUpSoundEffect;
        private Bubble bubble;
        private bool inUnderwater = false;


        // Start is called before the first frame update 
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            bubble = GetComponent<Bubble>();
        }

        private void FixedUpdate()
        {
            CheckGround();
        }

        // Update is called once per frame 
        void Update()
        {
            if (bubbleImage != null)
            {
                getBubble = bubbleImage.enabled;
            }

            if (inUnderwater && bubble.IsBubbleActive())
            {
                if (Input.GetButton("Vertical"))
                {
                    moveInput = Input.GetAxis("Vertical");
                    Vector3 direction = transform.up * moveInput;
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                    animator.SetInteger("playerState", 1); // Turn on run animation 
                }
                else
                {
                    if (isGrounded) animator.SetInteger("playerState", 0); //Turn on idle animation 
                }
            }

            if (Input.GetButton("Horizontal"))
            {
                moveInput = Input.GetAxis("Horizontal");
                Vector3 direction = transform.right * moveInput;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                animator.SetInteger("playerState", 1); // Turn on run animation 
            }
            else
            {
                if (isGrounded) animator.SetInteger("playerState", 0); //Turn on idle animation 
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                jumpSoundEffect.Play();
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
            if (!isGrounded) animator.SetInteger("playerState", 2); //Turn on jump animation 

            if (facingRight == false && moveInput < 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput > 0)
            {
                Flip();
            }
        }
        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("MagicLeaf"))
            {
                Destroy(other.gameObject);
                ApplySpeedBoost(6f, 10f);
            }

            if (other.gameObject.CompareTag("Underwater"))
            {
                inUnderwater = true;
                rb.gravityScale = 0.0f;

            }

            if (other.gameObject.CompareTag("Ground"))
            {
                inUnderwater = false;
                rb.gravityScale = 2.0f;
            }
        }

        public void ApplySpeedBoost(float boostAmount, float duration)
        {
            boostUpSoundEffect.Play();
            StartCoroutine(SpeedBoostCoroutine(boostAmount, duration));
        }

        private IEnumerator SpeedBoostCoroutine(float boostAmount, float duration)
        {
            Debug.Log("Boost started. Current speed: " + movingSpeed);

            // Increase player's moving speed
            movingSpeed += boostAmount;
            Debug.Log("Speed boosted: " + movingSpeed);

            // Wait for the specified duration
            yield return new WaitForSeconds(duration);

            // Restore original moving speed
            movingSpeed -= boostAmount;
            Debug.Log("Speed restored: " + movingSpeed);
        }

    }
}