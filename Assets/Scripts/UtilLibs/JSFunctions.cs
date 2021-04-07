using UnityEngine;
using System.Runtime.InteropServices;

public class JSFunctions : MonoBehaviour
{
    // .jslib functions

    [DllImport("__Internal")]
    private static extern void Speak(string str);

    [DllImport("__Internal")]
    private static extern void Exit();

    [DllImport("__Internal")]
    private static extern void CloseTab();

    // Static function API

    public static void TextToSpeech(string line)
    {
        // the jslib only works while in the browser
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            Speak(line);
        else
            Debug.LogError("TextToSpeech can only be used in WebGLPlayer");
    }

    public static void ReturnToPreviousPage()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            Exit();
        else
            Debug.LogError("ReturnToPreviousPage can only be used in WebGLPlayer");
    }

    public static void CloseCurrentPage()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Application.Quit();
            CloseTab();
        }
        else
            Debug.LogError("CloseCurrentPage can only be used in WebGLPlayer");
    }
}