using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShowSecret : MonoBehaviour
{
    public int EasterId;
    private Secret easter;
    public GameObject canvas;
    private TextMeshProUGUI textMeshPro;
    private int actualIndex = 0;
    private float phraseTime = 2f;
    private float afterPhrase = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        canvas.SetActive(true);
        textMeshPro = canvas.GetComponentInChildren<TextMeshProUGUI>();

        // Lectura del Archivo ee.vvvvv
        string path = $"Assets/StreamingAssets/Files/ee.vvvvv";
        //string path = Path.Combine(Application.persistentDataPath, "Files/ee.vvvvv");
        StreamReader sr = File.OpenText(path);
        string fileReaded = sr.ReadToEnd();
        string[] fileSplitted = fileReaded.Split(new char[] { '\r', '\n' });

        foreach (string line in fileSplitted)
        {
            // Separa cada linea del archivo en secciones y comprueba si la primera posicion es igual a la id y si lo es mostrara su texto y activara su efecto
            string[] secciones = line.Split(new char[] { ';' });
            if (secciones[0] == Convert.ToString(EasterId))
            {
                List<string> list = new List<string>();
                for(int i=2; i<secciones.Length; i++)
                {
                    list.Add(secciones[i]);
                }
                easter = new Secret(Convert.ToInt32(secciones[0]), secciones[1], list);

                StartCoroutine(ShowContent(easter.Phrases));
            }
        }
    }

    private IEnumerator ShowContent(List<string> frases)
    {
        while(actualIndex < frases.Count)
        {
            textMeshPro.text = frases[actualIndex];
            yield return new WaitForSeconds(phraseTime);
            actualIndex++;
        }

        yield return new WaitForSeconds(afterPhrase);

        SecretEffect.FromShowEaster(easter.Id);
        canvas.SetActive(false);
    }
}
