using System;
using Newtonsoft.Json;
using System.Timers;
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;


namespace AppsRepeatProject
{
    public partial class MainPage : ContentPage
    {
        //APIKEY: f95a746971msh82f62cd195b8924p11e97ejsnb5a03f4800ab 

        // Arrays for vowels and consonants
        private readonly char[] vowels = new char[67];
        private readonly char[] consonants = new char[74];
        private Random random = new Random();
        private System.Timers.Timer timer;
        private int timeLeft;
        private int lettersPicked;
        private int player1Points = 0;
        private int player2Points = 0;
        private bool isPlayerOneTurn = true;
        private string playerOneResult = string.Empty;
        private string playerTwoResult = string.Empty;
        private string[] randomLetters = new string[9];
        private string[] playerOneEntries = new string[9];
        private string[] playerTwoEntries = new string[9];
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

        private void OnNavigateToSettingsClicked(object sender, EventArgs e)
        {
            // Navigate to the Settings page
            Navigation.PushAsync(new SettingsPage());
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
            randomLetters[lettersPicked] = letter.ToString(); // Store the random letters
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
        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            timer.Stop();
            isGameFinished = true;

            if (isPlayerOneTurn)
            {
                playerOneResult = GetPlayerEntries();

                bool isValid = await CheckWordValidity(playerOneResult);

                if (isValid)
                {
                    player1Points += playerOneResult.Length;
                }

                isPlayerOneTurn = false;
                CurrentPlayerLabel.Text = $"Player 2's Turn ({playerTwoName})";
                ClearLetterEntries();
                lettersPicked = 0;
                timeLeft = 30;
                TimerLabel.Text = timeLeft.ToString();
                StartTimer();
            }
            else
            {
                playerTwoResult = GetPlayerEntries();

                bool isValid = await CheckWordValidity(playerTwoResult);

                if (isValid)
                {
                    player2Points += playerTwoResult.Length;
                }

                await DisplayAlert("Results",
                    $"Player 1 ({playerOneName}): {player1Points} points\nPlayer 2 ({playerTwoName}): {player2Points} points",
                    "OK");

                StartNewRoundButton.IsEnabled = true;
                ConsonantButton.IsEnabled = false;
                VowelButton.IsEnabled = false;
                SubmitButton.IsEnabled = false;
            }
        }



        //Models for API key, using MeriamWebbster
        public class Definition
        {
            public string Fl { get; set; } // Part of Speech
            public List<string> Shortdef { get; set; } // List of Definitions
        }

        public class WordEntry
        {
            public List<Definition> Def { get; set; }
        }

        public class ApiResponse
        {
            public List<WordEntry> WordEntries { get; set; }
        }




        // Checks the validity of a given word by calling the WordsAPI.
        private async Task<bool> CheckWordValidity(string word)
        {
            using (HttpClient client = new HttpClient())
            {
                // I know in real life scenarios storing API keys directly in the source code is not best practice
                //Due to security reasons. For purpose of this academic project I did not store them in secure location
                string apiKey = "1afe06d2-92cc-47b4-9491-ead2bbf852f7";
                string url = $"https://dictionaryapi.com/api/v3/references/collegiate/json/{word}?key={apiKey}";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string content = await response.Content.ReadAsStringAsync();

                   
                    var entries = JsonConvert.DeserializeObject<List<WordEntry>>(content);

                    
                    if (entries != null && entries.Count > 0)
                    {
                        foreach (var entry in entries)
                        {
                            if (entry.Def != null && entry.Def.Count > 0)
                            {
                                return true; 
                            }
                        }
                    }
                }
                catch (HttpRequestException)
                {
                    // Handle HTTP request error
                }
                catch (JsonException)
                {
                    // Handle JSON parsing error
                }
                catch (Exception)
                {
                    // Handle general exceptions
                }

                return false;
            }
        }




        private async void TestApi()
        {
            string word = "example";
            bool isValid = await CheckWordValidity(word);

            Console.WriteLine($"The word '{word}' is {(isValid ? "valid" : "invalid")}.");
        }



        // Method to calculate and display the scores
        // Update this method to directly display points
        private async void CalculateAndDisplayScores()
        {
            try
            {
                await DisplayAlert("Results",
                    $"Player 1 ({playerOneName}): {player1Points} points\nPlayer 2 ({playerTwoName}): {player2Points} points",
                    "OK");
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
            }
        }

        private void ClearLetterEntries()
        {
            Entry0.Text = string.Empty;
            Entry1.Text = string.Empty;
            Entry2.Text = string.Empty;
            Entry3.Text = string.Empty;
            Entry4.Text = string.Empty;
            Entry5.Text = string.Empty;
            Entry6.Text = string.Empty;
            Entry7.Text = string.Empty;
            Entry8.Text = string.Empty;
        }

        private void ClearLetterBoxes()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing letter boxes: {ex.Message}");
            }
        }

        // Check if all 9 Entry controls have text
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            
            bool allEntriesFilled = !string.IsNullOrEmpty(Entry0.Text) &&
                                    !string.IsNullOrEmpty(Entry1.Text) &&
                                    !string.IsNullOrEmpty(Entry2.Text) &&
                                    !string.IsNullOrEmpty(Entry3.Text) &&
                                    !string.IsNullOrEmpty(Entry4.Text) &&
                                    !string.IsNullOrEmpty(Entry5.Text) &&
                                    !string.IsNullOrEmpty(Entry6.Text) &&
                                    !string.IsNullOrEmpty(Entry7.Text) &&
                                    !string.IsNullOrEmpty(Entry8.Text);

            // Enable or disable the Submit button based on whether all entries are filled
            SubmitButton.IsEnabled = allEntriesFilled;
        }


        // Returns the letters
        private string GetPlayerEntries()
        {
            string result = string.Empty;

            for (int i = 0; i < 9; i++)
            {
                var entry = (Entry)this.FindByName($"Entry{i}");
                if (!string.IsNullOrEmpty(entry.Text))
                {
                    result += entry.Text;
                }
            }

            return result;
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
            ClearLetterEntries(); 

            CurrentPlayerLabel.Text = $"Player 1's Turn ({playerOneName})";
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
            TestApi();
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
            ClearLetterBoxes();

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

                SubmitButton.IsEnabled = true;
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




