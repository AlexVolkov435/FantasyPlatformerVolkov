using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    public string battleSceneName;
    
    /*
     * @return LoadSceneCoroutine
     */
    public void LoadBattleSceneAsync()
    {
        StartCoroutine(LoadScene());
    }

    /*
     * @param имя сцены
     * @return выбранная сцена
     */
    private IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(battleSceneName);

        // Можно добавить загрузочный экран
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Загрузка: " + (progress * 100) + "%");
            yield return null;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("ExitGame");
    }
}