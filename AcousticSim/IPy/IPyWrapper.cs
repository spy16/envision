
using Microsoft.VisualBasic.Devices;
using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Envision.IPy
{
    public class IPyEngine
    {

        #region "PublicVars"
        public Microsoft.Scripting.Hosting.ScriptScope EngineScope;
        public ScriptEngine Engine;
        #endregion

        public ScriptRuntime Runtime;
        #region "PrivateVars"
        private Microsoft.VisualBasic.Devices.Computer my = new Computer();
        private System.IO.Stream _io;
        #endregion
        private Microsoft.Scripting.Hosting.ScriptSource gScript;

        #region "Basic"
        /// <summary>
        /// Creates new instance of IPy engine with given stream
        /// </summary>
        /// <param name="iostream">stream object to be used for I/O</param>
        /// <remarks></remarks>
        public IPyEngine(System.IO.Stream iostream, bool AddExecutingAssembly = true)
        {
            this.Engine = Python.CreateEngine();
            Runtime = this.Engine.Runtime;
            EngineScope = this.Engine.CreateScope();
            _io = iostream;
            SetStreams(_io);
            if (AddExecutingAssembly)
            {
                Runtime.LoadAssembly(Assembly.GetExecutingAssembly());
            }
        }

        /// <summary>
        /// Sets output and error stream handles to the given 'stream' object
        /// </summary>
        /// <param name="stream">Stream object to use</param>
        /// <remarks>This function will not change the Input stream of the engine. 
        /// Input stream will be set to the stream object used during the initialization 
        /// of the class</remarks>
        public void SetStreams(System.IO.Stream stream)
        {
            Runtime.IO.SetInput(_io, Encoding.UTF8);
            Runtime.IO.SetOutput(stream, Encoding.UTF8);
            Runtime.IO.SetErrorOutput(stream, Encoding.UTF8);
        }

        /// <summary>
        /// Returns Python style traceback of the given exception object
        /// </summary>
        /// <param name="e">exception handle</param>
        /// <returns>String representing the traceback information</returns>
        public object getTraceback(Exception e)
        {
            ExceptionOperations eo = Engine.GetService<ExceptionOperations>();
            return eo.FormatException(e);
        }

        /// <summary>
        /// Adds a .NET Dynamic Link Library(DLL) to Python scope
        /// </summary>
        /// <param name="path">Fully qualified file path</param>
        /// <remarks>To add a directory containing multiple DLL files, use LoadAssemblies()</remarks>
        public void AddAssembly(string path)
        {
            Assembly pluginsAssembly = Assembly.LoadFile(path);
            Runtime.LoadAssembly(pluginsAssembly);
        }

        /// <summary>
        /// Run the script and import all the variables/functions/objects to scope
        /// </summary>
        /// <param name="path">path to python script</param>
        /// <returns>True if file found and loaded, false otherwise</returns>
        /// <remarks>No errors will be raised in case the file does not exist</remarks>
        public bool LoadScript(string path)
        {
            if (my.FileSystem.FileExists(path))
            {
                Debug.Print("loading script " + path.Split('\\').Last());
                gScript = Engine.CreateScriptSourceFromFile(path);
                gScript.Compile();
                gScript.Execute(EngineScope);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds all the .NET Dynamic Link Libraries(DLL) availble in the taget
        /// dir to Python scope
        /// </summary>
        /// <param name="dir">Folder containing DLL files</param>
        /// <remarks>No errors will be raised</remarks>
        public void LoadAssemblies(string dir)
        {
            dynamic fList = my.FileSystem.GetFiles(dir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "*.dll");
            foreach (string dll_file in fList)
            {
                AddAssembly(dll_file);
            }
        }

        /// <summary>
        /// If the path contains init.py file, the directory is assumed as a library 
        /// and all the script files inside the path will be loaded. Also, DLLs from @lib
        /// will be loaded.
        /// </summary>
        /// <param name="path">path to the library folder</param>
        /// <param name="preInit">if set, loads the init script before loading other scripts in the directory</param>
        /// <remarks></remarks>
        public void LoadLibs(string path, bool preInit = false)
        {
            string init_path = Path.Combine(path, "init.py");
            string lib_path = Path.Combine(path, "@libs");
            if (my.FileSystem.FileExists(init_path))
            {
                LoadAssemblies(lib_path);
                if (preInit) LoadScript(init_path);
                dynamic fList = my.FileSystem.GetFiles(path, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "*.py");
                foreach (string item in fList)
                {
                    if (item.EndsWith("init.py")) continue;
                    LoadScript(item);
                }
                if (!preInit) LoadScript(init_path);
            }
        }

        #endregion

        #region "User"

        public object PyExecute(string source, Microsoft.Scripting.SourceCodeKind kind = Microsoft.Scripting.SourceCodeKind.Expression)
        {
            Microsoft.Scripting.Hosting.ScriptSource src = default(Microsoft.Scripting.Hosting.ScriptSource);
            src = Engine.CreateScriptSourceFromString(source, kind);
            object res = src.Execute(EngineScope);
            return res;
        }

        #endregion

        #region "ScopeMgmt"
        public bool IsDefined(string name)
        {
            return EngineScope.ContainsVariable(name);
        }

        public object getFunction(string name)
        {
            object p = EngineScope.GetVariable(name);
            if ((p != null))
            {
                if (Engine.Operations.IsCallable(p))
                {
                    return p;
                }
            }
            return null;
        }

        public void ClearScope()
        {
            EngineScope = Engine.CreateScope();
        }
        #endregion

    }


}


