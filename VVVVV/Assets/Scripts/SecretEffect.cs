using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretEffect : MonoBehaviour
{
    private static Tilemap EEtilemap;

    public static void FromShowEaster(int id)
    {
        switch(id)
        {
            case 0:
                TestLevel();
                ActiveNextLevelEE(id);
                break;
            case 1:
                Level1();
                ActiveNextLevelEE(id);
                break;
            case 2:
                Level2();
                ActiveNextLevelEE(id);
                break;
            case 3:
                Level3();
                ActiveNextLevelEE(id);
                break;
            case 4:
                Level4();
                ActiveNextLevelEE(id);
                break;
            case 5:
                Level5();
                break;
        }
    }

    private static Tilemap GetEETilemap()
    {
        // Buscamos el Grid
        Grid grid = FindAnyObjectByType<Grid>();
        if (grid == null)
        {
            Debug.LogError("No se ha encontrado ningun grid");
            return null;
        }

        // Buscamos los tilemaps
        Tilemap[] tilemap = grid.GetComponentsInChildren<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("No se ha encontrado ningun TileMap");
            return null;
        }

        return tilemap[1];
    }

    private static void TestLevel()
    {
        EEtilemap = GetEETilemap();

        // Activamos el TilemapRenderer del segundor tilemap
        TilemapRenderer tmRen = EEtilemap.GetComponent<TilemapRenderer>();
        tmRen.enabled = true;

        // Buscamos los BoxCollider2D del segundo tilemap y activamos todo aquel que este desactivado
        BoxCollider2D[] colliders = EEtilemap.GetComponentsInChildren<BoxCollider2D>();
        foreach(BoxCollider2D boxCollider2D in colliders)
        {
            if (!boxCollider2D.enabled)
            {
                boxCollider2D.enabled = true;
            }
        }
    }

    /// <summary>
    /// Desactivamos los colliders del EEtilemap
    /// </summary>
    private static void Level1()
    {
        EEtilemap = GetEETilemap();

        BoxCollider2D[] colliders = EEtilemap.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D boxCollider2D in colliders)
        {
            if (boxCollider2D.enabled)
            {
                boxCollider2D.enabled = false;
            }
        }
    }
    
    /// <summary>
    /// Activamos la antigravedad y desactivamos los colliders alrededor del jugador para que muera
    /// </summary>
    private static void Level2()
    {
        Level1();
        EEtilemap = GetEETilemap();
        SpriteRenderer sR = EEtilemap.GetComponentInChildren<SpriteRenderer>();
        if (sR != null)
        {
            sR.enabled = false;
            EEtilemap.GetComponentInChildren<CircleCollider2D>().enabled = false;
        }
        PlayerController.OnGravityTrap("ingravity");
    }

    /// <summary>
    /// Cambiamos la ubicacion del jugador y activamos los efectos de la trapa de gravedad
    /// </summary>
    private static void Level3()
    {
        PlayerController.player.transform.position = new Vector3(47.06f, -30.4f, 4.083272f);
        PlayerController.OnGravityTrap("ingravity");
    }

    /// <summary>
    /// Hacemos que el jugador no se pueda mover y que no pueda invertir la gravedad
    /// </summary>
    private static void Level4()
    {
        PlayerController.player.Speed = 0f;
        PlayerController.Trap();
    }

    /// <summary>
    /// Mostramos al jefe final y cerramos el juego de forma automatica
    /// </summary>
    private static void Level5()
    {
        EEtilemap = GetEETilemap();
        SpriteRenderer[] sr = EEtilemap.GetComponentsInChildren<SpriteRenderer>();
        BoxCollider2D[] colliders = EEtilemap.GetComponentsInChildren<BoxCollider2D>();
        Debug.Log(sr[0].sprite);
        if (sr[0].tag == "GrimReaper")
        {
            sr[0].enabled = true;
            foreach(BoxCollider2D collider in colliders)
            {
                if (!collider.enabled)
                {
                    collider.enabled = true;
                }
            }
        }

        Secret.EERestart();
        WaitForIt(2.0f);
        Application.Quit();
    }

    private static void ActiveNextLevelEE(int id)
    {
        // Indicamos que se activado el Easter Egg por lo que activamos el del siguiente nivel.
        string path = $"Assets/StreamingAssets/Files/eeEffect.vvvvv";
        //string path = Path.Combine(Application.persistentDataPath, "Files/eeEffect.vvvvv");
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();

        sr.Close();

        string[] fileLines = file.Split('\r', '\n');
        for(int i=0;i<fileLines.Length;i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (sections[0] == Convert.ToString(id+1))
            {
                sections[1] = "on";
                fileLines[i] = string.Join(";", sections);
            }
        }

        // Borramos el contenido del archivo
        File.WriteAllText(path, string.Empty);

        string modifiedContent = string.Join("\n", fileLines.Where(line => !string.IsNullOrWhiteSpace(line)));

        // Escribimos las nuevas líneas en el archivo
        File.WriteAllText(path, modifiedContent);
    }

    static IEnumerator WaitForIt(float time)
    {
        yield return new WaitForSeconds(time);
    }

    
}
