using UnityEngine;
using UnityEngine.SceneManagement;
using Naninovel;

public class MiniGameController : MonoBehaviour
{
    
    public void StartMiniGame()
    {
        Debug.Log("Starting mini game...");
        SceneManager.LoadScene("MemoryCardGame");
    }

    public async void EndMiniGame()
    {
        Debug.Log("EndMiniGame called");

        // Устанавливаем переменную для Naninovel
        var customVariableManager = Engine.GetService<ICustomVariableManager>();
        if (customVariableManager != null)
        {
            customVariableManager.SetVariableValue("isMiniGameFinished", "true");
            Debug.Log("Set isMiniGameFinished to true");
        }
        else
        {
            Debug.LogError("Failed to get the custom variable manager.");
        }

        // Выгрузите все ресурсы, используемые Naninovel, и остановите все сервисы движка
        var stateManager = Engine.GetService<IStateManager>();
        if (stateManager != null)
        {
            await stateManager.ResetStateAsync();
            Debug.Log("Reset state of Naninovel");
        }
        else
        {
            Debug.LogError("Failed to get the state manager.");
        }

        // Загружаем основную сцену
        await SceneManager.LoadSceneAsync("First");
        Debug.Log("Loaded the 'First' scene");

        // Проигрывание сценария Naninovel после загрузки сцены
        var player = Engine.GetService<IScriptPlayer>();
        if (player != null)
        {
            Debug.Log("Attempting to play 'SecondScene' from 'postMiniGame' label");

            await player.PreloadAndPlayAsync(scriptName: "Bar", label: "postMiniGame");
        }
        else
        {
            Debug.LogError("Failed to get the script player.");
        }
    }
}
