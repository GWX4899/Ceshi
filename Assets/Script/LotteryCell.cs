using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryCell : MonoBehaviour
{
    private Transform UIImage;
    private Transform UIStars;
    private Transform UINew;
    private PackageLocalItem PackageLocalItem;
    private PackageTableItem PackageTableItem;

    private LotteryPanel uiParent;


    private void Awake()
    {
        InitUI();
    }

    private void InitUI()
    {
        UIImage = transform.Find("Center/Image");
        UIStars = transform.Find("Bottom/Stars");
        UINew = transform.Find("Top/New");
        UINew.gameObject.SetActive(false);
    }

    public void Refresh(PackageLocalItem packageLocalItem,LotteryPanel uiParent)
    {
        //数据初始化
        this.PackageLocalItem = packageLocalItem;
        this.PackageTableItem = GameManager.Instance.GetPackageItemById(this.PackageLocalItem.id);
        this.uiParent = uiParent;

        RefreshImage();
    }

    private void RefreshImage()
    {
        Texture2D t = (Texture2D)Resources.Load(this.PackageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        UIImage.GetComponent<Image>().sprite = temp;
    }

    public void RefreshStars()
    {
        for(int i = 0; i < UIStars.childCount; i++)
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
