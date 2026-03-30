using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public static void AddScore(int amount) //adds score to the player score
    {
        if (Instance != null)
        {
            Instance.addscore(amount); 
        }
    }

    void Start()
    {
    

        UpdateScoreText();
    }

    [ContextMenu("increase Score")] 
    public void addscore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = playerScore.ToString();
        }
    }

    public void restartgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   

}