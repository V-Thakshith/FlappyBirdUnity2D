using UnityEngine;
using UnityEngine.InputSystem;
public class Bird : MonoBehaviour
{
    private float upwardForce = 10f;
    public float maxUpwardVelocity = 10f;
    public float forwardSpeed = 5f;
    private Rigidbody2D _rb;


    [SerializeField] private GameManager gameManager;
    private int count = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 0f;
    }

    private void FixedUpdate()
    {
        // Maintain constant forward movement
        _rb.linearVelocity = new Vector2(forwardSpeed, _rb.linearVelocity.y);
        _rb.freezeRotation = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.TogglePause();
            gameManager.startScreenObject.SetActive(false);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
        }

        if (_rb.linearVelocity.y > maxUpwardVelocity)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, maxUpwardVelocity);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.LoseGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {
            gameManager.LoseGame();
        }

        if (collision.gameObject.CompareTag("Counter"))
        {
            gameManager.AddScore(1);
            Debug.Log("Count: " + count);
        }
    }
}
