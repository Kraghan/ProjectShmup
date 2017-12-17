#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;

[CustomEditor(typeof(Burst), true)]
public class BurstEditor : Editor
{
    private ReorderableList list;
    private GameObject genericBulletPrefab;
    private GUIStyle modulePadding = new GUIStyle();

    private bool displayGenericParameters = true;
    private float genericSpeed;
    private float genericAcceleration;
    private float genericDirection;
    private float genericRotation;

    private string arrow_1 = "↖";
    private string arrow_2 = "▲";
    private string arrow_3 = "↗";

    private string arrow_4 = "◄";
    private string arrow_5 = "►";

    private string arrow_6 = "↙";
    private string arrow_7 = "▼";
    private string arrow_8 = "↘";

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        drawGUI();

        if (!UnityEditor.EditorApplication.isPlaying)
        {
            EditorUtility.SetDirty((Burst)target);
        }

        serializedObject.ApplyModifiedProperties();

        if (!UnityEditor.EditorApplication.isPlaying)
        {
            EditorUtility.SetDirty((Burst)target);
        }
    }

    void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("shoots"), true, true, true, false);

        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, rect.height), "Shoots");
        };


        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            // Get Shoot
            Shoot shoot = ((Burst)target).shoots[index];
            if (shoot == null)
                shoot = new Shoot();
            //Shoot shoot;
            bool willBeDeleted = false;

            // Initialization
            float left = rect.x;
            float fullWidth = rect.width;
            float top = rect.y;
            float fullHeight = rect.height;
            float lineHeight = EditorGUIUtility.singleLineHeight;
            Color greyUI = new Color(0.6f, 0.6f, 0.6f, 1);
            float spaceWidth = 0;
            float speedWidth = 0;
            float accelerationWidth = 0;
            float rotationWidth = 0;

            float debug_requieredPixels = 4;
            float debug_requieredLines = 0;

            #region Draw DisplayModeButton
            string displayModeButtonString = "";// "[" + index + "] - " + (shoot.largerDisplayInEditor ? "Hide": "Display") + "\n";
            #region Find Arrow
            string arrow = "";
            switch ((int)shoot.direction)
            {
                case 0:
                    arrow = arrow_5;
                    break;
                case 45:
                    arrow = arrow_8;
                    break;
                case 90:
                    arrow = arrow_7;
                    break;
                case 135:
                    arrow = arrow_6;
                    break;
                case 180:
                    arrow = arrow_4;
                    break;
                case 225:
                    arrow = arrow_1;
                    break;
                case 270:
                    arrow = arrow_2;
                    break;
                case 315:
                    arrow = arrow_3;
                    break;
                case 360:
                    arrow = arrow_5;
                    break;
                default:
                    arrow = "(" + ((int)shoot.direction).ToString() + "°)";
                    break;
            }
            #endregion
            displayModeButtonString += arrow + " - " + shoot.speed + " speed";
            if (GUI.Button(new Rect(left, top + 2, fullWidth - 40, lineHeight * 2), displayModeButtonString)) { shoot.largerDisplayInEditor = !shoot.largerDisplayInEditor; }
            if (GUI.Button(new Rect(left + (fullWidth - 24), top + 2 + (lineHeight / 2), 20, lineHeight), "X"))
                willBeDeleted = true;
            top += ((lineHeight * 2) + 4);
            fullHeight -= ((lineHeight * 2) + 4);
            debug_requieredLines += 2;
            debug_requieredPixels += 4;
            #endregion

            if (shoot.largerDisplayInEditor)
            {

                #region Draw ShootBox
                // Initialization
                Color shootBoxColorBackground = new Color(0.8f, 0.8f, 0.9f, 1);
                float shootBoxMargin = 2;

                // Reduce space
                left += shootBoxMargin;
                top += shootBoxMargin;
                fullWidth -= shootBoxMargin * 2;
                fullHeight -= shootBoxMargin * 2;
                debug_requieredPixels += shootBoxMargin;

                // Draw outline
                Rect shootBoxOutline = new Rect(left, top, fullWidth, fullHeight);
                EditorGUI.DrawRect(shootBoxOutline, greyUI);

                // Reduce space
                left += shootBoxMargin;
                top += shootBoxMargin;
                fullWidth -= shootBoxMargin * 2;
                fullHeight -= shootBoxMargin * 2;
                debug_requieredPixels += shootBoxMargin;

                // Draw background
                Rect shootBox = new Rect(left, top, fullWidth, fullHeight);
                EditorGUI.DrawRect(shootBox, shootBoxColorBackground);
                #endregion

                #region Draw Headers
                // Set columns width
                spaceWidth = ((fullWidth / 10 * 0.5f) / 7);
                speedWidth = (fullWidth / 10 * 9.5f / 3);
                accelerationWidth = (fullWidth / 10 * 9.5f / 3);
                rotationWidth = (fullWidth / 10 * 9.5f / 3);

                // Draw labels
                Rect headerSpeed = new Rect(left + spaceWidth, top, speedWidth, lineHeight);
                Rect headerAcceleration = new Rect(left + spaceWidth + speedWidth, top, accelerationWidth, lineHeight);
                Rect headerRotation = new Rect(left + spaceWidth + speedWidth + accelerationWidth, top, rotationWidth, lineHeight);
                EditorGUI.LabelField(headerSpeed, "Speed");
                EditorGUI.LabelField(headerAcceleration, "Acceleration");
                EditorGUI.LabelField(headerRotation, "Rotation");

                // Reduce space
                top += lineHeight;
                fullHeight -= lineHeight;
                debug_requieredLines++;

                // Draw pics
                Rect headerSpeedPic = new Rect(left + spaceWidth, top, speedWidth, lineHeight);
                Rect headerAccelerationPic = new Rect(left + spaceWidth + speedWidth, top, accelerationWidth, lineHeight);
                Rect headerRotationPic = new Rect(left + spaceWidth + speedWidth + accelerationWidth, top, rotationWidth, lineHeight);
                EditorGUI.LabelField(headerSpeedPic, EditorGUIUtility.IconContent("Assets/Graphics/Sprites/Editor_Icons/CustomEditor_Icon_Speed.png", "Speed"));
                EditorGUI.LabelField(headerAccelerationPic, EditorGUIUtility.IconContent("Assets/Graphics/Sprites/Editor_Icons/CustomEditor_Icon_Acceleration.png", "Acceleration"));
                EditorGUI.LabelField(headerRotationPic, EditorGUIUtility.IconContent("Assets/Graphics/Sprites/Editor_Icons/CustomEditor_Icon_Rotation.png", "Rotation"));

                // Reduce space
                top += lineHeight;
                fullHeight -= lineHeight;
                debug_requieredLines++;

                // Draw line
                //EditorGUI.DrawRect(new Rect(left, top, fullWidth, 2), greyUI);
                top += 4;
                fullHeight -= 4;
                debug_requieredPixels += 4;
                #endregion

                #region Draw FloatFields (Speed, Acceleration, Rotation)
                // Initialization
                float floatFieldsMargin = 5;

                // Draw fields
                shoot.speed = (int)(EditorGUI.FloatField(new Rect(left + floatFieldsMargin, top, speedWidth, lineHeight), shoot.speed));
                if (shoot.speed < 0)
                    shoot.speed = 0;
                shoot.acceleration = (int)(EditorGUI.FloatField(new Rect(left + floatFieldsMargin + speedWidth, top, speedWidth, lineHeight), shoot.acceleration));
                shoot.rotation = (int)(EditorGUI.FloatField(new Rect(left + floatFieldsMargin + speedWidth + accelerationWidth, top, speedWidth, lineHeight), shoot.rotation));

                // Reduce space
                top += lineHeight + 2;
                fullHeight -= lineHeight - 2;
                debug_requieredLines++;
                debug_requieredPixels += 2;
                #endregion

                #region Draw Direction Interface
                // Initialization
                Texture2D directionCircleTexture = (Texture2D)EditorGUIUtility.Load("Assets/Graphics/Sprites/Editor_Icons/DirectionBox2.png");
                float directionCircleSize = 84;
                float buttonSize = 20;
                float directionCircleMargin = 2;

                // Draw line
                EditorGUI.DrawRect(new Rect(left, top, fullWidth, 2), greyUI);
                top += 4;
                fullHeight -= 4;
                debug_requieredPixels += 4;

                // Draw Direction header
                EditorGUI.LabelField(new Rect(left, top, fullWidth, fullHeight), new GUIContent("Direction (angle de départ en degrés):", "Direction"));
                top += lineHeight;
                fullHeight -= lineHeight;
                debug_requieredLines++;

                // Initalization Part II
                float dcLeft = left;
                float dcFullWidth = fullWidth;
                float dcTop = top;
                float dcFullHeight = 84 + directionCircleMargin * 2;

                #region Draw Direction Circle Fast Selector
                // Initialization
                float buttonMargin = 5;
                float dcfsTop = dcTop + directionCircleMargin;
                float dcfsFullHeight = dcFullHeight;
                dcLeft += directionCircleMargin;
                Rect directionCircleFastSelector = new Rect(dcLeft, top, directionCircleSize, directionCircleSize);
                dcLeft += directionCircleMargin + buttonMargin;
                dcFullWidth -= (directionCircleMargin * 2);
                dcfsTop += directionCircleMargin + buttonMargin;
                dcfsFullHeight -= (directionCircleMargin * 2 + buttonMargin);

                // Draw Direction Circle
                GUI.DrawTexture(directionCircleFastSelector, directionCircleTexture);

                // Draw Buttons Line 1
                if (GUI.Button(new Rect(dcLeft + 0 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_1)) { shoot.direction = 225; }
                if (GUI.Button(new Rect(dcLeft + 1 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_2)) { shoot.direction = 270; }
                if (GUI.Button(new Rect(dcLeft + 2 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_3)) { shoot.direction = 315; }
                dcfsTop += buttonSize + buttonMargin;
                dcfsFullHeight -= (buttonSize + buttonMargin);

                // Draw Buttons Line 2
                if (GUI.Button(new Rect(dcLeft + 0 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_4)) { shoot.direction = 180; }
                if (GUI.Button(new Rect(dcLeft + 2 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_5)) { shoot.direction = 0; }
                dcfsTop += buttonSize + buttonMargin;
                dcfsFullHeight -= (buttonSize + buttonMargin);

                // Draw Buttons Line 3
                if (GUI.Button(new Rect(dcLeft + 0 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_6)) { shoot.direction = 135; }
                if (GUI.Button(new Rect(dcLeft + 1 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_7)) { shoot.direction = 90; }
                if (GUI.Button(new Rect(dcLeft + 2 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_8)) { shoot.direction = 45; }
                dcfsTop += buttonSize + buttonMargin;
                dcfsFullHeight -= (buttonSize + buttonMargin);

                // Reduce space
                dcLeft += directionCircleSize + directionCircleMargin * 2 - buttonMargin;
                dcFullWidth -= (directionCircleSize + directionCircleMargin * 2);
                #endregion

                #region Draw Direction Selector
                // Initialization
                float dsTop = top;
                float dsFullWidth = (dcFullWidth - (directionCircleSize - 2 * directionCircleMargin + 8));

                // Draw Slider
                Rect shootDirectionRect = new Rect(dcLeft, dsTop, dsFullWidth, lineHeight);
                shoot.direction = (int)(GUI.HorizontalSlider(shootDirectionRect, shoot.direction, 0.0f, 360.0f));
                dsTop += lineHeight + 2;

                // Draw FloatField
                Rect shootDirectionRect2 = new Rect(dcLeft, dsTop, dsFullWidth, lineHeight);
                shoot.direction = (int)(EditorGUI.FloatField(shootDirectionRect2, shoot.direction));
                Utility.Cap(ref shoot.direction, 0, 360);
                dsTop += lineHeight + 2;

                // Reduce space
                dcLeft += dsFullWidth;
                dcFullWidth -= dsFullWidth;
                #endregion

                #region Draw Direction Circle Indicator
                // Initialization
                float dciTop = dcTop + directionCircleMargin;
                float dciFullHeight = dcFullHeight;
                dcLeft += directionCircleMargin;
                Rect directionCircleIndicator = new Rect(dcLeft, top, directionCircleSize, directionCircleSize);
                dcLeft += directionCircleMargin;
                dcFullWidth -= directionCircleMargin * 2;
                dciTop += directionCircleMargin;
                dciFullHeight -= directionCircleMargin * 2;

                // Draw Direction Circle Indicator
                GUI.DrawTexture(directionCircleIndicator, directionCircleTexture);

                // Draw Direction Indicator Line
                Handles.BeginGUI();
                Handles.color = Color.red;
                float angleRadian = shoot.direction * Mathf.PI / 180.0f;
                float destinationX = Mathf.Cos(angleRadian) * (directionCircleSize / 2);
                float destinationY = Mathf.Sin(angleRadian) * (directionCircleSize / 2);
                Vector3 directionLineOrigin = new Vector3(directionCircleIndicator.x + (directionCircleIndicator.width / 2), directionCircleIndicator.y + (directionCircleIndicator.height / 2), 0);
                Vector3 directionLineDestination = new Vector3(directionLineOrigin.x + destinationX, directionLineOrigin.y + destinationY, 0);
                Handles.DrawLine(directionLineOrigin, directionLineDestination);
                Handles.EndGUI();
                #endregion

                // Reduce space
                top += directionCircleSize + 2 * directionCircleMargin;
                fullHeight -= (directionCircleSize + (2 * directionCircleMargin));
                debug_requieredPixels += directionCircleSize + 2 * directionCircleMargin;
                #endregion

            }

            // Save modifications
            if (((Burst)target).shoots != null)
                if (((Burst)target).shoots.Count > index)
                    ((Burst)target).shoots[index] = shoot;

            if (willBeDeleted)
                ((Burst)target).shoots.RemoveAt(index);
        };

        list.elementHeightCallback = (index) => {
            bool largerDisplay = false;

            // Get Shoot
            if (((Burst)target).shoots != null)
                if (((Burst)target).shoots.Count > index)
                    if (((Burst)target).shoots[index] != null)
                        largerDisplay = ((Burst)target).shoots[index].largerDisplayInEditor;

            Repaint();
            if (largerDisplay)
            {
                return (EditorGUIUtility.singleLineHeight * 6 + 106 + 2 + 8);
            }
            else
            {
                return (EditorGUIUtility.singleLineHeight * 2 + 8);
            }
        };

    }
    
    public void drawGUI()
    {
        GUILayout.BeginVertical("ShurikenEffectBg", new GUILayoutOption[0]);
        EditorGUILayout.BeginVertical();
        modulePadding.padding = new RectOffset(0, 0, 0, 0);
        Rect position = EditorGUILayout.BeginVertical(modulePadding, new GUILayoutOption[0]);
        GUI.Label(position, GUIContent.none, "ShurikenModuleBg");

        float labelWidthInPixels = 140;
        float buttonWidthInPixels = 100;
        // Display stuff here
        // Burst Preview
        // Initialization
        float burstPreview_directionCircleSize = 84;
        float burstPreview_directionCircleMargin = 2;
        float burstPreview_dcfsLeft = position.x + 150;
        float burstPreview_dcfsTop = position.y + 2;
        burstPreview_dcfsTop += burstPreview_directionCircleMargin;
        burstPreview_dcfsLeft += burstPreview_directionCircleMargin;
        Rect directionCircleIndicator = new Rect(burstPreview_dcfsLeft, burstPreview_dcfsTop, burstPreview_directionCircleSize, burstPreview_directionCircleSize);

        // Draw Label
        EditorGUI.LabelField(new Rect(position.x + 4, position.y + 44, 130, EditorGUIUtility.singleLineHeight), "Burst Previsualization");

        // Draw Direction Circle
        Texture2D burstPreview_directionCircleTexture = (Texture2D)EditorGUIUtility.Load("Assets/Graphics/Sprites/Editor_Icons/DirectionBox2.png");
        GUI.DrawTexture(directionCircleIndicator, burstPreview_directionCircleTexture);

        // Draw Lines
        if ((Burst)target != null)
        {
            if (((Burst)target).shoots != null)
            {
                if (((Burst)target).shoots.Count > 0)
                {
                    Handles.BeginGUI();
                    Handles.color = Color.red;
                    foreach (Shoot shoot in ((Burst)target).shoots)
                    {
                        if (shoot == null)
                            break;
                        float angleRadian = shoot.direction * Mathf.PI / 180.0f;
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

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Generic Speed
        displayGenericParameters = EditorGUI.Foldout(new Rect(position.x + 12, position.y + 88, position.width, EditorGUIUtility.singleLineHeight), displayGenericParameters, "Global parameters");
        if (displayGenericParameters)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Generic speed", GUILayout.Width(labelWidthInPixels));
            genericSpeed = (int)EditorGUILayout.FloatField(genericSpeed, GUILayout.ExpandWidth(true));
            if (genericSpeed < 0)
                genericSpeed = 0;
            if (GUILayout.Button("Assign to all", GUILayout.Width(buttonWidthInPixels)))
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.speed = genericSpeed;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            // Generic Acceleration
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Generic acceleration", GUILayout.Width(labelWidthInPixels));
            genericAcceleration = (int)EditorGUILayout.FloatField(genericAcceleration, GUILayout.ExpandWidth(true));
            if (GUILayout.Button("Assign to all", GUILayout.Width(buttonWidthInPixels)))
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.acceleration = genericAcceleration;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            // Generic Rotation
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Generic rotation", GUILayout.Width(labelWidthInPixels));
            genericRotation = (int)EditorGUILayout.FloatField(genericRotation, GUILayout.ExpandWidth(true));
            if (GUILayout.Button("Assign to all", GUILayout.Width(buttonWidthInPixels)))
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.rotation = genericRotation;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            #region Draw Direction Circle Fast Selector
            // Initialization
            float directionCircleSize = 84;
            float directionCircleMargin = 2;
            float dcfsLeft = position.x + 150;
            float dcfsTop = position.y + 188;
            float buttonMargin = 5;
            float buttonSize = 20;
            dcfsTop += directionCircleMargin;
            dcfsLeft += directionCircleMargin;
            Rect directionCircleFastSelector = new Rect(dcfsLeft, dcfsTop, directionCircleSize, directionCircleSize);
            dcfsLeft += directionCircleMargin + buttonMargin;
            dcfsTop += directionCircleMargin + buttonMargin;

            // Draw Label
            EditorGUI.LabelField(new Rect(position.x + 4, position.y + 142 + 88, 130, EditorGUIUtility.singleLineHeight), "Generic direction");

            // Draw Direction Circle
            Texture2D directionCircleTexture = (Texture2D)EditorGUIUtility.Load("Assets/Graphics/Sprites/Editor_Icons/DirectionBox2.png");
            GUI.DrawTexture(directionCircleFastSelector, directionCircleTexture);

            // Draw Buttons Line 1
            if (GUI.Button(new Rect(dcfsLeft + 0 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_1))
            {
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.direction = 225;
            }
            if (GUI.Button(new Rect(dcfsLeft + 1 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_2))
            {
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.direction = 270;
            }
            if (GUI.Button(new Rect(dcfsLeft + 2 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_3))
            {
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.direction = 315;
            }
            dcfsTop += buttonSize + buttonMargin;

            // Draw Buttons Line 2
            if (GUI.Button(new Rect(dcfsLeft + 0 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_4))
            {
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.direction = 180;
            }
            if (GUI.Button(new Rect(dcfsLeft + 2 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_5))
            {
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.direction = 0;
            }
            dcfsTop += buttonSize + buttonMargin;

            // Draw Buttons Line 3
            if (GUI.Button(new Rect(dcfsLeft + 0 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_6))
            {
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.direction = 135;
            }
            if (GUI.Button(new Rect(dcfsLeft + 1 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_7))
            {
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.direction = 90;
            }
            if (GUI.Button(new Rect(dcfsLeft + 2 * (buttonSize + buttonMargin), dcfsTop, buttonSize, buttonSize), arrow_8))
            {
                foreach (Shoot shoot in ((Burst)target).shoots)
                    shoot.direction = 45;
            }
            dcfsTop += buttonSize + buttonMargin;
            #endregion

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Shoots list
        /*
        if(GUILayout.Button("Add Shoot"))
        {
            ((Burst)target).shoots.Add(new Shoot());
        }
        Rect r;
        if(displayGenericParameters)
            r = new Rect(4, 0, 200, 200);
        else
            r = new Rect(4, 0, 200, 200);
        scrool = EditorGUILayout.BeginScrollView(scrool);
        DrawList(r);
        EditorGUILayout.EndScrollView();*/
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
#endif

