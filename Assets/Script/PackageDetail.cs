using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageDetail : MonoBehaviour
{
    private Transform UIStars;
    private Transform UIDescription;
    private Transform UIIcon;
    private Transform UITitle;
    private Transform UILevelText;
    private Transform UISkillDescription;

    private PackageLocalItem PackageLocalData;
    private PackageTableItem PackageTableItem;
    private PackagePanel uiParent;

    private void Awake()
    {
        InitUIName();
    }


    private void Start()
    {
        Test();
    }
    private void Test()
    {
        Refresh(GameManager.Instance.GetSortPackageLocalData()[0], null);
    }


    private void InitUIName()
    {
        UIStars = transform.Find("Center/Stars");
        UIDescription = transform.Find("Center/Description");
        UIIcon = transform.Find("Center/Icon");
        UITitle = transform.Find("Top/Title");
        UILevelText = transform.Find("Bottom/LevelPnl/LevelText");
        UISkillDescription = transform.Find("Bottom/Description");
    }

    public void Refresh(PackageLocalItem packageLocalData, PackagePanel uiParent)
    {
        //初始化：动态数据、静态数据、父物品逻辑
        this.PackageLocalData = packageLocalData;
        this.PackageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);
        this.uiParent = uiParent;

        if(UILevelText==null) Debug.Log(this.PackageLocalData.level);
        UILevelText.GetComponent<Text>().text = string.Format("Lv.{0}/40", this.PackageLocalData.level.ToString());

        UIDescription.GetComponent<Text>().text = this.PackageTableItem.description;

        UISkillDescription.GetComponent<Text>().text = this.PackageTableItem.skillDescription;

        UITitle.GetComponent<Text>().text = this.PackageTableItem.name;

        Texture2D t = (Texture2D)Resources.Load(this.PackageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = temp;

        RefreshStars();
    }

    private void RefreshStars()
    {
        for (int i = 0; i < UIStars.childCount; i++)
        {
            Transform star = UIStars.GetChild(i);
            if (this.PackageTableItem.star > i)
            {
                star.gameObject.SetActive(true);
            }
            else
            {
                star.gameObject.SetActive(false);
            }
        }
    }
}
