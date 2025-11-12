using UnityEngine;

public class NormalMovement : MonoBehaviour
{
    [Header("Player")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool cooldownOver = true;

    [Header("Animation & Rendering")]
    public SpriteRenderer spriteRenderer;
    public Sprite playerIdle;
    public Sprite playerUp;
    public Sprite playerDown;
    public Sprite playerRight;
    public Sprite playerLeft;
    public Sprite playerUpLeft;
    public Sprite playerUpRight;
    public Sprite playerDownLeft;
    public Sprite playerDownRight;

    [Header("Misc")]
    public GameObject flag;
    public ButtonPressed buttonPressed;
    public GameObject button;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) moveInput.y = 1;
        if (Input.GetKey(KeyCode.S)) moveInput.y = -1;
        if (Input.GetKey(KeyCode.A)) moveInput.x = -1;
        if (Input.GetKey(KeyCode.D)) moveInput.x = 1;

        if (Input.GetKeyDown(KeyCode.R) && cooldownOver)
        {
            Respawn();
        }

        if (buttonPressed.gameStarted)
        {
            button.SetActive(false);
        }

        UpdateSpriteDirection();
    }

    void FixedUpdate()
    {
        Vector2 moveDir = (Vector2)(transform.right * moveInput.x + transform.up * moveInput.y);
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateSpriteDirection()
    {
        if (moveInput == Vector2.zero)
            return;

        float x = moveInput.x;
        float y = moveInput.y;

        if (x > 0 && y > 0)
            spriteRenderer.sprite = playerUpRight;
        else if (x < 0 && y > 0)
            spriteRenderer.sprite = playerUpLeft;
        else if (x > 0 && y < 0)
            spriteRenderer.sprite = playerDownRight;
        else if (x < 0 && y < 0)
            spriteRenderer.sprite = playerDownLeft;
        else if (y > 0)
            spriteRenderer.sprite = playerUp;
        else if (y < 0)
            spriteRenderer.sprite = playerDown;
        else if (x > 0)
            spriteRenderer.sprite = playerRight;
        else if (x < 0)
            spriteRenderer.sprite = playerLeft;
        else if (x == 0 && y == 0)
            spriteRenderer.sprite = playerIdle;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            Respawn();
        }
    }
    public void Respawn()
    {
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = flag.transform.position;
        transform.rotation = Quaternion.identity;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
