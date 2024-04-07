using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Secret
{
    public int Id;
    public string Description;
    public List<string> Phrases;

    public Secret(int id, string desc, List<string> frases)
    {
        this.Id = id;
        this.Description = desc;
        this.Phrases = frases;
    }

    public static void EERestart()
    {
        // Indicamos que se activado el Easter Egg por lo que activamos el del siguiente nivel.
        string path = $"Assets/StreamingAssets/Files/eeEffect.vvvvv";
        //string path = Path.Combine(Application.persistentDataPath, "Files/eeEffect.vvvvv");
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();

        sr.Close();

        string[] fileLines = file.Split('\r', '\n');
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            sections[1] = "off";
            fileLines[i] = string.Join(";", sections);
        }

        // Borramos el contenido del archivo
        File.WriteAllText(path, string.Empty);

        string modifiedContent = string.Join("\n", fileLines.Where(line => !string.IsNullOrWhiteSpace(line)));

        // Escribimos las nuevas líneas en el archivo
        File.WriteAllText(path, modifiedContent);
    }
}
