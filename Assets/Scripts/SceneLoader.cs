using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] CoinManager coinManager;

    public string battleSceneName; // Имя сцены боя

    /*
     * @return LoadSceneCoroutine
     */
    public void LoadBattleSceneAsync()
    {
        coinManager.SaveToProgress();
        StartCoroutine(LoadSceneCoroutine());
    }

    /*
     * @param имя сцены
     * @return выбранная сцена
     */
    private IEnumerator LoadSceneCoroutine()
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

    /*
     * @param Time
     * @return остаётесть на сцене
     */
    public void StayScene()
    {
        float Time = 1f;

        Invoke(nameof(CloseMenu), Time);
    }

    /*
     * закрытие меню выбора сцены
     */
    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}