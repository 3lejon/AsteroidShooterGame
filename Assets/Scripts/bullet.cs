using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime = 1f;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private int damage = 1;

    void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Asteroid asteroid = other.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                asteroid.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
