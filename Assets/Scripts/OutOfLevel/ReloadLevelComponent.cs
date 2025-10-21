using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevelComponent : MonoBehaviour
{
    private string _startNameScene;
    
    /*
     * переход на текущую сцену
     */
    public void Reload()
    {
        _startNameScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(_startNameScene);
    }
}