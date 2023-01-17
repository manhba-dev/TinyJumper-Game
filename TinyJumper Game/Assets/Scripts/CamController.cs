using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // time di chuyen cua cam (Lerp)
    public float lerpTime;
    public float xOffset;

    bool m_canlerp;
    float m_learpDir;

    private void Update()
    {
        if (m_canlerp)
            MoveLerp();
    }

    void MoveLerp()
    {
        float xPos = transform.position.x;
        //update theo time
        // (pos, độ dài cần di chuyển, time đến được điểm đó update từ từ)
        xPos = Mathf.Lerp(xPos,m_learpDir, lerpTime * Time.deltaTime);
        transform.position = new Vector3 (xPos,transform.position.y,transform.position.z);

        // xOffset nếu cam cách điểm cần đến xOffset thì sẽ dừng cam lại 
        if(transform.position.x >= (m_learpDir - xOffset))
        {
            m_canlerp = false;
        }

    }

    // dist khoảng cách cần di chuyển đến
    public void LerpTrigger(float dist)
    {
        m_canlerp = true;
        m_learpDir = dist;
    }
}
