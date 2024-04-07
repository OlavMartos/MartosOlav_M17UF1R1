using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private static MusicController _instance;

    public static MusicController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MusicController>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("MusicController");
                    _instance = obj.AddComponent<MusicController>();
                }
            }
            return _instance;
        }
    }

    private AudioSource audioSource;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();

        // Creamos la carpeta en AppData para Secret
        string sourcePath = Path.Combine(Application.streamingAssetsPath, "Files"); // Ruta de origen en StreamingAssets
        string targetPath = Path.Combine(Application.persistentDataPath, "Files");   // Ruta de destino en persistentDataPath

        if(!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        if (!Directory.Exists(targetPath))
        {
            // Si la carpeta "Files" no existe en persistentDataPath, copia los archivos desde StreamingAssets
            Directory.CreateDirectory(targetPath); // Crea la carpeta en persistentDataPath
            string[] filesS = Directory.GetFiles(sourcePath);

            foreach (string file in filesS)
            {
                string targetFile = Path.Combine(targetPath, Path.GetFileName(file));
                File.Copy(file, targetFile);
            }
        }

        string[] files = Directory.GetFiles(sourcePath);
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string targetFile = Path.Combine(targetPath, fileName);

            if (!File.Exists(targetFile))
            {
                File.Copy(file, targetFile);
            }
        }
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    // Otros métodos para controlar la música según tus necesidades.
    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 6)
        {
            StopMusic();
        }
    }
}
