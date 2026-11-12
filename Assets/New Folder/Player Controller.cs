using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public int targetScore = 50;

    public bool isGrounded;
    public int score = 0;

    private Rigidbody rb;
    private float moveX;
    private float moveZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (transform.position.y < -10f)
        {
            RestartGame();
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        rb.AddForce(movement * speed);
    }

    void OnCollisionStay(Collision collision)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Green"))
        {
            score = score + 10;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Red"))
        {
            score = score - 5;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Finish"))
        {
            if (score >= targetScore)
            {
                RestartGame();
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 30), "Puan: " + score + " / " + targetScore);
    }
}