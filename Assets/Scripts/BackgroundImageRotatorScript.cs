using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//  Rotates through an array of images for the background on a timer
public class BackgroundImageRotatorScript : MonoBehaviour
{
    public Texture[] m_BackgroundImages;
    public float m_TimePerImage;

    private float m_Timer;
    private int m_CurrentImage;
    private RawImage m_CurrentBackground;

    private void Start()
    {
        m_Timer = 0;
        m_CurrentImage = 0;

        m_CurrentBackground = this.GetComponent<RawImage>();
        m_CurrentBackground.texture = m_BackgroundImages[m_CurrentImage];
    }

  
    void Update()
    {
        m_Timer += Time.deltaTime;

        //  If the time for this image has elapsed, swap images and reset the timer
        if(m_Timer >= m_TimePerImage)
        {
            SwapImage();
            m_Timer = 0;
        }
    }


    //  Set and apply the new image as the background texture
    private void SwapImage()
    {
        if(m_CurrentImage + 1 == m_BackgroundImages.Length)
        {
            m_CurrentImage = 0;
        }
        else
        {
            m_CurrentImage++;
        }

        m_CurrentBackground.texture = m_BackgroundImages[m_CurrentImage];
    } 
}
