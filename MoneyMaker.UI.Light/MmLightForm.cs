using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using MoneyMaker.BLL.Files;

namespace MoneyMaker.UI.Light
{
    public partial class MmLightForm : Form
    {
        //forms...
        private readonly FormSettings _formSettings;

        private readonly FileTrackingManager _fileTrackingManager;

        private bool _trackingState;

        private readonly IDictionary<string, HudForm> _huds;

        public MmLightForm()
        {
            _huds=new Dictionary<string, HudForm>();
            _trackingState = false;
            InitializeComponent();
            _formSettings = new FormSettings();
            _fileTrackingManager = new FileTrackingManager();
            _fileTrackingManager.PokerFileChanged += OnPokerFileChanged;
            _fileTrackingManager.Initialize("*.txt", NotifyFilters.LastWrite | NotifyFilters.FileName, Properties.Settings.Default.FileTrackingFolder);
            Properties.Settings.Default.PropertyChanged += OnSettingsPropertyChanged;
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FileTrackingFolder")
                _fileTrackingManager.Initialize("*.txt", NotifyFilters.LastWrite | NotifyFilters.FileName, Properties.Settings.Default.FileTrackingFolder);
        }

        private void OnPokerFileChanged(object sender, FileSystemEventArgs e)
        {
            Action action = () => ShowHudForm( e.FullPath);
            Invoke(action);
        }

        private void pictureBoxTrack_Click(object sender, EventArgs e)
        {
            _trackingState = !_trackingState;
            _fileTrackingManager.EnableTracking = _trackingState;
            ToggleTrackingButton(_trackingState);
            if (Properties.Settings.Default.MinimizeInTray &&_trackingState)
                WindowState = FormWindowState.Minimized;

        }

        private void ToggleTrackingButton(bool trackingState)
        {
            pictureBoxTrack.Image = trackingState ? Properties.Resources.track_of : Properties.Resources.track_on;
        }

        private void pictureBoxSettings_Click(object sender, EventArgs e)
        {
            _formSettings.Show();
        }

        private void MmLightForm_Load(object sender, EventArgs e)
        {
            ToggleTrackingButton(_trackingState);
        }

        private void ShowHudForm(string fullPath)
        {
            if (!_huds.ContainsKey(fullPath))
            {
                var hudForm=new HudForm(fullPath);
                hudForm.Show();
                _huds.Add(fullPath,hudForm);
            }
            else
            {
                _huds[fullPath].Invalidate();
                _huds[fullPath].Show();
            }
        }

        private void MmLightForm_Resize(object sender, EventArgs e)
        {
            if(WindowState==FormWindowState.Minimized)
                Hide();
        }

        private void notifyIconTray_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}
