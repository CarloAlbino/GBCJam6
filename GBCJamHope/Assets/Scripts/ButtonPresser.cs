using UnityEngine;
using System.Collections;

public class ButtonPresser : MonoBehaviour {

    public float m_camSpeed = 2.0f;
    private Camera m_camera;
    private Vector3 m_originalCamPosition;
    private Quaternion m_originalCamRotation;
    private Transform m_nextCamTransform;
    private bool m_puzzleMode;
    private bool m_movingCamera;
    private PuzzleController m_currentPuzzle = null;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController m_player;

	void Start () 
    {
        m_camera = GetComponent<Camera>();
        m_originalCamPosition = m_camera.transform.position;
        m_originalCamRotation = m_camera.transform.rotation;
        m_player = GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
	}

    void Update()
    {
        if (m_movingCamera)
        {
            if (m_puzzleMode)
            {
                m_camera.transform.position = Vector3.Lerp(transform.position, m_nextCamTransform.position, m_camSpeed * Time.deltaTime);
                m_camera.transform.rotation = Quaternion.Lerp(transform.rotation, m_nextCamTransform.rotation, m_camSpeed * 10 * Time.deltaTime);
                if (Vector3.Distance(m_camera.transform.position, m_nextCamTransform.position) < 0.3f && Quaternion.Angle(m_camera.transform.rotation, m_nextCamTransform.rotation) < 1.0f)
                {
                    //Debug.Log("DoneA");
                    m_movingCamera = false;
                }
            }
            else
            {
                m_camera.transform.position = Vector3.Lerp(transform.position, m_originalCamPosition, m_camSpeed * Time.deltaTime);
                m_camera.transform.rotation = Quaternion.Lerp(transform.rotation, m_originalCamRotation, m_camSpeed * 10 * Time.deltaTime);
                if (Vector3.Distance(m_camera.transform.position, m_originalCamPosition) < 0.3f && Quaternion.Angle(m_camera.transform.rotation, m_originalCamRotation) < 1.0f)
                {
                    //Debug.Log("DoneB");
                    m_movingCamera = false;
                    m_player.ToggleMouseFollow(true);
                }
            }
        }

        if(m_currentPuzzle != null)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1) || m_currentPuzzle.IsComplete())
            {
                m_puzzleMode = false;
                m_movingCamera = true;
                m_currentPuzzle.EnableCollider();
                m_player.SetSpeed(5);
                m_player.SetCamLookSpeed(2);
                m_currentPuzzle = null;
            }
        }
   // }

    //void FixedUpdate()
    //{
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.DrawRay(ray.origin, m_camera.transform.forward * 100.0f);
                Transform other = hit.transform;

                if (other.GetComponent<LightButton>())
                {
                    LightButton button = other.GetComponent<LightButton>();
                    if (!button.IsLightOn())
                    {
                        button.PressButton();
                    }
                    else
                    {
                        button.TurnOffButton();
                    }
                }
                else if(other.GetComponent<PuzzleController>())
                {
                    // else if puzzle panel
                    // turn of fps control
                    // maybe move camera to focus on puzzle
                    // The Witness style
                    m_currentPuzzle = other.GetComponent<PuzzleController>();
                    // If within range
                    if (Vector3.Distance(transform.position, m_currentPuzzle.transform.position) < m_currentPuzzle.m_maxDistance)
                    {
                        m_originalCamPosition = m_camera.transform.position;
                        m_originalCamRotation = m_camera.transform.rotation;
                        m_nextCamTransform = m_currentPuzzle.m_puzzleCam.transform;
                        m_currentPuzzle.DisableCollider();
                        m_player.SetSpeed(0);
                        m_player.SetCamLookSpeed(0);
                        m_player.ToggleMouseFollow(false);
                        m_puzzleMode = true;
                        m_movingCamera = true;
                    }
                }
            }
        }
    }
}
