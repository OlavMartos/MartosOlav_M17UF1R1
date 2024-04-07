using System;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Desactivamos o activamos un collider si se cumple una determinada condicion
/// </summary>
public class DisableCollider : MonoBehaviour
{
    public int EasterId;
    private Tilemap tilemap;
    private bool actived = false;
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.name == "Character" && actived == false)
        {
            string path = $"Assets/StreamingAssets/Files/eeEffect.vvvvv";
            //string path = Path.Combine(Application.persistentDataPath, "Files/eeEffect.vvvvv");
            StreamReader sr = File.OpenText(path);
            string fileReaded = sr.ReadToEnd();

            string[] fileSplitted = fileReaded.Split(new char[] { '\r', '\n' });

            foreach (string line in fileSplitted)
            {
                string[] secciones = line.Split(new char[] { ';' });
                if (secciones[0] == Convert.ToString(EasterId))
                {
                    if (secciones[1] == "off")
                    {
                        tilemap = ActiveORNot();
                        if(tilemap != null)
                        {
                            TilemapRenderer tmRen = tilemap.GetComponent<TilemapRenderer>();
                            BoxCollider2D[] colliders = tilemap.GetComponentsInChildren<BoxCollider2D>();
                            if (EasterId == 1) EE1(tmRen, colliders);
                            if (EasterId == 2) EE2(tmRen, colliders);
                            if (EasterId == 4) EE4(tmRen, colliders);
                            if (EasterId == 5) EE5(tmRen, colliders);
                        }
                    }
                    else
                    {
                        tilemap = ActiveORNot();
                        if(tilemap != null)
                        {
                            TilemapRenderer tmRen = tilemap.GetComponent<TilemapRenderer>();
                            BoxCollider2D[] colliders = tilemap.GetComponentsInChildren<BoxCollider2D>();
                            if(EasterId == 3) EE3(tmRen, colliders);
                        }
                    }
                }
            }
        }
    }

    void EE1(TilemapRenderer tmRen, BoxCollider2D[] colliders)
    {
        tmRen.enabled = false;
        foreach (BoxCollider2D boxCollider2D in colliders)
        {
            if (boxCollider2D.enabled)
            {
                boxCollider2D.enabled = false;
                actived = true;
                SpriteRenderer sR = tilemap.GetComponentInChildren<SpriteRenderer>();
                if (sR != null)
                {
                    sR.enabled = false;
                    tilemap.GetComponentInChildren<CircleCollider2D>().enabled = false;
                }
            }
        }
        
    }

    void EE2(TilemapRenderer tmRen, BoxCollider2D[] colliders)
    {
        foreach (BoxCollider2D collider in colliders)
        {
            if (!collider.enabled) { collider.enabled = true; }
        }
    }

    void EE3(TilemapRenderer tmRen, BoxCollider2D[] colliders)
    {
        colliders[1].enabled = false;
    }
    void EE4(TilemapRenderer tmRen, BoxCollider2D[] colliders)
    {
        foreach(BoxCollider2D col in colliders) 
        {
            if (!col.enabled) { col.enabled = true; }
        }
    }

    void EE5(TilemapRenderer tmRen, BoxCollider2D[] colliders)
    {
        foreach(BoxCollider2D col in colliders)
        {
            if(!col.enabled) { col.enabled = true; }
        }
    }

    Tilemap ActiveORNot()
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
}
