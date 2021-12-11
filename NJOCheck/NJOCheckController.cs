using BeatSaberMarkupLanguage;
using HMUI;
using IPA.Utilities;
using NJOCheck.Configuration;
using NJOCheck.Extentions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRUIControls;
using Zenject;

namespace NJOCheck
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class NJOCheckController : MonoBehaviour
    {
        // These methods are automatically called by Unity, you should remove any you aren't using.
        [Inject]
        void Constractor(GameplaySetupViewController container, PlayerDataModel model, StandardLevelDetailViewController standard)
        {
            this.CreateParams();
            this.gameplaySetupViewController = container;
            this.playerDataModel = model;
            var detailview = standard.GetField<StandardLevelDetailView, StandardLevelDetailViewController>("_standardLevelDetailView");
            this._actionButton = detailview.actionButton;
            if (this._actionButton is NoTransitionsButton noTransitionsButton) {
                foreach (var bg in noTransitionsButton.gameObject.GetComponentsInChildren<ImageView>()) {
                    if (bg.name == "BG") {
                        bg.SetField("_gradient", false);
                        this._defaultColor = bg.color;
                    }
                }
            }
            this.playerSettingsPanelController = this.gameplaySetupViewController.GetField<PlayerSettingsPanelController, GameplaySetupViewController>("_playerSettingsPanelController");
        }

        private void CreateParams()
        {
            this.textParameters = new TextParameter[5];
            if (PluginConfig.Instance.CloseVisible) {
                this.textParameters[0] = defaultParams[0];
            }
            else {
                this.textParameters[0] = visibleParam;
            }
            if (PluginConfig.Instance.CloserVisible) {
                this.textParameters[1] = defaultParams[1];
            }
            else {
                this.textParameters[1] = visibleParam;
            }
            if (PluginConfig.Instance.DefaultVisible) {
                this.textParameters[2] = defaultParams[2];
            }
            else {
                this.textParameters[2] = visibleParam;
            }
            if (PluginConfig.Instance.FurtherVisible) {
                this.textParameters[3] = defaultParams[3];
            }
            else {
                this.textParameters[3] = visibleParam;
            }
            if (PluginConfig.Instance.FarVisible) {
                this.textParameters[4] = defaultParams[4];
            }
            else {
                this.textParameters[4] = visibleParam;
            }
        }

        #region Monobehaviour Messages
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            // For this particular MonoBehaviour, we only want one instance to exist at any time, so store a reference to it in a static property
            //   and destroy any that are created while one already exists.
            Plugin.Log?.Debug($"{name}: Awake()");
        }
        /// <summary>
        /// Only ever called once on the first frame the script is Enabled. Start is called after any other script's Awake() and before Update().
        /// </summary>
        private void Start()
        {
            try {
                this.screenGO = new GameObject("NotificationText", typeof(CanvasScaler), typeof(RectMask2D), typeof(VRGraphicRaycaster), typeof(CurvedCanvasSettings));
                this.screenGO.GetComponent<VRGraphicRaycaster>().SetField("_physicsRaycaster", BeatSaberUI.PhysicsRaycasterWithCache);
                this.screenGO.transform.localScale = new Vector3(1f, 1f, 1f);
                this.notificationText = BeatSaberUI.CreateText(screenGO.gameObject.transform as RectTransform, "DEFAULT", Vector2.zero);
                this.notificationText.alignment = TextAlignmentOptions.Center;
                this.notificationText.autoSizeTextContainer = false;
                this.noteJumpStartBeatOffsetDropdown = this.playerSettingsPanelController.GetField<NoteJumpStartBeatOffsetDropdown, PlayerSettingsPanelController>("_noteJumpStartBeatOffsetDropdown");
                this.noteJumpStartBeatOffsetDropdown.didSelectCellWithIdxEvent += this.NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent; // += this.NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent;
                this.NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent(noteJumpStartBeatOffsetDropdown.GetIdxForOffset(playerDataModel.playerData.playerSpecificSettings.noteJumpStartBeatOffset), playerDataModel.playerData.playerSpecificSettings.noteJumpStartBeatOffset);
            }
            catch (Exception e) {
                Plugin.Log.Error(e);
            }
        }

        private void NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent(int arg1, float arg2)
        {
            if (textParameters.Length < (uint)arg1) {
                return;
            }

            notificationText.text = this.textParameters[arg1].Text;
            this.screenGO.transform.localScale = this.textParameters[arg1].Scale;
            this.screenGO.transform.localPosition = this.textParameters[arg1].Position;
            notificationText.color = this.textParameters[arg1].TextColor;
            if (this._actionButton is NoTransitionsButton noTransitionsButton) {
                foreach (var bg in noTransitionsButton.gameObject.GetComponentsInChildren<ImageView>()) {
                    if (bg.name == "BG") {
                        if (arg1 == 2) {
                            bg.color = this._defaultColor;
                            bg.SetField("_gradient", true);
                        }
                        else {
                            bg.color = this.textParameters[arg1].TextColor;
                            bg.SetField("_gradient", false);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{name}: OnDestroy()");
            Destroy(this.notificationText.gameObject);
        }
        #endregion

        GameObject screenGO;
        PlayerDataModel playerDataModel;
        GameplaySetupViewController gameplaySetupViewController;
        PlayerSettingsPanelController playerSettingsPanelController;
        NoteJumpStartBeatOffsetDropdown noteJumpStartBeatOffsetDropdown;
        TextMeshProUGUI notificationText;
        Button _actionButton;
        Color _defaultColor;

        private static readonly TextParameter visibleParam = new TextParameter
        {
            TextColor = new Color(0, 0, 0, 0),
        };
        TextParameter[] textParameters;
        private static readonly NJOCheckController.TextParameter[] defaultParams = new NJOCheckController.TextParameter[5]
        {
            new NJOCheckController.TextParameter()
            {
                Text = "CLOSE",
                Scale = new Vector3(0.3f, 0.3f, 0.3f),
                Position = new Vector3(0f, 1.5f, 6f),
                TextColor = Color.red
            },
            new NJOCheckController.TextParameter()
            {
                Text = "CLOSER",
                Scale = new Vector3(0.7f, 0.7f, 0.7f),
                Position = new Vector3(0f, 1.5f, 14f),
                TextColor = Color.yellow
            },
            new NJOCheckController.TextParameter()
            {
                Text = "DEFAULT",
                Scale = new Vector3(1f, 1f, 1f),
                Position = new Vector3(0f, 1.5f, 22f),
                TextColor = Color.gray
            },
            new NJOCheckController.TextParameter()
            {
                Text = "FURTHER",
                Scale = new Vector3(1.5f, 1.5f, 1.5f),
                Position = new Vector3(0f, 3f, 30f),
                TextColor = Color.blue
            },
            new NJOCheckController.TextParameter()
            {
                Text = "FAR",
                Scale = new Vector3(3f, 3f, 3f),
                Position = new Vector3(0f, 4f, 50f),
                TextColor = Color.green
            }
        };
        public struct TextParameter
        {
            public string Text;
            public Vector3 Scale;
            public Vector3 Position;
            public Color TextColor;
        }
    }
}