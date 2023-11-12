using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl: MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
}