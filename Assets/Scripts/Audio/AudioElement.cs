using System;
using UnityEngine;

[Serializable] // note: "System.Serializable" allows for the fields to be set via Unity Inspector
public struct AudioElement
{
    /* Description:
    This struct is used in each AudioElementGameEvent (scriptable object event) to pass information to the
    AudioManager about which AudioType and AudioClip needs to be played.

    This struct is also used in the AudioElements scriptable object, which stores every AudioElement used in the game.
    */

    public AudioType audioType;
    public AudioClip audioClip;
}