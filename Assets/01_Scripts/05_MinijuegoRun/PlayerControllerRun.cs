using UnityEngine;

public class PlayerControllerRun : MonoBehaviour
{
    public float jumpForce = 10f;
    public GameObject gameOverPanel;
    public GameObject death;
    private Rigidbody rb;
    private bool isGrounded;

    public AudioSource audioSalto;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            audioSalto.Play();
        }
        if (gameObject.transform.position.x <= death.transform.position.x)
        {
            GameOver();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }


}
