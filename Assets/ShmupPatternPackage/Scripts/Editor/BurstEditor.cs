#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;

namespace ShmupPatternPackage
{
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

        private float spreadangle_1;
        private float spreadangle_2;

        private string arrow_1 = "↖";
        private string arrow_2 = "▲";
        private string arrow_3 = "↗";

        private string arrow_4 = "◄";
        private string arrow_5 = "►";

        private string arrow_6 = "↙";
        private string arrow_7 = "▼";
        private string arrow_8 = "↘";

        private float newShootCount;

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

            list.drawHeaderCallback = (Rect rect) =>
            {
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
            string displayModeButtonString = (shoot.largerDisplayInEditor ? "Hide" : "Display");// "[" + index + "] - " + (shoot.largerDisplayInEditor ? "Hide": "Display") + "\n";
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
                    EditorGUI.LabelField(headerSpeedPic, EditorGUIUtility.IconContent("Assets/ShmupPatternPackage/EditorSprites/Icon_Speed.png", "Speed"));
                    EditorGUI.LabelField(headerAccelerationPic, EditorGUIUtility.IconContent("Assets/ShmupPatternPackage/EditorSprites/Icon_Acceleration.png", "Acceleration"));
                    EditorGUI.LabelField(headerRotationPic, EditorGUIUtility.IconContent("Assets/ShmupPatternPackage/EditorSprites/Icon_Rotation.png", "Rotation"));

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
                Texture2D directionCircleTexture = (Texture2D)EditorGUIUtility.Load("Assets/ShmupPatternPackage/EditorSprites/Radar.png");
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

            list.elementHeightCallback = (index) =>
            {
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
            float burstPreview_dcfsLeft = position.x + 100;
            float burstPreview_dcfsTop = position.y + 2;
            burstPreview_dcfsTop += burstPreview_directionCircleMargin;
            burstPreview_dcfsLeft += burstPreview_directionCircleMargin;
            Rect directionCircleIndicator = new Rect(burstPreview_dcfsLeft, burstPreview_dcfsTop, burstPreview_directionCircleSize, burstPreview_directionCircleSize);

            // Draw Label
            // Burst Preview
            EditorGUI.LabelField(new Rect(position.x + 4, position.y + 44, 130, EditorGUIUtility.singleLineHeight), "Burst Preview");

            // Burst Direction
            /*
            EditorGUI.LabelField(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, position.y, 130, EditorGUIUtility.singleLineHeight), "Burst Direction: " + (0) + "°");
            0 = (int)GUI.HorizontalSlider(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, position.y + EditorGUIUtility.singleLineHeight + 2, 120, EditorGUIUtility.singleLineHeight), 0, 0, 360);
            0 = (int)EditorGUI.FloatField(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin + 122, position.y + EditorGUIUtility.singleLineHeight + 2, 84, EditorGUIUtility.singleLineHeight), 0);
            if (GUI.Button(new Rect(burstPreview_dcfsLeft + 10 + 86 + 122 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, position.y + EditorGUIUtility.singleLineHeight + 2, 40, EditorGUIUtility.singleLineHeight), "+45°"))
                0 += 45;
            if (GUI.Button(new Rect(burstPreview_dcfsLeft + 10 + 86 + 164 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, position.y + EditorGUIUtility.singleLineHeight + 2, 40, EditorGUIUtility.singleLineHeight), "-45°"))
                0 -= 45;
            Utility.Cap(ref 0, 0, 360);*/

            // Burst Spread
            EditorGUI.LabelField(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, position.y + 4 + 2 * EditorGUIUtility.singleLineHeight, 130, EditorGUIUtility.singleLineHeight), "Burst Spread: " + (((Burst)target).spread) + "°");
            ((Burst)target).spread = (int)GUI.HorizontalSlider(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, position.y + 3 * EditorGUIUtility.singleLineHeight + 6, 120, EditorGUIUtility.singleLineHeight), ((Burst)target).spread, 0, 360);
            ((Burst)target).spread = (int)EditorGUI.FloatField(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin + 122, position.y + 3 * EditorGUIUtility.singleLineHeight + 2, 84, EditorGUIUtility.singleLineHeight), ((Burst)target).spread);
            if (GUI.Button(new Rect(burstPreview_dcfsLeft + 10 + 86 + 122 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, position.y + 3 * EditorGUIUtility.singleLineHeight + 2, 40, EditorGUIUtility.singleLineHeight), "+45°"))
                ((Burst)target).spread += 45;
            if (GUI.Button(new Rect(burstPreview_dcfsLeft + 10 + 86 + 164 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, position.y + 3 * EditorGUIUtility.singleLineHeight + 2, 40, EditorGUIUtility.singleLineHeight), "-45°"))
                ((Burst)target).spread -= 45;
            Utility.Cap(ref ((Burst)target).spread, 0, 360);

            // Modify Shoots by spread
            if (GUI.Button(new Rect(burstPreview_dcfsLeft + 10 + burstPreview_directionCircleSize + burstPreview_directionCircleMargin, position.y + 4 * EditorGUIUtility.singleLineHeight + 8, 180, EditorGUIUtility.singleLineHeight), "Align all shoots on spread"))
            {
                // Draw Bullet Lines
                if ((Burst)target != null)
                {
                    if (((Burst)target).shoots != null)
                    {
                        if (((Burst)target).shoots.Count > 0)
                        {
                            float resetSpreadAngle1 = 0;
                            float resetSpreadAngle2 = 0;
                            resetSpreadAngle1 = ((Burst)target).spread / 2;
                            resetSpreadAngle2 = (-(((Burst)target).spread / 2));
                            resetSpreadAngle1 %= 360;
                            resetSpreadAngle2 = 360 + resetSpreadAngle2;
                            resetSpreadAngle2 %= 360;
                            resetSpreadAngle1 = (int)resetSpreadAngle1;
                            resetSpreadAngle2 = (int)resetSpreadAngle2;

                            int shootCount = ((Burst)target).shoots.Count;

                            for (int shootID = 0; shootID < shootCount; shootID++)
                            {
                                Shoot currentShoot = ((Burst)target).shoots[shootID];
                                //V1
                                /*
                                if ((currentShoot.direction > resetSpreadAngle1) && (currentShoot.direction < resetSpreadAngle2))
                                {
                                    if (currentShoot.direction <= (resetSpreadAngle1 + (resetSpreadAngle2 - resetSpreadAngle1) / 2))
                                        currentShoot.direction = resetSpreadAngle1;
                                    else
                                        currentShoot.direction = resetSpreadAngle2;
                                }*/
                                //V2
                                if (shootCount == 1)
                                    currentShoot.direction = 0;
                                else if (shootCount % 2 == 0)
                                {
                                    //float angleSize = (resetSpreadAngle1 + (resetSpreadAngle2 - resetSpreadAngle1) / 2) / shootCount;
                                    float angleSize = ((Burst)target).spread / shootCount;
                                    int a = (shootCount / 2) - 1;
                                    if (shootID <= a)
                                    {
                                        currentShoot.direction = ((int)((angleSize + (angleSize * shootID)))) % 360;
                                    }
                                    else
                                    {
                                        int esperance = (int)((-(angleSize * (shootID - a))));
                                        while (esperance < 0)
                                            esperance += 360;
                                        currentShoot.direction = esperance % 360;
                                    }
                                }
                                else
                                {
                                    float angleSize = ((Burst)target).spread / (shootCount-1);
                                    int a = (shootCount - 1) / 2;
                                    if (shootID < a)
                                    {
                                        currentShoot.direction = ((int)((angleSize + (angleSize * shootID)))) % 360;
                                    }
                                    else if (shootID > a)
                                    {
                                        int esperance = (int)((-(angleSize * (shootID - a))));
                                        while (esperance < 0)
                                            esperance += 360;
                                        currentShoot.direction = esperance % 360;
                                    }
                                    else
                                    {
                                        currentShoot.direction = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }


            // Draw Direction Circle
            Texture2D burstPreview_directionCircleTexture = (Texture2D)EditorGUIUtility.Load("Assets/ShmupPatternPackage/EditorSprites/Radar.png");
            GUI.DrawTexture(directionCircleIndicator, burstPreview_directionCircleTexture);

            // Draw Spread Lines
            Handles.BeginGUI();
            Handles.color = Color.yellow;
            float burstDir_angleRadian = 0 * Mathf.PI / 180.0f;
            float burstDir_destinationX = Mathf.Cos(burstDir_angleRadian) * (burstPreview_directionCircleSize / 2);
            float burstDir_destinationY = Mathf.Sin(burstDir_angleRadian) * (burstPreview_directionCircleSize / 2);
            Vector3 burstDir_directionLineOrigin = new Vector3(directionCircleIndicator.x + (directionCircleIndicator.width / 2), directionCircleIndicator.y + (directionCircleIndicator.height / 2), 0);
            Vector3 burstDir_directionLineDestination = new Vector3(burstDir_directionLineOrigin.x + burstDir_destinationX, burstDir_directionLineOrigin.y + burstDir_destinationY, 0);
            Handles.DrawLine(burstDir_directionLineOrigin, burstDir_directionLineDestination);

            Handles.color = Color.blue;
            spreadangle_1 = (0 + (((Burst)target).spread / 2));
            spreadangle_2 = (0 + (-(((Burst)target).spread / 2)));
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

            // Draw Bullet Lines
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
                            float angleRadian = (0 + shoot.direction) * Mathf.PI / 180.0f;
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
                Texture2D directionCircleTexture = (Texture2D)EditorGUIUtility.Load("Assets/ShmupPatternPackage/EditorSprites/Radar.png");
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

                EditorGUI.LabelField(new Rect(position.x + 4 + (directionCircleSize*3) + 4, position.y + 142 + 88, 100, EditorGUIUtility.singleLineHeight), "Shoot amount:");
                newShootCount = (int)EditorGUI.FloatField(new Rect(102 + position.x + 4 + (directionCircleSize*3) + 4, position.y + 142 + 88, 100, EditorGUIUtility.singleLineHeight), newShootCount);
                if (newShootCount < 0)
                    newShootCount = 0;
                if (GUI.Button(new Rect(position.x + 4 + (directionCircleSize * 3) + 4, position.y + 142 + 88 + EditorGUIUtility.singleLineHeight, 180, EditorGUIUtility.singleLineHeight), "Set shoot count"))
                {
                    if (newShootCount >= 0 && newShootCount != ((Burst)target).shoots.Count)
                    {
                        ((Burst)target).shoots = new List<Shoot>();
                        for (int i = 0; i < newShootCount; i++)
                            ((Burst)target).shoots.Add(new Shoot());
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
}
#endif

