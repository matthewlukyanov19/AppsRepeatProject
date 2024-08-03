using System;
using System.Timers;
using Microsoft.Maui.Controls;

namespace AppsRepeatProject
{
    public partial class MainPage : ContentPage
    {
        // Arrays for vowels and consonants
        private readonly char[] vowels = new char[67];
        private readonly char[] consonants = new char[74];
        private Random random = new Random();
        private System.Timers.Timer timer;
        private int timeLeft;
        private int lettersPicked;
        private bool isPlayerOneTurn = true;
        private string playerOneResult = string.Empty;
        private string playerTwoResult = string.Empty;
        private bool isGameFinished = false;
        private int roundNumber = 1;
        private string playerOneName;
        private string playerTwoName;

        public MainPage()
        {
            InitializeComponent();
            InitializeVowelArray();
            InitializeConsonantArray();
            SubmitButton.IsEnabled = false;
            StartNewRoundButton.IsEnabled = false;
            UpdateRoundLabel();
            HideGameElements();
        }

        // Initialize the vowel array with the specified units
        private void InitializeVowelArray()
        {
            int index = 0;
            for (int i = 0; i < 15; i++) vowels[index++] = 'A';
            for (int i = 0; i < 21; i++) vowels[index++] = 'E';
            for (int i = 0; i < 13; i++) vowels[index++] = 'I';
            for (int i = 0; i < 13; i++) vowels[index++] = 'O';
            for (int i = 0; i < 5; i++) vowels[index++] = 'U';
        }

        // Initialize the consonant array with the specified units
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

        // Get a random vowel from the vowel array
        private char GetRandomVowel()
        {
            return vowels[random.Next(vowels.Length)];
        }

        // Get a random consonant from the consonant array
        private char GetRandomConsonant()
        {
            return consonants[random.Next(consonants.Length)];
        }

        // Handler for when the Consonant button is clicked
        private void OnConsonantClicked(object sender, EventArgs e)
        {
            if (!isGameFinished && lettersPicked < 9)
            {
                SetLetter(GetRandomConsonant());
                lettersPicked++;
                CheckSubmitEnabled();
            }
        }

        // Handler for when the Vowel button is clicked
        private void OnVowelClicked(object sender, EventArgs e)
        {
            if (!isGameFinished && lettersPicked < 9)
            {
                SetLetter(GetRandomVowel());
                lettersPicked++;
                CheckSubmitEnabled();
            }
        }

        // Set a letter in the right position of the 3x3 grid based on the number of letters picked
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

        // Check if all 9 letters are entered and enable Submit button if true
        private void CheckSubmitEnabled()
        {
            SubmitButton.IsEnabled = lettersPicked == 9;
        }

        // Clear all letter boxes
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

        // Handler for when the Submit button is clicked
        private void OnSubmitClicked(object sender, EventArgs e)
        {
            timer.Stop();
            isGameFinished = true;
            if (isPlayerOneTurn)
            {
                playerOneResult = GetLetters();
                isPlayerOneTurn = false;
                CurrentPlayerLabel.Text = $"{playerTwoName}'s Turn";
                ClearLetters();
                lettersPicked = 0;
                timeLeft = 30;
                TimerLabel.Text = timeLeft.ToString();
                StartTimer();
            }
            else
            {
                playerTwoResult = GetLetters();
                DisplayAlert("Results", $"{playerOneName}: {playerOneResult}\n{playerTwoName}: {playerTwoResult}", "OK");
                StartNewRoundButton.IsEnabled = true;
                ConsonantButton.IsEnabled = false;
                VowelButton.IsEnabled = false;
                SubmitButton.IsEnabled = false;
            }
        }

        // Returns the letters
        private string GetLetters()
        {
            return $"{Letter0.Text}{Letter1.Text}{Letter2.Text}{Letter3.Text}{Letter4.Text}{Letter5.Text}{Letter6.Text}{Letter7.Text}{Letter8.Text}";
        }

