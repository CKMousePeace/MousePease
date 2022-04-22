using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCSVParsing
{
    private TextAsset m_TextAsset;
    private List<string> m_TextLine; // 라인을 자른는 함수 (컷씬에서 사용 가능)
    private List<List<string>> m_TextElement; // 원소를 나누는 함수 (데이터 파싱에서 사용 가능)

    // 파일 생성하는 코드
    private CCSVParsing(string path)
    {
        m_TextAsset = Resources.Load<TextAsset>(path);
        m_TextElement = new List<List<string>>();
        m_TextLine = new List<string>();
        TextSplit();
    }

    //파일을 불러오는 함수
    static public CCSVParsing FileLoad(string path)
    {
        var Parsing = new CCSVParsing(path);
        if (Parsing == null)
        {
            Debug.LogError("string::Path에 맞는 파일 이름이 없습니다.");
            return null;
        }
        return Parsing;
    }
       
    //파일을 자르는 함수 
    private void TextSplit()
    {
        var text = m_TextAsset.text;
        var TextLineArray = text.Split('\n');

        for (int i = 0; i < TextLineArray.Length; i++)
        {
            m_TextLine.Add(TextLineArray[i]);
            var TextElementArray = TextLineArray[i].Split(',');


            m_TextElement.Add(new List<string>());
            for (int ii = 0; ii < TextElementArray.Length; ii++)
            {
                m_TextElement[i].Add(TextElementArray[ii]);
            }
        }
    }


    //(string)Text를 찾는함수
    private string SearchText(string column, string row)
    {
        int indexrow = -1;
        int indexcolumn = -1;
        if (m_TextAsset == null)
        {
            Debug.Log("CCSVParsing.FileLoad()를 통해서 TextAsset을 생성해주세요");
            return null;
        }

        var FileFirstList = m_TextElement[0];
        for (int i = 1; i < FileFirstList.Count; i++)
        {
            if (row == FileFirstList[i])
            {
                indexrow = i;
                break;
            }
        }
        for (int i = 1; i < m_TextElement.Count; i++)
        {
            if (column == m_TextElement[i][0])
            {
                indexcolumn = i;
                break;
            }
        }

        return m_TextElement[indexcolumn][indexrow];
    }


    //(index)Text를 찾는 함수
    private string SearchText(int column, int row)
    {
        try
        {
            var Data = m_TextElement[column][row];
            return Data;
        }
        catch
        {
            return null;
        }            
    }


    //리턴 값을 오브젝트로 해서 반환을 합니다.
    // 이때 중요한점은 사용 가능할 데이터는 (float와 bool)만 가능합니다.
    //striung 으로 데이터를 찾는 함수
    private object GetObject(string ParsingText)
    {
        object ParsingData = null;
        try
        {
            ParsingData = (object)(float.Parse(ParsingText));
        }
        catch
        {
            if (ParsingText == "T")
                ParsingData = (object)(false);
            if (ParsingText == "F")
                ParsingData = (object)(true);
        }
        return ParsingData;
    }

    //string로 데이터 찾는 함수
    public object GetParsingData(string column , string row) 
    {
        
        string ParsingText = SearchText(column , row);
        if (ParsingText == null)
        {
            Debug.LogError("CSV에 데이터가 존재 하지 않습니다.");
            return null;
        }
        return GetObject(ParsingText);
    }

    //index로 데이터 찾는 함수
    public object GetParsingData(int column, int row)
    {

        string ParsingText = SearchText(column, row);
        if (ParsingText == null)
        {
            Debug.LogError("CSV에 데이터가 존재 하지 않습니다.");
            return null;
        }
        return GetObject(ParsingText);
    }



    //라인 전체를 반환합니다.
    public string GetLine(int index)
    {
        if (m_TextLine.Count <= index || index < 0)
        {
            Debug.LogError("파일과 맞치않는 index 번호 입니다.");
        }
        return m_TextLine[index];
    }
}
