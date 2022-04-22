using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCSVParsing
{
    private TextAsset m_TextAsset;
    private List<string> m_TextLine; // ������ �ڸ��� �Լ� (�ƾ����� ��� ����)
    private List<List<string>> m_TextElement; // ���Ҹ� ������ �Լ� (������ �Ľ̿��� ��� ����)

    // ���� �����ϴ� �ڵ�
    private CCSVParsing(string path)
    {
        m_TextAsset = Resources.Load<TextAsset>(path);
        m_TextElement = new List<List<string>>();
        m_TextLine = new List<string>();
        TextSplit();
    }

    //������ �ҷ����� �Լ�
    static public CCSVParsing FileLoad(string path)
    {
        var Parsing = new CCSVParsing(path);
        if (Parsing == null)
        {
            Debug.LogError("string::Path�� �´� ���� �̸��� �����ϴ�.");
            return null;
        }
        return Parsing;
    }
       
    //������ �ڸ��� �Լ� 
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


    //(string)Text�� ã���Լ�
    private string SearchText(string column, string row)
    {
        int indexrow = -1;
        int indexcolumn = -1;
        if (m_TextAsset == null)
        {
            Debug.Log("CCSVParsing.FileLoad()�� ���ؼ� TextAsset�� �������ּ���");
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


    //(index)Text�� ã�� �Լ�
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


    //���� ���� ������Ʈ�� �ؼ� ��ȯ�� �մϴ�.
    // �̶� �߿������� ��� ������ �����ʹ� (float�� bool)�� �����մϴ�.
    //striung ���� �����͸� ã�� �Լ�
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

    //string�� ������ ã�� �Լ�
    public object GetParsingData(string column , string row) 
    {
        
        string ParsingText = SearchText(column , row);
        if (ParsingText == null)
        {
            Debug.LogError("CSV�� �����Ͱ� ���� ���� �ʽ��ϴ�.");
            return null;
        }
        return GetObject(ParsingText);
    }

    //index�� ������ ã�� �Լ�
    public object GetParsingData(int column, int row)
    {

        string ParsingText = SearchText(column, row);
        if (ParsingText == null)
        {
            Debug.LogError("CSV�� �����Ͱ� ���� ���� �ʽ��ϴ�.");
            return null;
        }
        return GetObject(ParsingText);
    }



    //���� ��ü�� ��ȯ�մϴ�.
    public string GetLine(int index)
    {
        if (m_TextLine.Count <= index || index < 0)
        {
            Debug.LogError("���ϰ� ��ġ�ʴ� index ��ȣ �Դϴ�.");
        }
        return m_TextLine[index];
    }
}
