using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxHealth = 3;
    public int scoreForDestroy = 10;

    private Vector2 velocity;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        float angle = Random.Range(0f, 360f);
        velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * moveSpeed;
    }

    void Update()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DestroyAsteroid();
        }
    }

    private void DestroyAsteroid()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.addscore(10);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Add score for bullet hit on asteroid
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.addscore(10);
            }

            // Damage asteroid and destroy bullet
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
