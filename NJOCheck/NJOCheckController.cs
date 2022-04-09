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
        #region // プライベートメソッド
        private void CreateParams()
        {
            this._dynamicTextParameters = new TextParameter[5];
            if (PluginConfig.Instance.CloseVisible) {
                this._dynamicTextParameters[0] = s_defaultDynamicParams[0];
            }
            else {
                this._dynamicTextParameters[0] = s_visibleParam;
            }
            if (PluginConfig.Instance.CloserVisible) {
                this._dynamicTextParameters[1] = s_defaultDynamicParams[1];
            }
            else {
                this._dynamicTextParameters[1] = s_visibleParam;
            }
            if (PluginConfig.Instance.DefaultVisible) {
                this._dynamicTextParameters[2] = s_defaultDynamicParams[2];
            }
            else {
                this._dynamicTextParameters[2] = s_visibleParam;
            }
            if (PluginConfig.Instance.FurtherVisible) {
                this._dynamicTextParameters[3] = s_defaultDynamicParams[3];
            }
            else {
                this._dynamicTextParameters[3] = s_visibleParam;
            }
            if (PluginConfig.Instance.FarVisible) {
                this._dynamicTextParameters[4] = s_defaultDynamicParams[4];
            }
            else {
                this._dynamicTextParameters[4] = s_visibleParam;
            }
            if (PluginConfig.Instance.StaticVisible) {
                this._staticParam = s_defaultStaticParam;
            }
            else {
                this._staticParam = s_visibleParam;
            }
        }

        private void OnNoteJumpDurationTypeSettingsDropdown_didSelectCellWithIdxEvent(int arg1, NoteJumpDurationTypeSettings arg2)
        {
            this._currentDurationType = arg2;
            this.UpdateNotificationScreen(arg2);
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
                        if (this._dynamicTextParameters.Length < (uint)this._currentOffsetIndex) {
                            return;
                        }
                        this._notificationText.text = this._duratonNames.ElementAt(this._currentOffsetIndex)?.Item2;
                        this._screenGO.transform.localScale = this._dynamicTextParameters[this._currentOffsetIndex].Scale;
                        this._screenGO.transform.localPosition = this._dynamicTextParameters[this._currentOffsetIndex].Position;
                        this._notificationText.color = this._dynamicTextParameters[this._currentOffsetIndex].TextColor;
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
                                    bg.color = this._dynamicTextParameters[this._currentOffsetIndex].TextColor;
                                    bg.SetField("_gradient", false);
                                }
                            }
                        }
                        break;
                    }
                case NoteJumpDurationTypeSettings.Static: {
                        this._notificationText.text = s_staticName;
                        this._screenGO.transform.localScale = this._staticParam.Scale;
                        this._screenGO.transform.localPosition = this._staticParam.Position;
                        this._notificationText.color = this._staticParam.TextColor;
                        if (this._actionButton is NoTransitionsButton noTransitionsButton) {
                            foreach (var bg in noTransitionsButton.gameObject.GetComponentsInChildren<ImageView>()) {
                                if (bg.name != "BG") {
                                    continue;
                                }
                                bg.color = PluginConfig.Instance.StaticVisible ? this._staticParam.TextColor : this._defaultColor;
                                bg.SetField("_gradient", !PluginConfig.Instance.StaticVisible);
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
        private GameObject _screenGO;
        private PlayerDataModel _playerDataModel;
        private GameplaySetupViewController _gameplaySetupViewController;
        private PlayerSettingsPanelController _playerSettingsPanelController;
        private NoteJumpStartBeatOffsetDropdown _noteJumpStartBeatOffsetDropdown;
        private NoteJumpDurationTypeSettingsDropdown _noteJumpDurationTypeSettingsDropdown;
        private TextMeshProUGUI _notificationText;
        private Button _actionButton;
        private Color _defaultColor;
        private int _currentOffsetIndex = 0;
        private NoteJumpDurationTypeSettings _currentDurationType = NoteJumpDurationTypeSettings.Dynamic;
        private List<Tuple<float, string>> _duratonNames;
        private static readonly string s_staticName = Localization.Get("PLAYER_SETTINGS_NOTE_JUMP_DURATION_TYPE_STATIC");
        private TextParameter[] _dynamicTextParameters;
        private TextParameter _staticParam;
        private bool _disposedValue;

        private static readonly TextParameter s_visibleParam = new TextParameter
        {
            TextColor = new Color(0, 0, 0, 0),
        };
        private static readonly NJOCheckController.TextParameter[] s_defaultDynamicParams = new NJOCheckController.TextParameter[]
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
        private static readonly TextParameter s_defaultStaticParam = new NJOCheckController.TextParameter()
        {
            Text = "STATIC",
            Scale = new Vector3(1f, 1f, 1f),
            Position = new Vector3(0f, 1.5f, 22f),
            TextColor = Color.cyan
        };
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        [Inject]
        private void Constractor(GameplaySetupViewController container, PlayerDataModel model, StandardLevelDetailViewController standard)
        {
            this.CreateParams();
            this._gameplaySetupViewController = container;
            this._playerDataModel = model;
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
            this._playerSettingsPanelController = this._gameplaySetupViewController.GetField<PlayerSettingsPanelController, GameplaySetupViewController>("_playerSettingsPanelController");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue) {
                if (disposing) {
                    try {
                        this._noteJumpDurationTypeSettingsDropdown.didSelectCellWithIdxEvent -= this.OnNoteJumpDurationTypeSettingsDropdown_didSelectCellWithIdxEvent;
                        this._noteJumpStartBeatOffsetDropdown.didSelectCellWithIdxEvent -= this.NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent;
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
                this._screenGO = new GameObject("NotificationText", typeof(CanvasScaler), typeof(RectMask2D), typeof(VRGraphicRaycaster), typeof(CurvedCanvasSettings));
                this._screenGO.GetComponent<VRGraphicRaycaster>().SetField("_physicsRaycaster", BeatSaberUI.PhysicsRaycasterWithCache);
                this._screenGO.transform.localScale = new Vector3(1f, 1f, 1f);
                this._notificationText = BeatSaberUI.CreateText(this._screenGO.gameObject.transform as RectTransform, "DEFAULT", Vector2.zero);
                this._notificationText.alignment = TextAlignmentOptions.Center;
                this._notificationText.autoSizeTextContainer = false;
                this._noteJumpStartBeatOffsetDropdown = this._playerSettingsPanelController.GetField<NoteJumpStartBeatOffsetDropdown, PlayerSettingsPanelController>("_noteJumpStartBeatOffsetDropdown");
                this._noteJumpDurationTypeSettingsDropdown = this._playerSettingsPanelController.GetField<NoteJumpDurationTypeSettingsDropdown, PlayerSettingsPanelController>("_noteJumpDurationTypeSettingsDropdown");
                this._noteJumpStartBeatOffsetDropdown.didSelectCellWithIdxEvent += this.NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent;
                this._noteJumpDurationTypeSettingsDropdown.didSelectCellWithIdxEvent += this.OnNoteJumpDurationTypeSettingsDropdown_didSelectCellWithIdxEvent;
                this._duratonNames = new List<Tuple<float, string>>();
                TupleListExtensions.Add(this._duratonNames, -0.5f, Localization.Get("PLAYER_SETTINGS_JUMP_START_CLOSE"));
                TupleListExtensions.Add(this._duratonNames, -0.25f, Localization.Get("PLAYER_SETTINGS_JUMP_START_CLOSER"));
                TupleListExtensions.Add(this._duratonNames, 0f, Localization.Get("PLAYER_SETTINGS_JUMP_START_DEFAULT"));
                TupleListExtensions.Add(this._duratonNames, 0.25f, Localization.Get("PLAYER_SETTINGS_JUMP_START_FURTHER"));
                TupleListExtensions.Add(this._duratonNames, 0.5f, Localization.Get("PLAYER_SETTINGS_JUMP_START_FAR"));
                this._currentDurationType = this._playerDataModel.playerData.playerSpecificSettings.noteJumpDurationTypeSettings;
                this._currentOffsetIndex = this._noteJumpStartBeatOffsetDropdown.GetIdxForOffset(this._playerDataModel.playerData.playerSpecificSettings.noteJumpStartBeatOffset);
                switch (this._currentDurationType) {
                    case NoteJumpDurationTypeSettings.Dynamic:
                        this.OnNoteJumpDurationTypeSettingsDropdown_didSelectCellWithIdxEvent((int)this._currentDurationType, NoteJumpDurationTypeSettings.Dynamic);
                        this.NoteJumpStartBeatOffsetDropdown_didSelectCellWithIdxEvent(this._currentOffsetIndex, this._playerDataModel.playerData.playerSpecificSettings.noteJumpStartBeatOffset);
                        break;
                    case NoteJumpDurationTypeSettings.Static:
                        this.OnNoteJumpDurationTypeSettingsDropdown_didSelectCellWithIdxEvent((int)this._currentDurationType, NoteJumpDurationTypeSettings.Static);
                        break;
                    default:
                        break;
                }
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
            Destroy(this._screenGO);
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