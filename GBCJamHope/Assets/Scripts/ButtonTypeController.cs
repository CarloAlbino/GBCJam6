using UnityEngine;
using System.Collections;

public enum Colour
{
    Red,
    Blue,
    Green,
    Length
};

public class ButtonTypeController : MonoBehaviour {

    // For both type of puzzle
    public Colour m_colour = Colour.Red;
    private Color m_colorValue;
    public Material[] m_availableMats;
    private Material m_colourMaterial;

    // For second type of puzzle
    public bool m_hasTargetColour = false;
    public Colour m_targetColour = Colour.Red;
    private bool m_isAtTargetColour = false;

    void Start()
    {
        SetColourValue();
    }

    public Color GetColour()
    {
        SetColourValue();

        return m_colorValue;
    }

    public Material GetMaterial()
    {
        SetColourValue();

        return m_colourMaterial;
    }

    public bool IsAtTargetColour()
    {
        if(m_colour == m_targetColour)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CycleColour()
    {
        int newColour = (int)m_colour;
        if(newColour < (int)Colour.Length)
        {
            newColour++;
            if(newColour == (int)Colour.Length)
            {
                newColour = 0;
            }
        }

        m_colour = (Colour)newColour;
    }

    public void SetColourValue()
    {
        switch (m_colour)
        {
            case Colour.Red:
                m_colorValue = Color.red;
                m_colourMaterial = m_availableMats[(int)m_colour];
                break;
            case Colour.Blue:
                m_colorValue = Color.blue;
                m_colourMaterial = m_availableMats[(int)m_colour];
                break;
            case Colour.Green:
                m_colorValue = Color.green;
                m_colourMaterial = m_availableMats[(int)m_colour];
                break;
            default:
                m_colorValue = Color.red;
                m_colourMaterial = m_availableMats[(int)Colour.Red];
                break;
        }
    }
}
