﻿using UnityEngine;
using System.Collections;
using System;

public class ScenesWindowActiveAreas : LayoutWindow, DialogReceiverInterface
{

    private Texture2D backgroundPreviewTex = null;
    private Texture2D conditionTex = null;

    private Texture2D addTexture = null;
    private Texture2D moveUp, moveDown = null;
    private Texture2D clearImg = null;
    private Texture2D duplicateImg = null;

    private string backgroundPath = "";

    private static float windowWidth, windowHeight;
    private static Rect tableRect;
    private static Rect previewRect;
    private static Rect infoPreviewRect;
    private Rect rightPanelRect;

    private static Vector2 scrollPosition;

    private static GUISkin selectedAreaSkin;
    private static GUISkin defaultSkin;
    private static GUISkin noBackgroundSkin;

    private int selectedArea;

    public ScenesWindowActiveAreas(Rect aStartPos, GUIContent aContent, GUIStyle aStyle,
        params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        clearImg = (Texture2D) Resources.Load("EAdventureData/img/icons/deleteContent", typeof (Texture2D));
        addTexture = (Texture2D) Resources.Load("EAdventureData/img/icons/addNode", typeof (Texture2D));
        moveUp = (Texture2D) Resources.Load("EAdventureData/img/icons/moveNodeUp", typeof (Texture2D));
        moveDown = (Texture2D) Resources.Load("EAdventureData/img/icons/moveNodeDown", typeof (Texture2D));
        duplicateImg = (Texture2D) Resources.Load("EAdventureData/img/icons/duplicateNode", typeof (Texture2D));

        windowWidth = aStartPos.width;
        windowHeight = aStartPos.height;

        if (GameRources.GetInstance().selectedSceneIndex >= 0)
            backgroundPath =
                Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getPreviewBackground();
        if (backgroundPath != null && !backgroundPath.Equals(""))
            backgroundPreviewTex =
                (Texture2D)
                    Resources.Load(backgroundPath.Substring(0, backgroundPath.LastIndexOf(".")), typeof (Texture2D));

        conditionTex = (Texture2D) Resources.Load("EAdventureData/img/icons/no-conditions-24x24", typeof (Texture2D));

        //TODO: do new skin?
        selectedAreaSkin = (GUISkin) Resources.Load("Editor/EditorLeftMenuItemSkinConcreteOptions", typeof (GUISkin));
        noBackgroundSkin = (GUISkin) Resources.Load("Editor/EditorNoBackgroundSkin", typeof (GUISkin));

        tableRect = new Rect(0f, 0.1f*windowHeight, 0.9f*windowWidth, windowHeight*0.33f);
        rightPanelRect = new Rect(0.9f*windowWidth, 0.1f*windowHeight, 0.08f*windowWidth, 0.33f*windowHeight);
        infoPreviewRect = new Rect(0f, 0.45f*windowHeight, windowWidth, windowHeight*0.05f);
        previewRect = new Rect(0f, 0.5f*windowHeight, windowWidth, windowHeight*0.45f);

        selectedArea = 0;
    }

    public override void Draw(int aID)
    {
        GUILayout.BeginArea(tableRect);
        GUILayout.BeginHorizontal();
        GUILayout.Box("ID", GUILayout.Width(windowWidth*0.54f));
        GUILayout.Box("Conditions", GUILayout.Width(windowWidth*0.14f));
        GUILayout.Box("Actions", GUILayout.Width(windowWidth*0.09f));
        GUILayout.Box("Documentation", GUILayout.Width(windowWidth*0.09f));
        GUILayout.EndHorizontal();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        for (int i = 0;
            i <
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getActiveAreasList().getActiveAreasList().Count;
            i++)
        {
            if (i == selectedArea)
                GUI.skin = selectedAreaSkin;

            GUILayout.BeginHorizontal();

            if (GUILayout.Button(Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getActiveAreasList().getActiveAreasList()[i].getId(),
                GUILayout.Width(windowWidth*0.54f)))
            {
                selectedArea = i;
            }

            if (GUILayout.Button(conditionTex, GUILayout.Width(windowWidth*0.14f)))
            {
                selectedArea = i;
            }
            if (GUILayout.Button("Edit actions", GUILayout.Width(windowWidth*0.09f)))
            {
                selectedArea = i;
            }
            if (GUILayout.Button("Edit documentation", GUILayout.Width(windowWidth*0.09f)))
            {
                selectedArea = i;
            }

            GUILayout.EndHorizontal();
            GUI.skin = defaultSkin;
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();



        /*
        * Right panel
        */
        GUILayout.BeginArea(rightPanelRect);
        GUI.skin = noBackgroundSkin;
        if (GUILayout.Button(addTexture, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            ActiveAreaNewName window =
                  (ActiveAreaNewName)ScriptableObject.CreateInstance(typeof(ActiveAreaNewName));
            window.Init(this, "IdObject");
        }
        if (GUILayout.Button(duplicateImg, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("Duplicate");
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getActiveAreasList()
                .duplicateElement(Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getActiveAreasList().getActiveAreas()[selectedArea]);
        }
        if (GUILayout.Button(moveUp, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("Up");
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getActiveAreasList()
                .moveElementUp(Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getActiveAreasList().getActiveAreas()[selectedArea]);
        }
        if (GUILayout.Button(moveDown, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("Down");
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getActiveAreasList()
                .moveElementDown(Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getActiveAreasList().getActiveAreas()[selectedArea]);
        }
        if (GUILayout.Button(clearImg, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("Clear");
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getActiveAreasList()
                .deleteElement(Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getActiveAreasList().getActiveAreas()[selectedArea],
                    false);
        }
        GUI.skin = defaultSkin;
        GUILayout.EndArea();


        if (backgroundPath != "")
        {

            GUILayout.BeginArea(infoPreviewRect);
            // Show preview dialog
            if (GUILayout.Button("Show preview/edit window"))
            {
                ActiveAreasEditor window =
                    (ActiveAreasEditor) ScriptableObject.CreateInstance(typeof (ActiveAreasEditor));
                window.Init(this, Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex], selectedArea);
            }
            GUILayout.EndArea();
            GUI.DrawTexture(previewRect, backgroundPreviewTex, ScaleMode.ScaleToFit);

        }
        else
        {
            GUILayout.BeginArea(infoPreviewRect);
            GUILayout.Button("No background!");
            GUILayout.EndArea();
        }
    }

    public void OnDialogOk(string message, object workingObject = null)
    {
        Debug.Log("Apply");
        if (workingObject is ActiveAreaNewName)
        {
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getActiveAreasList()
                .addElement(Controller.ACTIVE_AREA, message);
        }
    }

    public void OnDialogCanceled(object workingObject = null)
    {
        Debug.Log("Cancel");
    }
}