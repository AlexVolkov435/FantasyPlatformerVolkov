using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform portal;

    private void Awake()
    {
        portal.gameObject.SetActive(false);
    }

    /*
     * При столкновении с триггеромм с компонентом Player открывается меню портала.
     * @param collision
     * @return portal.gameObject.SetActive(true);
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
            portal.gameObject.SetActive(true);
        }
    }
    
    public void LoadMenue()
    {
        SceneManager.LoadScene("Menu");
    }
}