using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
   // public AudioMixer audioClip;
    public Slider soundSlider;

    // Start is called before the first frame update
   public void SetVoulume()
    {
        float volume = soundSlider.value;
        //audioClip.SetFloat("music", Mathf.Log10(volume) * 20);
    }
}
