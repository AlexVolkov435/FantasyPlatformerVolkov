using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevelComponent : MonoBehaviour
{
    private string _startNameScene;
    
    public void Reload()
    {
        _startNameScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(_startNameScene);
    }
}