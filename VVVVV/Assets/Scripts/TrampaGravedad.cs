using UnityEngine;
using UnityEngine.Tilemaps;

public class TrampaGravedad : MonoBehaviour
{
    public string InvertGravity;
    public Tilemap tilemap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el collider que ha entrado ha sido el del Jugador se activara un collider desactivado para que no pueda regresar y se invertira la gravedad
        if(collision.tag == "Player")
        {
            BoxCollider2D[] colliders = tilemap.GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D boxCollider2D in colliders)
            {
                if (!boxCollider2D.enabled)
                {
                    boxCollider2D.enabled = true;
                }
            }
            PlayerController.OnGravityTrap(InvertGravity);
        }
    }
}
