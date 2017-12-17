#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Pattern), true)]
public class PatternEditor : Editor
{
    private GameObject genericBulletPrefab;
    private GUIStyle modulePadding = new GUIStyle();
    private ReorderableList list;

    private float handlersWidth;
    private float timingsWidth;
    private float burstsWidth;
    private float bulletsWidth;
    
    // Data
    private Pattern pattern;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        drawGUI();

        if(!UnityEditor.EditorApplication.isPlaying)
        {
            EditorUtility.SetDirty((Pattern)target);
        }

        //serializedObject.FindProperty("bursts"). = ((Pattern)target).bursts;
        serializedObject.ApplyModifiedProperties();

        if (!UnityEditor.EditorApplication.isPlaying)
        {
            EditorUtility.SetDirty((Pattern)target);
        }
    }

    void OnEnable()
    {
        pattern = (Pattern)target;

        list = new ReorderableList(serializedObject, serializedObject.FindProperty("bursts"), true, true, true, true);

        list.drawHeaderCallback = (Rect rect) => {
            handlersWidth = (rect.width / 10 * 0.2f);
            timingsWidth = (rect.width / 10 * 0.8f);
            burstsWidth = (rect.width / 10 * 6);
            bulletsWidth = (rect.width / 10 * 2.5f);

            EditorGUI.LabelField(new Rect(rect.x + handlersWidth, rect.y, timingsWidth, rect.height), "Time");
            EditorGUI.LabelField(new Rect(rect.x + handlersWidth + timingsWidth, rect.y, burstsWidth, rect.height), "Burst");
            EditorGUI.LabelField(new Rect(rect.x + handlersWidth + timingsWidth + burstsWidth, rect.y, bulletsWidth, rect.height), "Bullet");
        };


        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
    
            rect.y += 2;
            BurstTiming burstTiming = ((Pattern)target).bursts[index];
            if(burstTiming == null)
                burstTiming = new BurstTiming();
            burstTiming.timing = EditorGUI.FloatField(new Rect(rect.x + handlersWidth / 7, rect.y, timingsWidth, EditorGUIUtility.singleLineHeight), burstTiming.timing);
            burstTiming.burst = EditorGUI.ObjectField(new Rect(rect.x + handlersWidth/7 + timingsWidth, rect.y, burstsWidth, EditorGUIUtility.singleLineHeight), burstTiming.burst, typeof(Burst), false) as Burst;
            burstTiming.bullet = EditorGUI.ObjectField(new Rect(rect.x + handlersWidth + timingsWidth + burstsWidth, rect.y, bulletsWidth, EditorGUIUtility.singleLineHeight), burstTiming.bullet, typeof(GameObject), false) as GameObject;

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
        EditorGUILayout.LabelField(new GUIContent("Generic bullet prefab", "The bullet prefab for all the bullets shot by the pattern"), GUILayout.Width(labelWidthInPixels * 1.5f));
        genericBulletPrefab = EditorGUILayout.ObjectField(genericBulletPrefab, typeof(GameObject), false, GUILayout.ExpandWidth(true)) as GameObject;
        if (GUILayout.Button("Assign to all", GUILayout.Width(labelWidthInPixels)))
            foreach (BurstTiming burstTiming in ((Pattern)target).bursts)
                burstTiming.bullet = genericBulletPrefab;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Duration", "The duration (in seconds) that the pattern takes to complete"), GUILayout.Width(labelWidthInPixels));
        ((Pattern)target).duration = EditorGUILayout.FloatField(((Pattern)target).duration);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cycles", GUILayout.Width(labelWidthInPixels));
        ((Pattern)target).selectedRepetitionsMode = (RepetitionsMode)EditorGUILayout.EnumPopup(((Pattern)target).selectedRepetitionsMode, GUILayout.ExpandWidth(true));
        EditorGUILayout.BeginVertical();
        switch (((Pattern)target).selectedRepetitionsMode)
        {
            case RepetitionsMode.Infinite:
                ((Pattern)target).cycles = -1;
                break;
            case RepetitionsMode.Finite:
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Amount", "The number of times the pattern will play (-1 = Infinite)"), GUILayout.Width(labelWidthInPixels));
                ((Pattern)target).cycles = EditorGUILayout.IntField(((Pattern)target).cycles);
                if (((Pattern)target).cycles <= 0)
                    ((Pattern)target).cycles = 1;
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
#endif
