using UnityEngine;
using System.Collections;
using TMPro;

public class DamageRendering : MonoBehaviour
{
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private Vector2 textOffset;
    [SerializeField] private float damageDisplayTime = 1f;
    
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private float _timeLiveText = 0.1f;

    private void Awake()
    {
        Initialization();
    }

    /*
     * Инициализация переменных
     */
    private void Initialization()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (_spriteRenderer != null)
        {
            _originalColor = _spriteRenderer.color;
        }
    }
    
    /*
    * Метод получения урона.
    * @param damage,damageTextPrefab,spawnPosition
    * @return изображения количество нанесенного урона 
    */
    public void ShowDamageText(float damage)
    {
        if (damageTextPrefab != null)
        {
            Vector3 spawnPosition = transform.position + (Vector3)textOffset;
            GameObject damageText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity);
            
            TextMeshPro textMesh = damageText.GetComponent<TextMeshPro>();
            
            if (textMesh != null)
            {
                textMesh.text = "-" + damage.ToString();
                textMesh.color = Color.red;
                
                // Анимация всплывающего текста  
                LeanTween.moveY(damageText, spawnPosition.y + 1f, damageDisplayTime)
                    .setEase(LeanTweenType.easeOutQuad);
                
                LeanTween.alphaText(damageText.GetComponent<RectTransform>(), 0f, damageDisplayTime)
                    .setDestroyOnComplete(true);
            }
        }
        
        StartCoroutine(DamageEffect());
    }
    
    /*
     * IEnumerator для мигания игрока после нанесения урона  
     */
    private IEnumerator DamageEffect()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(_timeLiveText);
            _spriteRenderer.color = _originalColor;
        }
    }
}
