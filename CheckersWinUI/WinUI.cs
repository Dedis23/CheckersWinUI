namespace CheckersWinUI
{
    public class WinUI
    {
        private SettingsForm r_SettingsForm;
        private GameForm r_GameForm;

        public void Run()
        {
            r_SettingsForm = new SettingsForm();
            r_SettingsForm.ShowDialog();
            if (r_SettingsForm.ValidSettings == true)
            {
                r_GameForm = new GameForm(r_SettingsForm.BoardSize, r_SettingsForm.IsHumanPlayer2, r_SettingsForm.Player1Name, r_SettingsForm.Player2Name);
                r_GameForm.ShowDialog();
            }
        }
    }
}
