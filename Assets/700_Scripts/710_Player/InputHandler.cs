using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [HideInInspector] public static PlayerActionMap Actions;

    #region References
    static PlayerController player;
    static TrajectoryPrediction trajPred;
    static PlayerRoomCam freeCam;
    static ReloadScene rlScene;
    static NoClip nClip;
    static PauseMenu pMenu;
    static SwapControls spControls;
    static UI_Skip skip;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        Actions = new PlayerActionMap();

        Actions.Cheat.Enable();
        Actions.Swap.Enable();
    }

    #region Enable
    public static void PlayerControllerEnable(PlayerController playerController)
    {
        player = playerController;

        #region Gamepad
        Actions.Gamepad.ThrowPlayer.started += playerController.GamepadStrengthGauge;
        Actions.Gamepad.ThrowPlayer.canceled += playerController.GamepadStrengthGauge;
        Actions.Gamepad.ThrowPlayer.canceled += playerController.Throw;
        Actions.Gamepad.GamepadStrenght.performed += playerController.GamepadDirection;
        Actions.Gamepad.CancelThrow.started += playerController.GamepadCancelThrow;
        #endregion
        #region Mouse/Keyboard
        Actions.MouseKeyboard.MouseStrenght.performed += playerController.MouseStrenght;
        Actions.MouseKeyboard.MouseStartDrag.started += playerController.MouseStartDrag;
        Actions.MouseKeyboard.MouseStartDrag.performed += playerController.MouseStartDrag;
        Actions.MouseKeyboard.MouseStartDrag.canceled += playerController.MouseThrow;
        Actions.MouseKeyboard.MouseCancelThrow.performed += playerController.MouseCancelThrow;
        #endregion
    }

    public static void TrajectoryPredictionEnable(TrajectoryPrediction trajectoryPrediction)
    {
        trajPred = trajectoryPrediction;

        Actions.MouseKeyboard.MouseStrenght.performed += trajectoryPrediction.MouseDirection;
        Actions.Gamepad.GamepadStrenght.performed += trajectoryPrediction.GamepadDirection;
    }

    public static void FreeCamEnable(PlayerRoomCam playerFreeCam)
    {
        freeCam = playerFreeCam;

        //Actions.Gamepad.FreeCam.performed += playerFreeCam.FreeCam;
        //Actions.Gamepad.FreeCam.canceled += playerFreeCam.FreeCam;
        //Actions.Gamepad.StartFreeCam.started += playerFreeCam.StartFreeCam;
        Actions.Gamepad.RoomCam.canceled += playerFreeCam.StartFreeCam;
        //Actions.MouseKeyboard.FreeCam.performed += playerFreeCam.FreeCam;
        //Actions.MouseKeyboard.StartFreeCam.started += playerFreeCam.StartFreeCam;
        Actions.MouseKeyboard.RoomCam.canceled += playerFreeCam.StartFreeCam;
    }

    public static void ReloadSceneEnable(ReloadScene reloadScene)
    {
        rlScene = reloadScene;

        Actions.Cheat.ReloadScene.performed += reloadScene.Reload;
    }

    public static void NoClipEnable(NoClip noClip)
    {
        nClip = noClip;

        Actions.Cheat.NoClip.performed += noClip.NoClipMode;
        Actions.Cheat.NoClipControl.performed += noClip.MovePlayer;
        Actions.Cheat.NoClipControl.canceled += noClip.MovePlayer;
    }

    public static void PauseMenuEnable(PauseMenu pauseMenu)
    {
        pMenu = pauseMenu;

        Actions.Gamepad.PauseMenu.started += pauseMenu.PauseMenuState;
        Actions.MouseKeyboard.PauseMenu.started += pauseMenu.PauseMenuState;
    }

    public static void SwapEnable(SwapControls swapControls)
    {
        spControls = swapControls;

        Actions.Swap.ToMouseKeyboard.started += swapControls.ToMouseKeyboard;
        Actions.Swap.ToGamepad.started += swapControls.ToGamepad;
    }

    public static void UISkipEnable(UI_Skip uiskip)
    {
        skip = uiskip;
        Actions.MouseKeyboard.MouseStartDrag.canceled += uiskip.SkipCanva;
        Actions.Gamepad.ThrowPlayer.started += uiskip.SkipCanva;
    }
    #endregion
    #region Disable
    public static void PlayerControllerDisable()
    {
        #region Gamepad
        Actions.Gamepad.ThrowPlayer.started -= player.GamepadStrengthGauge;
        Actions.Gamepad.ThrowPlayer.canceled -= player.GamepadStrengthGauge;
        Actions.Gamepad.ThrowPlayer.canceled -= player.Throw;
        Actions.Gamepad.GamepadStrenght.performed -= player.GamepadDirection;
        #endregion
        #region Mouse/Keyboard
        Actions.MouseKeyboard.MouseStrenght.performed -= player.MouseStrenght;
        Actions.MouseKeyboard.MouseStartDrag.performed -= player.MouseStartDrag;
        Actions.MouseKeyboard.MouseStartDrag.canceled -= player.MouseStartDrag;
        Actions.MouseKeyboard.MouseCancelThrow.performed -= player.MouseCancelThrow;
        #endregion
    }

    public static void TrajectoryPredictionDisable()
    {
        Actions.MouseKeyboard.MouseStrenght.performed -= trajPred.MouseDirection;
        Actions.Gamepad.GamepadStrenght.performed -= trajPred.GamepadDirection;
    }

    public static void FreeCamDisable()
    {
        //Actions.Gamepad.FreeCam.performed -= freeCam.FreeCam;
        //Actions.Gamepad.FreeCam.canceled -= freeCam.FreeCam;
        //Actions.Gamepad.StartFreeCam.started -= freeCam.StartFreeCam;
        Actions.Gamepad.RoomCam.canceled -= freeCam.StartFreeCam;
        //Actions.MouseKeyboard.FreeCam.performed -= freeCam.FreeCam;
        //Actions.MouseKeyboard.StartFreeCam.started -= freeCam.StartFreeCam;
        Actions.MouseKeyboard.RoomCam.canceled -= freeCam.StartFreeCam;
    }

    public static void ReloadSceneDisable()
    {
        Actions.Cheat.ReloadScene.performed -= rlScene.Reload;
    }

    public static void NoClipDisable()
    {
        Actions.Cheat.NoClip.performed -= nClip.NoClipMode;
        Actions.Cheat.NoClipControl.performed -= nClip.MovePlayer;
        Actions.Cheat.NoClipControl.canceled -= nClip.MovePlayer;
    }

    public static void PauseMenuDisable()
    {
        Actions.Gamepad.PauseMenu.started -= pMenu.PauseMenuState;
        Actions.MouseKeyboard.PauseMenu.started -= pMenu.PauseMenuState;
    }

    public static void SwapDisable()
    {
        Actions.Swap.ToMouseKeyboard.started -= spControls.ToMouseKeyboard;
        Actions.Swap.ToGamepad.started -= spControls.ToGamepad;
    }

    public static void UISkipDisable()
    {
        Actions.MouseKeyboard.MouseStartDrag.canceled -= skip.SkipCanva;
        Actions.Gamepad.ThrowPlayer.started -= skip.SkipCanva;
    }
    #endregion

    #region EnableOverload
    public static void PlayerControllerEnable()
    {
        PlayerControllerEnable(player);
    }

    public static void TrajectoryPredictionEnable()
    {
        TrajectoryPredictionEnable(trajPred);
    }

    public static void FreeCamEnable()
    {
        FreeCamEnable(freeCam);
    }

    public static void ReloadSceneEnable()
    {
        ReloadSceneEnable(rlScene);
    }

    public static void NoClipEnable()
    {
        NoClipEnable(nClip);
    }

    public static void PauseMenuEnable()
    {
        PauseMenuEnable(pMenu);
    }

    public static void SwapEnable()
    {
        SwapEnable(spControls);
    }

    public static void UISkipEnable()
    {
        UISkipEnable(skip);
    }
    #endregion
}