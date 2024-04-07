using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDead : MonoBehaviour
{
    private new AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos la tag del enemigo para reproducir un sonido
        if(other.tag == "Player" && tag == "Enemy")
        {
            audio.Play();
            Debug.Log("You Died!");
            other.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(ReloadSceneAfterSound());
        }
        else if(other.tag == "Player" && tag == "Pinchos")
        {
            audio.Play();
            Debug.Log("Te has pinchado");
            other.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(ReloadSceneAfterSound());
        }
    }

    IEnumerator ReloadSceneAfterSound()
    {
        PlayerController.Trap();
        yield return new WaitForSeconds(audio.clip.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
