using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace MTAmalachukwuAzubike
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddToSchedule_Click(object sender, RoutedEventArgs e)
        {
            // Reset message text and set to red initially for error messages
            MessageTextBlock.Text = string.Empty;
            MessageTextBlock.Foreground = Brushes.Red;

            // Retrieve input values
            string fullName = FullNameTextBox.Text.Trim();
            string medicalRecord = MedicalRecordTextBox.Text.Trim();
            string ageText = AgeTextBox.Text.Trim();

            // Error list Initializaion
            List<string> errors = new List<string>();

            // Validation check
            if (string.IsNullOrWhiteSpace(fullName) || !fullName.Contains(" "))
                errors.Add("Please enter the patient's full name in 'First Last' format.");

            if (!Regex.IsMatch(medicalRecord, @"^[1-9]\d{4}$"))
                errors.Add("Medical record number must be exactly 5 digits, with the first digit non-zero.");

            if (!int.TryParse(ageText, out int age) || age < 0 || age > 120)
                errors.Add("Please enter a valid age between 0 and 120.");

            // Add patient to schedule if there are no validation errors
            if (errors.Count == 0)
            {
                // To Format and add patient info to list box
                string formattedPatientInfo = $"{fullName.ToUpper()} ({medicalRecord}); Age: {age}";
                ScheduleListBox.Items.Add(formattedPatientInfo);

                // To Update message to success and clear fields
                MessageTextBlock.Text = "Patient added successfully!";
                MessageTextBlock.Foreground = Brushes.Green;

                FullNameTextBox.Clear();
                MedicalRecordTextBox.Clear();
                AgeTextBox.Clear();
            }
            else
            {
                // To Display validation errors
                MessageTextBlock.Text = string.Join(Environment.NewLine, errors);
            }
        }
    }
}
