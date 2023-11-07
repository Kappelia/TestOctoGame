using Naninovel;
using Naninovel.Commands;
using UnityEngine;

[CommandAlias("startMiniGame")]
public class StartMiniGameCommand : Command
{
    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var controller = Object.FindObjectOfType<MiniGameController>();
        if (controller)
            controller.StartMiniGame();
        else
            Debug.LogError("MiniGameController не найден!");

        return UniTask.CompletedTask;
    }
}
