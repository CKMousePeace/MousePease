using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class CMagnetSkill : CSkillBase
{
    [SerializeField] private Image Magnetimage;
    [SerializeField] private CMagnet.MagnetType m_magnetType = CMagnet.MagnetType.NPole;    
    [SerializeField] private KeyCode m_NMagnetCode;


    //마크넷 매니저
    private CMagnetManager m_magnetManager;    
    //일반 상태일때l
    private bool m_isNormal;
    //마그넷 체크하는 부분 다른 컨포넌트들에게 알려주기 위한 변수
    private bool m_MagnetCheck;
    //자석 오브젝트 확인하는 변수
    private (List<CMagnet>, List<CMagnet>) m_Magnet;
    //최종적으로 끌어당길 오브젝트 
    private CMagnet m_MagnetChoice;
    //최종적으로 끌어당길 오브젝트에 붙었을때 저장하는 변수
    private CMagnet m_RmagnetChoice;
    //8방향을 판단하기 위한 cos 값을 저장하는 변수
    private float m_CosineData;
    public bool g_MagnetCheck => m_MagnetCheck;
    public CMagnet.MagnetType g_MagnetType => m_magnetType;



    public override void init(CDynamicObject actor)
    {       
        base.init(actor);
        Magnetimage.color = Color.gray;
        var manager = GameObject.Find("MagnetManager");
        if (manager != null)
            m_magnetManager = manager.GetComponent<CMagnetManager>();
        m_CosineData = Mathf.Cos(22.5f * Mathf.Deg2Rad);
    }

    private void Update()
    {
        MagnetChange();
        if (m_isNormal) return;
        m_Magnet = MagnetCountCheck();        
    }

    private void FixedUpdate()
    {
        if (m_isNormal) return;
        Attraction();
        Repulsion();
    }

    // 자석 상태를 변경하는 함수 입니다.
    private void MagnetChange()
    {
        bool TempKeyDown = false ;
        if (Input.GetKeyDown(g_KeyCode))
        {
            //자석 효과 바뀔때 마다 초기화
            if (m_magnetType == CMagnet.MagnetType.SPole)
            {
                if (!m_isNormal)
                    m_isNormal = true;
                else
                    m_isNormal = false;

            }
            else
                m_isNormal = false;
            m_magnetType = CMagnet.MagnetType.SPole;
            TempKeyDown = true;

        }
        else if (Input.GetKeyDown(m_NMagnetCode))
        {
            //자석 효과 바뀔때 마다 초기화

            if (m_magnetType == CMagnet.MagnetType.NPole )
            {
                if (!m_isNormal)
                    m_isNormal = true;
                else
                    m_isNormal = false;
            }
            else
                m_isNormal = false;

            m_magnetType = CMagnet.MagnetType.NPole;
            TempKeyDown = true;
        }
        if (TempKeyDown)
        {
            ClearMagnetSkill();
            initMagnet();

            if (m_RmagnetChoice != null)
                if (m_RmagnetChoice.g_Pole != m_magnetType)
                    m_RmagnetChoice = null;
        }


        if (m_isNormal)
        {
            Color TempColor = Color.white;
            if (m_magnetType == CMagnet.MagnetType.NPole)
                TempColor = Color.red * 0.5f;
            if (m_magnetType == CMagnet.MagnetType.SPole)
                TempColor = Color.blue * 0.5f;           

            Magnetimage.color = new Color(TempColor.r, TempColor.g, TempColor.b, 1.0f);
        }
        else
        {

            if (m_magnetType == CMagnet.MagnetType.NPole)
                Magnetimage.color = Color.red;
            if (m_magnetType == CMagnet.MagnetType.SPole)
                Magnetimage.color = Color.blue;
        }
    }    
    //범위에 있는 자석 수 카운트 하는 함수
    //첫 번째 리턴 값은 다른 자석을 리턴
    //두 번째 리턴 값은 같은 자석을 리턴
    private (List<CMagnet> , List<CMagnet>) MagnetCountCheck()
    {

        List<CMagnet> MagnetAnotherList = new List<CMagnet>();
        List<CMagnet> MagnetSameList = new List<CMagnet>();

        foreach (var magnet in m_magnetManager.g_magnet)
        {            

            var Dist = Vector3.Distance(m_Actor.transform.position, magnet.transform.position);

            //자석의 force가 거리보다 작다면 리턴을 해줍니다.
            if (Dist > magnet.g_Force) continue;

            //주변에 자석이 있는지 검사합니다.
            if (m_magnetType != magnet.g_Pole)
            {
                if (Vector3.Distance(m_Actor.transform.position, magnet.transform.position) >= 0.1f)
                    MagnetAnotherList.Add(magnet);                
            }
            else
                MagnetSameList.Add(magnet);
        }  
        return (MagnetAnotherList, MagnetSameList);
    }
    //인력 효과 적용

    //인력 효과 정하는 부분
    private bool AttractionChioce()
    {
        var AttractionMagnet = m_Magnet.Item1;

        if (m_MagnetChoice == null)
        {
            // 글어당기는 오브젝트 정하는 부분
            if (AttractionMagnet.Count == 1)
            {
                m_MagnetChoice = AttractionMagnet[0];
            }
            else
            {
                var m_DirX = Input.GetAxisRaw("Horizontal");
                var m_DirY = Input.GetAxisRaw("Vertical");

                //8방향 정하는 부분
                foreach (var magnet in AttractionMagnet)
                {
                    var magnetpos = magnet.transform.position;
                    var playerPos = m_Actor.transform.position;

                    var Tempmagnet = new Vector2(magnetpos.x, magnetpos.y);
                    var TempPlayer = new Vector2(playerPos.x, playerPos.y);

                    var Magnetdirection = (Tempmagnet - TempPlayer).normalized;

                    
                    var PlayerDir = new Vector3(m_DirX, m_DirY).normalized;
                    var CosData = Vector2.Dot(PlayerDir, Magnetdirection);
                    if (CosData == 0) break;
                    if (CosData > m_CosineData)
                    {
                        m_MagnetChoice = magnet;
                        break;
                    }

                }
            }

            if (m_MagnetChoice == null) return false;
        }
        return true;
    }

    //인력 효과 적용
    private void Attraction()
    {
        var AttractionMagnet = m_Magnet.Item1;

        //자석 선택 및 주변에 있는 자석 몇개 있는지 체크
        if (AttractionMagnet == null || AttractionMagnet.Count == 0 || !AttractionChioce()) 
        {
            if (m_RmagnetChoice == null)
                ClearMagnetSkill();
            return;
        }

        // 당기는 힘 적용하는 부분
        Vector3 Velocity = Vector3.zero;
        var Distance = Vector2.Distance(m_Actor.transform.position, m_MagnetChoice.transform.position);
        var Direction = (m_MagnetChoice.transform.position - m_Actor.transform.position).normalized;

        var Magneticforce = Mathf.Sqrt(-2.0f * Physics.gravity.y * (m_MagnetChoice.g_Force / Distance));
        Velocity += Direction * Magneticforce;
        // 만약 거리가 짧아지면 힘적용 안함
        if (Distance > 0.5f)
        {
            m_Actor.g_Rigid.velocity = Velocity;            
        }
        else
        {
            m_Actor.g_Rigid.velocity = Vector3.zero;
            m_Actor.g_Rigid.useGravity = false;
            m_MagnetCheck = true;
            m_RmagnetChoice = m_MagnetChoice;
            if (m_Actor.CompareController("Movement"))
            {
                m_Actor.DestroyController("Movement");
            }
            m_Actor.transform.position = Vector3.Lerp(m_Actor.transform.position, m_MagnetChoice.transform.position, 0.3f);
        }
    }

    private void Repulsion()
    {
        var RepulsionMagnet = m_Magnet.Item2;
        if (RepulsionMagnet == null || RepulsionMagnet.Count == 0)
        {
            if (m_MagnetChoice == null)
                ClearMagnetSkill();
            return;
        }

        if (m_RmagnetChoice == null)
            MultiRepulsion(RepulsionMagnet);        
        else
            SingleRepulsion();
    }   


    //플레이어가 끌려와 자석에 붙었을때 8방향으로 나라가는 함수
    void SingleRepulsion()
    {
        var m_DirX = Input.GetAxisRaw("Horizontal");
        var m_DirZ = Input.GetAxisRaw("Vertical");

        if (m_Actor.CompareController("Movement"))
        {
            m_Actor.DestroyController("Movement");
        }


        m_Actor.g_Rigid.useGravity = false;
        m_MagnetCheck = true;

        var PlayerDir = new Vector3(m_DirX ,0.0f , m_DirZ).normalized;
        var Magneticforce = Mathf.Sqrt(-2.0f * Physics.gravity.y * (m_RmagnetChoice.g_Force));


        if (m_DirX != 0 || m_DirZ != 0)
        {
            initMagnet();
            m_RmagnetChoice = null;
            var Velocity = PlayerDir * Magneticforce;
            m_Actor.g_Rigid.velocity += Velocity * 0.5f;
        }
    }



    //평상시 같은 극이 되었을때 날라가는 함수
    void MultiRepulsion(List<CMagnet> RepulsionMagnet)
    {
        var Velocity = Vector3.zero;
        foreach (var magnet in RepulsionMagnet)
        {
            var Distance = Vector2.Distance(m_Actor.transform.position, magnet.transform.position);
            var Direction = (m_Actor.transform.position - magnet.transform.position).normalized;
            var Magneticforce = Mathf.Sqrt(-2.0f * Physics.gravity.y * (magnet.g_Force / Distance) * Time.fixedDeltaTime);
            Velocity += Direction * Magneticforce;
        }
        m_Actor.g_Rigid.velocity += Velocity * 0.3f;
    }



    void initMagnet()
    {
        m_Actor.GenerateController("Movement");
        m_Actor.g_Rigid.useGravity = true;
        m_MagnetCheck = false;
        m_MagnetChoice = null;
    }
    void ClearMagnetSkill()
    {
        m_MagnetCheck = false;
    }
}

