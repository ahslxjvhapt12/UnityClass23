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
        ////여기서 해야할 일이 : 도큐먼트에서 내가 원하는 오브젝트만 가져와서 뭔가를 하고싶다

        //Button btn = root.Q<Button>("BtnClick");
        //btn.RegisterCallback<ClickEvent>(e =>
        //{
        //    Debug.Log("버튼 클릭");
        //    btn.style.backgroundColor = Random.ColorHSV();
        //});
    }
}
