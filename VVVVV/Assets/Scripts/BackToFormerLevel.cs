using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToFormerLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FormerLevel();
        }
    }

    void FormerLevel()
    {
        // Obtenemos la id de la escena acutal
        int actualLevel = SceneManager.GetActiveScene().buildIndex;

        // Leemos el archivos nivelesPasados
        string path = $"Assets/StreamingAssets/Files/nivelesPasados.vvvvv";
        //string path = Path.Combine(Application.persistentDataPath, "Files/nivelesPasados.vvvvv");
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();
        sr.Close();

        // Escribimos un "yes" en la escena actual-1
        string[] fileLines = file.Split('\r', '\n');
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (sections[0] == Convert.ToString(actualLevel-1))
            {
                sections[1] = "yes";
                fileLines[i] = string.Join(";", sections);
            }
        }
        File.WriteAllText(path, string.Empty);
        string modifiedContent = string.Join("\n", fileLines.Where(line => !string.IsNullOrWhiteSpace(line)));
        File.WriteAllText(path, modifiedContent);
    }
}
