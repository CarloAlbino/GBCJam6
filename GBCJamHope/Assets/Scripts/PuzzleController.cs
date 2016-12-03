using UnityEngine;
using System.Collections;

public class PuzzleController : MonoBehaviour {
    [System.Serializable]
    public struct AffectedButtons
    {
        public int[] buttons;
    }
    public LightButton[] m_buttons;
    public AffectedButtons[] m_canTurnOff;
    public Transform m_puzzleCam;
    private bool m_isPuzzleComplete = false;
    private BoxCollider m_collider;
    public float m_maxDistance = 5.0f;
    public bool m_isActive = false;
    public PuzzleController m_nextPuzzle;
    public Material m_activeMaterial;
    public MeshRenderer m_panelRenederer;

    void Start()
    {
        m_collider = GetComponent<BoxCollider>();
    }

	void Update () {
        if(!m_isPuzzleComplete)
        {
            CheckForPuzzleCompletion();
            if(m_isPuzzleComplete)
            {
                for(int i = 0; i < m_buttons.Length; i++)
                {
                    m_buttons[i].DisableButton();
                }
            }
        }
	}

    private void CheckForPuzzleCompletion()
    {
        m_isPuzzleComplete = true;

        for(int i = 0; i < m_buttons.Length; i++)
        {
            if(!m_buttons[i].IsLightOn())
            {
                m_isPuzzleComplete = false;
                break;
            }
        }

        if (m_isPuzzleComplete)
        {
            if (m_nextPuzzle)
            {
                m_nextPuzzle.m_isActive = true;
                m_nextPuzzle.m_panelRenederer.material = m_nextPuzzle.m_activeMaterial;
            }
        }
    }

    public bool IsComplete()
    {
        return m_isPuzzleComplete;
    }

    public void DisableCollider()
    {
        if (m_isActive)
        {
            m_collider.size = Vector3.one;
        }
    }

    public void EnableCollider()
    {
        m_collider.size = new Vector3(1, 3, 1);
    }

    // This is really hack-y
    public void AffectOtherButtons(int buttonNum)
    {
        if (m_canTurnOff.Length > 0)
        {
            for (int i = 0; i < m_canTurnOff[buttonNum].buttons.Length; i++)
            {
                m_buttons[m_canTurnOff[buttonNum].buttons[i]].TurnOffButton();
            }
        }
    }


}
