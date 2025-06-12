using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_End : UI
{
    [SerializeField] private Button restartButton;

    protected override void Awake()
    {
        base.Awake();

        restartButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
        });
    }
}
