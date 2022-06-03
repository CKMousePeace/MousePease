using UnityEngine;
using System.IO;

public class CSaveController : MonoBehaviour
{
    [Header("���̺� ����Ʈ ������� ����")]
    public GameObject[] g_Save;


    static GameObject _container;
    public static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static CSaveController _instance;
    public static CSaveController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "CSaveController";
                _instance = _container.AddComponent(typeof(CSaveController)) as CSaveController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string GameDataFileName = "SaveDataBoi.json";

    public CGameData _gameData;
    public CGameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Start()
    {
        LoadGameData();
        SaveGameData();
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            print("successfully load ");
            string FromJsonData = File.ReadAllText(filePath);
            Debug.Log(filePath);
            _gameData = JsonUtility.FromJson<CGameData>(FromJsonData);
        }

        else
        {
            print("generate New Save");
            _gameData = new CGameData();
        }
    }

    // ���� �����ϱ�
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData);

        print("Save Complete :)");
    }


    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
        _gameData = new CGameData();
        SaveGameData();
    }

}
