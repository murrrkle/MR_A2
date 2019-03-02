using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicManager : MonoBehaviour
{
    public float LevelMax;
    private AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        LevelMax = 0;
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.clip = Microphone.Start(null, true, 1, 44100);
        while (!(Microphone.GetPosition(null) > 0)) { }
        m_AudioSource.Play();

    }

    // Update is called once per frame
    float GetMax()
    {

        float levelMax = 0;
        int window = 128;
        float[] waveData = new float[window];
        int micPosition = Microphone.GetPosition(null) - (window + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        m_AudioSource.clip.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < window; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        return levelMax;
    }

    private void Update()
    {
        LevelMax = GetMax();
    }

}
