using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class amoguscheck : MonoBehaviour
{
    public GameObject vid;
    public VideoPlayer video;
    public string input;
    private string keyword = "amogus";
    public void ReadString(string s)
    {
        input = s;
        if(input.Equals(keyword, System.StringComparison.OrdinalIgnoreCase))
        {
            vid.SetActive(true);
            video.Play();
        }
        else
        {
            vid.SetActive(false);
            video.Stop();
        }
    }
}
