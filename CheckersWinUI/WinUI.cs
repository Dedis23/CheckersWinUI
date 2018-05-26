using System;

namespace CheckersWinUI
{
    public class WinUI
    {
        private SettingsForm m_SettingsForm;
        private GameForm m_GameForm;

        public void Run()
        {
            m_SettingsForm = new SettingsForm();
            m_SettingsForm.ShowDialog();
        }
    }
}
