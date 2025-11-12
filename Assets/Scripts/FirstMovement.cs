using UnityEngine;

public class FirstMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private bool isMoving = false;
    private Vector2 targetPos;
    public bool cooldownOver = true;
    public Transform flag;
    public LayerMask obstacleLayer;

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

    private Vector2 lastMoveDir = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = rb.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector2 moveInput = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.W)) moveInput = Vector2.up;
            if (Input.GetKeyDown(KeyCode.S)) moveInput = Vector2.down;
            if (Input.GetKeyDown(KeyCode.A)) moveInput = Vector2.left;
            if (Input.GetKeyDown(KeyCode.D)) moveInput = Vector2.right;

            if (moveInput != Vector2.zero)
            {
                Vector2 worldMove = transform.TransformDirection(moveInput);

                worldMove = new Vector2(
                    Mathf.Round(worldMove.x),
                    Mathf.Round(worldMove.y)
                ).normalized;

                UpdateSpriteDirection(worldMove);
                lastMoveDir = worldMove;

                if (!Physics2D.OverlapCircle(rb.position + worldMove, 0.1f, obstacleLayer))
                {
                    targetPos = rb.position + worldMove;
                    StartCoroutine(MoveToTarget());
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && cooldownOver)
            {
                Respawn();
            }
        }
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        isMoving = true;

        while ((targetPos - rb.position).sqrMagnitude > 0.01f)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(targetPos);
        isMoving = false;

        spriteRenderer.sprite = playerIdle;
    }

    void UpdateSpriteDirection(Vector2 dir)
    {
        float x = dir.x;
        float y = dir.y;

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
        else
            spriteRenderer.sprite = playerIdle;
    }

    public void Respawn()
    {
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = flag.position;
        transform.rotation = Quaternion.identity;
        rb.bodyType = RigidbodyType2D.Dynamic;
        targetPos = rb.position;
    }
}
