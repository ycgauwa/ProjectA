using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PPD
{
    [Serializable]
    public class ED_SelectedSceneObject
    {
        private static int _lastMenuCallTimestamp = 0;
        static bool NotFirstTime()
        {
            var currentTime = (int)EditorApplication.timeSinceStartup;
            //hack https://answers.unity.com/questions/608256/how-to-execute-menuitem-for-multiple-objects-once.html
            if (currentTime < _lastMenuCallTimestamp + 1)
            {
                return true;
            }
            else
            {
                _lastMenuCallTimestamp = currentTime;
                return false;
            }
        }

        // [MenuItem("GameObject/Create Empty 000", false, 0)]
        // public static void CreateEmpty_0_0_0()
        // {
        //     if (NotFirstTime()) return;

        //     GameObject go = new GameObject("000");
        //     go.transform.position = new Vector3(0, 0, 0);
        //     Undo.RegisterCreatedObjectUndo(go, "Generated 000");

        //     Selection.activeObject = go;
        // }


        // [MenuItem("GameObject/★便利メソッド★/選択中をまとめる(Ctrl+G) %g", true)]
        // [MenuItem("GameObject/★便利メソッド★/選択中を000にまとめる(Shift+Ctrl+G) %#g", true)]
        // public static bool ValidateSelectionIsMorethan1() => Selection.objects.Length > 1;

        [MenuItem("GameObject/★便利メソッド★/選択中をまとめる(Ctrl+G) %g", false, 0)]
        public static void Group_Objects_Into_Centered_Empty()
        {
            if (NotFirstTime()) return;

            GameObject go = new GameObject("Grouped GameObject");
            Undo.RegisterCreatedObjectUndo(go, "Grouped");

            float x = 0, y = 0, z = 0;
            var transforms = Selection.transforms;
            foreach (var transform in transforms)
            {
                x += transform.position.x;
                y += transform.position.y;
                z += transform.position.z;
            }
            go.transform.position = new Vector3(x, y, z) / transforms.Length;
            foreach (var trans in Selection.transforms)
            {
                Undo.SetTransformParent(trans, go.transform, "Grouped");
            }
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★便利メソッド★/選択中を000にまとめる(Shift+Ctrl+G) %#g", false, 0)]
        public static void Group_Objects_Into_Empty_0_0_0()
        {
            if (NotFirstTime()) return;

            GameObject go = new GameObject("Grouped 000");
            go.transform.position = new Vector3(0, 0, 0);
            GameObjectUtility.SetParentAndAlign(go, null);
            Undo.RegisterCreatedObjectUndo(go, "Grouped");
            var transforms = Selection.transforms;
            foreach (var trans in Selection.transforms)
            {
                Undo.SetTransformParent(trans, go.transform, "Grouped");
            }
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/+X", false, 0)]
        public static void SortByX() => SortBy(t => t.localPosition.x);

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/+Y", false, 0)]
        public static void SortByY() => SortBy(t => t.localPosition.y);

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/+Z", false, 0)]
        public static void SortByZ() => SortBy(t => t.localPosition.z);

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/逆順/X", false, 0)]
        public static void SortBy_X() => SortBy(t => -t.localPosition.x);

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/逆順/Y", false, 0)]
        public static void SortBy_Y() => SortBy(t => -t.localPosition.y);

        [MenuItem("GameObject/★便利メソッド★/子をPositionでソート/逆順/Z", false, 0)]
        public static void SortBy_Z() => SortBy(t => -t.localPosition.z);

        public static void SortBy(Func<Transform, float> func)
        {
            if (NotFirstTime()) return;

            var o = SelectGameObjects();
            foreach (var e in o)
            {
                Undo.RecordObject(e, "名前順にソート");
                foreach (var t in e.transform.GetComponentsInChildren<Transform>().OrderBy(func))
                {
                    if (t != e.transform)
                    {
                        t.SetAsLastSibling();
                        EditorUtility.SetDirty(t);
                    }
                }
            }
        }

        [MenuItem("GameObject/★便利メソッド★/子を名前順にソート", false, 0)]
        public static void SortSiblingByName()
        {
            if (NotFirstTime()) return;

            var o = SelectGameObjects();
            foreach (var e in o)
            {
                Undo.RecordObject(e, "名前順にソート");
                foreach (var t in e.transform.GetComponentsInChildren<Transform>().OrderBy(t => t.name))
                {
                    if (t != e.transform)
                    {
                        t.SetAsLastSibling();
                    }
                }
            }
        }

        public static GameObject[] SelectGameObjects()
        {
            // return Selection.gameObjects;
            var o = Selection.gameObjects.Select(e => e as GameObject);

            if (o.Any(e => e == null))
            {
                //UNITY_EDITOR.Dialog.Error("GameObjectを選択してください");
            }

            return o.ToArray();
        }
    }
}
