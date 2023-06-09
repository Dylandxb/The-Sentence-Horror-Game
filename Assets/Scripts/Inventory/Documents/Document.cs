using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Document", menuName = "Item/Document")]
public class Document : ScriptableObject
{
    public int docID;
    public string docTitle;

    [TextArea] public string docText;
}
