using System;
using System.Timers; 
using Microsoft.Maui.Controls;

namespace AppsRepeatProject
{
    public partial class MainPage : ContentPage
    {
        private readonly char[] vowels = new char[67];
        private readonly char[] consonants = new char[74];
        private Random random = new Random();
        private System.Timers.Timer timer; 
        private int timeLeft;
        private int lettersPicked;
        private bool isPlayerOneTurn = true;

        public MainPage()
        {
            InitializeComponent();
            InitializeVowelArray();
            InitializeConsonantArray();
            SubmitButton.IsEnabled = false; 
        }

        private void InitializeVowelArray()
        {
            int index = 0;
            for (int i = 0; i < 15; i++) vowels[index++] = 'A';
            for (int i = 0; i < 21; i++) vowels[index++] = 'E';
            for (int i = 0; i < 13; i++) vowels[index++] = 'I';
            for (int i = 0; i < 13; i++) vowels[index++] = 'O';
            for (int i = 0; i < 5; i++) vowels[index++] = 'U';
        }

        private void InitializeConsonantArray()
        {
            int index = 0;
            for (int i = 0; i < 2; i++) consonants[index++] = 'B';
            for (int i = 0; i < 3; i++) consonants[index++] = 'C';
            for (int i = 0; i < 6; i++) consonants[index++] = 'D';
            for (int i = 0; i < 2; i++) consonants[index++] = 'F';
            for (int i = 0; i < 3; i++) consonants[index++] = 'G';
            for (int i = 0; i < 2; i++) consonants[index++] = 'H';
            for (int i = 0; i < 1; i++) consonants[index++] = 'J';
            for (int i = 0; i < 1; i++) consonants[index++] = 'K';
            for (int i = 0; i < 5; i++) consonants[index++] = 'L';
            for (int i = 0; i < 4; i++) consonants[index++] = 'M';
            for (int i = 0; i < 8; i++) consonants[index++] = 'N';
            for (int i = 0; i < 4; i++) consonants[index++] = 'P';
            for (int i = 0; i < 1; i++) consonants[index++] = 'Q';
            for (int i = 0; i < 9; i++) consonants[index++] = 'R';
            for (int i = 0; i < 9; i++) consonants[index++] = 'S';
            for (int i = 0; i < 9; i++) consonants[index++] = 'T';
            for (int i = 0; i < 1; i++) consonants[index++] = 'V';
            for (int i = 0; i < 1; i++) consonants[index++] = 'W';
            for (int i = 0; i < 1; i++) consonants[index++] = 'X';
            for (int i = 0; i < 1; i++) consonants[index++] = 'Y';
            for (int i = 0; i < 1; i++) consonants[index++] = 'Z';
        }

        private char GetRandomVowel()
        {
            return vowels[random.Next(vowels.Length)];
        }

        private char GetRandomConsonant()
        {
            return consonants[random.Next(consonants.Length)];
        }

        private void OnConsonantClicked(object sender, EventArgs e)
        {
            if (lettersPicked < 9)
            {
                SetLetter(GetRandomConsonant());
                lettersPicked++;
                if (lettersPicked == 9)
                {
                    ConsonantButton.IsEnabled = false;
                    VowelButton.IsEnabled = false;
                    SubmitButton.IsEnabled = true;
                }
            }
        }

        private void OnVowelClicked(object sender, EventArgs e)
        {
            if (lettersPicked < 9)
            {
                SetLetter(GetRandomVowel());
                lettersPicked++;
                if (lettersPicked == 9)
                {
                    ConsonantButton.IsEnabled = false;
                    VowelButton.IsEnabled = false;
                    SubmitButton.IsEnabled = true;
                }
            }
        }

        private void SetLetter(char letter)
        {
            switch (lettersPicked)
            {
                case 0:
                    Letter0.Text = letter.ToString();
                    break;
                case 1:
                    Letter1.Text = letter.ToString();
                    break;
                case 2:
                    Letter2.Text = letter.ToString();
                    break;
                case 3:
                    Letter3.Text = letter.ToString();
                    break;
                case 4:
                    Letter4.Text = letter.ToString();
                    break;
                case 5:
                    Letter5.Text = letter.ToString();
                    break;
                case 6:
                    Letter6.Text = letter.ToString();
                    break;
                case 7:
                    Letter7.Text = letter.ToString();
                    break;
                case 8:
                    Letter8.Text = letter.ToString();
                    break;
            }
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            // Stop the timer when submitting
            if (timer != null)
            {
                timer.Stop();
            }

            // Concatenate the text from all entries and display it in an alert
            var enteredText = $"{Letter0.Text}{Letter1.Text}{Letter2.Text}{Letter3.Text}{Letter4.Text}{Letter5.Text}{Letter6.Text}{Letter7.Text}{Letter8.Text}";
            DisplayAlert("Entered Text", enteredText, "OK");

            // Resets game for next player
            lettersPicked = 0;
            ConsonantButton.IsEnabled = true;
            VowelButton.IsEnabled = true;
            SubmitButton.IsEnabled = false;
            ClearLetters();

            // Switches the players
            isPlayerOneTurn = !isPlayerOneTurn;
            CurrentPlayerLabel.Text = isPlayerOneTurn ? "Player 1's Turn" : "Player 2's Turn";
        }

        private void ClearLetters()
        {
            Letter0.Text = string.Empty;
            Letter1.Text = string.Empty;
            Letter2.Text = string.Empty;
            Letter3.Text = string.Empty;
            Letter4.Text = string.Empty;
            Letter5.Text = string.Empty;
            Letter6.Text = string.Empty;
            Letter7.Text = string.Empty;
            Letter8.Text = string.Empty;
        }

        private void OnStartTimerClicked(object sender, EventArgs e)
        {
            timeLeft = 30;
            TimerLabel.Text = timeLeft.ToString();
            if (timer != null)
            {
                timer.Stop();
                timer.Elapsed -= OnTimedEvent;
                timer.Dispose();
            }
            timer = new System.Timers.Timer(1000); 
            timer.Elapsed += OnTimedEvent;
            timer.Start();
            SubmitButton.IsEnabled = true; 
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            timeLeft--;
            Device.BeginInvokeOnMainThread(() =>
            {
                TimerLabel.Text = timeLeft.ToString();
                if (timeLeft <= 0)
                {
                    timer.Stop();
                    SubmitButton.IsEnabled = false; 
                    DisplayAlert("Time's up!", "The 30 seconds are over.", "OK");
                }
            });
        }
    }
}
