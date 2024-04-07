using System.Collections;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    // Parameter of the shot launcher
    public GameObject shotPrefab;
    public Transform target;
    public float timeBetweenShots = 2.0f;
    public int numberOfShots;
    private Transform[] shots;

    private void Start()
    {
        // Añadimos el numero de disparos que puede hacer y que se vayan desactivando
        shots = new Transform[numberOfShots];
        for (int i = 0; i < numberOfShots; i++)
        {
            shots[i] = Instantiate(shotPrefab).transform;
            shots[i].gameObject.SetActive(false);
            shots[i].GetComponent<Shot>().target = target;
            shots[i].transform.SetParent(transform);
        }

        // Activamos la corutina
        StartCoroutine(ActiveLauncher());
    }

    private IEnumerator ActiveLauncher()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);

            // Find a disable shot and active it
            for (int i = 0; i < numberOfShots; i++)
            {
                if (!shots[i].gameObject.activeSelf)
                {
                    if (target.gameObject.activeSelf)
                    {
                        shots[i].gameObject.SetActive(true);
                        shots[i].GetComponent<Shot>().Disparar();
                    }
                    break;
                }
            }
        }
    }
}
