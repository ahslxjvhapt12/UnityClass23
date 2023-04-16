using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    private TMP_Text _tmpText;
    [SerializeField][Range(0f, 100f)] private float curve;
    [SerializeField][Range(0f, 100f)] private float time;
    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _tmpText.ForceMeshUpdate(); //���������� ���� �ؽ�Ʈ�� �°� �޽� ������ ������Ʈ �Ѵ�.

        TMP_TextInfo textInfo = _tmpText.textInfo; //���� �������� �Ѿ��

        //TMP_CharacterInfo second = textInfo.characterInfo[1];

        //Vector3[] vertices = textInfo.meshInfo[second.materialReferenceIndex].vertices;
        //int vIndex0 = second.vertexIndex;

        //for (int i = 0; i < 4; i++)
        //{
        //    Vector3 origin = vertices[vIndex0 + i];
        //    if (i == 1 || i == 2)
        //        vertices[vIndex0 + i] = origin + new Vector3(0, 0.5f, 0);
        //}

        //var meshInfo = textInfo.meshInfo[second.materialReferenceIndex];
        //meshInfo.mesh.vertices = meshInfo.vertices; //��ŷ�����Ϳ� �巡��Ʈ �����͸� �ִ´�.

        //_tmpText.UpdateGeometry(meshInfo.mesh, second.materialReferenceIndex);

        //Debug.Log(second.vertexIndex);

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (charInfo.isVisible == false) continue; // �Ⱥ��̴� �ֵ�(����)�� �޽ð� ��� �ǳʶٱ�

            Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            int vIndex0 = charInfo.vertexIndex;

            for (int j = 0; j < 4; j++)
            {
                Vector3 origin = vertices[vIndex0 + j];
                vertices[vIndex0 + j] = origin + new Vector3(0, Mathf.Sin(Time.time * 2 * time + origin.x * curve), 0);
            }
            //Debug.Log($"{charInfo.character} : {charInfo.isVisible}");
        }

        //_tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices | TMP_VertexDataUpdateFlags.Colors32);

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;

            _tmpText.UpdateGeometry(meshInfo.mesh, i);
        }
    }


}
