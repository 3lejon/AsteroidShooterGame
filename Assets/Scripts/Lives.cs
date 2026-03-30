using UnityEngine;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    public static Lives Instance; // Singleton for easy access

    [Header("Lives Settings")]
    public int maxLives = 3; // Total lives player starts with
    public GameObject playerPrefab; 
    public Transform spawnPoint;
    public float respawnDelay = 3f; // Time before player respawns after death

    [Header("UI")]
    public UnityEngine.UI.Text livesText; // Assign a UI Text component

    private int currentLives; 
    private GameObject currentPlayer; 

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
        SpawnPlayer();
    }

    public void PlayerDied()
    {
        currentLives--;
        UpdateLivesUI();

        if (currentLives > 0)
        {
            // Respawn after delay
            Invoke("SpawnPlayer", respawnDelay);
        }
        else
        {
            GameOver();
        }
    }

    void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab is not assigned!");
            return;
        }
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn Point is not assigned!");
            return;
        }

        currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        // add game over UI, restart scene, etc. later
        // For now, reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
