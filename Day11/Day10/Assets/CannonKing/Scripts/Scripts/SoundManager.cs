using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
   
    public Slider audioSlider;
   public void SetVolume()
    {
        float volume = audioSlider.value;
       
    }
}
