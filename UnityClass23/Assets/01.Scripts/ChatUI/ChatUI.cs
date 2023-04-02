using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ChatUI : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset _chatMsgTemplate;

    private UIDocument _document;
    private VisualElement _root;

    private TextField _txtChat;
    private Button _sendBtn;

    private ScrollView _chatScroll;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        //OnEnable에서 꼭 해줘야함
        _root = _document.rootVisualElement;

        //_msg = _root.Q<VisualElement>(className: "chat");

        _txtChat = _root.Q<TextField>("TextChat");
        _sendBtn = _root.Q<Button>("BtnSend");
        _chatScroll = _root.Q<ScrollView>("ChatContent");

        _sendBtn.RegisterCallback<ClickEvent>(SendClickHandle);
        _txtChat.RegisterCallback<KeyUpEvent>((e) =>
        {
            if (e.keyCode == KeyCode.Return)
            {
                SendProcess();
            }
        });
    }

    private void SendProcess()
    {
        if (_txtChat.value == string.Empty) return;

        VisualElement chatXML = _chatMsgTemplate.Instantiate();
        VisualElement chat = chatXML.Q<VisualElement>("ChatMsg");
        chat.AddToClassList("chat");

        Label lblMsg = chatXML.Q<Label>("MsgLabel");
        lblMsg.text = _txtChat.value;

        _chatScroll.Add(chatXML);

        _txtChat.value = string.Empty;
        StartCoroutine(ChatAnimation(chat));

        //_chatScroll.verticalScroller.value = _chatScroll.verticalScroller.highValue > 0 ? _chatScroll.verticalScroller.highValue : 0;
    }

    private void SendClickHandle(ClickEvent e)
    {
        SendProcess();
    }
    private IEnumerator ChatAnimation(VisualElement chat)
    {
        yield return new WaitForSeconds(0.1f);
        chat.AddToClassList("on");
        _chatScroll.verticalScroller.value = _chatScroll.verticalScroller.highValue > 0 ? _chatScroll.verticalScroller.highValue : 0;
    }
}
