using System.Collections.Generic;

namespace AtlirVPNConnect.Util
{
    class EventRegistrar
    {
        public delegate void DataRec(string data);
        public delegate void NoData();

        public class Interface
        {
            public static event NoData UpdateInterfaceStats;

            public static void OnUpdateInterfaceStats()
            {
                NoData handler = UpdateInterfaceStats;
                if (handler != null) handler();
            }
        }

        public class Status
        {
            private static readonly List<Task> Tasks = new List<Task>();
            public class Task
            {
                public Task(string msg, string endMessage = "")
                {
                    Message = msg;
                    EndMessage = endMessage;
                }
                public string Message;
                public string EndMessage;

                /// <summary>
                /// Removes the task from the active task list
                /// </summary>
                public void Lock()
                {
                    RemoveTask(this);
                }

                protected bool Equals(Task other)
                {
                    return string.Equals(Message, other.Message) && string.Equals(EndMessage, other.EndMessage);
                }

                public override int GetHashCode()
                {
                    unchecked
                    {
                        return ((Message != null ? Message.GetHashCode() : 0) * 397) ^ (EndMessage != null ? EndMessage.GetHashCode() : 0);
                    }
                }
            }

            /// <summary>
            /// Add running task to be handled by statusBar on main controller
            /// </summary>
            /// <param name="msg">Message for the task</param>
            /// <returns>Returns the task created by the msg</returns>
            public static Task AddTask(string msg, string end = "")
            {
                var stat = new Task(msg, end);
                OnStatusUpdate(stat.Message);
                Tasks.Add(stat);
                OnRunningTasks(Tasks.Count.ToString());
                return stat;
            }

            /// <summary>
            /// Handles the removing of tasks
            /// </summary>
            /// <param name="task">The task to remove</param>
            private static void RemoveTask(Task task)
            {
                Tasks.Remove(task);
                if (!string.IsNullOrEmpty(task.EndMessage))
                    OnStatusUpdate(task.EndMessage);
                else if(Tasks.Count > 0)
                    OnStatusUpdate("Done: " + task.Message);
                else
                    OnStatusUpdate("Ready");
                OnRunningTasks(Tasks.Count == 0 ? "" : Tasks.Count.ToString());
            }

            public static event DataRec StatusUpdate;
            public static void OnStatusUpdate(string data)
            {
                DataRec handler = StatusUpdate;
                if (handler != null) handler(data);
            }

            public static event DataRec RunningTasks;
            public static void OnRunningTasks(string data)
            {
                DataRec handler = RunningTasks;
                if (handler != null) handler(data);
            }
        }

        public class OpenVpn
        {

            public static event NoData Connect;

            public static void OnConnect()
            {
                NoData handler = Connect;
                if (handler != null) handler();
            }

            public static event NoData Disconnect;

            public static void OnDisconnect()
            {
                NoData handler = Disconnect;
                if (handler != null) handler();
            }

            public static event DataRec DataRecieved;

            public static void OnDataRecieved(string data)
            {
                DataRec handler = DataRecieved;
                if (handler != null) handler(data);
            }

            public static event DataRec ErrorRecieved;

            public static void OnErrorRecieved(string data)
            {
                DataRec handler = ErrorRecieved;
                if (handler != null) handler(data);
            }
        }

    }
}
