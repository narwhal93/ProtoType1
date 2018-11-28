using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;
using System.Reflection;

public class TypeInspector : EditorWindow
{
    #region Private Enums
    private enum EMemberType
    {
        Field,
        Property,
        Method,
        Max
    }
    
    [Flags]
    private enum EAccessType
    {
        PublicOnly = 1 << 0,
        NonPublicOnly = 1 << 1,
        Inherited = 1 << 2
    }

    [Flags]
    private enum EAssignableType
    {
        ValueOnly = 1 << 0,
        ReferenceOnly = 1 << 1
    }
    #endregion

    private class ShowFlag
    {
        public EAccessType AccessFlag;
        public EAssignableType AssingableFlag;
    }

    private ShowFlag[] _showFlags = new ShowFlag[(int)EMemberType.Max];
    private string[] _flagLabel = new string[]
    {
        "Show fields",
        "Show properties",
        "Show methods"
    };

    private Object _target;
    private Vector2 _scrollPos;
    private bool[] _foldoutFlag = new bool[(int)EMemberType.Max];
    
    #region Initializers
    [MenuItem("Window/Type Inspector")]
    public static void ShowWindow()
    {
        var window = GetWindow<TypeInspector>();
        window.titleContent = new GUIContent("Type Inspector");
        
        window.Show();
    }

    private void OnEnable()
    {
        InitFlags();
    }

    private void InitFlags()
    {
        for (int i = 0; i < _showFlags.Length; i++)
        {
            _showFlags[i] = new ShowFlag();
            _showFlags[i].AccessFlag = (EAccessType)Enum.ToObject(typeof(EAccessType), int.MaxValue);
            _showFlags[i].AssingableFlag = (EAssignableType)Enum.ToObject(typeof(EAssignableType), int.MaxValue);
        }
    }
    #endregion

    protected virtual void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        _target = EditorGUILayout.ObjectField("Inspecting Object", _target, typeof(Object), true);
        EditorGUILayout.Space();

        ShowBasicInfoGUI(_target);
        EditorGUILayout.Space();

