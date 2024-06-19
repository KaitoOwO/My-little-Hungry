using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioController;

    public void VolumenMaster(float slidervalue)
    {
        audioController.SetFloat("Master", Mathf.Log10(slidervalue) * 20);
    }
    public void VolumenMusica(float slidervalue)
    {
        audioController.SetFloat("Music", Mathf.Log10(slidervalue) * 20);
    }
    public void VolumenSfx(float slidervalue)
    {
        audioController.SetFloat("SFX", Mathf.Log10(slidervalue) * 20);
    }
}    
