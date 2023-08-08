using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
   [SerializeField] private Slider volumnSlider;
    public void SetVolumn()
    {
        float volumn = volumnSlider.value;
    }
}
