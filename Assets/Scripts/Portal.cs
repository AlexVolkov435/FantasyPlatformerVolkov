using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform portal;

    private void Awake()
    {
        portal.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            portal.gameObject.SetActive(true);
        }
    }
}