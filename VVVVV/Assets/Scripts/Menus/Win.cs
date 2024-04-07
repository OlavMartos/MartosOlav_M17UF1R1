using UnityEngine.SceneManagement;

public class Win : Menu
{
    public void RestartGame()
    {
        Secret.EERestart();
        SceneManager.LoadScene(1);
    }
}
