using UnityEngine;
using MelonLoader;

namespace Patch
{
    /*
     * Scenes are as followed
     * scene: MainMenu buildIndex: 0
     * scene: Loading buildIndex: 1
     * scene: CityGameplay buildIndex: 2
     */
    public class Patch : MelonMod
    {
        public override void OnEarlyInitializeMelon()
        {
            MelonLogger.Msg("From everyone at the DNFC Redux Project, we thank you for choosing our mod!");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MelonLogger.Msg($"Scene {sceneName} was loaded with build index {buildIndex}."); // Allows scene to be debugged
        }
    }
}
