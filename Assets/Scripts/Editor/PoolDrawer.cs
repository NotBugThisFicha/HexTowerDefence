using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PoolNonMono.TypeTag))]
public class PoolDrawer : PropertyDrawer
{
    private List<string> typeNames;
    private List<TypeInfo> typeInfos;

    private bool typesFull = false;

    private int index = 0;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (typesFull == false) GetTypes();

        SerializedProperty serializedProperty = property.FindPropertyRelative("_tag");

        index = GetTypeName(serializedProperty.stringValue);
        index = EditorGUI.Popup(position, index, typeNames.ToArray());
        serializedProperty.stringValue = typeInfos[index].Name;
    }
    private int GetTypeName(string name)
    {
        int count = 0;
        foreach (TypeInfo typeInfo in typeInfos)
        {
            if(typeInfo.Name == name)
                return count;
            count++;
        }
        return 0;
    }
    private void GetTypes()
    {
        typeNames = new List<string>();
        typeInfos = new List<TypeInfo>();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly assembly in assemblies)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.CustomAttributes.Count() > 0)
                {
                    PoolMember poolAttribute = type.GetCustomAttribute<PoolMember>();
                    if (poolAttribute != null)
                    {
                        typeInfos.Add((TypeInfo)type);
                        typeNames.Add($"{type}");
                    }
                }
            }
        }
        typesFull = true;
    }
}
