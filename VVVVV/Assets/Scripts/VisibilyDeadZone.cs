using UnityEngine;

public class VisibilyDeadZone : MonoBehaviour
{
    public bool WannaSee;
    void Update()
    {
        // Si el bool es true, se activara el SpriteRenderer de todos los hijos del GameObject (Si no, desapareceran
        if (WannaSee)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                //transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        
    }
}
