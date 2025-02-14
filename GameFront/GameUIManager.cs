using System.Windows.Forms;

namespace GameFront
{
    internal class GameUIManager
    {
        public void Run()
        {
            FormGameSettings settingsForm = new FormGameSettings();
            DialogResult result = settingsForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                FormGame gameForm = new FormGame(settingsForm.Player1Name, settingsForm.Player2Name, settingsForm.SecondPlayerIsComputer, settingsForm.BoardSize);

                gameForm.ShowDialog();
            }
        }
    }
}
