
package poe3;
import javax.swing.JOptionPane;

public class POE3 {
    static int size = TaskDetails.getNumberOfTasks();
    static String[] developerNames = new String[size];
    static String[] taskNames = new String[size];
    static String[] taskIDs = new String[size];
    static String[] taskStatuses = new String[size];
    static int[] taskDurations = new int[size];

    // Main method to manipulate arrays
    public static void manipulateArrays(String[] developerName, String[] taskName, String[] taskID, String[] status, int[] duration) {
        System.arraycopy(developerName, 0, developerNames, 0, size);
        System.arraycopy(taskName, 0, taskNames, 0, size);
        System.arraycopy(taskID, 0, taskIDs, 0, size);
        System.arraycopy(status, 0, taskStatuses, 0, size);
        System.arraycopy(duration, 0, taskDurations, 0, size);

        String choice = JOptionPane.showInputDialog("""
                Enter:
                a. To view the list of all tasks with the status of 'Done'
                b. To view the developer whose task takes the longest
                c. To search for a task
                d. To view tasks assigned to a developer
                e. To delete a task
                f. To get a report of all tasks
                """);

        switch (choice) {
            case "a" -> doneTasks();
            case "b" -> longestTask();
            case "c" -> {
                String search = JOptionPane.showInputDialog("Enter the task name to search:");
                searchTask(search);
            }
            case "d" -> {
                String devName = JOptionPane.showInputDialog("Enter the developer's full name:");
                developerTasks(devName);
            }
            case "e" -> {
                String taskToDelete = JOptionPane.showInputDialog("Enter the task name to delete:");
                deleteTask(taskToDelete);
            }
            case "f" -> displayReport();
            default -> JOptionPane.showMessageDialog(null, "Invalid input");
        }
    }

    // List tasks with 'Done' status
    public static void doneTasks() {
        StringBuilder done = new StringBuilder();
        for (int i = 0; i < size; i++) {
            if ("Done".equalsIgnoreCase(taskStatuses[i])) {
                done.append(developerNames[i]).append(", ").append(taskNames[i]).append(", ").append(taskDurations[i]).append("\n");
            }
        }
        JOptionPane.showMessageDialog(null, done.length() > 0 ? done.toString() : "No tasks with status 'Done'.");
    }

    // Disply the longest task
    public static void longestTask() {
        int maxIndex = 0;
        for (int i = 1; i < size; i++) {
            if (taskDurations[i] > taskDurations[maxIndex]) {
                maxIndex = i;
            }
        }
        String longest = developerNames[maxIndex] + ", " + taskDurations[maxIndex];
        JOptionPane.showMessageDialog(null, longest);
    }

    // Search array for a task by name
    public static void searchTask(String taskName) {
        for (int i = 0; i < size; i++) {
            if (taskNames[i].equalsIgnoreCase(taskName)) {
                String result = taskNames[i] + ", " + developerNames[i] + ", " + taskStatuses[i];
                JOptionPane.showMessageDialog(null, result);
                return;
            }
        }
        JOptionPane.showMessageDialog(null, "Task not found.");
    }

    // Tasks assigned to a developer
    public static void developerTasks(String developer) {
        StringBuilder tasks = new StringBuilder();
        for (int i = 0; i < size; i++) {
            if (developerNames[i].equalsIgnoreCase(developer)) {
                tasks.append(taskNames[i]).append(", ").append(taskStatuses[i]).append("\n");
            }
        }
        JOptionPane.showMessageDialog(null, tasks.length() > 0 ? tasks.toString() : "No tasks found for " + developer);
    }

    // Delete a task by name
    public static void deleteTask(String taskName) {
        for (int i = 0; i < size; i++) {
            if (taskNames[i].equalsIgnoreCase(taskName)) {
                developerNames[i] = taskNames[i] = taskIDs[i] = taskStatuses[i] = "";
                taskDurations[i] = 0;
                JOptionPane.showMessageDialog(null, "Task '" + taskName + "' successfully deleted.");
                return;
            }
        }
        JOptionPane.showMessageDialog(null, "Task not found.");
    }

    // Display report of all tasks
    public static void displayReport() {
        StringBuilder report = new StringBuilder();
        for (int i = 0; i < size; i++) {
            if (!taskNames[i].isEmpty()) {
                report.append("Developer: ").append(developerNames[i])
                      .append("\nTask Name: ").append(taskNames[i])
                      .append("\nTask ID: ").append(taskIDs[i])
                      .append("\nStatus: ").append(taskStatuses[i])
                      .append("\nDuration: ").append(taskDurations[i]).append("\n\n");
            }
        }
        JOptionPane.showMessageDialog(null, report.length() > 0 ? report.toString() : "No tasks available.");
    }

    
    public static void main(String[] args) {
        
    }    
}
