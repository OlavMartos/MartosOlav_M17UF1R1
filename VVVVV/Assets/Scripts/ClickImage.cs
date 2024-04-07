using UnityEngine;

public class ClickImage : MonoBehaviour
{
    // Al hacer click en la imagen de Github se te abrira el enlace github
    public void OpenGithub() { Application.OpenURL("https://github.com/olavma");  }
    
    // Al hacer click en la imagen Linkedin se te abrira el enlace a linkedin
    public void OpenLinkedin() { Application.OpenURL("https://es.linkedin.com/in/olav-martos-ace%C3%B1a-3a50b9254"); }
}
