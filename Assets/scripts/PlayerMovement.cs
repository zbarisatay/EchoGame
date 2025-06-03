using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Animator animator;

    private Rigidbody2D rb;
    private float moveInput;
    private bool hasJumped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (Input.GetButtonDown("Jump") && !hasJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            hasJumped = true;
            animator.SetTrigger("Jump");  // Trigger tetikle
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasJumped = false;
            // Burada artık bool sıfırlamaya gerek yok
            // animasyonun bitişi animator tarafından kontrol edilecek
        }

        /*
        if (collision.gameObject.CompareTag("Obstacle")) 
        {
            MainMenuController.Instance.ShowGameOver();
        }
        */
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(moveInput));
    }
}
