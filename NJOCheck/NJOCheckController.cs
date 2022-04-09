using BeatSaberMarkupLanguage;
using HMUI;
using IPA.Utilities;
using NJOCheck.Configuration;
using NJOCheck.Extentions;
using Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class NJOCheckController : MonoBehaviour, IDisposable
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void CreateParams()
        {
            this.textParameters = new TextParameter[6];
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
            if (PluginConfig.Instance.StaticVisible) {
                this._staticParam = new NJOCheckController.TextParameter()
                {
                    Text = "STATIC",
                    Scale = new Vector3(1f, 1f, 1f),
                    Position = new Vector3(0f, 1.5f, 22f),
                    TextColor = Color.cyan
                };
            }
            else {
                this._staticParam = visibleParam;
            }
        }

        private void OnNoteJumpDurationTypeSettingsDropdown_didSelectCellWithIdxEvent(int arg1, NoteJumpDurationTypeSettings arg2)
        {
            this._currentDurationType = arg2;
            this.UpdateNotificationScreen(NoteJumpDurationTypeSettings.Static);
        }

        private void NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent(int arg1, float arg2)
        {
            this._currentOffsetIndex = arg1;
            this.UpdateNotificationScreen(NoteJumpDurationTypeSettings.Dynamic);
        }

        public void UpdateNotificationScreen(NoteJumpDurationTypeSettings noteJumpDuration)
        {
            switch (noteJumpDuration) {
                case NoteJumpDurationTypeSettings.Dynamic: {
                        if (this.textParameters.Length < (uint)this._currentOffsetIndex) {
                            return;
                        }
                        this.notificationText.text = this._duratonNames.ElementAt(this._currentOffsetIndex)?.Item2;
                        this.screenGO.transform.localScale = this.textParameters[this._currentOffsetIndex].Scale;
                        this.screenGO.transform.localPosition = this.textParameters[this._currentOffsetIndex].Position;
                        this.notificationText.color = this.textParameters[this._currentOffsetIndex].TextColor;
                        if (this._actionButton is NoTransitionsButton noTransitionsButton) {
                            foreach (var bg in noTransitionsButton.gameObject.GetComponentsInChildren<ImageView>()) {
                                if (bg.name != "BG") {
                                    continue;
                                }
                                if (this._currentOffsetIndex == 2) {
                                    bg.color = this._defaultColor;
                                    bg.SetField("_gradient", true);
                                }
                                else {
                                    bg.color = this.textParameters[this._currentOffsetIndex].TextColor;
                                    bg.SetField("_gradient", false);
                                }
                            }
                        }
                        break;
                    }
                case NoteJumpDurationTypeSettings.Static: {
                        this.notificationText.text = this._staticName;
                        this.screenGO.transform.localScale = this._staticParam.Scale;
                        this.screenGO.transform.localPosition = this._staticParam.Position;
                        this.notificationText.color = this._staticParam.TextColor;
                        if (this._actionButton is NoTransitionsButton noTransitionsButton) {
                            foreach (var bg in noTransitionsButton.gameObject.GetComponentsInChildren<ImageView>()) {
                                if (bg.name != "BG") {
                                    continue;
                                }
                                this.screenGO.SetActive(false);
                                bg.color = this._defaultColor;
                                bg.SetField("_gradient", true);
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private GameObject screenGO;
        private PlayerDataModel playerDataModel;
        private GameplaySetupViewController gameplaySetupViewController;
        private PlayerSettingsPanelController playerSettingsPanelController;
        private NoteJumpStartBeatOffsetDropdown noteJumpStartBeatOffsetDropdown;
        private NoteJumpDurationTypeSettingsDropdown _noteJumpDurationTypeSettingsDropdown;
        private TextMeshProUGUI notificationText;
        private Button _actionButton;
        private Color _defaultColor;
        private int _currentOffsetIndex = 0;
        private NoteJumpDurationTypeSettings _currentDurationType = NoteJumpDurationTypeSettings.Dynamic;
        private List<Tuple<float, string>> _duratonNames;
        private string _staticName;

        private static readonly TextParameter visibleParam = new TextParameter
        {
            TextColor = new Color(0, 0, 0, 0),
        };
        private TextParameter _staticParam;
        private TextParameter[] textParameters;
        private bool _disposedValue;
        private static readonly NJOCheckController.TextParameter[] defaultParams = new NJOCheckController.TextParameter[]
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
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        [Inject]
        private void Constractor(GameplaySetupViewController container, PlayerDataModel model, StandardLevelDetailViewController standard)
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

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue) {
                if (disposing) {
                    try {
                        this._noteJumpDurationTypeSettingsDropdown.didSelectCellWithIdxEvent -= this.OnNoteJumpDurationTypeSettingsDropdown_didSelectCellWithIdxEvent;
                        this.noteJumpStartBeatOffsetDropdown.didSelectCellWithIdxEvent -= this.NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent;
                    }
                    catch (Exception e) {
                        Plugin.Log.Error(e);
                    }
                }
                this._disposedValue = true;
            }
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region Monobehaviour Messages
        // These methods are automatically called by Unity, you should remove any you aren't using.
        /// <summary>
        /// Only ever called once on the first frame the script is Enabled. Start is called after any other script's Awake() and before Update().
        /// </summary>
        private void Start()
        {
            try {
                this.screenGO = new GameObject("NotificationText", typeof(CanvasScaler), typeof(RectMask2D), typeof(VRGraphicRaycaster), typeof(CurvedCanvasSettings));
                this.screenGO.GetComponent<VRGraphicRaycaster>().SetField("_physicsRaycaster", BeatSaberUI.PhysicsRaycasterWithCache);
                this.screenGO.transform.localScale = new Vector3(1f, 1f, 1f);
                this.notificationText = BeatSaberUI.CreateText(this.screenGO.gameObject.transform as RectTransform, "DEFAULT", Vector2.zero);
                this.notificationText.alignment = TextAlignmentOptions.Center;
                this.notificationText.autoSizeTextContainer = false;
                this.noteJumpStartBeatOffsetDropdown = this.playerSettingsPanelController.GetField<NoteJumpStartBeatOffsetDropdown, PlayerSettingsPanelController>("_noteJumpStartBeatOffsetDropdown");
                this._noteJumpDurationTypeSettingsDropdown = this.playerSettingsPanelController.GetField<NoteJumpDurationTypeSettingsDropdown, PlayerSettingsPanelController>("_noteJumpDurationTypeSettingsDropdown");
                this.noteJumpStartBeatOffsetDropdown.didSelectCellWithIdxEvent += this.NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent;
                this._noteJumpDurationTypeSettingsDropdown.didSelectCellWithIdxEvent += this.OnNoteJumpDurationTypeSettingsDropdown_didSelectCellWithIdxEvent;
                this._duratonNames = new List<Tuple<float, string>>();
                TupleListExtensions.Add(this._duratonNames, -0.5f, Localization.Get("PLAYER_SETTINGS_JUMP_START_CLOSE"));
                TupleListExtensions.Add(this._duratonNames, -0.25f, Localization.Get("PLAYER_SETTINGS_JUMP_START_CLOSER"));
                TupleListExtensions.Add(this._duratonNames, 0f, Localization.Get("PLAYER_SETTINGS_JUMP_START_DEFAULT"));
                TupleListExtensions.Add(this._duratonNames, 0.25f, Localization.Get("PLAYER_SETTINGS_JUMP_START_FURTHER"));
                TupleListExtensions.Add(this._duratonNames, 0.5f, Localization.Get("PLAYER_SETTINGS_JUMP_START_FAR"));
                this._staticName = Localization.Get("PLAYER_SETTINGS_NOTE_JUMP_DURATION_TYPE_STATIC");
                this.OnNoteJumpDurationTypeSettingsDropdown_didSelectCellWithIdxEvent((int)this.playerDataModel.playerData.playerSpecificSettings.noteJumpDurationTypeSettings, this.playerDataModel.playerData.playerSpecificSettings.noteJumpDurationTypeSettings);
                this.NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent(this.noteJumpStartBeatOffsetDropdown.GetIdxForOffset(this.playerDataModel.playerData.playerSpecificSettings.noteJumpStartBeatOffset), this.playerDataModel.playerData.playerSpecificSettings.noteJumpStartBeatOffset);
            }
            catch (Exception e) {
                Plugin.Log.Error(e);
            }
        }

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{this.name}: OnDestroy()");
            Destroy(this.screenGO);
        }
        #endregion
        public struct TextParameter
        {
            public string Text;
            public Vector3 Scale;
            public Vector3 Position;
            public Color TextColor;
        }
    }
}