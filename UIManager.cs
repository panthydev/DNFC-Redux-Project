using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Patch
{
    public class UIManager
    {
        public UnityEngine.UI.Button MainMenuButton;

        public UIManager()
        {
        }

        public void InjectPauseMenuButtons()
        {
            var container = GameObject.Find("UI_Gameplay/Canvas/Panel_Settings/Panel_SaveAndExit/Container_Buttons");
            if (container == null)
            {
                MelonLogger.Msg("UIManager: Container_Buttons not found.");
                return;
            }

            var exitButton = container.transform.Find("Button_ExitGame");
            if (exitButton == null)
            {
                MelonLogger.Msg("UIManager: Button_ExitGame not found.");
                return;
            }

            // Clone the exit button
            var mainMenuButton = GameObject.Instantiate(exitButton.gameObject, container.transform);
            mainMenuButton.name = "Button_MainMenu";
            mainMenuButton.transform.SetSiblingIndex(1);

            // Remove the exit game specific component
            var exitComponent = mainMenuButton.GetComponent<Il2CppGame.ButtonExitGame>();
            if (exitComponent != null)
                GameObject.Destroy(exitComponent);

            // Remove localisation so it doesn't override our label
            var localisedText = mainMenuButton.GetComponentInChildren<Il2CppLocalisation.LocalisedText>();
            if (localisedText != null)
                GameObject.Destroy(localisedText);

            // Set the label
            var tmp = mainMenuButton.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
                tmp.text = "Main Menu";

            MainMenuButton = mainMenuButton.GetComponent<UnityEngine.UI.Button>();

            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
            container.transform.localPosition = new Vector3(118f, container.transform.localPosition.y, container.transform.localPosition.z);

            MelonLogger.Msg("UIManager: Main Menu button injected.");
        }
    }
}