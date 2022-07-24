using UnityEngine;
using UnityEngine.SceneManagement;

public class scPress_Space_To_Start : MonoBehaviour
{
    public void Playgame()
    {

        
            
            SceneManager.LoadScene("putangina");
        
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
