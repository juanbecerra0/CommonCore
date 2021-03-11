using UnityEngine;
using System.Runtime.InteropServices;

public class TextToSpeech : MonoBehaviour
{
    // this is supplied by TTS.jslib in the plugins folder
    [DllImport("__Internal")]
    private static extern void Speak(string str);

    public static void Say(string line)
    {
        // the jslib only works while in the browser
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Speak(line);
        }
    }
}