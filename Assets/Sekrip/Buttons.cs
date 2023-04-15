using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public string namascene;

    public void loadscene(string namascene)
    {
        SceneManager.LoadScene(namascene);
    }

    public void exit()
    {
        Application.Quit();
    }
}
