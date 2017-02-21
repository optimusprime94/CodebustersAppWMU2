namespace CodebustersAppWMU2
{
    /*
     * We expect to have a task class, to instantiate each individual class
     * in an appropriate manner. In this class we are able to contain the class name and
     * the responsible user for the task.
     */
    internal class Task
    {
        private string _task;
        private string _responsible = "None";

        public string TaskName
        {
            get { return _task; }
            set { _task = value; }
        }



        public string Responsible
        {
            /* We need to check if this user is responsible for the task, igf there is
             * no responsible user or if somebody else is.
             */
            get { return _responsible; }
            
            {
                
                    _responsible = value;

                
            }
        }


    }
}