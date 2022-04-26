using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CParsingManager
{

    static private Dictionary<string , CCSVParsing> m_Parsing = new Dictionary<string , CCSVParsing>();
    //�Ľ� ���� ���� �ϴ� ������ �Ѵ�.
    static private  CCSVParsing CreateParsingScript(string Path)
    {
        if (m_Parsing.ContainsKey(Path))
        {
            return m_Parsing[Path];
        }

        var Parsing = CCSVParsing.FileLoad(Path);
        m_Parsing.Add(Path, Parsing);

        if (Parsing == null)
        {
            Debug.LogError("�´� �ּҰ� �����ϴ�.");
            return null;
        }
        return Parsing;
    }

    //�Ľ� ������ �ҷ����� ������ �Ѵ�.
    static public CCSVParsing GetCParsing(string Path)
    {
        CreateParsingScript(Path);
        return m_Parsing[Path];
    }
}
