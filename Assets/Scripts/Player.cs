using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float thrustForce = 8f;       // Forward thrust
    public float rotationSpeed = 300f;   // Rotation speed
    public float maxSpeed = 10f;         // Maximum speed
    public float drag = 0.95f;           // Drag to slow down over time

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;      // Bullet prefab to instantiate
    public float bulletSpeed = 15f;      // Speed of bullets
    public float bulletLifetime = 3f;    // How long bullets live
    public float shootCooldown = 0.2f;   // Time between shots

    private Rigidbody2D rb;
    private float lastShotTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on Player!");
        }
    }

    void Update()
    {
        HandleRotation();
        HandleThrust();
        HandleShooting();
    }

    void FixedUpdate()
    {
        // Apply drag to slow down
        rb.linearVelocity *= drag;

        // Clamp max speed
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void HandleRotation()
    {
        float rotationInput = Input.GetAxis("Horizontal"); // Uses GetAxis for smoother input
        rb.rotation += rotationInput * rotationSpeed * Time.deltaTime;
    }

    void HandleThrust()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Vector2 force = transform.up * thrustForce;
            rb.AddForce(force);
        }
    }

    void HandleShooting()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time >= lastShotTime + shootCooldown)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned!");
            return;
        }

        // Instantiate bullet at player position
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // Set bullet velocity
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = transform.up * bulletSpeed;
        }

        // Destroy bullet after its lifetime expires
        Destroy(bullet, bulletLifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Player hit an asteroid = boom
            if (Lives.Instance != null)
            {
                Lives.Instance.PlayerDied();
            }
            Destroy(gameObject);
        }
    }
}