using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        private readonly TodoService _service;
        private readonly string _dataFile = "tasks.json";

        public MainWindow()
        {
            InitializeComponent();

            var repository = new JsonTaskRepository(_dataFile);
            _service = new TodoService(repository);

            _service.LoadFromFile();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            TasksGrid.ItemsSource = null;
            TasksGrid.ItemsSource = _service.Tasks;
        }

        private TodoModel GetSelected() => TasksGrid.SelectedItem as TodoModel;

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TitleBox.Text))
                {
                    MessageBox.Show("Введите название");
                    return;
                }

                var task = new TodoModel
                {
                    Title = TitleBox.Text,
                    Description = DescriptionBox.Text,
                    DueDate = DueDatePicker.SelectedDate ?? DateTime.Now,
                    Priority = (TaskPriority)PriorityBox.SelectedIndex,
                    Status = (TaskStatus)StatusBox.SelectedIndex
                };

                _service.AddTask(task);
                RefreshGrid();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var task = GetSelected();
            if (task == null)
            {
                MessageBox.Show("Выберите задачу");
                return;
            }

            try
            {
                _service.EditTask(task.Id, TitleBox.Text, DescriptionBox.Text,
                    DueDatePicker.SelectedDate ?? task.DueDate,
                    (TaskPriority)PriorityBox.SelectedIndex);
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var task = GetSelected();
            if (task == null) return;

            _service.DeleteTask(task.Id);
            RefreshGrid();
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            var task = GetSelected();
            if (task == null) return;

            _service.ChangeStatus(task.Id, (TaskStatus)StatusBox.SelectedIndex);
            RefreshGrid();
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            _service.SaveToFile();
            MessageBox.Show("Сохранено");
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            _service.LoadFromFile();
            RefreshGrid();
        }

        private void ClearInputs()
        {
            TitleBox.Clear();
            DescriptionBox.Clear();
            DueDatePicker.SelectedDate = DateTime.Now.AddDays(1);
            PriorityBox.SelectedIndex = 1;
            StatusBox.SelectedIndex = 0;
        }
    }
}