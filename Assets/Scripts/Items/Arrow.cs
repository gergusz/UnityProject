using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField] private float aliveTime = 5f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveAndEnabled)
        {
            aliveTime -= Time.deltaTime;
            if (aliveTime < 0)
            {
                gameObject.SetActive(false);
                aliveTime = 5f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
