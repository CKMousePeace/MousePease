using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlatform : MonoBehaviour
{
    [Header("����׿� ���� ��.")]
    [SerializeField] private bool m_DebugLine = true;

    [Header("��ǥ �Է�")]
    [SerializeField] private Vector3[] m_RelativeMovePoint;

    [Header("���� ���۰� ���ÿ� �����°�?")]
    [SerializeField] private bool m_AwakeStart = true;

    [Header("���� ���� ó�� �����̱���� ��� �ð�")]
    [SerializeField] private float m_AwakeWaitTime = 0;

    [Header("�̵� �ӵ�")]
    [SerializeField] private float m_Speed = 3;

    [Header("������ ��� �ð�")]
    [SerializeField] private float m_WaitTime = 0;

    [Header("���� �������� �����ϰ� �����Ǳ������ ��� �ð�")]
    [SerializeField] private float m_DestroyWaitTime = 1.0f;

    [Header("���� ���� ����")]
    [SerializeField] private bool m_PlatformDes = false;

    [Header("���� �÷��̾�� ø�� �� ���� �ð�")]
    [SerializeField] private float m_DestroyTime = 1.0f;

    [Header("����� �ð�")]
    [SerializeField] private float m_RespawnTime = 1.0f;

    //[Header("���� ���� ����")]
    //private bool m_JumpInertia = false;

    private float m_JumpInertiaValue;                                            //jumpInertia�� true�� �� �����¿� �������� ��

    private int m_Cur = 1;                                                       //Current route number                         ���� ������ ��� ��ȣ
    private bool m_Back = false;                                                 //Make sure you have arrived at your current destination and are returning ���� �������� �����ϰ� ���ư��� �ִ��� Ȯ��
    //private bool m_movingOn = false;                                             //check moving                             ���� �����̰� �ִ���. m_awakeStart�� false �� �ʿ�

    private bool m_playerCheck = false;                                          //Checking player is on the floor     �÷��̾� ĳ���Ͱ� ž���ߴ���
    private GameObject m_player;                                                 //correct the player component        �÷��̾� ĳ���͸� ã�� ������Ʈ���� ����
    private CJump m_playerScript;
    public GameObject Player;

    Vector3[] m_Pos;                                                             //m_relativeMovePoint���� ���� ��ȯ�� ���� ������ǥ�� ������ �ִ� �迭

    Vector3 m_firstPos = Vector3.zero;                                           //for OnDrawGizmos ���� ���� ���¸� �ľ��ϰ� �ʱ� ��ǥ�� ����

    void Awake()
    {
        if (m_RelativeMovePoint.Length <= 0)                                     //if there is no component, just delet it.       ������Ʈ ������ ����.
        {
            Destroy(this);
            return;
        }

        m_player = GameObject.Find("Jump");                                      //find play character.
        if (m_player)
        {
            m_playerScript = m_player.GetComponent<CJump>();
        }

        m_Pos = new Vector3[m_RelativeMovePoint.Length + 1];                                    //The reason we do +1 is because we have to store up to the first coordinates. +1? ���� ��ǥ ����
        m_Pos[0] = m_firstPos = transform.position;                                             //Save the first coordinates. firstm_Pos is used for OnDrawGizmos. ���� ��ǥ ����, m_firstPos�� OnDrawGizmos�� ���
        for (int i = 1; i < m_RelativeMovePoint.Length + 1; i++)                                //0 has already been filled, fill in from 1.                  0���� �̹� ä�����Ƿ� 1������ ä���
        {
            m_Pos[i] = m_Pos[i - 1] + transform.TransformDirection(m_RelativeMovePoint[i - 1]); //������Ʈ�� ������ ����, �����ǥ�� ���� ���� ���� ��ǥ�� ����.
        }
        if (m_AwakeStart)                                                                       //If m_awakeStart is true, it will start moving immediately.     m_awakeStart�� true��� �ٷ� �����̰� �Ѵ�.
        {
            //m_movingOn = true;
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        WaitForFixedUpdate delay = new WaitForFixedUpdate();
        //����ȭ

        if (m_AwakeStart && m_AwakeWaitTime != 0)               //Used to make other moving objects move at different speeds. �ٸ� �����̴� ������Ʈ��� �ٸ� �ӵ��� �����̰� ���鶧 ���
            yield return new WaitForSeconds(m_AwakeWaitTime);
        while (true)
        {
            if (transform.position == m_Pos[m_Cur])             //m_Pos[m_cur]�迭 ��� ������ �����ϸ� ����
            {
                if (!m_Back)
                {
                    if (++m_Cur == m_Pos.Length)
                    {
                        if (!m_AwakeStart)
                        {
                            Invoke("DestroyWait", m_DestroyWaitTime);
                            this.enabled = false;
                            yield break;
                        }
                        else 
                        {
                            m_Back = true;
                            m_Cur = m_Cur - 2;
                        }
                    }
                }
                else
                {
                    if (--m_Cur == -1)    //It searches the previous path, and if there is no value, it means to return to the beginning.     ���� ��� Ž��. ���� ����? ó������ ���ƿ�.
                    {
                        m_Back = false;   //Make m_back false again to make it move in the forward direction.                                 �ٽ� m_back�� false�� ����� ���������� �̵��ϰ� �����.
                        m_Cur = m_Cur + 2;
                    }
                }
                if (m_WaitTime != 0)
                    yield return new WaitForSeconds(m_WaitTime);  //If there is a waiting time at the stopover, wait that much. ������ ���ð��� �ִٸ� �׸�ŭ ��ٸ���
                else
                    yield return delay;                           //If not, it goes directly to the next frame. ������ �ٷ� ���� ���������� �ѱ��.
            }
            else                                                  //The platform is moving unless it arrives at a stopover. �������� �����Ѱ� �ƴ϶�� �̵� ���̴�.
            {
                //Vector3 prevPos = transform.position;  //�̵� ���� ��ǥ�� �ӽ÷� �����Ѵ�.

                transform.position = Vector3.MoveTowards(transform.position, m_Pos[m_Cur], m_Speed * Time.deltaTime); //move towards the destination. �������� ���� �̵��Ѵ�.
                if (m_playerCheck)
                {
                    //if (m_playerScript.m_isJump == false)  //�÷��̾ ���� ������ �ƴ� ����� Ÿ�� �ִٸ�
                    ////{
                    //Player.transform.position = (gameObject.transform.position - prevPos);    //�̵� �� ��ǥ���� �̵� ���� ��ǥ�� ����ŭ �÷��̾� ĳ���͸� �̵���Ų��.
                    ////Player.transform.position = (new Vector3(0, -2.0f * Time.deltaTime, 0));


                    //if (m_JumpInertia == true)
                    //        m_JumpInertiaValue = ((m_Pos[m_Cur] - transform.position).normalized * m_Speed).y;  //deltatime�� ������ ���� �״���� �ӵ����� y���� ���Ѵ�.
                    ////}
                }
                yield return delay;
            }
        }
    }

    //�����̴� ��θ� ���信 �׷��ش�.
    void OnDrawGizmos()
    {
        if (!m_DebugLine || m_RelativeMovePoint.Length <= 0)                        //m_DebugLine != Dont Draw. m_DebugLine���������� �׸��� �ʴ´�.
            return;
        Vector3 t1, t2; //�ӽ� ��ǥ
        if (m_firstPos == Vector3.zero)                                             //firstm_Pos�� Awake���� ���� ������Ʈ�� ��ǥ�� �ʱ�ȭ�ȴ�. ��, Vector3.zero�� ������ ���۵��� �ʾҴٴ� �Ҹ�
            t1 = t2 = transform.position;                                           //������ ���۵Ǳ� ���̶�� �ڽ��� ���� ��ǥ�� �ӽð��� �ִ´�.
        else
            t1 = t2 = m_firstPos;                                                   //������ ���۵Ǿ��ٸ� firstm_Pos�� ����ִ� �ʱ� ��ġ���� �ӽð��� �ִ´�.
                                                                                    //�̷��� ������ ������Ʈ�� �������� ��� ���� ��θ� �׷��ֱ� ���Կ� �ִ�.
        for (int i = 0; i < m_RelativeMovePoint.Length; i++)                        //�Էµ� ����� ������ŭ �ݺ�.
        {
            t2 += transform.TransformDirection(m_RelativeMovePoint[i]);             //�ι�° �ӽ� ��ǥ�� �Էµ� ��� ����� ����ŭ �����༭ ������ ��ǥ�� �־��ش�.
            if (0 < i)                                                              //ù��° �ӽ� ��ǥ�� �ι�° �ӽ� ��ǥ�� ������ǥ�� ���� �Ѵ�.
                t1 += transform.TransformDirection(m_RelativeMovePoint[i - 1]);     //��, 0��°�� ��� i-1 �迭�� �����Ƿ� �����Ѵ�.
            Debug.DrawLine(t1, t2, Color.red);                                      //�� ��ǥ�� ��� �׷��ָ� ��� ��ΰ� ���信 �׷�����.
        }
    }


    private void OnCollisionEnter(Collision Col)
    {
        if (Col.collider.CompareTag("Player"))
        {
            m_playerCheck = true;                           //Player is on the platform �÷��̾ ž��
            //Player.transform.SetParent(transform,false);
           // Player.transform.parent = transform;            //Fixed the character to move when stepping on the footrest using Parent Parent �̿��Ͽ� ���� ����� �� ĳ���� �̵��ϰ� ����

            if (m_PlatformDes == true)                      //if the platform destination is enabled... then start coroutine. m_PlatformDes �� Ȱ��ȭ �Ǿ������� ���� �ڷ�ƾ ����
            {
                StartCoroutine(DestroyWaitForHold());
            }
        }
    }

    private void OnCollisionExit(Collision Col)
    {
        if (Col.collider.CompareTag("Player"))
        {
            m_playerCheck = false;
            //Player.transform.SetParent(null, false);
            //Player.transform.parent = null;         //Fixed the character to move when stepping on the footrest using Parent Parent �̿��Ͽ� ���� ����� �� ĳ���� �̵��ϰ� ����

            //if (m_JumpInertia)                    //���� ������ �����ִٸ� ������ ���ص� ���� �����Ѵ�. ������ ������� �ʴ´�.
        }
    }




    private void DestroyWait()                //A function that is called when it needs to disappear after reaching the final destination from above. ������ ������������ �����Ͽ� ������� �� �� ȣ��Ǵ� �Լ�
    {
        
        this.gameObject.SetActive(false);     //It needs to be reused, so just disable it.  ���� �ؾ��ϹǷ� ��Ȱ��ȭ�� �����ش�.
        Invoke("Respawn", m_RespawnTime);     //Wait as much as the regeneration waiting time with the Invoke function.  ����� ���ð���ŭ Invoke�Լ��� ����Ų��.

    }

    IEnumerator DestroyWaitForHold()          //������ ������������ �����Ͽ� ������� �� �� ȣ��Ǵ� �Լ�
    {
        yield return new WaitForSeconds(m_DestroyTime);
       // Player.transform.parent = null;
        this.gameObject.SetActive(false);     //��Ȱ��ȭ�� �����ش�.

        Invoke("Respawn", m_RespawnTime);     //����� ���ð���ŭ Invoke�Լ��� ����Ų��.

    }




    private void Respawn()                     //Rebuild initialization. Initialize all values and re-enable them.   ����� �ʱ�ȭ. ��� ���� �ʱ�ȭ, �ٽ� Ȱ��ȭ ��Ų��.
    {
        m_Cur = 1;
        transform.position = m_firstPos;
        m_Back = false;
        //m_movingOn = false;
        m_playerCheck = false;
        this.enabled = true;
        this.gameObject.SetActive(true);
        StartCoroutine(Move());
    }
}