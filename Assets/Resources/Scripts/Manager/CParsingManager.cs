using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CParsingManager
{

    static private Dictionary<string , CCSVParsing> m_Parsing = new Dictionary<string , CCSVParsing>();
    //파싱 파일 저장 하는 역할을 한다.
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
            Debug.LogError("맞는 주소가 없습니다.");
            return null;
        }
        return Parsing;
    }

    //파싱 파일을 불러오는 역할을 한다.
    static public CCSVParsing GetCParsing(string Path)
    {
        CreateParsingScript(Path);
        return m_Parsing[Path];
    }
}
