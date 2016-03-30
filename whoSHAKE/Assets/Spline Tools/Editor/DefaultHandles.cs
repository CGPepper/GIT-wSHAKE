// Copyright (c) 2015 Raed Abdullah
// this version is 1.4
// this script controls the default tools if they are shown or not

using System.Reflection;
using System;
using UnityEditor;

public class DefaultHandles
{
    public static bool Hidden
    {
        get
        {
            Type type = typeof(Tools);
            FieldInfo field = type.GetField("s_Hidden", BindingFlags.NonPublic | BindingFlags.Static);
            return ((bool)field.GetValue(null));
        }
        set
        {
            Type type = typeof(Tools);
            FieldInfo field = type.GetField("s_Hidden", BindingFlags.NonPublic | BindingFlags.Static);
            field.SetValue(null, value);
        }
    }
}
