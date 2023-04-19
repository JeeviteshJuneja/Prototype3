using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 gameStartPos;
    private int numOfJumps = 0;
    private float score = 0f;
    private float startUpLerp = 0f;
    public bool gameStart = false;
    public bool gameOver = false;
    public float dash = 1f;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            if (Input.GetKeyDown(KeyCode.Space) && numOfJumps < 2 && !gameOver)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                numOfJumps++;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 0.75f);
            }
            if (Input.GetKey(KeyCode.D) && !gameOver)
            {
                playerAnim.speed = 2;
                dash = 2f;
            }
            else
            {
                playerAnim.speed = 1;
                dash = 1f;
            }
            if (!gameOver)
            {
                score += Time.deltaTime * dash;
                Debug.Log(Mathf.Floor(score));
            }
        }
        else
        {
            transform.position = Vector3.Lerp(startPos, gameStartPos, startUpLerp);
            if (startUpLerp > 1f)
            {
                gameStart = true; 
            }
            startUpLerp += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            numOfJumps = 0;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 0.75f);
        }
    }
}
