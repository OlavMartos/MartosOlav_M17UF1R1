using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ReadLevel(collision);
        }
    }

    void ReadLevel(Collider2D collision)
    {
        int actualLevel = SceneManager.GetActiveScene().buildIndex;
        string path = $"Assets/StreamingAssets/Files/nivelesPasados.vvvvv";
        //string path = Path.Combine(Application.persistentDataPath, "Files/nivelesPasados.vvvvv");
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();

        sr.Close();

        string[] fileLines = file.Split('\r', '\n');
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (sections[0] == Convert.ToString(actualLevel))
            {
                if (sections[1] == "yes")
                {
                    collision.transform.position = target.position;
                    sections[1] = "no";
                    fileLines[i] = string.Join(";", sections);
                }
            }
        }

        File.WriteAllText(path, string.Empty);

        string modifiedContent = string.Join("\n", fileLines.Where(line => !string.IsNullOrWhiteSpace(line)));

        // Escribimos las nuevas líneas en el archivo
        File.WriteAllText(path, modifiedContent);
    }
}
