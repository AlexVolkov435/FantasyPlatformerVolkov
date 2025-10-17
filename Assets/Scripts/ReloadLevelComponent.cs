using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevelComponent : MonoBehaviour
{
    private string _startNameScene = "Base";
    
    public void Reload()
    {
        SceneManager.LoadScene(_startNameScene);
    }
}