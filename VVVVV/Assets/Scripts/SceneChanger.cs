using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int nextScene;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { SceneManager.LoadScene(nextScene, LoadSceneMode.Single); }
    }
}
