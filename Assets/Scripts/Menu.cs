using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("SampleScene"); //load the game
    }
    public void OnQuitButton()
    {
        Application.Quit();//quits the game
    }
    

}