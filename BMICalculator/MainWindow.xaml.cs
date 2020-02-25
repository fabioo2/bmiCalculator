using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace BMICalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //unit conversion constants
        const int TO_METERS = 100;
        const int TO_INCHES = 12;
        const int TO_IMPERIAL = 703;
        //bmi constants
        const double OVERWEIGHT = 25;
        const double UNDERWEIGHT = 18.5;
        const double OBESE = 30;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BmiCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double bmi;
                if (measurementSystem.SelectedItem == metricSystem)
                {
                    double cm = double.Parse(centimetres.Text);
                    double kg = double.Parse(kilograms.Text);
                    bmi = kg / Math.Pow(cm / TO_METERS, 2);
                }
                else
                {
                    double ft = double.Parse(feet.Text);
                    double inch = double.Parse(inches.Text);
                    double lb = double.Parse(pounds.Text);
                    bmi = TO_IMPERIAL * (lb / Math.Pow(ft * TO_INCHES + inch, 2));
                }

                if (bmi > OBESE)
                {
                    resultText.Content = "Your BMI indicates that you may be";
                    bmiResult.Foreground = Brushes.Red;
                    bmiResult.Content = "OBESE";
                }
                else if (bmi > OVERWEIGHT)
                {
                    resultText.Content = "Your BMI indicates that you may be";
                    bmiResult.Foreground = Brushes.Black;
                    bmiResult.Content = "OVERWEIGHT";
                }
                else if (bmi > UNDERWEIGHT)
                {
                    resultText.Content = "Congratulations your weight is";
                    bmiResult.Foreground = Brushes.Black;
                    bmiResult.Content = "OPTIMAL";
                }
                else
                {
                    resultText.Content = "Your BMI indicates that you may be";
                    bmiResult.Foreground = Brushes.Red;
                    bmiResult.Content = "UNDERWEIGHT";
                }
                bmiText.Visibility = Visibility.Visible;
                bmiNumber.Content = bmi.ToString("#.##");
            }
            catch
            {
                MessageBox.Show("Please fill in all fields");
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            //clear textboxes/labels
            bmiText.Visibility = Visibility.Collapsed;
            bmiNumber.Content = "";
            bmiResult.Content = "";
            resultText.Content = "";
            //clear inputs
            centimetres.Text = "";
            kilograms.Text = "";
            feet.Text = "";
            inches.Text = "";
            pounds.Text = "";
        }
        //prevent users from entering non-numbers
        private void bmiTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back && e.Key != Key.Delete && e.Key != Key.Tab)
            {
                e.Handled = true;
            }
        }
        private void bmiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int result;
            int result2;
            int.TryParse((sender as TextBox).Text, out result);
            int.TryParse(inches.Text, out result2);
            //prevent user from inputting 0 for weight/height
            if (result == 0)
            {
                (sender as TextBox).Text = "";
            }
            //prevent user from entering inches greater than 12
            if (result2 > TO_INCHES)
            {
                MessageBox.Show("Inches cannot exceed 12");
                inches.Text = "";
            }
        }
    }
}
