using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Pattern), true)]
public class PatternEditor : Editor
{
    private enum RepetitionsMode
    {
        Infinite,
        Finite
    }

    private GameObject genericBulletPrefab;

    private RepetitionsMode selectedRepetitionsMode = RepetitionsMode.Infinite;
    private GUIStyle modulePadding = new GUIStyle();
    private ReorderableList list;

    private float handlersWidth;
    private float timingsWidth;
    private float burstsWidth;
    private float bulletsWidth;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        drawGUI();

        serializedObject.ApplyModifiedProperties();
    }

    void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("bursts"), true, true, true, true);

        list.drawHeaderCallback = (Rect rect) => {
            handlersWidth = (rect.width / 10 * 0.5f);
            timingsWidth = (rect.width / 10 * 1.5f);
            burstsWidth = (rect.width / 10 * 6);
            bulletsWidth = (rect.width / 10 * 1.5f);

            EditorGUI.LabelField(new Rect(rect.x + handlersWidth, rect.y, timingsWidth, rect.height), "Timings");
            EditorGUI.LabelField(new Rect(rect.x + handlersWidth + timingsWidth, rect.y, burstsWidth, rect.height), "Bursts");
            EditorGUI.LabelField(new Rect(rect.x + handlersWidth + timingsWidth + burstsWidth, rect.y, bulletsWidth, rect.height), "Bullets");
        };


        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            //EditorGUI.PropertyField(new Rect(rect.x, rect.y, 100, 300), element, GUIContent.none);
            BurstTiming burstTiming = ((Pattern)target).bursts[index];
            if(burstTiming == null)
            {
                burstTiming = new BurstTiming(0, null);
            }
            //EditorGUI.PropertyField(new Rect(rect.x + handlersWidth / 7, rect.y, timingsWidth, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("timing"), GUIContent.none);
            //EditorGUI.PropertyField(new Rect(rect.x + handlersWidth / 7 + timingsWidth, rect.y, burstsWidth, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("burst"), GUIContent.none);
            EditorGUI.FloatField(new Rect(rect.x + handlersWidth / 7, rect.y, timingsWidth, EditorGUIUtility.singleLineHeight), burstTiming.timing);
            burstTiming.burst = EditorGUI.ObjectField(new Rect(rect.x + handlersWidth/7 + timingsWidth, rect.y, burstsWidth, EditorGUIUtility.singleLineHeight), burstTiming.burst, typeof(Burst), false) as Burst;
            //burstTiming.bullet = EditorGUI.ObjectField(new Rect(rect.x + handlersWidth + timingsWidth + burstsWidth, rect.y, bulletsWidth, EditorGUIUtility.singleLineHeight), burstTiming.bullet, typeof(GameObject), false) as GameObject;
            ((Pattern)target).bursts[index] = burstTiming;
        };
            
        list.elementHeightCallback = (index) => {
            Repaint();
            return (EditorGUIUtility.singleLineHeight + 2);
        };
    }

    public void drawGUI()
    {
        GUILayout.BeginVertical("ShurikenEffectBg", new GUILayoutOption[0]);
        EditorGUILayout.BeginVertical();
        Rect rect;
        rect = GUILayoutUtility.GetRect(0, 0);
        GUIStyle style;
        style = (GUIStyle)"ShurikenEmitterTitle";
        style.clipping = TextClipping.Clip;
        style.padding.right = 0;
        modulePadding.padding = new RectOffset(0, 0, 0, 0);
        Rect position = EditorGUILayout.BeginVertical(modulePadding, new GUILayoutOption[0]);
        GUI.Label(position, GUIContent.none, "ShurikenModuleBg");

        // Label style
        int labelWidthInPixels = 100;

        // Display stuff here
        
        //EditorGUILayout.DropdownButton(new GUIContent())
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Bullet prefab", "The bullet prefab for all the bullets shot by the pattern"), GUILayout.Width(labelWidthInPixels));
        ((Pattern)target).bullet = EditorGUILayout.ObjectField(((Pattern)target).bullet, typeof(GameObject), false, GUILayout.ExpandWidth(true)) as GameObject;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Duration", "The duration (in seconds) that the pattern takes to complete"), GUILayout.Width(labelWidthInPixels));
        ((Pattern)target).duration = EditorGUILayout.FloatField(((Pattern)target).duration);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Repetitions", GUILayout.Width(labelWidthInPixels));
        selectedRepetitionsMode = (RepetitionsMode)EditorGUILayout.EnumPopup(selectedRepetitionsMode, GUILayout.ExpandWidth(true));
        EditorGUILayout.BeginVertical();
        switch (selectedRepetitionsMode)
        {
            case RepetitionsMode.Infinite:
                ((Pattern)target).repetitions = -1;
                break;
            case RepetitionsMode.Finite:
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Amount", "The number of times the pattern will play (-1 = Infinite)"), GUILayout.Width(labelWidthInPixels));
                if (((Pattern)target).repetitions < 0)
                    ((Pattern)target).repetitions = 0;
                ((Pattern)target).repetitions = EditorGUILayout.FloatField(((Pattern)target).repetitions);
                EditorGUILayout.EndHorizontal();
                break;
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        list.DoLayoutList();

        // End display stuff
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        GUILayout.EndVertical();
    }
}
