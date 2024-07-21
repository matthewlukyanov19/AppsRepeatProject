using System;
using Microsoft.Maui.Controls;

namespace AppsRepeatProject
{
    public partial class MainPage : ContentPage
    {
        private readonly char[] vowels = new char[67];
        private readonly char[] consonants = new char[74];
        private Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
            InitializeVowelArray();
            InitializeConsonantArray();
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

        private char GetRandomLetter()
        {
            if (random.Next(2) == 0) // 50% chance to pick a vowel or consonant
            {
                int index = random.Next(0, 67);
                return vowels[index];
            }
            else
            {
                int index = random.Next(0, 74);
                return consonants[index];
            }
        }

        private void OnGenerateClicked(object sender, EventArgs e)
        {
            // Set the text of each entry to a random letter (vowel or consonant) when the button is clicked
            Entry0.Text = GetRandomLetter().ToString();
            Entry1.Text = GetRandomLetter().ToString();
            Entry2.Text = GetRandomLetter().ToString();
            Entry3.Text = GetRandomLetter().ToString();
            Entry4.Text = GetRandomLetter().ToString();
            Entry5.Text = GetRandomLetter().ToString();
            Entry6.Text = GetRandomLetter().ToString();
            Entry7.Text = GetRandomLetter().ToString();
            Entry8.Text = GetRandomLetter().ToString();
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            // Concatenate the text from all entries and display it in an alert
            var enteredText = $"{Entry0.Text}{Entry1.Text}{Entry2.Text}{Entry3.Text}{Entry4.Text}{Entry5.Text}{Entry6.Text}{Entry7.Text}{Entry8.Text}";
            DisplayAlert("Entered Text", enteredText, "OK");
        }
    }
}


