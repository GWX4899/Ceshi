using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform UIIcon;
    private Transform UIHead;
    private Transform UINew;
    private Transform UISelect;
    private Transform UILevel;
    private Transform UIStars;
    private Transform UIDeleteSelect;


    private Transform UISelectAnimation;
    private Transform UIMouseOverAnimation;


    private PackageLocalItem packageLocalData;
    private PackageTableItem packageTableItem;
    private PackagePanel uiParent;


    private void Awake()
    {
        InitUIName();
    }
    private void InitUIName()
    {
        UIIcon = transform.Find("Top/Icon");
        UIHead = transform.Find("Top/Head");
        UINew = transform.Find("Top/New");
        UILevel = transform.Find("Bottom/LevelText");
        if (UILevel == null) Debug.Log(1);

        UIStars = transform.Find("Bottom/Stars");
        UISelect = transform.Find("Select");
        UIDeleteSelect = transform.Find("DeleteSelect");

        UISelectAnimation = transform.Find("SelectAnimation");
        UIMouseOverAnimation = transform.Find("MouseOverAnimation");

        UISelectAnimation.gameObject.SetActive(false);
        UIMouseOverAnimation.gameObject.SetActive(false);

        UIDeleteSelect.gameObject.SetActive(false);
    }

    public void Refresh(PackageLocalItem packageLocalData, PackagePanel uiParent)
    {
        // ���ݳ�ʼ��
        this.packageLocalData = packageLocalData;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);
        this.uiParent = uiParent;
        // �ȼ���Ϣ
        UILevel.GetComponent<Text>().text = "Lv." + this.packageLocalData.level.ToString();
        // �Ƿ����»�ã�
        UINew.gameObject.SetActive(this.packageLocalData.isNew);
        // ��Ʒ��ͼƬ
        Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));

        UIIcon.GetComponent<Image>().sprite = temp;
        // ˢ���Ǽ�
        RefreshStars();
    }

    private void RefreshStars()
    {
        for (int i = 0; i < UIStars.childCount; i++)
        {
            Transform star = UIStars.GetChild(i);
            if (this.packageTableItem.star > i)
            {
                star.gameObject.SetActive(true);
            }
            else
            {
                star.gameObject.SetActive(false);
            }
        }
    }
    public void RefreshDeleteState()
    {
        if (this.uiParent.deleteChooseUid.Contains(this.packageLocalData.uid))
        {
            this.UIDeleteSelect.gameObject.SetActive(true);
        }
        else
        {
            this.UIDeleteSelect.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnPointerClick" + eventData.ToString());

        if (this.uiParent.curMode == PackageMode.delete)
        {
            this.uiParent.AddChooseDeleteUid(this.packageLocalData.uid);
        }

        if (this.uiParent.chooseUID == this.packageLocalData.uid) return;
        this.uiParent.chooseUID = this.packageLocalData.uid;

        UISelectAnimation.gameObject.SetActive(true);
        UISelectAnimation.GetComponent<Animator>().SetTrigger("In");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter" + eventData.ToString());
        UIMouseOverAnimation.gameObject.SetActive(true);
        UIMouseOverAnimation.GetComponent<Animator>().SetTrigger("In");
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit" + eventData.ToString());
        UIMouseOverAnimation.GetComponent<Animator>().SetTrigger("Out");
    }


}
