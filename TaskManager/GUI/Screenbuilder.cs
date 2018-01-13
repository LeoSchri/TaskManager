using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static TaskManager.Data.Types;

namespace TaskManager
{
    class Screenbuilder
    {
        public static void BuildTaskField(StackPanel parent)
        {
            parent.Children.Clear();

            var height = 30;

            var lines = new List<StackPanel>();
            var tasks = DatabaseManager.GetAllTasks();

            switch(MainWindow.OrderBy)
            {
                case "State": tasks = tasks.OrderBy(t => t.State).ToList(); break;
                case "Importance": tasks = tasks.OrderBy(t => t.Importance).ToList(); break;
                case "Name": tasks = tasks.OrderBy(t => t.Name).ToList(); break;
                case "Description": tasks = tasks.OrderBy(t => t.Description).ToList(); break;
                case "Due date": tasks = tasks.OrderBy(t => t.DueDate).ToList(); break;
                case "Message": tasks = tasks.OrderBy(t => t.Message).ToList(); break;
                case "Last modified": tasks = tasks.OrderBy(t => t.LastModified).ToList(); break;
            }

            if (MainWindow.HideDoneTasks)
            {
                tasks = tasks.Where(t => t.State != State.Done).ToList();
            }

            var header = new StackPanel();
            header.Orientation = Orientation.Horizontal;
            header.HorizontalAlignment = HorizontalAlignment.Stretch;
            header.VerticalAlignment = VerticalAlignment.Stretch;
            header.Margin = new Thickness(0, 0, 0, 2);

            var btnStateH = new Button();
            btnStateH.Background = Brushes.Black;
            btnStateH.VerticalContentAlignment = VerticalAlignment.Center;
            btnStateH.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnStateH.Height = height;
            btnStateH.FontSize = 10;
            btnStateH.Width = 50;
            btnStateH.Margin = new Thickness(0, 0, 2, 0);
            btnStateH.BorderThickness = new Thickness(0);
            btnStateH.Content = "State";
            btnStateH.Foreground = Brushes.White;
            btnStateH.Click += ChangeOrderBy;
            header.Children.Add(btnStateH);

            var btnImportanceH = new Button();
            btnImportanceH.Height = height;
            btnImportanceH.Width = 40;
            btnImportanceH.Background = Brushes.Black;
            btnImportanceH.VerticalContentAlignment = VerticalAlignment.Center;
            btnImportanceH.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnImportanceH.Margin = new Thickness(0, 0, 2, 0);
            btnImportanceH.FontSize = 6;
            btnImportanceH.Content = "Importance";
            btnImportanceH.Foreground = Brushes.White;
            btnImportanceH.BorderThickness = new Thickness(0);
            btnImportanceH.Click += ChangeOrderBy;
            header.Children.Add(btnImportanceH);

            var btnNameH = new Button();
            btnNameH.Foreground = Brushes.White;
            btnNameH.Background = Brushes.Black;
            btnNameH.Height = height;
            btnNameH.VerticalContentAlignment = VerticalAlignment.Center;
            btnNameH.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnNameH.FontSize = 10;
            btnNameH.Padding = new Thickness(2);
            btnNameH.Margin = new Thickness(0, 0, 2, 0);
            btnNameH.Width = 65;
            btnNameH.Content = "Name";
            btnNameH.BorderThickness = new Thickness(0);
            btnNameH.Click += ChangeOrderBy;
            header.Children.Add(btnNameH);

            var btnDescriptionH = new Button();
            btnDescriptionH.FontSize = 10;
            btnDescriptionH.Background = Brushes.Black;
            btnDescriptionH.Foreground = Brushes.White;
            btnDescriptionH.Height = height;
            btnDescriptionH.Margin = new Thickness(0, 0, 2, 0);
            btnDescriptionH.VerticalContentAlignment = VerticalAlignment.Center;
            btnDescriptionH.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnDescriptionH.Padding = new Thickness(2);
            btnDescriptionH.Width = 125;
            btnDescriptionH.Content = "Description";
            btnDescriptionH.BorderThickness = new Thickness(0);
            btnDescriptionH.Click += ChangeOrderBy;
            header.Children.Add(btnDescriptionH);

            var btnDueDateH = new Button();
            btnDueDateH.FontSize = 8;
            btnDueDateH.Background = Brushes.Black;
            btnDueDateH.Foreground = Brushes.White;
            btnDueDateH.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnDueDateH.VerticalContentAlignment = VerticalAlignment.Center;
            btnDueDateH.Margin = new Thickness(0, 0, 2, 0);
            btnDueDateH.Height = height;
            btnDueDateH.Width = 47.5;
            btnDueDateH.Content = "Due date";
            btnDueDateH.BorderThickness = new Thickness(0);
            btnDueDateH.Click += ChangeOrderBy;
            header.Children.Add(btnDueDateH);

            var btnMessageH = new Button();
            btnMessageH.FontSize = 10;
            btnMessageH.Background = Brushes.Black;
            btnMessageH.Foreground = Brushes.White;
            btnMessageH.Padding = new Thickness(2);
            btnMessageH.Margin = new Thickness(0, 0, 2, 0);
            btnMessageH.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnMessageH.VerticalContentAlignment = VerticalAlignment.Center;
            btnMessageH.Height = height;
            btnMessageH.Width = 65;
            btnMessageH.Content = "Message";
            btnMessageH.BorderThickness = new Thickness(0);
            btnMessageH.Click += ChangeOrderBy;
            header.Children.Add(btnMessageH);

            var gap1 = new Label();
            gap1.FontSize = 10;
            gap1.Background = Brushes.Black;
            gap1.Height = height;
            gap1.Margin = new Thickness(0, 0, 2, 0);
            gap1.Width = 30;
            header.Children.Add(gap1);

            var gap2 = new Label();
            gap2.FontSize = 10;
            gap2.Background = Brushes.Black;
            gap2.Margin = new Thickness(0, 0, 2, 0);
            gap2.Height = height;
            gap2.Width = 30;
            header.Children.Add(gap2);

            var btnLastModifiedH = new Button();
            btnLastModifiedH.FontSize = 8;
            btnLastModifiedH.Background = Brushes.Black;
            btnLastModifiedH.Foreground = Brushes.White;
            btnLastModifiedH.Padding = new Thickness(2);
            btnLastModifiedH.Margin = new Thickness(0, 0, 2, 0);
            btnLastModifiedH.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnLastModifiedH.VerticalContentAlignment = VerticalAlignment.Center;
            btnLastModifiedH.Height = height;
            btnLastModifiedH.Width = 75;
            btnLastModifiedH.Content = "Last modified";
            btnLastModifiedH.BorderThickness = new Thickness(0);
            btnLastModifiedH.Click += ChangeOrderBy;
            header.Children.Add(btnLastModifiedH);

            lines.Add(header);


            if (tasks != null && tasks.Any())
            {
                tasks.ForEach(task =>
                {
                    var line = new StackPanel();
                    line.Orientation = Orientation.Horizontal;
                    line.HorizontalAlignment = HorizontalAlignment.Stretch;
                    line.VerticalAlignment = VerticalAlignment.Stretch;
                    line.Margin = new Thickness(0, 0, 0, 2);

                    var labelState = new Label();
                    labelState.Foreground = Brushes.Black;
                    labelState.VerticalContentAlignment = VerticalAlignment.Center;
                    labelState.HorizontalContentAlignment = HorizontalAlignment.Center;
                    labelState.FontSize = 8;
                    labelState.Height = height;
                    labelState.Width = 50;
                    labelState.Margin = new Thickness(0,0,2,0);
                    labelState.Content = task.State;
                    switch (task.State)
                    {
                        case State.New: labelState.Background = Brushes.Beige; break;
                        case State.InProgress: labelState.Background = Brushes.DarkCyan; break;
                        case State.Paused: labelState.Background = Brushes.PaleVioletRed; break;
                        case State.Done: labelState.Background = Brushes.Aquamarine; break;
                    }
                    line.Children.Add(labelState);

                    var labelImportance = new Label();
                    labelImportance.Height = height;
                    labelImportance.FontSize = 8;
                    labelImportance.Width = 40;
                    labelImportance.Foreground = Brushes.Black;
                    labelImportance.VerticalContentAlignment = VerticalAlignment.Center;
                    labelImportance.HorizontalContentAlignment = HorizontalAlignment.Center;
                    labelImportance.Margin = new Thickness(0,0,2,0);
                    labelImportance.Content = task.Importance;
                    switch (task.Importance)
                    {
                        case Importance.Low: labelImportance.Background = Brushes.Beige; break;
                        case Importance.Medium: labelImportance.Background = Brushes.SandyBrown; break;
                        case Importance.High: labelImportance.Background = Brushes.Tomato; break;
                    }
                    line.Children.Add(labelImportance);

                    var tbName = new TextBlock();
                    tbName.Foreground = Brushes.Black;
                    tbName.Background = Brushes.White;
                    tbName.FontSize = 8;
                    tbName.Height = height;
                    tbName.TextWrapping = TextWrapping.Wrap;
                    tbName.TextAlignment = TextAlignment.Center;
                    tbName.Padding = new Thickness(2);
                    tbName.Margin = new Thickness(0,0,2,0);
                    tbName.Width = 65;
                    tbName.Text = task.Name;
                    line.Children.Add(tbName);

                    var tbDescription = new TextBlock();
                    tbDescription.Background = Brushes.White;
                    tbDescription.Foreground = Brushes.Black;
                    tbDescription.FontSize = 8;
                    tbDescription.Height = height;
                    tbDescription.Margin = new Thickness(0,0,2,0);
                    tbDescription.Padding = new Thickness(2);
                    tbDescription.TextAlignment = TextAlignment.Center;
                    tbDescription.TextWrapping = TextWrapping.Wrap;
                    tbDescription.Width = 125;
                    tbDescription.Text = task.Description;
                    line.Children.Add(tbDescription);

                    var labelDueDate = new Label();
                    labelDueDate.FontSize = 6;
                    labelDueDate.Background = Brushes.White;
                    labelDueDate.Foreground = Brushes.Black;
                    labelDueDate.VerticalContentAlignment = VerticalAlignment.Center;
                    labelDueDate.Margin = new Thickness(0,0,2,0);
                    labelDueDate.Height = height;
                    labelDueDate.Width = 47.5;
                    labelDueDate.Content = task.DueDate.Date.ToString("G").Split(' ')[0];
                    line.Children.Add(labelDueDate);

                    var tbMessage = new Label();
                    
                    tbMessage.FontSize = 8;
                    tbMessage.VerticalContentAlignment = VerticalAlignment.Center;
                    tbMessage.HorizontalContentAlignment = HorizontalAlignment.Center;
                    tbMessage.Foreground = Brushes.Black;
                    tbMessage.Margin = new Thickness(0,0,2,0);
                    tbMessage.Padding = new Thickness(2);
                    tbMessage.Height = height;
                    tbMessage.Width = 65;
                    tbMessage.Content = task.Message;
                    switch (tbMessage.Content)
                    {
                        case "Close!": tbMessage.Background = Brushes.Beige; break;
                        case "Urgent!!": tbMessage.Background = Brushes.SandyBrown; break;
                        case "Overdue!!!": tbMessage.Background = Brushes.Tomato; break;
                        default: tbMessage.Background = Brushes.White; break;
                    }
                    line.Children.Add(tbMessage);

                    var editBtn = new Button();
                    editBtn.Name = $"edit_{task.ID}";
                    editBtn.Background = Brushes.DarkCyan;
                    editBtn.FontSize = 8;
                    editBtn.Foreground = Brushes.Black;
                    editBtn.Margin = new Thickness(0,0,2,0);
                    editBtn.Height = height;
                    editBtn.Width = 30;
                    editBtn.BorderThickness = new Thickness(0);
                    editBtn.Content = "Edit";
                    editBtn.Click += EditTask;
                    line.Children.Add(editBtn);

                    var deleteBtn = new Button();
                    deleteBtn.Name = $"delete_{task.ID}";
                    deleteBtn.BorderThickness = new Thickness(0);
                    deleteBtn.Background = Brushes.Tomato;
                    deleteBtn.FontSize = 8;
                    deleteBtn.Margin = new Thickness(0,0,2,0);
                    deleteBtn.Foreground = Brushes.Black;
                    deleteBtn.Height = height;
                    deleteBtn.Width = 30;
                    deleteBtn.Content = "Delete";
                    deleteBtn.Click += DeleteTask;
                    line.Children.Add(deleteBtn);

                    var labelLastModified = new Label();
                    labelLastModified.Background = Brushes.White;
                    labelLastModified.FontSize = 6;
                    labelLastModified.Foreground = Brushes.Black;
                    labelLastModified.Margin = new Thickness(0, 0, 2, 0);
                    labelLastModified.VerticalContentAlignment = VerticalAlignment.Center;
                    labelLastModified.HorizontalContentAlignment = HorizontalAlignment.Center;
                    labelLastModified.Padding = new Thickness(2);
                    labelLastModified.Height = height;
                    labelLastModified.Width = 75;
                    labelLastModified.Content = task.LastModified==null?string.Empty:((DateTime)task.LastModified).ToString("G");
                    line.Children.Add(labelLastModified);

                    lines.Add(line);
                });
            }

            lines.ForEach(line => parent.Children.Add(line));
        }

