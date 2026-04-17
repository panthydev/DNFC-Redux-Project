using DNFC_Redux_Library;
using Il2Cpp;
using MelonLoader;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Patch
{
    public class Patch : MelonMod
    {
        public EconomyLib _economyLib;
        public UIManager _uiManager;
        private GameObject _pauseMenu;

        public override void OnEarlyInitializeMelon()
        {
            MelonLogger.Msg(@"
         _   _ ___  _  _ ___ ___   ___      _      _    
        | | | |   \| \| | __/ __| | _ \__ _| |_ __| |_  
        | |_| | |) | .` | _| (__  |  _/ _` |  _/ _| ' \ 
         \___/|___/|_|\_|_| \___| |_| \__,_|\__\__|_||_|
    From everyone at the DNFC Redux Project, we hope you enjoy 
    this mod and the work we've put into it. If you have any questions, 
    suggestions, or want to contribute, feel free to join our Discord server!");
        }

        public override void OnInitializeMelon()
        {
            _economyLib = new EconomyLib();
            _uiManager = new UIManager();
        }

        public override void OnUpdate()
        {
            // Pause menu button check
            if (_pauseMenu != null && _pauseMenu.activeSelf && _uiManager.MainMenuButton != null)
            {
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    var buttonRect = _uiManager.MainMenuButton.GetComponent<RectTransform>();
                    if (RectTransformUtility.RectangleContainsScreenPoint(buttonRect, UnityEngine.Input.mousePosition))
                    {
                        MelonLogger.Msg("Main Menu button clicked!");
                        SceneManager.LoadScene("MainMenu");
                    }
                }
            }

            // All debug keys are gated behind Developer Mode.
            // Toggle Developer Mode with Ctrl+Shift+D.
            if (!CoreLib.DeveloperMode)
                return;

            // X key — placeholder for character component debugging.
            // TODO: Implement GetWorkerCharacterComponent in EmployeeLib and call it here.
            if (Keyboard.current.xKey.wasPressedThisFrame)
            {
                MelonLogger.Msg("[DEV] X key pressed — GetWorkerCharacterComponent not yet implemented in EmployeeLib.");
            }

            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                _economyLib.GetBankBalance();
                _economyLib.AddBankBalance(1000000);
            }

            if (Keyboard.current.mKey.wasPressedThisFrame)
            {
                MelonLogger.Msg("M pressed - attempting scene load");
                SceneManager.LoadScene("MainMenu");
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            _pauseMenu = GameObject.Find("UI_Gameplay/Canvas/Panel_Settings");

            if (sceneName == "CityGameplay")
            {
                _uiManager.InjectPauseMenuButtons();

                var inputManager = InputManager.Instance;
                if (inputManager != null)
                {
                    inputManager.ResetControls();
                    MelonLogger.Msg("InputManager: Controls reset.");
                }
                else
                {
                    MelonLogger.Msg("InputManager: Instance not found.");
                }

                var loadingDisplay = GameObject.FindObjectOfType<LoadingDisplay>();
                if (loadingDisplay != null)
                {
                    loadingDisplay.CloseLoading();
                    MelonLogger.Msg("LoadingDisplay closed.");
                }
                else
                {
                    MelonLogger.Msg("LoadingDisplay not found.");
                }
            }
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            MelonLogger.Msg($"Scene Unloaded: {sceneName}");
        }
    }
}