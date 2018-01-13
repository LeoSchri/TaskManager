using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static TaskManager.Data.Types;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static StackPanel TaskStackPanel { get; private set; }
        public static StackPanel CreateTaskStackPanel { get; private set; }
        public static Button AddTaskBtn { get; private set; }

        public static StackPanel StateStackPanel { get; private set; }
        public static TextBox TaskID { get; set; }      
        public static TextBox TaskName { get; set; }
        public static TextBox TaskDescription { get; set; }
        public static DatePicker TaskDueDate { get; set; }
        public static ComboBox TaskImportance { get; set; }
        public static ComboBox TaskState { get; set; }

        public static string SaveCommand { get; set; }
        public static bool HideDoneTasks { get; set; }
        public static string OrderBy { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DatabaseConnector.Connect();


            Closing += new CancelEventHandler(MainWindow_Closing);

            double screenHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            Height = screenHeight;
            Top = 0;
            Width = 600;
            Left = SystemParameters.PrimaryScreenWidth - Width;

            FontFamily = new FontFamily("Franklin Gothic Medium");

            addBtn.Background = Brushes.Aquamarine;
            settingBtn.Background = Brushes.PaleVioletRed;
            settingBtn.FontSize = 8;
            saveBtn.Background = Brushes.Aquamarine;
            cancelBtn.Background = Brushes.Tomato;

            comboImportance.ItemsSource = new List<string> { "Low", "Medium", "High" };
            comboState.ItemsSource = new List<string> { "New", "InProgress", "Paused", "Done"};

            Screenbuilder.BuildTaskField(taskStack);

            TaskStackPanel = taskStack;
            CreateTaskStackPanel = CreateTaskStack;
            AddTaskBtn = addBtn;

            StateStackPanel = StateStack;
            TaskID = txtID;
            TaskName = txtName;
            TaskDescription = txtDescription;
            TaskDueDate = DatePick;
            TaskImportance = comboImportance;
            TaskState = comboState;

            HideDoneTasks = false;

        }

        public void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            DatabaseConnector.Disconnect();
        }

        public void addTask(object sender, RoutedEventArgs e)
        {
            CreateTaskStackPanel.Visibility = Visibility.Visible;
            AddTaskBtn.Visibility = Visibility.Hidden;

            StateStack.Visibility = Visibility.Collapsed;
            txtID.Text = "0";

            SaveCommand = "Create";

            MainWindow.TaskName.Text = string.Empty;
            MainWindow.TaskDescription.Text = string.Empty;
            MainWindow.TaskDueDate.Text = string.Empty;
            MainWindow.TaskImportance.SelectedItem = string.Empty;
            MainWindow.TaskState.SelectedItem = string.Empty;
        }

        public void Close_CreateTask(object sender, RoutedEventArgs e)
        {
            CreateTaskStackPanel.Visibility = Visibility.Collapsed;
            AddTaskBtn.Visibility = Visibility.Visible;

            Screenbuilder.BuildTaskField(taskStack);
        }

        public void Save_Task(object sender, RoutedEventArgs e)
        {
            if(Validate())
            {
                Importance importance = Importance.Low;
                switch (comboImportance.Text)
                {
                    case "Medium": importance = Importance.Medium; break;
                    case "High": importance = Importance.High; break;
                }

                State state = State.New;
                switch (comboState.Text)
                {
                    case "InProgress": state = State.InProgress; break;
                    case "Paused": state = State.Paused; break;
                    case "Done": state = State.Done; break;
                }

                if (SaveCommand == "Create")
                {
                    var task = new Task(0, txtName.Text, txtDescription.Text, DateTime.Parse(DatePick.Text), State.New, importance, string.Empty, null);
                    DatabaseManager.InsertTask(task);
                }
                else if(SaveCommand == "Edit")
                {
                    var task = new Task(Convert.ToInt32(txtID.Text), txtName.Text, txtDescription.Text, DateTime.Parse(DatePick.Text), state, importance, string.Empty,null);
                    DatabaseManager.UpdateTask(task);
                }

                CreateTaskStackPanel.Visibility = Visibility.Collapsed;
                AddTaskBtn.Visibility = Visibility.Visible;

                txtID.Text = string.Empty;
                SaveCommand = string.Empty;

                Screenbuilder.BuildTaskField(taskStack);
            }
        }

        public void Hide_UnhideTask(object sender, RoutedEventArgs e)
        {
            if (HideDoneTasks)
            {
                HideDoneTasks = false;
                settingBtn.Content = "Hide Done Tasks";
                settingBtn.Background = Brushes.PaleVioletRed;
            }
            else
            {
                HideDoneTasks = true;
                settingBtn.Content = "Show Done Tasks";
                settingBtn.Background = Brushes.DarkCyan;
            }

            Screenbuilder.BuildTaskField(taskStack);
        }

        private bool Validate()
        {
            var isValid = true;
            if (string.IsNullOrEmpty(txtID.Text))
                isValid = false;
            if (string.IsNullOrEmpty(txtName.Text))
                isValid = false;
            if (string.IsNullOrEmpty(comboImportance.Text))
                isValid = false;
            if (string.IsNullOrEmpty(DatePick.Text))
                isValid = false;
            if (DateTime.Parse(DatePick.Text) == null)
                isValid = false;
            return isValid;
        }

        
    }
}
