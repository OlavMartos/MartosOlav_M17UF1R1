using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Private and public parameters
    private static Animator _animator;
    private static Rigidbody2D _rb;
    private static SpriteRenderer _sr;
    private KeyCode[] konamiSequence = {
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.B,
        KeyCode.A,
        KeyCode.Return
    };
    private int currentIndex = 0;
    public static PlayerController player;
    public float Speed;
    public GameObject canvas;
    private static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _sr = gameObject.GetComponent<SpriteRenderer>();
        player = gameObject.GetComponent<PlayerController>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move to right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _sr.flipX = false;
            //_rb.velocity = new Vector2(1, 0) * Speed; // La velocidad comenzaba en 12 e iba disminuyendo
            transform.position += transform.right * Speed * Time.deltaTime;
            _animator.SetBool("isRunning", true);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _rb.velocity = Vector2.zero; // Comenta esta linea si quieres que patine el personaje
            _animator.SetBool("isRunning", false);
        }

        // Move to left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _sr.flipX = true;
            //_rb.velocity = new Vector2(-1, 0)*Speed;
            transform.position += transform.right * -Speed * Time.deltaTime;
            _animator.SetBool("isRunning", true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _rb.velocity = Vector2.zero;
            _animator.SetBool("isRunning", false);
        }

        // Turn off the ingravity
        if (Input.GetKey(KeyCode.Space) && _animator.GetBool("changeGravity") == true && _rb.velocity.y == 0.00f)
        {
            _animator.SetBool("changeGravity", false);
        }

        // Turn on the ingravity
        if (Input.GetKeyDown(KeyCode.Space) && _animator.GetBool("changeGravity") == false && _rb.velocity.y == 0.00f)
        {
            _animator.SetBool("changeGravity", true); 
            _rb.gravityScale *= -1;
            _sr.flipY = !_sr.flipY;
            audioSource.Play();
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseMenu();
        }

        Konami();
    }

    void Konami()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(konamiSequence[currentIndex]))
            {
                currentIndex++;
                if (currentIndex == konamiSequence.Length)
                {
                    // Código Konami ingresado correctamente, ejecuta tus acciones aquí
                    Debug.Log("Código Konami ingresado correctamente");
                    currentIndex = 0;
                    Secret.EERestart();
                }
            }
            // Restart the Keys if the key in is incorrect
            else currentIndex = 0; 
        }
    }

    public static void OnGravityTrap(string InvertGravity)
    {
        if (InvertGravity == "ingravity")
        {
            _animator.SetBool("changeGravity", true);
            _rb.gravityScale *= -1; 
            audioSource.Play();
        }
        else 
        {
            audioSource.Play();
            _animator.SetBool("changeGravity", false); 
        }

        _sr.flipY = !_sr.flipY;
    }

    public static void Trap() { _rb.gravityScale = 0; }

    void PauseMenu()
    {
        Time.timeScale = 0;
        canvas.SetActive(true);
    }
}
