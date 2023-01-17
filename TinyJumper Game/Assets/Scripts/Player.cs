using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 jumpForce;
    // do luc nhay khi user giu chuot
    public Vector2 jumpForceUp;
    public float minForceX;
    public float minForceY;
    public float maxForceX;
    public float maxForceY;

    [HideInInspector]
    // id của platform cuối cùng user chạm phải
    public int lastFlatformId;

    bool m_didJump;
    // ktra luc nhay cua nv da dc dua vao chua
    bool m_powerSetted;

    Rigidbody2D m_rb;
    Animator m_anim;
    float m_curPowerBarVal = 0;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // chi khi game bat dau thi ng dung moi set power duoc
        if (GameManager.Ins.IsGameStarted)
        {
            SetPower();
            // nhấn chuột xuống 0 ~ trái
            if (Input.GetMouseButtonDown(0))
            {
                SetPower(true);
            }
            // thoát chuột
            if (Input.GetMouseButtonUp(0))
            {
                SetPower(false);
            }
        }
    }

    void SetPower()
    {
        // khi đã set lực và chwua nhảy lên
        if(m_powerSetted && !m_didJump)
        {
            jumpForce.x += jumpForceUp.x * Time.deltaTime;
            jumpForce.y += jumpForceUp.y * Time.deltaTime;

            // giới hạn pos x và y
            jumpForce.x = Mathf.Clamp(jumpForce.x, minForceX, maxForceX);
            jumpForce.y = Mathf.Clamp(jumpForce.y, minForceY, maxForceY);

            m_curPowerBarVal += GameManager.Ins.powerBarUp * Time.deltaTime;

            GameGUIManager.Ins.UpdatePowerBar(m_curPowerBarVal, 1);

            
        }
    }

    public void SetPower(bool isHoldingMouse)
    {
        m_powerSetted = isHoldingMouse;

        if(!m_powerSetted && !m_didJump)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (!m_rb || jumpForce.x <= 0 || jumpForce.y <= 0) return;

        m_rb.velocity = jumpForce;

        m_didJump = true;

        // check nếu biến didJump  = true thì sẽ chuyển anim trong animator của nhân vật
        if (m_anim)
        {
            m_anim.SetBool("didJump", true);
        }
        AudioController.Ins.PlaySound(AudioController.Ins.jump);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagConst.GROUND)){
            // do ground là con và nằm trên của platform nên khi bắt va chạm ta bắt với ground, và get ra cái root tức là cha của nó          
            Platform p = col.transform.root.GetComponent<Platform>();

            if (m_didJump)
            {
                m_didJump = false;

                if (m_anim)
                {
                    m_anim.SetBool("didJump", false);
                }

                if (m_rb)
                {
                    m_rb.velocity = Vector2.zero;
                }
                jumpForce = Vector2.zero;

                m_curPowerBarVal = 0;
                GameGUIManager.Ins.UpdatePowerBar(m_curPowerBarVal, 1);

                // kiểm tra xem nhân vật đã nhảy lên platform mới chưa. nếu có thì cập nhật lại lastformID
                if (p && p.id != lastFlatformId)
                {
                    GameManager.Ins.CreatePlatformAndLerp(transform.position.x);
                    GameManager.Ins.AddScore();
                    lastFlatformId = p.id;

                    
                }
            }
        }

        if (col.CompareTag(TagConst.DEATH_ZONE))
        {
            GameGUIManager.Ins.ShowGameoverDialog();

            AudioController.Ins.PlaySound(AudioController.Ins.gameover);
            Destroy(gameObject);
        }
    }
}
