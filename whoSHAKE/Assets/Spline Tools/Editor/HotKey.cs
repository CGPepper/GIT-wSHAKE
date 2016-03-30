// Copyright (c) 2015 Raed Abdullah
// this version is 1.4
// this script is used to make the keys changable in the user perfrences 

using UnityEngine;

namespace SplineTool
{
    public class HotKey
    {
        public KeyCode keyCode;
        public bool alt;
        public bool ctrl;
        public bool shift;

        public HotKey(KeyCode kc, bool useAlt, bool useCtrl, bool useShift)
        {
            keyCode = kc;
            alt = useAlt;
            ctrl = useCtrl;
            shift = useShift;
        }
    }
}
