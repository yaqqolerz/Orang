using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public void loadscene(string namascene)
    {
        SceneManager.LoadScene(namascene);
    }

    public void exit()
    {
        Application.Quit();
    }
}
