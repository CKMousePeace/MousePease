using System.Collections.Generic;
using UnityEngine;

public class CObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class ObjectInfo
    {
        
        public ObjectInfo(string objectName, GameObject obj)
        {
            m_Name = objectName;
            m_Object= obj;
        }

        [SerializeField] private GameObject m_Object;
        [SerializeField] private string m_Name;

        public GameObject g_Object=> m_Object;
        public string g_Name => m_Name;
    }


    [SerializeField] private List<ObjectInfo> m_ListObject;
    private Dictionary<string, GameObject> m_ObjectDic = new Dictionary<string, GameObject>();
    
    private Dictionary<string, Queue<GameObject>> m_GameQue =  new Dictionary<string, Queue<GameObject>>();
    private Dictionary<int, ObjectInfo> m_UseObject = new Dictionary<int, ObjectInfo>();

    private static CObjectPool m_instance;
    public static CObjectPool g_instance => m_instance;



    private void Awake()
    {
        if (m_instance != null)
            Destroy(this.gameObject);

        if (m_instance == null)
        {
            DontDestroyOnLoad(this);
            m_instance = this;
        }

        foreach (var item in m_ListObject)
        {
            m_ObjectDic.Add(item.g_Name, item.g_Object);
            m_GameQue.Add(item.g_Name, new Queue<GameObject>());
        }
    }

    //오브젝트 해쉬 값을 리턴을 하게 된다.
    public int ObjectPop(string ObjectName, Vector3 Position , Quaternion Rotate , Vector3 Scale)
    {
        if (!m_ObjectDic.ContainsKey(ObjectName))
            return -1;
       
        if (m_GameQue[ObjectName] == null)
        {
            if (!AddItem(ObjectName))
                return -1;
        }
        if (m_GameQue[ObjectName].Count == 0)
        {
            if (!AddItem(ObjectName))
                return -1;
        }
        
        var Object = m_GameQue[ObjectName].Dequeue();
        int HashData = Object.GetHashCode();

       
        Object.SetActive(true);

        Object.transform.SetPositionAndRotation(Position, Rotate);
        Object.transform.localScale = Scale;
       
        m_UseObject.Add(HashData, new ObjectInfo(ObjectName, Object));
        return HashData;
    }



    //해쉬 값을 이용해서 오브젝트를 가져 오게 된다.
    public GameObject UsingGameObject(int HashData)
    {
        if (!m_UseObject.ContainsKey(HashData))
            return null;
        return m_UseObject[HashData].g_Object;
    }

    
    // 비활성화시 오브젝트 풀에 넣는 역할을 합니다.
    public void ObjectPush()
    {
        var Liststr = new List<int>();
        foreach (var item in m_UseObject)
        {
            if (!item.Value.g_Object.activeInHierarchy)
                Liststr.Add(item.Key);
        }
        foreach (var item in Liststr)
        {
            m_GameQue[m_UseObject[item].g_Name].Enqueue(m_UseObject[item].g_Object);
            m_UseObject.Remove(item);
        }

    }    
    //풀에 데이터를 넣는 작업을 한다.
    private bool AddItem(string ObjectName)
    {
        if (!m_GameQue.ContainsKey(ObjectName)) 
            return false;

        for (int i = 0; i < 4; i++)
        {            
            var obj = Instantiate(m_ObjectDic[ObjectName], transform);
            m_GameQue[ObjectName].Enqueue(obj);
            obj.SetActive(false);
        }
        return true;
    }
    private void Update()
    {
        ObjectPush();
        if (Input.GetKeyDown(KeyCode.W))
        {
            ObjectPop("Test" , new Vector3(-20.73f , 5.43f , -36.13f), 
                Quaternion.identity , new Vector3(1.0f , 1.0f , 1.0f));                
        }
    }
}
