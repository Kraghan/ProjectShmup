#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEditor.SceneManagement;

namespace ShmupPatternPackage
{
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

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            drawGUI();

            if (!UnityEditor.EditorApplication.isPlaying)
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
            list = new ReorderableList(serializedObject, serializedObject.FindProperty("bursts"), true, true, true, true);

            list.drawHeaderCallback = (Rect rect) =>
            {
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
                if (burstTiming == null)
                    burstTiming = new BurstTiming();
                burstTiming.timing = EditorGUI.FloatField(new Rect(rect.x + handlersWidth / 7, rect.y, timingsWidth, EditorGUIUtility.singleLineHeight), burstTiming.timing);
                burstTiming.burst = EditorGUI.ObjectField(new Rect(rect.x + handlersWidth / 7 + timingsWidth, rect.y, burstsWidth, EditorGUIUtility.singleLineHeight), burstTiming.burst, typeof(Burst), false) as Burst;
                burstTiming.bullet = EditorGUI.ObjectField(new Rect(rect.x + handlersWidth + timingsWidth + burstsWidth, rect.y, bulletsWidth, EditorGUIUtility.singleLineHeight), burstTiming.bullet, typeof(GameObject), false) as GameObject;

            // Burst preview

            Burst burst = burstTiming.burst;

                float spreadangle_1 = 0;
                float spreadangle_2 = 0;
            // Initialization
            float burstPreview_directionCircleSize = 84;
                float burstPreview_directionCircleMargin = 2;
                float burstPreview_dcfsLeft = rect.x + 100;
                rect.y += 20;
                float burstPreview_dcfsTop = rect.y + 2;
                burstPreview_dcfsTop += burstPreview_directionCircleMargin;
                burstPreview_dcfsLeft += burstPreview_directionCircleMargin;
                Rect directionCircleIndicator = new Rect(burstPreview_dcfsLeft, burstPreview_dcfsTop, burstPreview_directionCircleSize, burstPreview_directionCircleSize);

            // Draw Label
            // Burst Preview
            EditorGUI.LabelField(new Rect(rect.x + 4, rect.y + 44, 130, EditorGUIUtility.singleLineHeight), "Burst Preview");

            // Burst Direction

            EditorGUI.LabelField(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, rect.y, 130, EditorGUIUtility.singleLineHeight), "Burst Direction: " + (burstTiming.direction) + "°");
                burstTiming.direction = (int)GUI.HorizontalSlider(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, rect.y + EditorGUIUtility.singleLineHeight + 2, 120, EditorGUIUtility.singleLineHeight), burstTiming.direction, 0, 360);
                burstTiming.direction = (int)EditorGUI.FloatField(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin + 122, rect.y + EditorGUIUtility.singleLineHeight + 2, 84, EditorGUIUtility.singleLineHeight), burstTiming.direction);
                if (GUI.Button(new Rect(burstPreview_dcfsLeft + 10 + 86 + 122 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, rect.y + EditorGUIUtility.singleLineHeight + 2, 40, EditorGUIUtility.singleLineHeight), "+45°"))
                    burstTiming.direction += 45;
                if (GUI.Button(new Rect(burstPreview_dcfsLeft + 10 + 86 + 164 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, rect.y + EditorGUIUtility.singleLineHeight + 2, 40, EditorGUIUtility.singleLineHeight), "-45°"))
                    burstTiming.direction -= 45;
                Utility.Cap(ref burstTiming.direction, 0, 360);

                // Aim Mode
                EditorGUI.LabelField(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, rect.y + (EditorGUIUtility.singleLineHeight * 3) + 2, 100, EditorGUIUtility.singleLineHeight), "Aim Mode");
                burstTiming.aimMode = (AimMode)EditorGUI.EnumPopup(new Rect(102 + burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, rect.y + (EditorGUIUtility.singleLineHeight * 3) + 2, 120, EditorGUIUtility.singleLineHeight), burstTiming.aimMode);
                switch (burstTiming.aimMode)
                {
                    case AimMode.WorldSpace:
                        EditorGUI.LabelField(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, rect.y + (EditorGUIUtility.singleLineHeight * 4) + 2, 300, EditorGUIUtility.singleLineHeight), "The right on the radar is the right in the world.");
                        break;
                    case AimMode.Targetted:
                        EditorGUI.LabelField(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, rect.y + (EditorGUIUtility.singleLineHeight * 4) + 2, 300, EditorGUIUtility.singleLineHeight), "The right on the radar is a straight line to the target.");
                        break;
                }

                // Draw Direction Circle
                Texture2D burstPreview_directionCircleTexture = (Texture2D)EditorGUIUtility.Load("Assets/ShmupPatternPackage/EditorSprites/Radar.png");
                GUI.DrawTexture(directionCircleIndicator, burstPreview_directionCircleTexture);

            // Draw Bullet Lines
            if (burst != null)
                {
                    if (burst.shoots != null)
                    {
                        if (burst.shoots.Count > 0)
                        {
                            Handles.BeginGUI();
                            Handles.color = Color.red;
                            foreach (Shoot shoot in burst.shoots)
                            {
                                if (shoot == null)
                                    break;
                                float angleRadian = (burstTiming.direction + shoot.direction) * Mathf.PI / 180.0f;
                                float destinationX = Mathf.Cos(angleRadian) * (burstPreview_directionCircleSize / 2);
                                float destinationY = Mathf.Sin(angleRadian) * (burstPreview_directionCircleSize / 2);
                                Vector3 directionLineOrigin = new Vector3(directionCircleIndicator.x + (directionCircleIndicator.width / 2), directionCircleIndicator.y + (directionCircleIndicator.height / 2), 0);
                                Vector3 directionLineDestination = new Vector3(directionLineOrigin.x + destinationX, directionLineOrigin.y + destinationY, 0);
                                Handles.DrawLine(directionLineOrigin, directionLineDestination);
                            }
                            Handles.EndGUI();
                        }
                    }
                }

            // Draw Spread Lines
            Handles.BeginGUI();
                Handles.color = Color.yellow;
                float burstDir_angleRadian = burstTiming.direction * Mathf.PI / 180.0f;
                float burstDir_destinationX = Mathf.Cos(burstDir_angleRadian) * (burstPreview_directionCircleSize / 2);
                float burstDir_destinationY = Mathf.Sin(burstDir_angleRadian) * (burstPreview_directionCircleSize / 2);
                Vector3 burstDir_directionLineOrigin = new Vector3(directionCircleIndicator.x + (directionCircleIndicator.width / 2), directionCircleIndicator.y + (directionCircleIndicator.height / 2), 0);
                Vector3 burstDir_directionLineDestination = new Vector3(burstDir_directionLineOrigin.x + burstDir_destinationX, burstDir_directionLineOrigin.y + burstDir_destinationY, 0);
                Handles.DrawLine(burstDir_directionLineOrigin, burstDir_directionLineDestination);

                Handles.color = Color.blue;
                spreadangle_1 = (burstTiming.direction + (burst.spread / 2));
                spreadangle_2 = (burstTiming.direction + (-(burst.spread / 2)));
                float burstSpread1_angleRadian = spreadangle_1 * Mathf.PI / 180.0f;
                float burstSpread1_destinationX = Mathf.Cos(burstSpread1_angleRadian) * (burstPreview_directionCircleSize / 2);
                float burstSpread1_destinationY = Mathf.Sin(burstSpread1_angleRadian) * (burstPreview_directionCircleSize / 2);
                Vector3 burstSpread1_directionLineOrigin = new Vector3(directionCircleIndicator.x + (directionCircleIndicator.width / 2), directionCircleIndicator.y + (directionCircleIndicator.height / 2), 0);
                Vector3 burstSpread1_directionLineDestination = new Vector3(burstSpread1_directionLineOrigin.x + burstSpread1_destinationX, burstSpread1_directionLineOrigin.y + burstSpread1_destinationY, 0);
                Handles.DrawLine(burstSpread1_directionLineOrigin, burstSpread1_directionLineDestination);
                float burstSpread2_angleRadian = spreadangle_2 * Mathf.PI / 180.0f;
                float burstSpread2_destinationX = Mathf.Cos(burstSpread2_angleRadian) * (burstPreview_directionCircleSize / 2);
                float burstSpread2_destinationY = Mathf.Sin(burstSpread2_angleRadian) * (burstPreview_directionCircleSize / 2);
                Vector3 burstSpread2_directionLineOrigin = new Vector3(directionCircleIndicator.x + (directionCircleIndicator.width / 2), directionCircleIndicator.y + (directionCircleIndicator.height / 2), 0);
                Vector3 burstSpread2_directionLineDestination = new Vector3(burstSpread2_directionLineOrigin.x + burstSpread2_destinationX, burstSpread2_directionLineOrigin.y + burstSpread2_destinationY, 0);
                Handles.DrawLine(burstSpread2_directionLineOrigin, burstSpread2_directionLineDestination);
                Handles.EndGUI();

                spreadangle_1 %= 360;
                spreadangle_2 = 360 + spreadangle_2;
                spreadangle_2 %= 360;
                spreadangle_1 = (int)spreadangle_1;
                spreadangle_2 = (int)spreadangle_2;

            // End Burst Preview

            burstTiming.burst = burst;

                ((Pattern)target).bursts[index] = burstTiming;
            };

            list.elementHeightCallback = (index) =>
            {
                Repaint();
                return (EditorGUIUtility.singleLineHeight + 110 + 2);
            };
        }

        public void drawGUI()
        {
            GUILayout.BeginVertical("ShurikenEffectBg", new GUILayoutOption[0]);
            EditorGUILayout.BeginVertical();
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
}
#endif
