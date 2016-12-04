using UnityEngine;
using System.Collections;

public class ActivateObject : MonoBehaviour {

    private PuzzleController m_controller;
    public EndMessage m_activeObject;

	// Use this for initialization
	void Start () {
        m_controller = GetComponent<PuzzleController>();
	}
	
	// Update is called once per frame
	void Update () {
        if(m_controller.IsComplete())
        {
            m_activeObject.Activate();
        }
	}
}