        private static void ChangeOrderBy(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            MainWindow.OrderBy = btn.Content.ToString();

            BuildTaskField(MainWindow.TaskStackPanel);
        }

        private static void DeleteTask(object sender, RoutedEventArgs e)
        {
            var tasks = DatabaseManager.GetAllTasks();
            if(tasks != null && tasks.Any())
            {
                var deleteBtn = (Button)sender;
                var task = tasks.Find(t=> t.ID == Convert.ToInt32(deleteBtn.Name.Split('_')[1]));

                if(task != null)
                {
                    var result = MessageBox.Show($"Do you really want to delete Task {task.Name}?","Confirm deletion",MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(result == MessageBoxResult.Yes)
                    {
                        DatabaseManager.DeleteTask(task.ID);
                    }
                }
            }

            Screenbuilder.BuildTaskField(MainWindow.TaskStackPanel);
        }

        public static void EditTask(object sender, RoutedEventArgs e)
        {
            var tasks = DatabaseManager.GetAllTasks();
            if (tasks != null && tasks.Any())
            {
                var deleteBtn = (Button)sender;
                var task = tasks.Find(t=> t.ID == Convert.ToInt32(deleteBtn.Name.Split('_')[1]));

                if (task != null)
                {
                    MainWindow.CreateTaskStackPanel.Visibility = Visibility.Visible;
                    MainWindow.AddTaskBtn.Visibility = Visibility.Hidden;

                    MainWindow.StateStackPanel.Visibility = Visibility.Visible;

                    MainWindow.SaveCommand = "Edit";

                    MainWindow.TaskID.Text = task.ID.ToString();
                    MainWindow.TaskName.Text = task.Name;
                    MainWindow.TaskDescription.Text = task.Description;
                    MainWindow.TaskDueDate.Text = task.DueDate.ToString("G").Split(' ')[0];
                    MainWindow.TaskImportance.SelectedItem = task.Importance.ToString();
                    MainWindow.TaskState.SelectedItem = task.State.ToString();
                }
            }
        }
    }
}
