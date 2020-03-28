using Envision.IPy;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Envision
{
   public static class AppGlobals
   {

       #region [ Application Functions ]

       internal static string[] args = new string[] { };
       internal const string INTERFACE_NAME = "Envision.Blocks.BlockBase";
       internal static Dictionary<string, ExtensionServices.AvailablePlugin> blocksetLib =
           new Dictionary<string, ExtensionServices.AvailablePlugin>();
       internal static Windows.mainForm mainFormInstance;
       internal static Windows.propertyInspector PropertyWinow;
       internal static Windows.BlockSetWindow BlockSetWindowInstance;
       internal static Windows.GenericResultsWindow GenericResultsWindowInstance;
       internal static Windows.ModelDesignerWindow CurrentDesigner;

       public static void ShowWin(WeifenLuo.WinFormsUI.Docking.DockContent dockWin,
           WeifenLuo.WinFormsUI.Docking.DockState state)
       {
           dockWin.Show(mainFormInstance.masterDockPanel, state);
       }

       public static void ShowBlocksetBrowser()
       {
           if (AppGlobals.BlockSetWindowInstance == null || AppGlobals.BlockSetWindowInstance.IsDisposed)
           {
               AppGlobals.BlockSetWindowInstance = new Windows.BlockSetWindow();
               AppGlobals.ShowWin(AppGlobals.BlockSetWindowInstance,
                   WeifenLuo.WinFormsUI.Docking.DockState.DockRight);
           }
       }

       public static void ShowGenericResults()
       {
           if (AppGlobals.GenericResultsWindowInstance == null || AppGlobals.GenericResultsWindowInstance.IsDisposed)
           {
               AppGlobals.GenericResultsWindowInstance = new Windows.GenericResultsWindow();
               AppGlobals.ShowWin(AppGlobals.GenericResultsWindowInstance,
                   WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide);
           }
       }

       public static void WriteResults(string msg, bool endnewline = true)
       {
           if (endnewline)
           {
               msg += "\n";
           }
           ShowGenericResults();
           AppGlobals.GenericResultsWindowInstance.rtfResultsDisplay.AppendText(msg);
       }

       public static void ShowProperties(object obj)
       {
           if (PropertyWinow == null || PropertyWinow.IsDisposed)
           {
               PropertyWinow = new Windows.propertyInspector();
           }
           PropertyWinow.Inspect(obj);
           if (!PropertyWinow.Visible)
           {
               ShowWin(PropertyWinow,
                   WeifenLuo.WinFormsUI.Docking.DockState.DockRight);
           }
       }
       #endregion

       #region [   IronPython Wrapper Methods    ]
       internal static IPyEngine ipy;
       internal static CustomStream cstream;

       public static object InvokeFunction(IronPython.Runtime.PythonFunction fun,  params object[] args)
       {
           return ipy.Engine.Operations.Invoke(fun, args);
       }
       public static bool IsFunction(string varname)
       {
           object obj = PyGetVar(varname);
           return (ipy.EngineScope.ContainsVariable(varname) && obj != null &&
               obj.GetType() == typeof(IronPython.Runtime.PythonFunction));
       }



       public static string[] PyGetVarNames()
       {
           return ipy.EngineScope.GetVariableNames().ToArray();
       }

       /// <summary>
       /// Retruns documentation of an object
       /// </summary>
       /// <param name="variable">variable</param>
       /// <param name="_default">if variable does not exists returns the specified string</param>
       /// <returns>documentation string</returns>
       public static string PyGetHelp(object variable, string _default = "N/A")
       {
           string doc = ipy.Engine.Operations.GetDocumentation(variable);
           return doc;
       }

       /// <summary>
       /// Retruns documentation of an object
       /// </summary>
       /// <param name="varname">name of the variable</param>
       /// <param name="_default">if variable does not exists returns the specified string</param>
       /// <returns>documentation string</returns>
       public static string PyGetHelp(string varname, string _default = "N/A")
       {
           string doc = ipy.Engine.Operations.GetDocumentation(PyExecuteExpr(varname));
           return doc;
       }

       /// <summary>
       /// Generates Python-style traceback info for exceptions
       /// </summary>
       /// <param name="ex">generated exception</param>
       /// <returns>python-style traceback info</returns>
       public static string PyGetTraceback(Exception ex)
       {
           return ipy.getTraceback(ex).ToString();
       }

       /// <summary>
       /// Modifies the stream for IronPython engine
       /// </summary>
       /// <param name="stream">Stream object to use</param>
       public static void PySetStreams(System.IO.Stream stream = null)
       {
           if (stream == null)
           {
               stream = new CustomStream(null);
           }
           ipy.SetStreams(stream);
       }

       /// <summary>
       /// Executes a python expression. Equivalent to PyExecute(expr, Microsoft.Scripting.SourceCodeKind.Expression)
       /// </summary>
       /// <param name="expr">expression to be executed. Assignments, Loops etc are not allowed</param>
       /// <returns>Returns the value generated by evaluating the expression</returns>
       public static object PyExecuteExpr(string expr)
       {
           return ipy.PyExecute(expr, Microsoft.Scripting.SourceCodeKind.Expression);
       }

       /// <summary>
       /// Executes the given Python code snippet
       /// </summary>
       /// <param name="source">code snippet</param>
       /// <param name="kind">source code kind</param>
       /// <returns>if kind is Expression, the result is returned. null otherwise</returns>
       public static object PyExecute(string source, Microsoft.Scripting.SourceCodeKind kind)
       {
           return ipy.PyExecute(source, kind);

       }

       /// <summary>
       /// Initializes the IronPython engine
       /// </summary>
       /// <param name="stream">IO stream to use</param>
       public static void PyInit(CustomStream stream = null)
       {
           if (stream == null)
           {
               stream = new CustomStream(null);
           }
           cstream = stream;
           ipy = new IPyEngine(cstream);
           ipy.LoadLibs("./core");
       }


       /// <summary>
       /// Looks for a python callable with given name in the scope and returns if found
       /// </summary>
       /// <param name="name">name of the callable to look for</param>
       /// <returns>The callable is returned if found, null otherwise</returns>
       public static object PyVarGetFunc(string name)
       {
           if (ipy.EngineScope.ContainsVariable(name))
           {
               object tmp = ipy.EngineScope.GetVariable(name);
               if (ipy.Engine.Operations.IsCallable(tmp))
               {
                   return tmp;
               }
               return null;
           }
           else
           {
               return null;
           }
       }


       /// <summary>
       /// Looks for a variable and if found, returns it as .NET object
       /// </summary>
       /// <param name="name">name of the variable to look for</param>
       /// <returns>if variable exists, value is returned</returns>
       /// <exception cref="UnboundNameException">Thrown when there exists no variable with given name </exception>
       public static object PyGetVar(string name)
       {
           if (ipy.EngineScope.ContainsVariable(name))
           {
               return ipy.EngineScope.GetVariable(name);
           }
           else
           {
               throw new UnboundNameException(string.Format("no variable exists with name '{0}'", name));
           }
       }

       /// <summary>
       /// Sets value for a variable
       /// </summary>
       /// <param name="name">fully qualified name of the variable</param>
       /// <param name="value">object of any type which can be passed as an argument</param>
       /// <returns>true if a new variable is created, false if an existing variable is modified</returns>
       public static bool PySetVar(string name, object value)
       {
           bool retval = !ipy.EngineScope.ContainsVariable(name);
           ipy.EngineScope.SetVariable(name, value);
           return retval;
       }

       public static bool PyRemVar(string name)
       {
           return ipy.EngineScope.RemoveVariable(name);
       }

       /// <summary>
       /// Checks if a variable with given name exists
       /// </summary>
       /// <param name="name">name of the variable</param>
       /// <returns>true of exists, false otherwise</returns>
       public static bool PyVarExists(string name)
       {
           return ipy.EngineScope.ContainsVariable(name);
       }


       /// <summary>
       /// Creates new scope by replacing the old one
       /// </summary>
       public static void PyClearScope()
       {
           ipy.EngineScope = ipy.Engine.CreateScope();
       }

       /// <summary>
       /// Replaces existing one with the provided
       /// </summary>
       /// <param name="scope">New scope to replace the old one with</param>
       /// <returns>old scope for backup purposes</returns>
       public static Microsoft.Scripting.Hosting.ScriptScope PySwitchScope(ScriptScope scope)
       {
           Microsoft.Scripting.Hosting.ScriptScope old = ipy.EngineScope;
           ipy.EngineScope = scope;
           return old;
       }

       #endregion

       #region [  BlockSet Mgmt.  ]

       internal static void LoadPyBlockset(string blockset_path = ".")
       {
           if (System.IO.Directory.Exists(blockset_path))
           {
               string[] files = System.IO.Directory.GetFiles(blockset_path);
               foreach (var file in files)
               {
                   if (file.EndsWith(".xml"))
                   {
                       XmlDocument xmldoc = new XmlDocument();
                       xmldoc.Load(file);
                       XmlNode root = xmldoc.SelectSingleNode("blocks");
                       if (root!=null)
                       {
                           XmlNodeList blocks = root.SelectNodes("block");
                           if (blocks != null)
                           {
                               foreach (XmlNode blocknode in blocks)
                               {
                                   //string name = blocknode.Attributes.GetNamedItem("name").;
                                   //string cate = blocknode.Attributes.GetNamedItem("category");

                               }
                           }
                       }
                   }
               }
           }
       }
       public static void LoadBlockset(string blockset_path = ".")
       {
           var t = AppGlobals.ExtensionServices.FindPlugins(blockset_path, INTERFACE_NAME);
           for (int i = 0 ; i < t.Length ; i++)
           {
               var item = t[i];
               var obj = (Blocks.BlockBase)AppGlobals.ExtensionServices.CreateInstance(item);
               if (blocksetLib.ContainsKey(obj.Name)) continue;
               System.Diagnostics.Debug.Print("Loaded " + obj.Name);
               item.Category = obj.GetProcessingTypeName();
               item.Description = obj.Description;
               blocksetLib.Add(obj.Name, item);
           }
       }
       public class ExtensionServices
       {
           public struct AvailablePlugin
           {
               public string AssemblyPath;
               public string ClassName;
               public string InterfaceName;
               public string Category;
               public string Description;
           }

           public static AvailablePlugin[] FindPlugins(string path, string interfacename)
           {
               List<AvailablePlugin> plugins = new List<AvailablePlugin>();
               string[] strDLLs = Directory.GetFileSystemEntries(path, "*.dll");
               foreach (string dllfile in strDLLs)
               {
                   try
                   {
                       Assembly objDLL = Assembly.LoadFrom(dllfile);
                       ExamineAssembly(objDLL, interfacename, plugins);
                   } catch (BadImageFormatException)
                   {
                       Logger.D("Ignored " + dllfile);
                   } catch (FileLoadException)
                   {
                       Logger.D("Ignored " + dllfile);
                   }
               }
               return plugins.ToArray();
           }

           public static void ExamineAssembly(Assembly objDLL, string interfaceName, List<AvailablePlugin> plugins)
           {
               Type objInterface;
               AvailablePlugin plugin;
               foreach (Type objType in objDLL.GetTypes())
               {
                   if (objType.IsPublic)
                   {
                       if (!objType.IsAbstract)
                       {
                           objInterface = objType.GetInterface(interfaceName, true);
                           if (objType.BaseType.FullName == interfaceName)
                           {
                               plugin = new AvailablePlugin();
                               plugin.AssemblyPath = objDLL.Location;
                               plugin.ClassName = objType.FullName;
                               plugin.InterfaceName = interfaceName;
                               plugins.Add(plugin);
                           }
                       }
                   }
               }
           }

           public static object CreateInstance(AvailablePlugin plugin)
           {
               Assembly objDLL;
               object objInstance;
               objDLL = Assembly.LoadFrom(plugin.AssemblyPath);
               objInstance = objDLL.CreateInstance(plugin.ClassName);
               return objInstance;
           }
       }

       #endregion
    }
}
