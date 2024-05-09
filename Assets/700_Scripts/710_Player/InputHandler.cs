using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [HideInInspector] public static PlayerActionMap Actions;

    #region References
    static PlayerController player;
    static TrajectoryPrediction trajPred;
    static PlayerRoomCam roomCam;
    static ReloadScene rlScene;
    static ReloadLevelSelector rlSelec;
    static NoClip nClip;
    static PauseMenu pMenu;
    static SwapControls spControls;
    static UI_Skip uSkip;
    static LevelSelectorManager lsManager;
    static Credits cr;
    static CutsceneSkip cutSkip;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        Actions = new PlayerActionMap();

        Actions.Cheat.Enable();
        Actions.Swap.Enable();
        Actions.MainMenu.Enable();
        Actions.Credits.Enable();
        Actions.Cutscenes.Enable();
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

    public static void RoomCamEnable(PlayerRoomCam playerRoomCam)
    {
        roomCam = playerRoomCam;

        //Actions.Gamepad.FreeCam.performed += playerFreeCam.FreeCam;
        //Actions.Gamepad.FreeCam.canceled += playerFreeCam.FreeCam;
        //Actions.Gamepad.StartFreeCam.started += playerFreeCam.StartFreeCam;
        Actions.Gamepad.RoomCam.canceled += playerRoomCam.StartFreeCam;
        //Actions.MouseKeyboard.FreeCam.performed += playerFreeCam.FreeCam;
        //Actions.MouseKeyboard.StartFreeCam.started += playerFreeCam.StartFreeCam;
        Actions.MouseKeyboard.RoomCam.canceled += playerRoomCam.StartFreeCam;
    }

    public static void ReloadSceneEnable(ReloadScene reloadScene)
    {
        rlScene = reloadScene;

        Actions.Cheat.ReloadScene.performed += reloadScene.Reload;
    }

    public static void ReloadLSEnable(ReloadLevelSelector reloadLS)
    {
        rlSelec = reloadLS;

        Actions.Cheat.ReloadLS.performed += reloadLS.Reload;
    }

    public static void MovePanelSelectorEnable(LevelSelectorManager levelSelectorManager)
    {
        lsManager = levelSelectorManager;

        Actions.MainMenu.ScrollLeft.performed += levelSelectorManager.PrevPanel;
        Actions.MainMenu.ScrollRight.performed += levelSelectorManager.NextPanel;
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
        uSkip = uiskip;
        Actions.MouseKeyboard.MouseStartDrag.canceled += uiskip.SkipCanva;
        Actions.Gamepad.ThrowPlayer.started += uiskip.SkipCanva;
    }

    public static void CreditsEnable(Credits credits)
    {
        cr = credits;

        Actions.Credits.AccelerateCredits.performed += credits.Accelerate;
        Actions.Credits.AccelerateCredits.canceled += credits.Accelerate;
        Actions.Credits.ExitCredits.performed += credits.Exit;
    }

    public static void CutscenesEnable(CutsceneSkip cutsceneSkip)
    {
        cutSkip = cutsceneSkip;

        Actions.Cutscenes.Skip.performed += cutsceneSkip.Skip;
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

    public static void RoomCamDisable()
    {
        //Actions.Gamepad.FreeCam.performed -= freeCam.FreeCam;
        //Actions.Gamepad.FreeCam.canceled -= freeCam.FreeCam;
        //Actions.Gamepad.StartFreeCam.started -= freeCam.StartFreeCam;
        Actions.Gamepad.RoomCam.canceled -= roomCam.StartFreeCam;
        //Actions.MouseKeyboard.FreeCam.performed -= freeCam.FreeCam;
        //Actions.MouseKeyboard.StartFreeCam.started -= freeCam.StartFreeCam;
        Actions.MouseKeyboard.RoomCam.canceled -= roomCam.StartFreeCam;
    }

    public static void ReloadSceneDisable()
    {
        Actions.Cheat.ReloadScene.performed -= rlScene.Reload;
    }

    public static void ReloadLSDisable()
    {
        Actions.Cheat.ReloadLS.performed -= rlSelec.Reload;
    }

    public static void MovePanelSelectorDisable()
    {
        Actions.MainMenu.ScrollLeft.performed -= lsManager.PrevPanel;
        Actions.MainMenu.ScrollRight.performed -= lsManager.NextPanel;
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
        Actions.MouseKeyboard.MouseStartDrag.canceled -= uSkip.SkipCanva;
        Actions.Gamepad.ThrowPlayer.started -= uSkip.SkipCanva;
    }

    public static void CreditsDisable()
    {
        Actions.Credits.AccelerateCredits.performed -= cr.Accelerate;
        Actions.Credits.AccelerateCredits.canceled -= cr.Accelerate;
        Actions.Credits.ExitCredits.performed -= cr.Exit;
    }

    public static void CutscenesDisable()
    {
        Actions.Cutscenes.Skip.performed -= cutSkip.Skip;
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

    public static void RoomCamEnable()
    {
        RoomCamEnable(roomCam);
    }

    public static void ReloadSceneEnable()
    {
        ReloadSceneEnable(rlScene);
    }

    public static void ReloadLSEnable()
    {
        ReloadLSEnable(rlSelec);
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
        UISkipEnable(uSkip);
    }

    public static void MovePanelSelectorEnable()
    {
        MovePanelSelectorEnable(lsManager);
    }

    public static void CreditsEnable()
    {
        CreditsEnable(cr);
    }

    public static void CutscenesEnable()
    {
        CutscenesEnable(cutSkip);
    }
    #endregion
}