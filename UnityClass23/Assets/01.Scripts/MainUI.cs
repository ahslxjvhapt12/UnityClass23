using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    private UIDocument _document;

    private void Awake()
    {
        //_document = GetComponent<UIDocument>();

        //VisualElement root = _document.rootVisualElement;
        ////���⼭ �ؾ��� ���� : ��ť��Ʈ���� ���� ���ϴ� ������Ʈ�� �����ͼ� ������ �ϰ�ʹ�

        //Button btn = root.Q<Button>("BtnClick");
        //btn.RegisterCallback<ClickEvent>(e =>
        //{
        //    Debug.Log("��ư Ŭ��");
        //    btn.style.backgroundColor = Random.ColorHSV();
        //});
    }
}
