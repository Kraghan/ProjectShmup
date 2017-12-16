using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Burst), true)]
public class BurstEditor : Editor
{
    private GameObject genericBulletPrefab;
    private GUIStyle modulePadding = new GUIStyle();
    private ReorderableList list;

    private float genericSpeed;
    private float genericAcceleration;
    private float genericDirection;
    private float genericRotation;

    private float handlersWidth;
    private float speedsWidth;
    private float accelerationsWidth;
    private float rotationsWidth;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        drawGUI();

        serializedObject.ApplyModifiedProperties();
    }

    void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("shoots"), true, true, true, true);

        list.drawHeaderCallback = (Rect rect) => {
            float headerHandlersWidth = (rect.width / 10 * 0.5f);
            float headerSpeedsWidth = (rect.width / 10 * 9.5f / 3);
            float headerAccelerationsWidth = (rect.width / 10 * 9.5f / 3);
            float headerRotationsWidth = (rect.width / 10 * 9.5f / 3);

            EditorGUI.LabelField(new Rect(rect.x + headerHandlersWidth, rect.y, headerSpeedsWidth, rect.height), "Speeds");
            EditorGUI.LabelField(new Rect(rect.x + headerHandlersWidth + headerSpeedsWidth, rect.y, headerAccelerationsWidth, rect.height), "Accelerations");
            EditorGUI.LabelField(new Rect(rect.x + headerHandlersWidth + headerSpeedsWidth + headerAccelerationsWidth, rect.y, headerRotationsWidth, rect.height), "Rotations");
        };


        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            Shoot shoot = ((Burst)target).shoots[index];
            if(shoot == null)
            {
                shoot = new Shoot();
            }
            Rect shootBoxOutline = new Rect(rect.x + 2, rect.y + 2, rect.width - 4, rect.height - 12);
            EditorGUI.DrawRect(shootBoxOutline, new Color(0.6f, 0.6f, 0.6f, 1));
            Rect shootBox = new Rect(rect.x + 4, rect.y + 4, rect.width - 8, rect.height - 16);
            EditorGUI.DrawRect(shootBox, new Color(0.8f, 0.8f, 0.9f, 1));
            handlersWidth = (shootBox.width / 10 * 0.5f);
            speedsWidth = (shootBox.width / 10 * 9.5f / 3);
            accelerationsWidth = (shootBox.width / 10 * 9.5f / 3);
            rotationsWidth = (shootBox.width / 10 * 9.5f / 3);
            EditorGUI.LabelField(new Rect(shootBox.x + handlersWidth / 7, shootBox.y, speedsWidth, shootBox.height), EditorGUIUtility.IconContent("Assets/Graphics/Sprites/Editor_Icons/CustomEditor_Icon_Speed.png", "Speed"));
            EditorGUI.LabelField(new Rect(shootBox.x + handlersWidth / 7 + speedsWidth, shootBox.y, accelerationsWidth, shootBox.height), EditorGUIUtility.IconContent("Assets/Graphics/Sprites/Editor_Icons/CustomEditor_Icon_Acceleration.png", "Acceleration"));
            EditorGUI.LabelField(new Rect(shootBox.x + handlersWidth / 7 + speedsWidth + accelerationsWidth, shootBox.y, rotationsWidth, shootBox.height), EditorGUIUtility.IconContent("Assets/Graphics/Sprites/Editor_Icons/CustomEditor_Icon_Rotation.png", "Rotation"));

            shoot.speed = EditorGUI.FloatField(new Rect(shootBox.x + 5 + handlersWidth / 7, shootBox.y + EditorGUIUtility.singleLineHeight, speedsWidth, EditorGUIUtility.singleLineHeight), shoot.speed);
            shoot.acceleration = EditorGUI.FloatField(new Rect(shootBox.x + 5 + handlersWidth / 7 + speedsWidth, shootBox.y + EditorGUIUtility.singleLineHeight, accelerationsWidth, EditorGUIUtility.singleLineHeight), shoot.acceleration);
            shoot.rotation = EditorGUI.FloatField(new Rect(shootBox.x + 5 + handlersWidth / 7 + speedsWidth + accelerationsWidth, shootBox.y + EditorGUIUtility.singleLineHeight, rotationsWidth, EditorGUIUtility.singleLineHeight), shoot.rotation);

            EditorGUI.LabelField(new Rect(shootBox.x + 45, shootBox.y + EditorGUIUtility.singleLineHeight * 2 + 2, shootBox.width, shootBox.height), new GUIContent("Direction (angle de départ en degrés):", "Direction"));

            float dboSize = 84;
            float dbSize = 80;
            float leftMargin = shootBox.x + handlersWidth / 7 + 5;
            float topMargin = shootBox.y + EditorGUIUtility.singleLineHeight * 3 + 4;
            float dboMargin = 2;
            float dbMargin = 5;
            float buttonSize = 20;
            /*
            Rect directionBoxOutline = new Rect(leftMargin, topMargin, dboSize, dboSize);
            EditorGUI.DrawRect(directionBoxOutline, new Color(0.6f, 0.6f, 0.6f, 1));
            Rect directionBox = new Rect(leftMargin + dboMargin, topMargin + dboMargin, dbSize, dbSize);
            EditorGUI.DrawRect(directionBox, new Color(0.8f, 0.6f, 0.6f, 1));
            */
            Texture2D directionBoxTexture = (Texture2D)EditorGUIUtility.Load("Assets/Graphics/Sprites/Editor_Icons/DirectionBox.png");
            GUI.DrawTexture(new Rect(leftMargin, topMargin, dboSize, dboSize), directionBoxTexture);

            float buttonLeftMargin = leftMargin + dboMargin + dbMargin;
            float buttonTopMargin = topMargin + dboMargin + dbMargin;
            GUI.Button(new Rect(buttonLeftMargin, buttonTopMargin, buttonSize, buttonSize), "↖");
            GUI.Button(new Rect(buttonLeftMargin + buttonSize + dbMargin, buttonTopMargin, buttonSize, buttonSize), "↑");
            GUI.Button(new Rect(buttonLeftMargin + buttonSize + dbMargin + buttonSize + dbMargin, buttonTopMargin, buttonSize, buttonSize), "↗");

            GUI.Button(new Rect(buttonLeftMargin, buttonTopMargin + buttonSize + dbMargin, buttonSize, buttonSize), "←");
            GUI.Button(new Rect(buttonLeftMargin + buttonSize + dbMargin + buttonSize + dbMargin, buttonTopMargin + buttonSize + dbMargin, buttonSize, buttonSize), "→");

            GUI.Button(new Rect(buttonLeftMargin, buttonTopMargin + buttonSize + dbMargin + buttonSize + dbMargin, buttonSize, buttonSize), "↙");
            GUI.Button(new Rect(buttonLeftMargin + buttonSize + dbMargin, buttonTopMargin + buttonSize + dbMargin + buttonSize + dbMargin, buttonSize, buttonSize), "↓");
            GUI.Button(new Rect(buttonLeftMargin + buttonSize + dbMargin + buttonSize + dbMargin, buttonTopMargin + buttonSize + dbMargin + buttonSize + dbMargin, buttonSize, buttonSize), "↘");

            float leftMargin2 = leftMargin + dboSize + 2;

            shoot.direction = EditorGUI.FloatField(new Rect(leftMargin2, buttonTopMargin + buttonSize + dbMargin, shootBox.width - leftMargin2, EditorGUIUtility.singleLineHeight), shoot.direction);
            
            ((Burst)target).shoots[index] = shoot;
        };
            
        list.elementHeightCallback = (index) => {
            Repaint();
            return (EditorGUIUtility.singleLineHeight * 3 + 120);
        };
    }

    public void drawGUI()
    {
        GUILayout.BeginVertical("ShurikenEffectBg", new GUILayoutOption[0]);
        EditorGUILayout.BeginVertical();
        GUIContent content = new GUIContent();
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
        // int labelWidthInPixels = 100;

        // Display stuff here
        
        //EditorGUILayout.DropdownButton(new GUIContent())

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        list.DoLayoutList();

        // End display stuff
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        GUILayout.EndVertical();
    }

    public static void DrawTexture(Rect position, Sprite sprite, Vector2 size)
    {
        Rect spriteRect = new Rect(sprite.rect.x / sprite.texture.width, sprite.rect.y / sprite.texture.height,
                                   sprite.rect.width / sprite.texture.width, sprite.rect.height / sprite.texture.height);
        Vector2 actualSize = size;

        actualSize.y *= (sprite.rect.height / sprite.rect.width);
        Graphics.DrawTexture(new Rect(position.x, position.y + (size.y - actualSize.y) / 2, actualSize.x, actualSize.y), sprite.texture, spriteRect, 0, 0, 0, 0);
    }

    public static void DrawTextureGUI(Rect position, Sprite sprite, Vector2 size)
    {
        Rect spriteRect = new Rect(sprite.rect.x / sprite.texture.width, sprite.rect.y / sprite.texture.height,
                                   sprite.rect.width / sprite.texture.width, sprite.rect.height / sprite.texture.height);
        Vector2 actualSize = size;

        actualSize.y *= (sprite.rect.height / sprite.rect.width);
        GUI.DrawTextureWithTexCoords(new Rect(position.x, position.y + (size.y - actualSize.y) / 2, actualSize.x, actualSize.y), sprite.texture, spriteRect);
    }
}
