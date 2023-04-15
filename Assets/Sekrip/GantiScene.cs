using UnityEngine;
using UnityEngine.SceneManagement;

public class GantiScene : MonoBehaviour
{
    public string namascene;

    public void pencet(string namascene)
    {
        SceneManager.LoadScene(namascene);
    }
}