        ShowFlagGUI();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Type Information", EditorStyles.boldLabel);
        if (_target)
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            var type = _target.GetType();
            for (int i = 0; i < (int)EMemberType.Max; i++)
            {
                ShowMemberGUI(_target, type, (EMemberType)i);
            }
            EditorGUILayout.EndScrollView();
        }
    }

    private void ShowFlagGUI()
    {
        EditorGUILayout.LabelField("Filtering Options", EditorStyles.boldLabel);
        for (int i = 0; i < _showFlags.Length; i++)
        {
            var flagInfo = _showFlags[i];
            var controlRect = EditorGUILayout.GetControlRect();
            var fieldWidth = (controlRect.width - EditorGUIUtility.labelWidth) / 2.0f;

            controlRect.width = EditorGUIUtility.labelWidth;
            EditorGUI.LabelField(controlRect, _flagLabel[i]);

            controlRect.x += controlRect.width;
            controlRect.width = fieldWidth;

            flagInfo.AccessFlag = (EAccessType)EditorGUI.EnumMaskPopup(controlRect, GUIContent.none, flagInfo.AccessFlag);
            if (i < 2)
            {
                controlRect.x += controlRect.width;
                flagInfo.AssingableFlag = (EAssignableType)EditorGUI.EnumMaskPopup(controlRect, GUIContent.none, flagInfo.AssingableFlag);
            }
        }
    }
        
    private void ShowMemberGUI(object target, Type type, EMemberType memberType)
    {
        var bindingFlag = GetBindingFlags(memberType);
        var memberTypeIndex = (int)memberType;
        var flagInfo = _showFlags[memberTypeIndex];
        
        var showValue = (flagInfo.AssingableFlag & EAssignableType.ValueOnly) != 0;
        var showRef = (flagInfo.AssingableFlag & EAssignableType.ReferenceOnly) != 0;

        if (memberType == EMemberType.Field)
        {
            var fieldInfoList = type.GetFields(bindingFlag);

            _foldoutFlag[memberTypeIndex] = EditorGUILayout.Foldout(_foldoutFlag[memberTypeIndex], "Fields");
            EditorGUI.indentLevel++;
            if (_foldoutFlag[memberTypeIndex])
            {
                foreach (var fieldInfo in fieldInfoList)
                {
                    var attr = fieldInfo.GetCustomAttributes(typeof(ObsoleteAttribute), true);

                    if (attr == null || attr.Length == 0)
                    {
                        var prefix = string.Format("[{0}]{1}", 
                                                   fieldInfo.Attributes.ToString(),
                                                   fieldInfo.FieldType.IsSerializable ? "[Serializable] " : "[Nonserializable] ");

                        if (fieldInfo.FieldType.IsValueType)
                        {
                            if (showValue)
                            {
                                DrawLabel(prefix + fieldInfo.Name, fieldInfo.GetValue(target));
                            }
                        }
                        else
                        {
                            if (showRef)
                            {
                                DrawLabel(prefix + fieldInfo.Name, fieldInfo.GetValue(target));
                            }
                        }
                    }
                }
            }
            EditorGUI.indentLevel--;
        }
        else if (memberType == EMemberType.Property)
        {
            var propertyInfoList = type.GetProperties(bindingFlag);

            _foldoutFlag[memberTypeIndex] = EditorGUILayout.Foldout(_foldoutFlag[memberTypeIndex], "Properties");
            EditorGUI.indentLevel++;
            if (_foldoutFlag[memberTypeIndex])
            {
                foreach (var propertyInfo in propertyInfoList)
                {
                    var attr = propertyInfo.GetCustomAttributes(typeof(ObsoleteAttribute), true);

                    if (attr == null || attr.Length == 0)
                    {
                        string prefix = propertyInfo.PropertyType.IsSerializable ? "[Serializable] " : "[Nonserializable] ";

                        if (propertyInfo.PropertyType.IsValueType)
                        {
                            if (showValue)
                            {
                                DrawLabel(prefix + propertyInfo.Name, propertyInfo.GetValue(target, null));
                            }
                        }
                        else
                        {
                            if (showRef)
                            {
                                DrawLabel(prefix + propertyInfo.Name, propertyInfo.GetValue(target, null));
                            }
                        }
                    }
                }
            }
            EditorGUI.indentLevel--;
        }
        else if (memberType == EMemberType.Method)
        {
            var methodInfoList = type.GetMethods(bindingFlag);

            _foldoutFlag[memberTypeIndex] = EditorGUILayout.Foldout(_foldoutFlag[memberTypeIndex], "Methods");
            EditorGUI.indentLevel++;
            if (_foldoutFlag[memberTypeIndex])
            {
                foreach (var methodInfo in methodInfoList)
                {
                    var attr = methodInfo.GetCustomAttributes(typeof(ObsoleteAttribute), true);
                    var prefix = string.Format("[{0}] ", methodInfo.Attributes.ToString());

                    if (attr == null || attr.Length == 0)
                    {
                        EditorGUILayout.LabelField(prefix + methodInfo.ToString());// prefix + methodInfo.Name + suffix);
                    }
                }
            }
            EditorGUI.indentLevel--;
        }
    }

    private BindingFlags GetBindingFlags(EMemberType memberType)
    {
        BindingFlags bindingFlag = BindingFlags.Instance | BindingFlags.DeclaredOnly;
        var flagInfo = _showFlags[(int)memberType];
        
        if ((flagInfo.AccessFlag & EAccessType.PublicOnly) != 0)
        {
            bindingFlag |= BindingFlags.Public;
        }

        if ((flagInfo.AccessFlag & EAccessType.NonPublicOnly) != 0)
        {
            bindingFlag |= BindingFlags.NonPublic;
        }

        if ((flagInfo.AccessFlag & EAccessType.Inherited) != 0)
        {
            bindingFlag ^= BindingFlags.DeclaredOnly;
        }

        return bindingFlag;
    }
    
    private void DrawLabel(string label, object value)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField(value == null ? "null" : value.ToString());
        EditorGUILayout.EndHorizontal();
    }
    
    private void ShowBasicInfoGUI(Object obj)
    {
        if (obj)
        {
            var type = obj.GetType();

            EditorGUILayout.LabelField("Basic information", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Namespace", type.Namespace);
            EditorGUILayout.LabelField("Type name", type.Name);
            EditorGUILayout.LabelField("Base class", type.BaseType.Name);
            EditorGUI.indentLevel--;
        }
        else
        {
            EditorGUILayout.LabelField("Basic information", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Namespace", string.Empty);
            EditorGUILayout.LabelField("Type name", string.Empty);
            EditorGUILayout.LabelField("Base class", string.Empty);
            EditorGUI.indentLevel--;
        }
    }
}