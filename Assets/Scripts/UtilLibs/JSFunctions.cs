using UnityEngine;
using System.Runtime.InteropServices;

public class JSFunctions : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Speak(string str);

    [DllImport("__Internal")]
    private static extern void Exit();

    [DllImport("__Internal")]
    private static extern void CloseTab();

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
            CloseTab();
        else
            Debug.LogError("CloseCurrentPage can only be used in WebGLPlayer");
    }
}