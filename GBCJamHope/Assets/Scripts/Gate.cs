using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {

    public GameObject m_gate;

    public void Activate()
    {
        m_gate.SetActive(false);
    }
}
