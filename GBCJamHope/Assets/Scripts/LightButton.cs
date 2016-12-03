using UnityEngine;
using System.Collections;

public class LightButton : MonoBehaviour {

    public int m_buttonNumber;
    private PuzzleController m_controller;
    private Material m_offMaterial;
    private ButtonTypeController m_button;
    private MeshRenderer m_renderer;
    //private Light m_light;
    //private Animator m_animator;

    private bool m_canBePressed = true;
    private bool m_isOn = false;

	void Start () 
    {
        m_controller = GetComponentInParent<PuzzleController>();
        m_button = GetComponent<ButtonTypeController>();
        m_renderer = GetComponent<MeshRenderer>();
        //m_light = GetComponent<Light>();
        //m_animator = GetComponent<Animator>();
        m_offMaterial = m_renderer.material;
        //m_light.intensity = 0;
        m_isOn = false;
	}

    public void PressButton()
    {
        if (m_canBePressed)
        {
            if (m_button.m_hasTargetColour)
            {
                m_button.CycleColour();
                m_renderer.material = m_button.GetMaterial();
                //m_light.color = m_button.GetColour();
            }
            else
            {
                m_renderer.material = m_button.GetMaterial();
                //m_light.color = m_button.GetColour();
            }
            //m_light.intensity = 1;
            m_isOn = true;
            m_controller.AffectOtherButtons(m_buttonNumber);
            // Play animation here
        }
    }

    public void TurnOffButton()
    {
        if (m_canBePressed)
        {
            m_renderer.material = m_offMaterial;
            //m_light.intensity = 0;
            m_isOn = false;
        }
    }

    public void DisableButton()
    {
        m_canBePressed = false;
    }

    public bool IsLightOn()
    {
        if (m_button.m_hasTargetColour)
        {
            if (m_button.IsAtTargetColour())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return m_isOn;
        }
    }
}
