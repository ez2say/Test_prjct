using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        gameObject.SetActive(false);
    }

    private void OnExitButtonClicked()
    {
        Debug.Log("Игра завершена!");
        Application.Quit();
    }
}