        // Starts the timer 
        private void OnStartTimerClicked(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void StartTimer()
        {
            timeLeft = 30;
            TimerLabel.Text = timeLeft.ToString();
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.Start();
            ConsonantButton.IsEnabled = true;
            VowelButton.IsEnabled = true;
            SubmitButton.IsEnabled = false;
            lettersPicked = 0;
            ClearLetters();
            isGameFinished = false;
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
                    if (!isGameFinished)
                    {
                        DisplayAlert("Time's up!", "The 30 seconds are over.", "OK");
                        isGameFinished = true;
                        ConsonantButton.IsEnabled = false;
                        VowelButton.IsEnabled = false;
                    }
                }
            });
        }

        // Handler for starting a new round
        private void OnStartNewRoundClicked(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
            }

            roundNumber++;
            UpdateRoundLabel();

            isPlayerOneTurn = true;
            playerOneResult = string.Empty;
            playerTwoResult = string.Empty;
            lettersPicked = 0;
            isGameFinished = false;
            ClearLetters();

            CurrentPlayerLabel.Text = $"{playerOneName}'s Turn";
            ConsonantButton.IsEnabled = false;
            VowelButton.IsEnabled = false;
            SubmitButton.IsEnabled = false;
            StartNewRoundButton.IsEnabled = false;
        }

        private void UpdateRoundLabel()
        {
            RoundLabel.Text = $"Round {roundNumber}";
        }

        // Event handler for Start Game button click
        private void OnStartGameClicked(object sender, EventArgs e)
        {
            playerOneName = PlayerOneNameEntry.Text;
            playerTwoName = PlayerTwoNameEntry.Text;

            if (string.IsNullOrWhiteSpace(playerOneName) || string.IsNullOrWhiteSpace(playerTwoName))
            {
                DisplayAlert("Error", "Please enter names for both players.", "OK");
                return;
            }

        
            PlayerOneNameEntry.IsVisible = false;
            PlayerTwoNameEntry.IsVisible = false;
            (sender as Button).IsVisible = false;

           
            ShowGameElements();

            // Initialize the game state
            StartNewRound();
        }

        private void StartNewRound()
        {
            roundNumber = 1;
            UpdateRoundLabel();

            isPlayerOneTurn = true;
            playerOneResult = string.Empty;
            playerTwoResult = string.Empty;
            lettersPicked = 0;
            isGameFinished = false;
            ClearLetters();

            CurrentPlayerLabel.Text = $"{playerOneName}'s Turn";
            ConsonantButton.IsEnabled = false;
            VowelButton.IsEnabled = false;
            SubmitButton.IsEnabled = false;
            StartNewRoundButton.IsEnabled = false;
        }

        private void OnLetterTapped(object sender, EventArgs e)
        {
            var tappedLabel = sender as Label;
            if (tappedLabel != null && !string.IsNullOrEmpty(tappedLabel.Text))
            {
                AddLetterToEntries(tappedLabel.Text[0]);
                tappedLabel.Text = string.Empty;
            }
        }

        private void AddLetterToEntries(char letter)
        {
            for (int i = 0; i < 9; i++)
            {
                var entry = (Entry)this.FindByName($"Entry{i}");
                if (string.IsNullOrEmpty(entry.Text))
                {
                    entry.Text = letter.ToString();
                    break;
                }
            }
        }
        private void HideGameElements()
        {
            ConsonantButton.IsVisible = false;
            VowelButton.IsVisible = false;
            SubmitButton.IsVisible = false;
            TimerLabel.IsVisible = false;
            RoundLabel.IsVisible = false;
            CurrentPlayerLabel.IsVisible = false;
            StartNewRoundButton.IsVisible = false;
        }

        private void ShowGameElements()
        {
            ConsonantButton.IsVisible = true;
            VowelButton.IsVisible = true;
            SubmitButton.IsVisible = true;
            TimerLabel.IsVisible = true;
            RoundLabel.IsVisible = true;
            CurrentPlayerLabel.IsVisible = true;
            StartNewRoundButton.IsVisible = true;
        }
    }
}




