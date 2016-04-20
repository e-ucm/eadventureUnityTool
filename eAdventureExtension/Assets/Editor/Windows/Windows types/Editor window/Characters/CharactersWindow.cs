﻿using UnityEngine;
using System.Collections;

public class CharactersWindow : LayoutWindow
{
    private enum CharactersWindowType { Action, Appearance, DialogConfiguration, Documentation }

    private static CharactersWindowType openedWindow = CharactersWindowType.DialogConfiguration;
    private static CharactersWindowActions charactersWindowActions;
    private static CharactersWindowAppearance charactersWindowAppearance;
    private static CharactersWindowDialogConfiguration charactersWindowDialogConfiguration;
    private static CharactersWindowDocumentation charactersWindowDocumentation;

    // Two methods responsible for showing right window content 
    // - concrete item info or base window view
    public void ShowBaseWindowView()
    {
        isConcreteItemVisible = false;
        GameRources.GetInstance().selectedCharacterIndex = -1;
    }

    public void ShowItemWindowView(int o)
    {
        isConcreteItemVisible = true;
        GameRources.GetInstance().selectedCharacterIndex = o;
    }

    // Flag determining visibility of concrete item information
    private bool isConcreteItemVisible = false;

    public CharactersWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        charactersWindowActions = new CharactersWindowActions(aStartPos, new GUIContent(Language.GetText("ACTIONS")), "Window");
        charactersWindowAppearance = new CharactersWindowAppearance(aStartPos, new GUIContent(Language.GetText("APPEARANCE")), "Window");
        charactersWindowDialogConfiguration = new CharactersWindowDialogConfiguration(aStartPos, new GUIContent(Language.GetText("DIALOG_CONFIGURATION")), "Window");
        charactersWindowDocumentation = new CharactersWindowDocumentation(aStartPos, new GUIContent(Language.GetText("DOCUMENTATION")), "Window");
    }


    public override void Draw(int aID)
    {
        // Show information of concrete item
        if (isConcreteItemVisible)
        {
            /**
            UPPER MENU
            */
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(Language.GetText("APPEARANCE")))
            {
                OnWindowTypeChanged(CharactersWindowType.Appearance);
            }
            if (GUILayout.Button(Language.GetText("DOCUMENTATION")))
            {
                OnWindowTypeChanged(CharactersWindowType.Documentation);
            }
            if (GUILayout.Button(Language.GetText("DIALOG_CONFIGURATION")))
            {
                OnWindowTypeChanged(CharactersWindowType.DialogConfiguration);
            }
            if (GUILayout.Button(Language.GetText("ACTIONS")))
            {
                OnWindowTypeChanged(CharactersWindowType.Action);
            }
            GUILayout.EndHorizontal();

            switch (openedWindow)
            {
                case CharactersWindowType.Appearance:
                    charactersWindowAppearance.Draw(aID);
                    break;
                case CharactersWindowType.Action:
                    charactersWindowActions.Draw(aID);
                    break;
                case CharactersWindowType.DialogConfiguration:
                    charactersWindowDialogConfiguration.Draw(aID);
                    break;
                case CharactersWindowType.Documentation:
                    charactersWindowDocumentation.Draw(aID);
                    break;
            }
        }
        else
        {
            GUILayout.Label("CharacterWindow");
        }
    }

    void OnWindowTypeChanged(CharactersWindowType type_)
    {
        openedWindow = type_;
    }
}