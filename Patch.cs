using UnityEngine;
using MelonLoader;
using DNFC_Redux_Library;
using UnityEngine.InputSystem;

// 10ms is wait time

namespace Patch
{
    public class Patch : MelonMod
    {
        int ms = 0;
        public readonly Library DNFC_Lib = new Library();
        public bool componentsSearched = false;

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

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "MainMenu")
            {
                DNFC_Lib.SetIsInMainMenu(true);
                MelonLogger.Msg($"IsInMainMenu: {DNFC_Lib.IsInMainMenu()}");
            }
            else if (sceneName == "Loading")
            {
                DNFC_Lib.SetIsInMainMenu(false);
                MelonLogger.Msg($"IsInLoading: {DNFC_Lib.IsInLoading()}");
                DNFC_Lib.SetIsInLoading(true);
            }
            else if (sceneName == "CityGameplay")
            {
                if (!DNFC_Lib.IsInitialized())
                {
                    DNFC_Lib.SetInitialized(true);
                    MelonLogger.Msg("Mod has been initialized");
                }
            }
        }

        public override void OnUpdate()
        {
            // If the player is in the game and the mod is initialized
            if (DNFC_Lib.IsInitialized() == true)
            {
                // This section is critical to ensure all components are loaded before reading them in
                {
                    if (ms != 11)    // Wait 11ms before finding components to ensure game has loaded them in.
                    {
                        ms++;
                    }

                    if (ms == 11 && componentsSearched == false)
                    {
                        DNFC_Lib.GetAllWorkers();
                        DNFC_Lib.FindSettingsManagerComponent();
                        componentsSearched = true;
                        MelonLogger.Msg("All components have been successfully loaded.");
                    }
                }

                // Code going here will ensure that components have been found
                if (componentsSearched == true)
                {
                    DNFC_Lib.CheckWorkerCount();
                }
                if (Keyboard.current.zKey.wasPressedThisFrame)
                {
                    // Debug for checking if values are as expected
                    DNFC_Lib.GetWorker();
                }
                if (Keyboard.current.xKey.wasPressedThisFrame)
                {
                    MelonLogger.Msg($"Worker List Count: {DNFC_Lib.GetWorkerListCount()}");
                    MelonLogger.Msg($"Worker Count: {DNFC_Lib.GetWorkerCount()}");
                }
            }
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            MelonLogger.Msg($"Scene Unloaded: {sceneName}");
        }
    }
}
