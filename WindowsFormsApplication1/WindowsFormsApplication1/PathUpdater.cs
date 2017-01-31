using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class PathUpdater
    {
        public class TreeConstructorEventArgs : EventArgs
        {
            public List<string> Path
            {
                set;
                get;
            }
        }

        public event EventHandler<TreeConstructorEventArgs> TreeConstruckt;

        public List<string> path = new List<string>();

        public List<string> PathUpdate
        {
            get { return path; }
        }


        public void Add_path(string add)
        {
            if (path.Count() > 0)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    try
                    {
                        if (!PathCanBeUpdated(path[i], add))
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                path.Add(add);
                Update_Config();
            }
            else
            {
                path.Add(add);
                Update_Config();
            }
        }

        private bool PathCanBeUpdated(string existingPath, string newPath)
        {
            char[] delimitersForFiltersParsing = new char[] { '\\' };
            string[] existingPatharr = existingPath.Split(delimitersForFiltersParsing, StringSplitOptions.RemoveEmptyEntries);
            string[] newPatharr = newPath.Split(delimitersForFiltersParsing, StringSplitOptions.RemoveEmptyEntries);
            if (existingPatharr == newPatharr)
            {
                return false;
            }
            else if (existingPath.Contains(newPath) || newPath.Contains(existingPath))
            {
                if (existingPatharr.Length == newPatharr.Length && newPatharr[newPatharr.Length - 1] != existingPatharr[existingPatharr.Length - 1])
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }


        public void Remove_path(string rem)
        {
            path.Remove(rem);
            Update_Config();
        }

        public void Update_Config()
        {
            List<string> temp = new List<string>();
            for (int i = 0; i < path.Count(); i++)
            {
                if (!string.IsNullOrEmpty(path[i]))
                {
                    temp.Add(path[i]);
                }
            }
            path = temp;
            if (TreeConstruckt != null)
            {
                TreeConstructorEventArgs Args = new TreeConstructorEventArgs();
                Args.Path = this.path;
                TreeConstruckt(this, Args);
            }
        }

    }
}

