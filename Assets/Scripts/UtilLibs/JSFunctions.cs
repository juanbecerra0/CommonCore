using UnityEngine;
using System.Runtime.InteropServices;

public class JSFunctions : MonoBehaviour
{
    // this is supplied by TTS.jslib in the plugins folder
    [DllImport("__Internal")]
    private static extern void Speak(string str);
    [DllImport("__Internal")]
    private static extern void Exit();

    public static void TextToSpeech(string line)
    {
        // the jslib only works while in the browser
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Speak(line);
        }
    }

    public static void ReturnToPreviousPage()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Exit();
        }
    }
}