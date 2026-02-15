using UnityEngine;
using MelonLoader;
using System.Collections;
using DNFC_Redux_Library;
//testing comment
namespace Patch
{
    public class Patch : MelonMod
    {
        public readonly Library DNFC_Lib = new Library();

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

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "MainMenu")
            {
                DNFC_Lib.SetIsInMainMenu(true);
            }
            else if (sceneName == "Loading")
            {
                DNFC_Lib.SetIsInMainMenu(false);
                DNFC_Lib.SetIsInLoading(true);
            }
            else if (sceneName == "CityGameplay")
            {
                if (!DNFC_Lib.IsInitialized())
                {
                    DNFC_Lib.SetInitialized(true);
                    MelonLogger.Msg("Mod has been initialized");
                }
                DNFC_Lib.FindSettingsManagerComponent();

                // Check if the SettingsManager GameObject was found
                if (DNFC_Lib.GetSettingsManagerComponent() == null)
                {
                    MelonLogger.Error("SettingsManager GameObject not found in the scene!");
                    return;
                }
            }
        }
    }
}
