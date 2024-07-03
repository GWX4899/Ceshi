using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GMCmd
{
    [MenuItem("CMCmd/��ȡ���")]
    public static void ReadTable()
    {
        PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
        foreach (PackageTableItem packageItem in packageTable.DataList)
        {
            Debug.Log(string.Format("��id��:{0},��name��:{1}", packageItem.id, packageItem.name));
        }
    }

    [MenuItem("CMCmd/����������������")]
    public static void CreateLocalPackageData()
    {
        //��������
        PackageLocalData.Instance.items = new List<PackageLocalItem>();
        for (int i = 0; i < 8; i++)
        {
            PackageLocalItem packageLocalItem = new PackageLocalItem()
            {
                uid = Guid.NewGuid().ToString(),
                id = i,
                num = i,
                level = i,
                isNew = i % 2 == 1
            };
            PackageLocalData.Instance.items.Add(packageLocalItem);
        }
        PackageLocalData.Instance.SavePackage();


    }
    [MenuItem("CMCmd/��ȡ������������")]
    public static void ReadLocalPackageData()
    {
        //��ȡ����
        List<PackageLocalItem> readItems = PackageLocalData.Instance.LoadPackage();
        foreach (PackageLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }

    [MenuItem("CMCmd/�򿪱���������")]
    public static void OpenPackagePanel()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }
}

