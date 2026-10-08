using UnityEngine;
using TMPro; 

public class ScorePopup : MonoBehaviour
{

    [SerializeField] private Canvas _parentCanvas; 
    [SerializeField] private GameObject _scorePrefab; 
    [SerializeField] private float _lifetime = 1f; 

    private void OnEnable() => EventHandler.OnScoreAmountDetermined += OnChangeScore; 
    private void OnDisable() => EventHandler.OnScoreAmountDetermined -= OnChangeScore; 

    private void OnChangeScore(int score)
    {
        GameObject scoreObject = Instantiate(_scorePrefab, _parentCanvas.transform); 
        TextMeshProUGUI scoreText = scoreObject.GetComponent<TextMeshProUGUI>(); 
        scoreText.text = "+" + score.ToString(); 
        Destroy(scoreObject, _lifetime); 
    }
}
