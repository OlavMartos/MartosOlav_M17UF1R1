using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Parameters of the Enemy who can move from a limited zone
    public float distanceMovement;
    public float speed;
    public bool isDead;
    private Vector3 initialPosition;
    private int direction = 1;
    private Animator _animator;

    private void Start()
    {
        initialPosition = transform.position;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Hacemos que se mueva por la zona y si llega al final que gire
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
        if(Vector3.Distance(transform.position, initialPosition) >= distanceMovement / 2 ) { direction *= -1; }

        // Activamos la animacion de muerte
        if(isDead) { _animator.SetBool("Dead", true); }
    }
}
