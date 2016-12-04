using UnityEngine;
using System.Collections;

public class EndMessage : MonoBehaviour {

    public GameObject m_endMessage;

    public void Activate()
    {
        m_endMessage.SetActive(true);
    }


    private IEnumerator EndTimer()
    {
        yield return new WaitForSeconds(60);
        Application.Quit();
    }
}
