using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSystem : MonoBehaviour
{

    private AudioSource source;

    public Image buttonMute;

    public Sprite mutedImg;
    public Sprite unmutedImg;

    private void Awake()
    {
        source = GameObject.Find("GameManager").GetComponent<AudioSource>();

        if(source.isActiveAndEnabled)
            buttonMute.overrideSprite = unmutedImg;
        else
            buttonMute.overrideSprite = mutedImg;
    }



    public void MuteAllSounds()
    {
        if(source.isActiveAndEnabled)
        {
            source.enabled = false;
            buttonMute.overrideSprite = mutedImg;
        }
        else
        {
            source.enabled = true;
            buttonMute.overrideSprite = unmutedImg;
        }

        
    }


}
