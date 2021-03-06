using UnityEngine;

[PreferBinarySerialization]
public class NoesisFont: ScriptableObject
{
    public string source;

    [HideInInspector]
    public byte[] content;
}
