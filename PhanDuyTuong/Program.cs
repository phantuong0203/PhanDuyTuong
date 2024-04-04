using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PhanDuyTuong
{
    class Task
    {
        public string TaskName { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public Task(string taskName, int priority, string description, string status)
        {
            TaskName = taskName;
            Priority = priority;
            Description = description;
            Status = status;
        }
    }
    public class TaskManager
    {
        private List<Task> taskList = new List<Task>();

        private void CheckTaskList()
        {
            if (taskList.Count <= 0)
            {
                Console.WriteLine(" Your task list is empty. Please add a task !");
                Console.WriteLine(" Please press any key to start typing a new task ");
                Console.ReadKey();
                AddTask();
            }
        }

        public void AddTask()
        {
            bool again = true;
            while (again)
            {
                Console.Write("Enter Task Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Priority (Please enter 1-5): ");
                int priority;
                while (!int.TryParse(Console.ReadLine(), out priority) || priority < 1 || priority > 5) Console.WriteLine("Please enter a number between 1 and 5!");
                Console.Write("Enter Description: ");
                string description = Console.ReadLine();
                if (description == "") description = ("No Description");
                Console.Write("Enter Status: ");
                string status = Console.ReadLine();
                if (status == "") status = "Pending";
                Task newTask = new Task(name, priority, description, status);
                taskList.Add(newTask);
                Console.WriteLine("Task added successfully.");
                Console.WriteLine(" Do you want to add another task ? (Y/N)");
                string response = Console.ReadLine().ToUpper();
                while (response != "Y" && response != "N")
                {
                    Console.WriteLine(" Please enter letter Y or N.");
                    response = Console.ReadLine().ToUpper();
                }
                if (response == "Y") again = true;
                else again = false;
            }
        }

        public void RemoveTask()
        {
            bool again = true;
            while (again)
            {
                CheckTaskList();
                int position; // Người dùng không nắm được vị trí 0 trong C#, nên người dùng nghĩ mặc định vị trí 1 tương ứng với số 1.
                Console.Write("Enter position of task to delete: ");
                while (!int.TryParse(Console.ReadLine(), out position) || position < 1 || position > taskList.Count)
                {
                    Console.WriteLine($"Invalid position! Please enter a position between 1 and {taskList.Count}.");
                    Console.Write("Enter position of task to delete: ");
                }
                taskList.RemoveAt(position - 1);
                Console.WriteLine("Task removed successfully!");
                Console.WriteLine("Would you like to delete another task ? (Y/N)");
                string response = Console.ReadLine().ToUpper();
                while (response != "Y" && response != "N")
                {
                    Console.WriteLine(" Please enter letter Y or N.");
                    response = Console.ReadLine().ToUpper();
                }
                if (response == "Y") again = true;
                else again = false;
            }
        }

        public void UpdateTasksStatus()
        {
            
            bool again = true;
            while (again)
            {
                CheckTaskList();
                Console.Write("Enter job name to update status: ");
                string name = Console.ReadLine();
                Console.Write("Enter new status: ");
                string status = Console.ReadLine();
                var updateStatus = taskList.Find(j => j.TaskName == name);
                if (updateStatus != null)
                {
                    updateStatus.Status = status;
                    Console.WriteLine("Task status updated successfully.");
                }
                else Console.WriteLine("Task not found.");
                Console.WriteLine("Would you like to update the status of another task ? (Y/N)");
                string response = Console.ReadLine().ToUpper();
                while (response != "Y" && response != "N")
                {
                    Console.WriteLine(" Please enter letter Y or N.");
                    response = Console.ReadLine().ToUpper();
                }
                if (response == "Y") again = true;
                else again = false;
            }
        }

        public void SearchTask()
        {
            CheckTaskList();
            Console.Write("Enter task name or priority to search: ");
            string keyword = Console.ReadLine();
            var results = taskList.Where(j => j.TaskName.Contains(keyword) || j.Priority.ToString().Contains(keyword)).ToArray();
            if (results.Count() > 0) foreach (var item in results) Console.WriteLine($"Name: {item.TaskName}, Priority: {item.Priority}, Description: {item.Description}, Status: {item.Status}");
            else Console.WriteLine("No matching tasks found!");
        }
        public void DisplayTasksByPriority()
        {
            CheckTaskList();
            var sortedTasks = taskList.OrderByDescending(j => j.Priority);
            Console.WriteLine("Tasks sorted by priority (descending):");
            foreach (var item in sortedTasks) Console.WriteLine($"Name: {item.TaskName}, Priority: {item.Priority}, Description: {item.Description}, Status: {item.Status}");
        }
        public void DisplayAllTask()
        {
            CheckTaskList();
            Console.WriteLine("AllTask: ");
            foreach (var item in taskList) Console.WriteLine($"Name: {item.TaskName}, Priority: {item.Priority}, Description: {item.Description}, Status: {item.Status}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            bool again = true;
            while (again)
            {
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Remove Task");
                Console.WriteLine("3. Update Status");
                Console.WriteLine("4. Search");
                Console.WriteLine("5. Display tasks in descending order of priority");
                Console.WriteLine("6. Display the entire list of tasks.");
                Console.WriteLine("Enter your choice (Please enter number 1-6): ");
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
                {
                    Console.WriteLine("Please enter a number between 1 and 6!");
                    Console.Write("Enter your choice (Please enter number 1-6):");
                }
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("1. Add Task.");
                        taskManager.AddTask();
                        break;
                    case 2:
                        Console.WriteLine("2. Remove Task.");
                        taskManager.RemoveTask();
                        break;
                    case 3:
                        Console.WriteLine("3. Update Status.");
                        taskManager.UpdateTasksStatus();
                        break;
                    case 4:
                        Console.WriteLine("4. Search.");
                        taskManager.SearchTask();
                        break;
                    case 5:
                        Console.WriteLine("5. Display tasks in descending order of priority.");
                        taskManager.DisplayTasksByPriority();
                        break;
                    case 6:
                        Console.WriteLine("6. Display the entire list of tasks.");
                        taskManager.DisplayAllTask();
                        break;
                }
                Console.WriteLine("Would you like to choose another option? (Y/N)");
                string response = Console.ReadLine().ToUpper();
                while (response != "Y" && response != "N")
                {
                    Console.WriteLine(" Please enter letter Y or N.");
                    response = Console.ReadLine().ToUpper();
                }
                if (response == "Y") again = true;
                else again = false;
            }
            Console.WriteLine(" Thank you. See you next time !");
            Console.ReadKey();
            Console.Clear();
        }
    }
}





